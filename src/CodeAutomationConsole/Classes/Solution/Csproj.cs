﻿namespace CodeAutomationConsole
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;

    public class Csproj
    {
        public Csproj(string name, string root)
        {
            var properties = new Dictionary<string, string>
            {
                {"TargetFrameworks", "net5.0-windows"},
                {"RuntimeIdentifier", "win-x64"},
                {"AppendRuntimeIdentifierToOutputPath", "false"},
                {"SelfContained", "true"},
                {"AssemblyName", name},
                {"RootNamespace", root},
                {"DefaultLanguage", "en-US"},
                {"SonarQubeExclude", "true"},
                {"UseWpf", "true"},
                {"UseWindowsForms", "true"},
                {"ExtrasEnableImplicitWpfReferences", "true"},
                {"ExtrasEnableImplicitWinFormsReferences", "true"},
                {"OutputType", "WinExe"},
                {"StartupObject", ""},
                {"NoWarn", "$(NoWarn);SA1652"},
                {"ApplicationIcon", @"Resources\Icons\Logo.ico"},
                {"ApplicationManifest", ".manifest"}
            };
            var includePivateAssetsAll = new Dictionary<string, string>
            {
                {"Catel.Fody", "4.7.0"},
                {"LoadAssembliesOnStartup.Fody", "4.6.0"},
                {"ModuleInit.Fody", "2.1.1"},
                {"Fody", "6.5.3"}
            };
            var include = new Dictionary<string, string>
            {
                {"Fluent.Ribbon", "8.0.3"},
                {"Orchestra.Core", "6.6.6"},
                {"Orchestra.Shell.Ribbon.Fluent", "6.6.6" },
                {"Orc.Csv", "4.3.2" }
            };

            var images = new[] { "CompanyLogo.png", "exit.png", "explorer.png", "file_menu.png", "keyboard.png", "open.png", "print.png", "refresh.png", "save.png" };

            var csproj = new XElement("Project",
                new XAttribute("Sdk", "MSBuild.Sdk.Extras"),
                new XElement("PropertyGroup",
                    properties.Select(p => new XElement(p.Key, p.Value))
                    ),
                new XElement("ItemGroup",
                    includePivateAssetsAll.Select(i => new XElement("PackageReference", new XAttribute("Include", i.Key), new XAttribute("Version", i.Value), new XAttribute("PrivateAssets", "all"))),
                    include.Select(i => new XElement("PackageReference", new XAttribute("Include", i.Key), new XAttribute("Version", i.Value)))
                    ),
                new XElement("ItemGroup", new XElement("PackageReference", new XAttribute("Update", "NETStandard.Library"), new XAttribute("Version", "2.0.3"))),
                new XElement("ItemGroup", new XElement("Resource", new XAttribute("Include", "Resources\\Fonts\\fontawesome-webfont.ttf"))),
                new XElement("ItemGroup", images.Select(x => new XElement("Resource", new XAttribute("Include", $"Resources\\Images\\{x}"))))
                );

            var fody = csproj.Descendants("PackageReference").Where(x => x.Attribute("Include") is not null).Where(x => x.Attribute("Include").Value.Equals("Fody")).FirstOrDefault();
            if (fody is not null)
            {
                fody.Add(new XElement("ExcludeAssets", "runtime"));
                fody.Add(new XElement("IncludeAssets", "build; native; contentfiles; analyzers"));
            }
            Content = csproj.ToString();
        }

        public string Content { get; }

    }
}
