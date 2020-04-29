using Common.Logging.Configuration;
using NLog;
using NLog.Config;
using NLog.Targets;
using Splitio.Services.Client.Classes;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace PushSupportNetFramework472
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            InitializeLogger();

            var apikey = "gmadcr3k2qr7kfhu34lemso7v341hfipmiha";

            var config = new ConfigurationOptions
            {
                Endpoint = "https://sdk.split-stage.io",
                EventsEndpoint = "https://events.split-stage.io",
                StreamingEnabled = true,
                FeaturesRefreshRate = 50000,
                SegmentsRefreshRate = 50000
            };

            var factory = new SplitFactory(apikey, config);
            var sdk = factory.Client();

            Application["sdk"] = sdk;
            Application["config"] = config;

            sdk.BlockUntilReady(10000);
        }

        private static void InitializeLogger()
        {
            //NLog configuration
            var fileTarget = new FileTarget();
            fileTarget.Name = "splitio";
            fileTarget.FileName = @"C:\Logs\472\splitio.log";
            fileTarget.ArchiveFileName = @"C:\Logs\472\splitio.{#}.log";
            fileTarget.LineEnding = LineEndingMode.CRLF;
            fileTarget.Layout = "${longdate} ${level: uppercase = true} ${logger} - ${message} - ${exception:format=tostring}";
            fileTarget.ConcurrentWrites = true;
            fileTarget.CreateDirs = true;
            fileTarget.ArchiveNumbering = ArchiveNumberingMode.DateAndSequence;
            fileTarget.ArchiveAboveSize = 200000000;
            fileTarget.ArchiveDateFormat = "yyyyMMdd";
            fileTarget.MaxArchiveFiles = 30;
            var rule = new LoggingRule("*", LogLevel.Debug, fileTarget);

            var config = new LoggingConfiguration();
            config.AddTarget("splitio", fileTarget);
            config.LoggingRules.Add(rule);
            LogManager.Configuration = config;

            //Commmon.Logging configuration with NLog adapter
            NameValueCollection properties = new NameValueCollection();
            properties["configType"] = "INLINE";

            Common.Logging.LogManager.Adapter = new Common.Logging.NLog.NLogLoggerFactoryAdapter(properties);
        }
    }
}
