namespace SES.Projects.Sanitarium.Chameleon.FactoryPlanning.Views
{
    using Gum.Controls;
    using Orc.Analytics;

    public partial class ItemShortagesDataGridView
    {
        public ItemShortagesDataGridView()
        {
            OpaqueAccentColorHelper.CreateOpaqueAccentColorResourceDictionary();

            InitializeComponent();

            this.TrackViewForAnalyticsAsync();
        }
    }
}
