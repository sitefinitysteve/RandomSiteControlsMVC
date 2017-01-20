using System;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using Telerik.Sitefinity.Mvc;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Modules.Pages.Configuration;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers.Attributes;
using RandomSiteControlsMVC.MVC.Models.Markdown;
using Telerik.Microsoft.Practices.EnterpriseLibrary.Logging;
using ServiceStack.Formats;
using ServiceStack;
using MarkdownSharp;

namespace SitefinityWebApp.Mvc.Controllers
{
    [EnhanceViewEnginesAttribute]
    [ControllerToolboxItem(Name = "MarkdownMVC", Title = "Content markdown", SectionName = ToolboxesConfig.ContentToolboxSectionName, CssClass = "sfContentBlockIcn sfMvcIcn")]
    public class MarkdownController : Controller, ICustomWidgetVisualization
    {
        public ActionResult Index()
        {
            MarkdownModel model = GetModel();

            return View("Default", model);
        }

        protected override void HandleUnknownAction(string actionName)
        {
            View("Default", this.GetModel()).ExecuteResult(this.ControllerContext);
        }

        private MarkdownModel GetModel()
        {
            var model = new MarkdownModel();

            if (!this.IsEmpty)
            {
                model.Markdown = this.GetHtml();
            }

            model.UseWrapper = this.UseWrapper;
            return model;
        }

        //From ServiceStack HtmlHelper
        protected string GetHtml()
        {
            try
            {
                var helper = new ServiceStack.Html.HtmlHelper();
                var html = helper.RenderMarkdownToHtml(this.Markdown);

                return html.ToHtmlString();
            }
            catch (Exception ex)
            {
                Logger.Writer.Write(ex);
                return "Error Rendering Markdown";
            }
        }


        #region PROPERTIES
        string _content = String.Empty;
        public string Markdown
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
        #endregion
    }
}