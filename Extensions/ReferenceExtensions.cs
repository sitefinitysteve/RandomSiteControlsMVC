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
        /// 🔥 From SitefinitySteve
        /// </summary>
        public static MvcHtmlString ScriptVersioned(this HtmlHelper helper, string filename)
        {
            string version = GetVersion(helper, filename);
            return MvcHtmlString.Create("<script type='text/javascript' src='{0}?v={1}'></script>".Arrange(filename, version));
        }

        /// <summary>
        /// 🔥 From SitefinitySteve
        /// </summary>
        public static MvcHtmlString StyleSheetVersioned(this HtmlHelper helper, string filename, bool fullyQualified = false)
        {
            string version = GetVersion(helper, filename);
            return MvcHtmlString.Create("<link href='{0}?v={1}' rel='stylesheet' type='text/css' />".Arrange(filename, version));
        }

        private static string GetVersion(this HtmlHelper helper, string filename)
        {
            var context = helper.ViewContext.RequestContext.HttpContext;

            if (context.Cache[filename] == null)
            {
                var physicalPath = context.Server.MapPath(filename);
                var version = new System.IO.FileInfo(physicalPath).LastWriteTime.ToString("MMddHHmmss");
                context.Cache.Add(filename, version, null,
                  DateTime.Now.AddMinutes(5), TimeSpan.Zero,
                  CacheItemPriority.Normal, null);
                return version;
            }
            else
            {
                return context.Cache[filename] as string;
            }
        }
    }
}
