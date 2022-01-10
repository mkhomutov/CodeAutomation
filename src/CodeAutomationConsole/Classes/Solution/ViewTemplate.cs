namespace CodeAutomationConsole
{
    using System.IO;
    using System.Xml.Linq;

    public class ViewTemplate
    {
        private readonly XNamespace _catel = "http://schemas.catelproject.com";
        private readonly XNamespace _orccontrols = "http://schemas.wildgums.com/orc/controls";
        private readonly XNamespace _orctheming = "http://schemas.wildgums.com/orc/theming";
        private readonly XNamespace _orchestra = "http://schemas.wildgums.com/orchestra";
        private readonly XNamespace _x = "http://schemas.microsoft.com/winfx/2006/xaml";
        private readonly XNamespace _gumui = "http://schemas.wildgums.com/gum/controls";
        private readonly XNamespace _fluent = "urn:fluent-ribbon";
        private readonly XNamespace _mc = "http://schemas.openxmlformats.org/markup-compatibility/2006";
        private readonly XNamespace _d = "http://schemas.microsoft.com/expression/blend/2008";
        private readonly XNamespace _gumprojects = "http://schemas.wildgums.com/gum/projects";
        private readonly XNamespace _default = "http://schemas.microsoft.com/winfx/2006/xaml/presentation";
        private readonly XNamespace _views = "http://schemas.wildgums.com/gum/ui";

        public ViewTemplate(string projectPath, string viewName)
        {
            ProjectPath = projectPath;
            ViewName = viewName;
        }

        public XNamespace Catel => _catel;
        public XNamespace Orccontrols => _orccontrols;
        public XNamespace Orctheming => _orctheming;
        public XNamespace Orchestra => _orchestra;
        public XNamespace X => _x;
        public XNamespace GumUI => _gumui;
        public XNamespace Fluent => _fluent;

        public XNamespace Mc => _mc;

        public XNamespace D => _d;
        public XNamespace GumProjects => _gumprojects;
        public XNamespace Default => _default;
        public XNamespace Views => _views;

        public string ProjectPath { get; set; }

        public string ViewContent { get; set; }

        public string ViewCsContent { get; set; }

        public string ViewModelContent { get; set; }

        public string ViewName { get; set; }

        

        public void Save()
        {
            var viewFile = Path.Combine(ProjectPath, "Views", $"{ViewName}.xaml");
            var viewCsFile = Path.Combine(ProjectPath, "Views", $"{ViewName}.xaml.cs");
            var viewModelFile = Path.Combine(ProjectPath, "ViewModels", $"{ViewName}Model.cs");

            ViewContent.SaveToFile(viewFile);
            ViewCsContent.AddCopyright($"{ViewName}.xaml.cs").SaveToFile(viewCsFile);
            ViewModelContent.AddCopyright($"{ViewName}Model.cs").SaveToFile(viewModelFile);
        }
    }
}
