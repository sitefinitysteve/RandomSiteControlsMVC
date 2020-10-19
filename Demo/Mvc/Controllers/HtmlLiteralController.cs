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
    [ControllerToolboxItem(Name = "HtmlLiteral_Demo_MVC", Title = "HtmlLiteral Demo", SectionName = "ContentToolboxSection", CssClass= "sfSearchBoxIcn sfMvcIcn")]
    public class HtmlLiteralController : Controller
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
            View(this.TemplateName);
        }

        public string TemplateName { get; set; } = "Default";
    }
}