namespace SES.Projects.Sanitarium.Chameleon.FactoryPlanning.Views
{
    using Gum.Controls;
    using Orc.Analytics;

    public partial class ItemRoutingsDataGridView
    {
        public ItemRoutingsDataGridView()
        {
            OpaqueAccentColorHelper.CreateOpaqueAccentColorResourceDictionary();

            InitializeComponent();

            this.TrackViewForAnalyticsAsync();
        }
    }
}
