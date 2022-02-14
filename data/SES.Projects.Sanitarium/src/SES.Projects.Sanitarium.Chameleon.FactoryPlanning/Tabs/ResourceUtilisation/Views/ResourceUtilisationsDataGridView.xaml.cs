namespace SES.Projects.Sanitarium.Chameleon.FactoryPlanning.Views
{
    using Gum.Controls;
    using Orc.Analytics;

    public partial class ResourceUtilisationsDataGridView
    {
        public ResourceUtilisationsDataGridView()
        {
            OpaqueAccentColorHelper.CreateOpaqueAccentColorResourceDictionary();

            InitializeComponent();

            this.TrackViewForAnalyticsAsync();
        }
    }
}
