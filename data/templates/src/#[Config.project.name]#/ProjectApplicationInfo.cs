using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gum.Services;
using Gum;
using Gum.Licensing;

namespace #[$Namespace(name)]#;

internal class ProjectApplicationInfo : Gum.ApplicationInfo
{
    public ProjectApplicationInfo(IAppDetectorService appDetector, IAutomaticUpdatesChannelService channelsDetector)
        : base(appDetector, channelsDetector)
    {
    }

    #region Methods
    protected override void InitializeGeneral(GeneralInfo info)
    {
        base.InitializeGeneral(info);

        // info.AppId = new Guid("0b772e88-cab5-4a1d-b4d3-e0308dd4c7c0");
    }

    protected override void InitializeFeedback(FeedbackInfo info)
    {
        base.InitializeFeedback(info);

        // info.FeedbackUrl = "https://www.wildgums.com";
    }

    protected override void InitializeFileAssociations(FileAssociationsInfo info)
    {
        base.InitializeFileAssociations(info);

        // info.FileExtensions.Add("txt");
    }

    protected override void InitializeOidcClient(OidcClientInfo oidcClient)
    {
        // oidcClient.ClientId = "gum-ui-example";
        // oidcClient.ClientSecret = "4323288b-1aaa-4943-96bb-df92ca715118"; // Replace with a valid secret.
        // oidcClient.ClientSecret = "50e85c10-0faf-4961-bd97-19ac23d66b37"; //
    }

    protected override void InitializeLicense(LicenseInfo info)
    {
        //info.ApplicationId ="MIIBKjCB4wYHKoZIzj0CATCB1wIBATAsBgcqhkjOPQEBAiEA/////wAAAAEAAAAAAAAAAAAAAAD///////////////8wWwQg/////wAAAAEAAAAAAAAAAAAAAAD///////////////wEIFrGNdiqOpPns+u9VXaYhrxlHQawzFOw9jvOPD4n0mBLAxUAxJ02CIbnBJNqZnjhE50mt4GffpAEIQNrF9Hy4SxCR/i85uVjpEDydwN9gS3rM6D0oTlF2JjClgIhAP////8AAAAA//////////+85vqtpxeehPO5ysL8YyVRAgEBA0IABKE5ewCeR/ee0sR83vvckcbeiy6zVJTTIwd/DDmiIcoFSUhhHjGitwvKZolsmXh6DPgNfZnx5200be779ik1R0c=";
        //info.LicenseServer = "https://licensing.webgums.com";
        // info.LicenseServer = "http://localhost:5001/";
    }

    protected override void InitializeAutomaticSupport(AutomaticSupportInfo info)
    {
        // info.SupportServer = "https://support.webgums.com";
        // info.SupportServer = "http://localhost:5003/";
    }

    protected override void InitializeExtensibility(ExtensibilityInfo extensibility)
    {
        extensibility.Mode = ExtensibilityMode.None;
    }

    protected override void InitializeCrashReporting(CrashReportingInfo info)
    {
        // info.ApiKey = "https://491a0c83b87c426f808f2414dcbf1313@o62373.ingest.sentry.io/5986214";
    }
    #endregion
}
