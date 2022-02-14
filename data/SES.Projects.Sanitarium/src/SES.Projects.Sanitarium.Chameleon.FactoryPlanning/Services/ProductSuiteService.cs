namespace SES.Projects.Sanitarium.Chameleon.FactoryPlanning.Services
{
    using System.Threading.Tasks;
    using Catel.Services;
    using Gum;

    public class ProductSuiteService : global::Chameleon.Services.ProductSuiteService
    {
        public ProductSuiteService(IProcessService processService)
            : base(processService)
        {
        }

        public override async Task OpenProductAsync(ApplicationChannel channel)
        {
            var application = channel.Application;
            if (application.Abbreviation == "RT")
            {
                var path = "";

                // TODO :Export to path

                var arguments = $"\"{path}\"";

                await OpenProductAsync(channel, arguments);

                return;
            }

            await base.OpenProductAsync(channel);
        }
    }
}
