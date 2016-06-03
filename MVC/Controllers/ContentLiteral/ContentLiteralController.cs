using System;

using System;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using Telerik.Sitefinity.Mvc;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Modules.Pages.Configuration;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers.Attributes;
using RandomSiteControlsMVC.MVC.Models.ContentLiteral;

namespace SitefinityWebApp.Mvc.Controllers
{
    [EnhanceViewEnginesAttribute]
    [ControllerToolboxItem(Name = "ContentLiteralMVC", Title = "Content html", SectionName = ToolboxesConfig.ContentToolboxSectionName, CssClass = "sfContentBlockIcn sfMvcIcn")]
    public class ContentLiteralController : Controller, ICustomWidgetVisualization
    {
        /// <summary>
        /// This is the default Action.
        /// </summary>
        public ActionResult Index()
        {
            var model = new ContentLiteralModel();

            model.Content = this.HtmlContent;
            model.RemoveWrapper = this.RemoveWrapper;

            return View("Default", model);
        }

        string _content = String.Empty;
        public string HtmlContent
        {
            get { return _content; }
            set
            {
                _content = value;
            }
        }

        public bool RemoveWrapper { get; set; }

        public bool IsEmpty
        {
            get
            {
                return String.IsNullOrEmpty(this.HtmlContent.Trim());
            }
        }

        public string EmptyLinkText
        {
            get
            {
                return "Click to add content";
            }
        }
    }
}