using System;
using System.Configuration;
using System.Linq;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;

namespace RandomSiteControlsMVC.Configuration
{
    public class PlaceholderConfigElement : ConfigElement
    {
        public PlaceholderConfigElement(ConfigElement parent)
            : base(parent)
        {
        }

        [ObjectInfo(Description = "Placeholder service Url", Title = "ServiceUrl")]
        [ConfigurationProperty("serviceUrl", IsRequired = true, IsKey = true, DefaultValue = "http://placehold.it/{0}x{1}/{2}/{3}&text")]
        public string ServiceUrl
        {
            get
            {
                return (string)this["serviceUrl"];
            }
            set
            {
                this["serviceUrl"] = value;
            }
        }

    }
}
