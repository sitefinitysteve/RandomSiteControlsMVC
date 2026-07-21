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
using Markdig;

namespace SitefinityWebApp.Mvc.Controllers
{
    [EnhanceViewEnginesAttribute]
    [ControllerToolboxItem(Name = "MarkdownMVC", Title = "Content markdown", SectionName = ToolboxesConfig.ContentToolboxSectionName, CssClass = "sfContentBlockIcn sfMvcIcn")]
    public class MarkdownController : Controller, ICustomWidgetVisualization
    {
        public ActionResult Index()
        {
            MarkdownModel model = GetModel();

            return View(this.Template, model);
        }

        protected override void HandleUnknownAction(string actionName)
        {
            View(this.Template, this.GetModel()).ExecuteResult(this.ControllerContext);
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

        //Pipelines are immutable and thread safe, build once and reuse
        //UseAutoLinks replaces the old AutoHyperlink\LinkEmails options
        private static readonly MarkdownPipeline Pipeline = new MarkdownPipelineBuilder()
            .UseAdvancedExtensions()
            .UseAutoLinks()
            .Build();

        protected string GetHtml()
        {
            try
            {
                //Markdig.Markdown is qualified because this controller has its own Markdown property
                var html = Markdig.Markdown.ToHtml(this.Markdown, Pipeline);

                return html;
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

        public string Template { get; set; } = "Markdown";


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

