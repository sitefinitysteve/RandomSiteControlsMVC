using System;
using System.Configuration;
using System.Linq;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;

namespace RandomSiteControlsMVC.Configuration
{
    public class DisqusConfigElement : ConfigElement
    {
        public DisqusConfigElement(ConfigElement parent)
            : base(parent)
        {
        }

        [ObjectInfo(Description = "Tells the Disqus service your forum's shortname, which is the unique identifier for your website as registered on Disqus", Title = "Short Name")]
        [ConfigurationProperty("shortname", IsRequired = true, IsKey = true)]
        public string ShortName
        {
            get
            {
                return (string)this["shortname"];
            }
            set
            {
                this["shortname"] = value;
            }
        }

        [ObjectInfo(Description="Tells the Disqus service that you are testing the system on an inaccessible website", Title="Developer Mode")]
        [ConfigurationProperty("developermode", DefaultValue = false, IsRequired = true)]
        public bool DeveloperMode
        {
            get
            {
                return (bool)this["developermode"];
            }
            set
            {
                this["developermode"] = value;
            }
        }

        [ObjectInfo(Description = "Runs the Disqus script in design mode.  Moving it or adding more than one probablly will cause script errors", Title = "Allow Script In DesignMode")]
        [ConfigurationProperty("renderInDesignMode", DefaultValue = false, IsRequired = true)]
        public bool RenderScriptInDesignMode
        {
            get
            {
                return (bool)this["renderInDesignMode"];
            }
            set
            {
                this["renderInDesignMode"] = value;
            }
        }
    }
}
