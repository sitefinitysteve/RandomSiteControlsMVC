using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Sitefinity.Configuration;
using System.Configuration;
using Telerik.Sitefinity.Localization;

namespace RandomSiteControlsMVC.Configuration
{
    public class HttpHeaderElement : ConfigElement
    {
        public HttpHeaderElement(ConfigElement parent)
            : base(parent)
        {
        }

        [ConfigurationProperty("Enabled", IsRequired = true, DefaultValue = true)]
        [ObjectInfo(Description = "When set to false, no checks take place", Title = "Enabled")]
        public bool Enabled
        {
            get
            {
                return (bool)this["Enabled"];
            }
            set
            {
                this["Enabled"] = value;
            }
        }

        [ConfigurationProperty("eTag", IsRequired = true, DefaultValue=true)]
        [ObjectInfo(Description = "Checked removes the Header, change requires AppPool Recycle", Title = "ETag")]
        public bool ETag
        {
            get
            {
                return (bool)this["eTag"];
            }
            set
            {
                this["eTag"] = value;
            }
        }

        [ConfigurationProperty("server", IsRequired = true, DefaultValue = true)]
        [ObjectInfo(Description = "Checked removes the Header, change requires AppPool Recycle", Title = "Server")]
        public bool Server
        {
            get
            {
                return (bool)this["server"];
            }
            set
            {
                this["server"] = value;
            }
        }

        [ConfigurationProperty("xAspNetVersion", IsRequired = true, DefaultValue = true)]
        [ObjectInfo(Description = "Checked removes the Header, change requires AppPool Recycle", Title = "X-AspNet-Version")]
        public bool XAspNetVersion
        {
            get
            {
                return (bool)this["xAspNetVersion"];
            }
            set
            {
                this["xAspNetVersion"] = value;
            }
        }


        [ConfigurationProperty("addxframeoptions", IsRequired = true, DefaultValue = false)]
        [ObjectInfo(Description = "Determines the allowed IFrame state of your site", Title = "X-Frame-Options Add Header")]
        public bool AddXFrameOptions
        {
            get
            {
                return (bool)this["addxframeoptions"];
            }
            set
            {
                this["addxframeoptions"] = value;
            }
        }


        [ConfigurationProperty("xframeoptionsmode", IsRequired = true, DefaultValue = "DENY")]
        [ObjectInfo(Description = "DENY, SAMEORIGIN, ALLOW-FROM", Title = "X-Frame-Options Mode")]
        public string XFrameOptionsMode
        {
            get
            {
                return (string)this["xframeoptionsmode"];
            }
            set
            {
                this["xframeoptionsmode"] = value;
            }
        }

        [ConfigurationProperty("ignorepath", IsRequired = true, DefaultValue = "/images, /sf-images, /docs, /sf-docs, /video, /sf-video, /media, /sf-media, /api/reports")]
        [ObjectInfo(Description = "Paths to not Process, matched by a StartsWith", Title = "Ignore Paths")]
        public string IgnorePaths
        {
            get
            {
                return (string)this["ignorepath"];
            }
            set
            {
                this["ignorepath"] = value;
            }
        }
    }
}
