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

        [ObjectInfo(Description = "This is where the Html.Script tag will render jQuery", Title = "jQuery Script Placeholder")]
        [ConfigurationProperty("jQueryScriptPlaceholder", IsRequired = true, DefaultValue = "top")]
        public string jQueryScriptPlaceholder
        {
            get
            {
                return (string) this["jQueryScriptPlaceholder"];
            }
            set
            {
                this["jQueryScriptPlaceholder"] = value;
            }
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

        [ObjectInfo(Description = "Default place where widget scripts will render", Title = "Script Placeholder")]
        [ConfigurationProperty("DefaultScriptPlaceholder", IsRequired = true, DefaultValue = "bottom")]
        public string DefaultScriptPlaceholder
        {
            get
            {
                return (string) this["DefaultScriptPlaceholder"];
            }
            set
            {
                this["DefaultScriptPlaceholder"] = value;
            }
        }

        [ObjectInfo(Description = "Default Cache time for widgets", Title = "Cache Timeout Minutes")]
        [ConfigurationProperty("CacheTimeout", IsRequired = true, DefaultValue = 30)]
        public int CacheTimeoutMinutes
        {
            get
            {
                return (int) this["CacheTimeout"];
            }
            set
            {
                this["CacheTimeout"] = value;
            }
        }

        [ObjectInfo(Description = "Default Tabstrip to use, Kendo, Bootstrap, or Custom (see docs)", Title = "Tabstrip Theme")]
        [ConfigurationProperty("TabstripTheme", IsRequired = true, DefaultValue = "Bootstrap")]
        public string TabstripTheme
        {
            get
            {
                return (string) this["TabstripTheme"];
            }
            set
            {
                this["TabstripTheme"] = value;
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
