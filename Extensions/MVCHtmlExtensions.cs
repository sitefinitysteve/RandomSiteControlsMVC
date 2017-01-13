using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity;
using Telerik.Sitefinity.GenericContent.Model;
using HtmlAgilityPack;
using RandomSiteControlsMVC.Helpers;
using Telerik.Sitefinity.Web.Utilities;
using Telerik.Sitefinity.Modules.GenericContent;
using System.IO;
using System.Web.Routing;
using Telerik.Sitefinity.Frontend.Media.Mvc.Controllers;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers;
using Telerik.Sitefinity.Frontend.Media.Mvc.Models.Image;
using Telerik.Sitefinity.Mvc;
using System.Diagnostics;
using RandomSiteControlsMVC.MVC.Models.InlineMarkupHelpers;

namespace Telerik.Sitefinity
{
    public static class SFSHtml
    {
        private const string _mediaItemPrefix = "sfvrsn";

        public static IHtmlString EnhanceRaw(string html, string imageViewPath = "/Views/Image/Image.Inline.cshtml", string documentViewPath = "/Views/Document/DocumentLink.Inline.cshtml")
        {
            //Resolve [OpenAccess provider style links first]
            html = LinkParser.ResolveLinks(html, DynamicLinksParser.GetContentUrl, null, false);

            if (html.Contains(_mediaItemPrefix))
            {
                try
                {
                    var doc = new HtmlDocument();
                    doc.LoadHtml(html);

                    //Parse images
                    var images = doc.DocumentNode.SelectNodes("//img[contains(@src, '" + _mediaItemPrefix + "')]");

                    if (images != null)
                    {
                        foreach (var node in images)
                        {
                            //Find the id
                            var imageRef = new SfImageLink(node.GetAttributeValue("src", "#"));

                            if (imageRef.FoundDataItem())
                            {
                                if (HttpContext.Current != null)
                                {
                                    var markup = SFSHtml.GetRazorViewAsString(new ImageController(), imageRef.DataItem, imageViewPath);
                                    var newNode = HtmlNode.CreateNode(markup);
                                    node.ParentNode.ReplaceChild(newNode, node);
                                }

                            }
                        }
                    }

                    //Parse documents
                    var documents = doc.DocumentNode.SelectNodes("//a[contains(@href, '" + _mediaItemPrefix + "')]");
                    if (documents != null)
                    {
                        foreach (var node in documents)
                        {
                            var src = node.GetAttributeValue("href", "#");

                            if (src.Contains("?{0}".Arrange(_mediaItemPrefix)) && src != "#")
                            {
                                //Find the id
                                var docRef = new SfDocLink(src);

                                if (docRef.FoundDataItem())
                                {
                                    var markup = SFSHtml.GetRazorViewAsString(new DocumentController(), docRef.DataItem, documentViewPath);
                                    var newNode = HtmlNode.CreateNode(markup);
                                    node.ParentNode.ReplaceChild(newNode, node);
                                }
                            }
                        }
                    }

                    //Parse pages
                    /*
                    var pages = doc.DocumentNode.SelectNodes("//a[starts-with(@href, '/')]);
                    if (pages != null)
                    {
                        foreach (var node in pages)
                        {
                            //TODO
                        }
                    }
                    */

                    var fixedHtml = doc.DocumentNode.OuterHtml;
                    //result = LinkParser.ResolveLinks(fixedHtml, DynamicLinksParser.GetContentUrl, null, false);

                    var finishedHtml = MvcHtmlString.Create(fixedHtml);
                    return finishedHtml;
                }
                catch (Exception ex)
                {
                    Telerik.Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Writer.Write(ex);
                    //Problem, just show something
                    return MvcHtmlString.Create(html);
                }
            }
            else
            {
                //No version
                return MvcHtmlString.Create(html);
            }
        }

        public static string GetRazorViewAsString(Controller controller, object model, string viewPath)
        {
            var st = new StringWriter();
            var context = new HttpContextWrapper(HttpContext.Current);
            var routeData = new RouteData();
            var controllerContext = new ControllerContext(new RequestContext(context, routeData), new ImageController());

            var razor = new RazorView(controllerContext, "~/Frontend-Assembly/Telerik.Sitefinity.Frontend/Mvc{0}".Arrange(viewPath), null, false, null);
            razor.Render(new ViewContext(controllerContext, razor, new ViewDataDictionary(model), new TempDataDictionary(), st), st);

            return st.ToString();
        }
    }
}