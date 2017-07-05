using System;
using System.Configuration;
using System.Linq;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;

namespace RandomSiteControlsMVC.Configuration
{
    public class GoogleMapsElement : ConfigElement
    {
        public GoogleMapsElement(ConfigElement parent)
            : base(parent)
        {
        }

        [ObjectInfo(Description = "Google maps api key", Title = "API Key")]
        [ConfigurationProperty("apikey", IsRequired = true, IsKey = true)]
        public string ApiKey
        {
            get
            {
                return (string)this["apikey"];
            }
            set
            {
                this["apikey"] = value;
            }
        }
        
    }
}
