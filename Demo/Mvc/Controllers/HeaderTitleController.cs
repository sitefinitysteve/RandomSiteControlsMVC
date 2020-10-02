using System;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using Telerik.Sitefinity.Mvc;
using RandomSiteControlsMVC.Mvc.Models;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers.Attributes;
using SitefinityWebApp.Mvc.Models.HeaderTitle;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Security.Configuration;
using Telerik.Sitefinity.Security.Claims;

namespace RandomSiteControlsMVC.Mvc.Controllers
{
    [EnhanceViewEnginesAttribute]
    [ControllerToolboxItem(Name = "HeaderTitle_MVC", Title = "Header Title", SectionName = "ContentToolboxSection", CssClass= "sfSearchBoxIcn sfMvcIcn")]
    public class HeaderTitleController : Controller
    {
        /// <summary>
        /// This is the default Action.
        /// </summary>
        public ActionResult Index()
        {
            var model = this.GetModel();

            return View(this.TemplateName, model);
        }

        public HeaderTitleModel GetModel()
        {
            return new HeaderTitleModel(this.Title, this.CssClass);
        }

        protected override void HandleUnknownAction(string actionName)
        {
            View(this.TemplateName, this.GetModel()).ExecuteResult(this.ControllerContext);
        }

        public string Title { get; set; } = "Header";

        public string CssClass { get; set; } = "mb-4";

        public string TemplateName { get; set; } = "HeaderTitle";
    }
}