using System;

using System;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using Telerik.Sitefinity.Mvc;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Modules.Pages.Configuration;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers.Attributes;
using RandomSiteControlsMVC.MVC.Models.Markdown;

namespace SitefinityWebApp.Mvc.Controllers
{
    [EnhanceViewEnginesAttribute]
    [ControllerToolboxItem(Name = "MarkdownMVC", Title = "Content markdown", SectionName = ToolboxesConfig.ContentToolboxSectionName, CssClass = "sfContentBlockIcn sfMvcIcn")]
    public class MarkdownController : Controller, ICustomWidgetVisualization
    {
        /// <summary>
        /// This is the default Action.
        /// </summary>
        public ActionResult Index()
        {
            var model = new MarkdownModel();

            model.Markdown = this.Markdown;
            model.UseWrapper = this.UseWrapper;

            return View("Default", model);
        }

        string _content = String.Empty;
        public string Markdown
        {
            get { return _content; }
            set
            {
                _content = value;
            }
        }

        public bool UseWrapper { get; set; }

        public bool IsEmpty
        {
            get
            {
                return String.IsNullOrEmpty(this.Markdown.Trim());
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