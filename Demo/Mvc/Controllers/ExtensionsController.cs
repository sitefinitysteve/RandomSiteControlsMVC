using System;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using Telerik.Sitefinity.Mvc;
using RandomSiteControlsMVC.Mvc.Models;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers.Attributes;
using SitefinityWebApp.Mvc.Models.Extensions;

namespace RandomSiteControlsMVC.Mvc.Controllers
{
    [EnhanceViewEnginesAttribute]
    [ControllerToolboxItem(Name = "ExtensionsController_MVC", Title = "Extensions", SectionName = "ContentToolboxSection", CssClass= "sfSearchBoxIcn sfMvcIcn")]
    public class ExtensionsController : Controller
    {
        /// <summary>
        /// This is the default Action.
        /// </summary>
        public ActionResult Index()
        {
            var model = this.GetModel();

            return View(this.TemplateName, model);
        }

        public ExtensionsModel GetModel()
        {
            return new ExtensionsModel();
        }

        protected override void HandleUnknownAction(string actionName)
        {
            View(this.TemplateName, this.GetModel()).ExecuteResult(this.ControllerContext);
        }

        public string TemplateName { get; set; } = "Extensions";
    }
}