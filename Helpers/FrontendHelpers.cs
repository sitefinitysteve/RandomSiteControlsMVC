using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Web;
using System.Web.Mvc;
using RandomSiteControlsMVC;
using System.Web.UI;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Layouts;
using Telerik.Sitefinity.Frontend.Resources;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Modules.Pages.Configuration;
using Telerik.Sitefinity.Mvc.Rendering;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Frontend.Mvc.Helpers;

namespace RandomSiteControlsMVC.Helpers
{
    public static class RandomSiteControlsFrontendHelpers
    {
        public static MvcHtmlString MarkdownScriptReferences(this HtmlHelper helper)
        {
            StringBuilder stringBuilder = new StringBuilder();
            UrlHelper urlHelper = new UrlHelper(helper.ViewContext.HttpContext.Request.RequestContext);

            stringBuilder.Append(helper.Script(urlHelper.EmbeddedResource(typeof(Reference).FullName, "RandomSiteControlsMVC.Scripts.Markdown.showdown.min.js")).ToHtmlString());

            var scriptString =  MvcHtmlString.Create(stringBuilder.ToString());
            return scriptString; //debugging...
        }
    }
}
