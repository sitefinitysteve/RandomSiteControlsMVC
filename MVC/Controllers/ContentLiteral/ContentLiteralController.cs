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
        public ActionResult Index()
        {
            var model = new ContentLiteralModel();

            model.Content = this.HtmlContent;
            model.UseWrapper = this.UseWrapper;

            return View("Default", model);
        }

        protected override void HandleUnknownAction(string actionName)
        {
            View("Default").ExecuteResult(this.ControllerContext);
        }

        #region PROPERTIES
        string _content = String.Empty;
        public string HtmlContent
        {
            get { return _content; }
            set
            {
                _content = value;
            }
        }

        bool _useWrapper = true;
        public bool UseWrapper
        {
            get { return _useWrapper; }
            set
            {
                _useWrapper = value;
            }
        }
        #endregion

        #region ICustomWidgetVisualization
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
        #endregion
    }
}