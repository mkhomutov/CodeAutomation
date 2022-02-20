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
            var solutionTree = new SolutionTree(_settings.TemplatesPath, context);

            InitializeGitRepo(_settings.OutputPath);

            ExecuteSetupCommand(_settings.OutputPath);

            solutionTree = BuildCode(_settings, solutionTree);

            solutionTree.Save(_settings.OutputPath); // save generated code

            CommitChanges(_settings.OutputPath);

            ExecuteTeardownCommand(_settings.OutputPath);
        }

        private void ExecuteSetupCommand(string path)
        {
            var command = _settings.Script.Setup;
            if (string.IsNullOrEmpty(command))
            {
                return;
            }

            ExecuteCommand(path, command);
        }

        private void ExecuteTeardownCommand(string path)
        {
            var command = _settings.Script.Teardown;
            if (string.IsNullOrEmpty(command))
            {
                return;
            }

            ExecuteCommand(path, command);
        }

        private void ExecuteCommand(string workingDirectory, string command)
        {
            var startInfo = new ProcessStartInfo
            {
                FileName = _settings.Script.Tool,
                Arguments = command,
                RedirectStandardOutput = false,
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

            process.WaitForExit();

            var errors = process.StandardError.ReadToEnd();

            if (!string.IsNullOrEmpty(errors))
            {
                Console.Write($"{errors}\n");
            }
            else
            {
                Console.Write("Completed successfully\n");
            }
        }
        

        private void CommitChanges(string path)
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
