using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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

            var context = _settings.Config.FixTypes();
            var solutionTree = new SolutionTree(_settings.TemplatesPath, context);

            InitializeGitRepo(_settings.OutputPath);

            solutionTree = BuildCode(_settings, solutionTree);

            solutionTree.Save(_settings.OutputPath); // save generated code

            CommitChanges(_settings.OutputPath);

            //           _settings.Save(Path.Combine(_settings.OutputPath, "config.yaml")); // save updated settings
        }

        private void CommitChanges(string path)
        {
            var gitFolder = GetGitFolder(path);
            using var repo = new Repository(gitFolder);

            var developBranch = repo.Branches["develop"];

            LibGit2Sharp.Commands.Checkout(repo, developBranch);

            LibGit2Sharp.Commands.Stage(repo, "*");

            var author = GetAuthorSignature();
            var committer = author;

            var commit = repo.Commit("Initial code generation", author, committer);
        }

        private static void InitializeGitRepo(string path)
        {
            var gitFolder = GetGitFolder(path);
            CreateRepository(gitFolder);
            using var repo = new Repository(gitFolder);

            var author = GetAuthorSignature();
            var committer = author;

            var commit = repo.Commit("Initial commit", author, committer);

            var branchCollection = repo.Branches;

            var developBranch = repo.CreateBranch("develop", repo.Commits.Single());
        }

        private static string GetGitFolder(string path)
        {
            return Path.Combine(path, ".git");
        }

        private static string CreateRepository(string gitFolder)
        {
            if (Directory.Exists(gitFolder))
            {
                Directory.Delete(gitFolder, true);
            }

            var init = Repository.Init(gitFolder, false);
            return init;
        }

        private static Signature GetAuthorSignature()
        {
            return new Signature("Max", "mkhomutov@gmail.com", DateTimeOffset.Now);
        }

        private SolutionTree BuildCode(AutomationSettings settings, SolutionTree solutionTree)
        {
            solutionTree.RenderTemplate();

            return solutionTree;
        }
    }
}
