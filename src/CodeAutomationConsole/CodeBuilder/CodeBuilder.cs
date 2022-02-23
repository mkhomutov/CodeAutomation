using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using LibGit2Sharp;

namespace CodeAutomationConsole
{
    public class CodeBuilder
    {
        private AutomationSettings _settings;

        public CodeBuilder(AutomationSettings settings)
        {
            _settings = settings;
        }

        public void Run()
        {
            ISettingsProcessor settingsProcessor = new SettingsProcessor();
            _settings = settingsProcessor.Run(_settings);

            var context = _settings.CodeModel.FixTypes();
            var solutionTree = new SolutionTree(_settings.TemplatesPath, context, _settings);

            InitializeGitRepo(_settings.OutputPath);

            solutionTree = BuildCode(_settings, solutionTree);

            solutionTree.Save(_settings.OutputPath); // save generated code

            CommitChanges(_settings.OutputPath);

            foreach (var command in _settings.Script.Commands)
            {
                ExecuteCommand(command.WorkingFolder, command.Command);
            }
        }

        private void ExecuteCommand(string workingDirectory, string command)
        {
            var startInfo = new ProcessStartInfo
            {
                FileName = _settings.Script.Tool,
                Arguments = command,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true,
                WorkingDirectory = workingDirectory
            };
            
            var process = new Process();

            process.StartInfo = startInfo;
            process.Start();

            Console.WriteLine($"\nExecuting command: {_settings.Script.Tool} {command}");
            Console.WriteLine("Please wait ...");

            while (!process.StandardOutput.EndOfStream || !process.HasExited)
            {
                Thread.Sleep(10);

                if (process.StandardOutput.EndOfStream)
                {
                    continue;
                }

                var output = process.StandardOutput.ReadLine();
                Console.WriteLine(output);
            }

            var errors = process.StandardError.ReadToEnd();

            var resultMessage = !string.IsNullOrEmpty(errors) 
                ? $"\n-----------ERROR-----------------------------\n{errors}\n" 
                : "Completed successfully\n";

            Console.Write(resultMessage);
        }
        

        private void CommitChanges(string path)
        {
            try
            {
                var gitFolder = GetGitFolder(path);
                using var repo = new Repository(gitFolder);

                var developBranch = repo.Branches[_settings.Git.BranchName];
                if (developBranch is null)
                {
                    CreateDevelopBranch(repo);
                }

                LibGit2Sharp.Commands.Checkout(repo, developBranch);

                LibGit2Sharp.Commands.Stage(repo, "*");

                var author = GetAuthorSignature();
                var committer = author;

                Console.WriteLine($"Creating commit '{_settings.Git.CommitName}'");

                var commit = repo.Commit(_settings.Git.CommitName, author, committer);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void InitializeGitRepo(string path)
        {
            var gitFolder = GetGitFolder(path);
            if (Directory.Exists(gitFolder))
            {
                return;
            }

            Console.WriteLine($"Initializing Git repository at '{path}'");

            _ = Repository.Init(gitFolder, false);

            using var repo = new Repository(gitFolder);

            var author = GetAuthorSignature();
            var committer = author;

            Console.WriteLine($"Creating initial commit");

            var commit = repo.Commit("Initial commit", author, committer);

            CreateDevelopBranch(repo);
        }

        private void CreateDevelopBranch(Repository repo)
        {
            var developBranch = repo.CreateBranch(_settings.Git.BranchName, repo.Commits.Single());
        }

        private static string GetGitFolder(string path)
        {
            return Path.Combine(path, ".git");
        }

        private Signature GetAuthorSignature()
        {
            return new Signature(_settings.Git.UserName, _settings.Git.Email, DateTimeOffset.Now);
        }

        private SolutionTree BuildCode(AutomationSettings settings, SolutionTree solutionTree)
        {
            Console.WriteLine($"Creating file system from the templates");

            solutionTree.RenderTemplate();

            return solutionTree;
        }
    }
}
