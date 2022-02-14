namespace SES.Projects.Sanitarium.Chameleon.FactoryPlanning.Views
{
    using Gum.Controls;
    using Orc.Analytics;

    public partial class BillOfMaterialRecordsDataGridView
    {
        public BillOfMaterialRecordsDataGridView()
        {
            OpaqueAccentColorHelper.CreateOpaqueAccentColorResourceDictionary();

            InitializeComponent();

            this.TrackViewForAnalyticsAsync();
        }
    }
}
