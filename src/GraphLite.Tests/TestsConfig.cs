# if NETCOREAPP2_0
using Microsoft.Extensions.Configuration;
#endif
# if NETFULL
using System.Configuration;
#endif

namespace GraphLite.Tests
{
    public class TestsConfig
    {        
        public string ApplicationId { get; set; }
        public string ApplicationSecret { get; set; }
        public string Tenant { get; set; }
        public string ExtensionPropertyName { get; set; }

        public static TestsConfig Create()
        {
#if NETCOREAPP2_0
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();
#endif
#if NETFULL
            var configuration = ConfigurationManager.AppSettings;
#endif
            return new TestsConfig
            {
                ApplicationId = configuration["applicationId"],
                ApplicationSecret = configuration["applicationSecret"],
                Tenant = configuration["tenant"],
                ExtensionPropertyName = configuration["extensionProperty"] ?? "TaxRegistrationNumber"
            };
        }
    }
}
