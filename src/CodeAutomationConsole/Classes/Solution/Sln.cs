namespace CodeAutomationConsole
{
    using System;
    using System.IO;

    public static class Sln
    {
        //public static string Content = Template.GetByName("SolutionName.sln").
        //    Replace("%PROJECTGUID2%", Global.ProjectGuid).
        //    Replace("%PROJECTGUID1%", Guid.NewGuid().ToString().ToUpper()).
        //    Replace("%SOLUTIONGUID%", Guid.NewGuid().ToString().ToUpper());

        private static readonly string Content = $@"
        Microsoft Visual Studio Solution File, Format Version 12.00
        # Visual Studio Version 17
        VisualStudioVersion = 17.0.31912.275
        MinimumVisualStudioVersion = 10.0.40219.1
        Project(""{{{Guid.NewGuid().ToString().ToUpper()}}}"") = ""{Global.Namespace}"", ""{Global.Namespace}\{Global.Namespace}.csproj"", ""{{{Global.ProjectGuid}}}""
        EndProject
        Global
        	GlobalSection(SolutionConfigurationPlatforms) = preSolution
        		Debug|Any CPU = Debug|Any CPU
        		Release|Any CPU = Release|Any CPU
        	EndGlobalSection
        	GlobalSection(ProjectConfigurationPlatforms) = postSolution
        		{{{Global.ProjectGuid}}}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
        		{{{Global.ProjectGuid}}}.Debug|Any CPU.Build.0 = Debug|Any CPU
        		{{{Global.ProjectGuid}}}.Release|Any CPU.ActiveCfg = Release|Any CPU
        		{{{Global.ProjectGuid}}}.Release|Any CPU.Build.0 = Release|Any CPU
        	EndGlobalSection
        	GlobalSection(SolutionProperties) = preSolution
        		HideSolutionNode = FALSE
        	EndGlobalSection
        	GlobalSection(ExtensibilityGlobals) = postSolution
        		SolutionGuid = {{{Guid.NewGuid()}}}
        	EndGlobalSection
        EndGlobal
        ";

        public static void Save()
        {
            var fileName = Path.Combine(Global.Path, $"{Global.Namespace}.sln");

            Content.SaveToFile(fileName);
        }

    }
}
