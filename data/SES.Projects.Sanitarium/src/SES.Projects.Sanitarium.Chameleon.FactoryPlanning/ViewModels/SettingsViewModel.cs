namespace SES.Projects.Sanitarium.Chameleon.FactoryPlanning.ViewModels
{
    using System.Threading.Tasks;
    using Catel;
    using Catel.Configuration;
    using Catel.MVVM;
    using Gum.Controls;

    public class SettingsViewModel : ViewModelBase
    {
        private readonly IConfigurationService _configurationService;

        public SettingsViewModel(IConfigurationService configurationService)
        {
            Argument.IsNotNull(() => configurationService);

            _configurationService = configurationService;
        }

        public override string Title
        {
            get { return "Sanitarium settings"; }
        }

        public bool EnableToolTips { get; set; }

        protected override async Task InitializeAsync()
        {
            await base.InitializeAsync();

            EnableToolTips = _configurationService.GetRoamingValue<bool>(DataGridConstants.Application.General.EnableToolTips,
                DataGridConstants.Application.General.EnableToolTipsDefaultValue);
        }

        protected override async Task CloseAsync()
        {
            using (_configurationService.SuspendNotifications())
            {
                _configurationService.SetRoamingValue(DataGridConstants.Application.General.EnableToolTips, EnableToolTips);
            }

            await base.CloseAsync();
        }
    }
}
