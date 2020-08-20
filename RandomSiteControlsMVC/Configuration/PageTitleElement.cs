using System;
using System.Configuration;
using System.Linq;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;

namespace RandomSiteControlsMVC.Configuration
{
    public class PageTitleElement : ConfigElement
    {
        public PageTitleElement(ConfigElement parent)
            : base(parent)
        {
        }

        [ConfigurationProperty("useSubTitle", IsRequired = true, IsKey = true, DefaultValue = true)]
        public bool UseSubTitle
        {
            get
            {
                return (bool)this["useSubTitle"];
            }
            set
            {
                this["useSubTitle"] = value;
            }
        }
        
    }
}
