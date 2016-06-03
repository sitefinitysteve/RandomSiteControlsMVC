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

        [ConfigurationProperty("ContentLiteral")]
        public ContentLiteralConfigElement ContentLiteral
        {
            get
            {
                return (ContentLiteralConfigElement)this["ContentLiteral"];
            }
            set
            {
                this["ContentLiteral"] = value;
            }
        }
    }
}
