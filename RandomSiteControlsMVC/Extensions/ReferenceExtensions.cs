using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Caching;
using System.Web.Mvc;

namespace Telerik.Sitefinity.Frontend.Mvc.Helpers
{
    /// <summary>
    /// Thanks to 
    /// https://stackoverflow.com/questions/2185872/force-browsers-to-get-latest-js-and-css-files-in-asp-net-application
    /// </summary>
    public static class FileReferenceExtensions
    {
        /// <summary>
        /// Append cache busting version a script tag
        /// 🔥 From SitefinitySteve
        /// </summary>
        /// <returns></returns>
        public static MvcHtmlString ScriptVersioned(this HtmlHelper helper, string scriptPath)
        {
            string version = GetVersion(helper, scriptPath);
            return MvcHtmlString.Create("<script type='text/javascript' src='{0}?v={1}'></script>".Arrange(scriptPath, version));
        }

        /// <summary>
        /// Append cache busting version a stylesheet link
        /// 🔥 From SitefinitySteve
        /// </summary>
        /// <returns>MvcHtmlString</returns>
        public static MvcHtmlString StyleSheetVersioned(this HtmlHelper helper, string filename, bool fullyQualified = false)
        {
            string version = GetVersion(helper, filename);
            return MvcHtmlString.Create("<link href='{0}?v={1}' rel='stylesheet' type='text/css' />".Arrange(filename, version));
        }

        /// <summary>
        /// Returns your sitefinity version
        /// 🔥 From SitefinitySteve
        /// </summary>
        /// <returns>string</returns>
        private static string GetVersion(this HtmlHelper helper, string ScriptPath)
        {
            var context = helper.ViewContext.RequestContext.HttpContext;

            if (context.Cache[ScriptPath] == null)
            {
                var physicalPath = context.Server.MapPath(ScriptPath);
                var version = new System.IO.FileInfo(physicalPath).LastWriteTime.ToString("MMddHHmmss");
                context.Cache.Add(ScriptPath, version, null,
                  DateTime.Now.AddMinutes(RSCUtil.SfsConfig.CacheTimeoutMinutesForFileVersions), TimeSpan.Zero,
                  CacheItemPriority.Normal, null);
                return version;
            }
            else
            {
                return context.Cache[ScriptPath] as string;
            }
        }
    }
}
