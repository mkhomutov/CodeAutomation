namespace SES.Projects.Sanitarium.Chameleon.FactoryPlanning.Views
{
    using Gum.Controls;
    using Orc.Analytics;

    public partial class ItemInventoryMovementsDataGridView
    {
        public ItemInventoryMovementsDataGridView()
        {
            OpaqueAccentColorHelper.CreateOpaqueAccentColorResourceDictionary();

            InitializeComponent();

            this.TrackViewForAnalyticsAsync();
        }
    }
}
