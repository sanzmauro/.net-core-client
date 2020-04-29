using Splitio.Services.Client.Classes;
using Splitio.Services.Client.Interfaces;

namespace PushSupportNetCore22
{
    public class MyAppData
    {
        public ISplitClient Sdk { get; set; }
        public ConfigurationOptions Config { get; set; }

        public MyAppData()
        {

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

            Sdk = sdk;
            Config = config;

            Sdk.BlockUntilReady(10000);
        }
    }
}
