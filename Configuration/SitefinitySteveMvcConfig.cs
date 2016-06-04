using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Sitefinity.Configuration;
using System.Configuration;
using Telerik.Sitefinity.Localization;

namespace RandomSiteControlsMVC.Configuration
{
    public class SitefinitySteveMvcConfig : ConfigSection
    {
        protected override void OnPropertiesInitialized()
        {
            base.OnPropertiesInitialized();
        }

        [ObjectInfo(Description = "This is where the Html.Script tag will render kendo", Title = "Kendo Script Placeholder")]
        [ConfigurationProperty("KendoScriptPlaceholder", IsRequired = true, DefaultValue = "top")]
        public string KendoScriptPlaceholder
        {
            get
            {
                return (string) this["KendoScriptPlaceholder"];
            }
            set
            {
                this["KendoScriptPlaceholder"] = value;
            }
        }

        [ConfigurationProperty("HttpHeaderModule")]
        public HttpHeaderElement HttpHeaderModule
        {
            get
            {
                return (HttpHeaderElement)this["HttpHeaderModule"];
            }
        }

        [ConfigurationProperty("disqus")]
        public DisqusConfigElement Disqus
        {
            get
            {
                return (DisqusConfigElement)this["disqus"];
            }
            set
            {
                this["disqus"] = value;
            }
        }
    }
}
