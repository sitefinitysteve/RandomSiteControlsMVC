using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Claims;
using Telerik.Sitefinity.Services;

namespace System
{
    public static class TailwindUtil
    {
        /// <summary>
        /// Returns your sitefinity version
        /// 🔥 From SitefinitySteve
        /// </summary>
        /// <returns>string</returns>
        public static string GetFileVersion(string path)
        {
            var context = SystemManager.CurrentHttpContext;
            var physicalPath = context.Server.MapPath(path);
            var version = new System.IO.FileInfo(physicalPath).LastWriteTime.ToString("MMddHHmmss");
            
            return $"{path}?v ={version}";

            /*
            var context = helper.ViewContext.RequestContext.HttpContext;

            if (context.Cache[ScriptPath] == null)
            {
                var physicalPath = context.Server.MapPath(ScriptPath);
                var version = new System.IO.FileInfo(physicalPath).LastWriteTime.ToString("MMddHHmmss");
                context.Cache.Add(ScriptPath, version, null,
                  DateTime.Now.AddMinutes(20), TimeSpan.Zero,
                  CacheItemPriority.Normal, null);
                return version;
            }
            else
            {
                return context.Cache[ScriptPath] as string;
            }
            */
        }

        /// <summary>
        /// Helper to check design mode, the SystemManager thinks Preview is also design
        /// 🔥 From SitefinitySteve
        /// </summary>
        public static bool IsDesignMode
        {
            get
            {
                return SystemManager.IsDesignMode && !SystemManager.IsPreviewMode;
            }
        }

        /// <summary>
        /// Helper to check live mode
        /// 🔥 From SitefinitySteve
        /// </summary>
        public static bool IsLiveMode
        {
            get
            {
                return !SystemManager.IsDesignMode;
            }
        }

        /// <summary>
        /// Helper to check preview mode
        /// 🔥 From SitefinitySteve
        /// </summary>
        public static bool IsPreviewMode
        {
            get
            {
                return SystemManager.IsPreviewMode;
            }
        }

        /// <summary>
        /// Is the site in development, checks for the www
        /// 🔥 From SitefinitySteve
        /// </summary>
        public static bool IsDev
        {
            get
            {
                return !HttpContext.Current.Request.Url.Host.Contains("www");
            }
        }
    }
}