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
using SitefinityWebApp.Mvc.Models.SideNav;

namespace RandomSiteControlsMVC.Mvc.Controllers
{
    [EnhanceViewEnginesAttribute]
    [ControllerToolboxItem(Name = "StaticPageController_MVC", Title = "StaticPage", SectionName = "ContentToolboxSection", CssClass= "sfSearchBoxIcn sfMvcIcn")]
    public class StaticPageController : Controller
    {
        /// <summary>
        /// This is the default Action.
        /// </summary>
        public ActionResult Index()
        {
            return View(this.TemplateName);
        }

        protected override void HandleUnknownAction(string actionName)
        {
            View(this.TemplateName).ExecuteResult(this.ControllerContext);
        }

        public string TemplateName { get; set; } = "Home";
    }
}