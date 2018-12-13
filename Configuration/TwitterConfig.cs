using System;
using System.Configuration;
using System.Linq;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;

namespace RandomSiteControlsMVC.Configuration
{
    public class TwitterConfigElement : ConfigElement
    {
        public TwitterConfigElement(ConfigElement parent)
            : base(parent)
        {
        }

        [ObjectInfo(Description = "https://developer.twitter.com/", Title = "ConsumerKey")]
        [ConfigurationProperty("ConsumerKey", IsRequired = true)]
        public string ConsumerKey
        {
            get
            {
                return (string)this["ConsumerKey"];
            }
            set
            {
                this["ConsumerKey"] = value;
            }
        }

        [ObjectInfo(Description = "https://developer.twitter.com/", Title = "ConsumerSecret")]
        [ConfigurationProperty("ConsumerSecret", IsRequired = true)]
        public string ConsumerSecret
        {
            get
            {
                return (string)this["ConsumerSecret"];
            }
            set
            {
                this["ConsumerSecret"] = value;
            }
        }

        [ObjectInfo(Description = "https://developer.twitter.com/", Title = "AccessToken")]
        [ConfigurationProperty("AccessToken", IsRequired = true)]
        public string AccessToken
        {
            get
            {
                return (string)this["AccessToken"];
            }
            set
            {
                this["AccessToken"] = value;
            }
        }

        [ObjectInfo(Description = "https://developer.twitter.com/", Title = "AccessTokenSecret")]
        [ConfigurationProperty("AccessTokenSecret", IsRequired = true, IsKey = true)]
        public string AccessTokenSecret
        {
            get
            {
                return (string)this["AccessTokenSecret"];
            }
            set
            {
                this["AccessTokenSecret"] = value;
            }
        }

        [ObjectInfo(Description = "Max Api tweet RETURN count, can prevent malicious calls", Title = "MaxCount")]
        [ConfigurationProperty("MaxCount", IsRequired = true, DefaultValue = 50)]
        public int MaxCount
        {
            get
            {
                return (int)this["MaxCount"];
            }
            set
            {
                this["MaxCount"] = value;
            }
        }

        [ObjectInfo(Description = "How long Sitefinity will cache the results, twitter has an API call\throttle limit", Title = "CacheTimeoutMinutes")]
        [ConfigurationProperty("CacheTimeoutMinutes", IsRequired = true, DefaultValue = 10)]
        public int CacheTimeoutMinutes
        {
            get
            {
                return (int)this["CacheTimeoutMinutes"];
            }
            set
            {
                this["CacheTimeoutMinutes"] = value;
            }
        }
    }
}
