namespace CodeAutomationConsole
{
    using System;

    public class Sln
    {

        public Sln(string name)
        {
            var projectGuid = Guid.NewGuid().ToString().ToUpper();

            Content = $@"
Microsoft Visual Studio Solution File, Format Version 12.00
# Visual Studio Version 16
VisualStudioVersion = 16.0.31624.102
MinimumVisualStudioVersion = 10.0.40219.1
Project(""{{{Guid.NewGuid().ToString().ToUpper()}}}"") = ""{name}"", ""{name}\{name}.csproj"", ""{{{projectGuid}}}""
EndProject
Global
	GlobalSection(SolutionConfigurationPlatforms) = preSolution
		Debug|Any CPU = Debug|Any CPU
		Release|Any CPU = Release|Any CPU
	EndGlobalSection
	GlobalSection(ProjectConfigurationPlatforms) = postSolution
		{{{projectGuid}}}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
		{{{projectGuid}}}.Debug|Any CPU.Build.0 = Debug|Any CPU
		{{{projectGuid}}}.Release|Any CPU.ActiveCfg = Release|Any CPU
		{{{projectGuid}}}.Release|Any CPU.Build.0 = Release|Any CPU
	EndGlobalSection
	GlobalSection(SolutionProperties) = preSolution
		HideSolutionNode = FALSE
	EndGlobalSection
	GlobalSection(ExtensibilityGlobals) = postSolution
		SolutionGuid = {{{Guid.NewGuid()}}}
	EndGlobalSection
EndGlobal
";
        }

        public string Content { get;  }

    }
}
