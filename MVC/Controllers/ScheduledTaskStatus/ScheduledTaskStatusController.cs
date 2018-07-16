using System;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using Telerik.Sitefinity.Mvc;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Modules.Pages.Configuration;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers.Attributes;
using RandomSiteControlsMVC.MVC.Models.ScheduledTaskStatus;
using Telerik.Sitefinity;
using Telerik.Sitefinity.Fluent.Pages;

namespace SitefinityWebApp.Mvc.Controllers
{
    [EnhanceViewEnginesAttribute]
    [ControllerToolboxItem(Name = "ScheduledTaskStatusMVC", Title = "Scheduled Task Status", SectionName = ToolboxesConfig.ContentToolboxSectionName, CssClass = "sfSearchResultIcn sfMvcIcn")]
    public class ScheduledTaskStatusController : Controller
    {
        public ActionResult Index(string control)
        {
            ScheduledTaskStatusModel model = GetModel();



            return View(this.TemplateName, model);
        }

        private ScheduledTaskStatusModel GetModel()
        {
            var model = new ScheduledTaskStatusModel();

            return model;
        }

        protected override void HandleUnknownAction(string actionName)
        {
            View(this.TemplateName, this.GetModel()).ExecuteResult(this.ControllerContext);
        }

        #region PROPERTIES
        private string _templateName = "ScheduledTaskStatus";
        public string TemplateName
        {
            get { return _templateName; }
            set
            {
                _templateName = value;
            }
        }
        
        #endregion
    }
}