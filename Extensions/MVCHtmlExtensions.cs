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
using Telerik.Sitefinity.Modules.Libraries;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity
{
    public enum RawContentResolveType
    {
        Default,
        None,
        Enhanced
    }

    public static class SFSHtml
    {
        private const string _mediaItemPrefix = "sfvrsn";

        private static string EnhanceRaw(string html, string imageViewPath, string documentViewPath)
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
                            var imageRef = new SfImageLink(node.GetAttributeValue("src", "#"), node.GetAttributeValue("title", ""), node.GetAttributeValue("alt", ""));

                            if (imageRef.FoundDataItem())
                            {
                                if (SystemManager.CurrentHttpContext != null)
                                {
                                    var markup = SFSHtml.GetRazorViewAsString(new ImageController(), imageRef.DataItem, imageViewPath);
                                    var newNode = HtmlNode.CreateNode(markup);

                                    //Reapply all the attributes that were on the original node
                                    foreach (var attribute in node.Attributes.Where(x => x.Name != "src" && x.Name != "title" && x.Name != "alt"))
                                    {
                                        if (newNode.Attributes[attribute.Name] != null)
                                        {
                                            //Merge
                                            var newNodeValue = newNode.GetAttributeValue(attribute.Name, "");
                                            newNode.SetAttributeValue(attribute.Name, newNodeValue + " " + attribute.Value);
                                        }
                                        else
                                        {
                                            //Just add
                                            newNode.SetAttributeValue(attribute.Name, attribute.Value);
                                        }
                                    }

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


                                    //Reapply all the attributes that were on the original node
                                    foreach (var attribute in node.Attributes.Where(x => x.Name != "src" && x.Name != "href" && x.Name != "title" && x.Name != "alt"))
                                    {
                                        if (newNode.Attributes[attribute.Name] != null)
                                        {
                                            //Merge
                                            var newNodeValue = newNode.GetAttributeValue(attribute.Name, "");
                                            newNode.SetAttributeValue(attribute.Name, newNodeValue + " " + attribute.Value);
                                        }
                                        else
                                        {
                                            //Just add
                                            newNode.SetAttributeValue(attribute.Name, attribute.Value);
                                        }
                                    }

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

                    return doc.DocumentNode.OuterHtml;
                }
                catch (Exception ex)
                {
                    Telerik.Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Writer.Write(ex);
                    //Problem, just show something
                    return html;
                }
            }
            else
            {
                //No version
                return html;
            }
        }

        public static IHtmlString Raw(object html)
        {
            var content = LinkParser.ResolveLinks(html.ToString(), DynamicLinksParser.GetContentUrl, null, false);

            return new System.Web.Mvc.MvcHtmlString(content);
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

        public static IHtmlString Raw(this HtmlHelper helper, object html, RawContentResolveType resolveType, string imageViewPath = "/Views/Image/Image.Inline.cshtml", string documentViewPath = "/Views/Document/DocumentLink.Inline.cshtml")
        {
            var content = html.ToString();

            switch (resolveType)
            {
                case RawContentResolveType.Default:
                    content = LinkParser.ResolveLinks(html.ToString(), DynamicLinksParser.GetContentUrl, null, false);
                    break;
                case RawContentResolveType.Enhanced:
                    content = SFSHtml.EnhanceRaw(html.ToString(), imageViewPath, documentViewPath);
                    break;
            }

            return new System.Web.Mvc.MvcHtmlString(content);
        }
    }
}