using System;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using Telerik.Sitefinity.Mvc;
using RandomSiteControlsMVC.Mvc.Models;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers.Attributes;
using SitefinityWebApp.Mvc.Models.HeaderTitle;
using SitefinityWebApp.Mvc.Models.DocLink;

namespace RandomSiteControlsMVC.Mvc.Controllers
{
    [EnhanceViewEnginesAttribute]
    [ControllerToolboxItem(Name = "DocLinkController_MVC", Title = "Doc Link", SectionName = "ContentToolboxSection", CssClass= "sfSearchBoxIcn sfMvcIcn")]
    public class DocLinkController : Controller
    {
        /// <summary>
        /// This is the default Action.
        /// </summary>
        public ActionResult Index()
        {
            var model = this.GetModel();

            return View(this.TemplateName, model);
        }

        public DocLinkModel GetModel()
        {
            return new DocLinkModel(this.Text, this.Link);
        }

        protected override void HandleUnknownAction(string actionName)
        {
            View(this.TemplateName, this.GetModel()).ExecuteResult(this.ControllerContext);
        }

        public string Text { get; set; } = "Visit the Wiki";

        public string Link { get; set; } = "https://github.com/sitefinitysteve/RandomSiteControlsMVC/wiki";

        public string TemplateName { get; set; } = "DocLink";
    }
}