using Newtonsoft.Json;
using System.IO;

namespace GraphLite.Tests
{
    public class TestsConfig
    {        
        public string ApplicationId { get; set; }
        public string ApplicationSecret { get; set; }
        public string Tenant { get; set; }
        public string ExtensionPropertyName { get; set; } = "TaxRegistrationNumber";

        public static TestsConfig Create()
        {
            var settingsFileName = "appsettings.json";
            if (!File.Exists(settingsFileName))
                throw new FileNotFoundException($"Settings file not present, copy appsettings.stub.json as {settingsFileName}", settingsFileName);

            var json = File.ReadAllText(settingsFileName);
            var config = JsonConvert.DeserializeObject<TestsConfig>(json);
            return config;
        }
    }
}