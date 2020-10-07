using System;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using Telerik.Sitefinity.Mvc;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Modules.Pages.Configuration;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers.Attributes;
using RandomSiteControlsMVC.MVC.Models.WidgetFinder;
using Telerik.Sitefinity;
using Telerik.Sitefinity.Fluent.Pages;
using Telerik.Sitefinity.Data;

namespace SitefinityWebApp.Mvc.Controllers
{
    [EnhanceViewEnginesAttribute]
    [ControllerToolboxItem(Name = "WidgetFinderMVC", Title = "Widget Finder", SectionName = ToolboxesConfig.ContentToolboxSectionName, CssClass = "sfSearchResultIcn sfMvcIcn")]
    public class WidgetFinderController : Controller
    {
        public ActionResult Index(string control)
        {
            WidgetFinderModel model = GetModel();

            if (this.AllowUnauthenticated)
            {
                var pageManager = App.WorkWith().Pages().GetManager();
                using (var elevatedModeRegion = new ElevatedModeRegion(pageManager))
                {
                    this.GetData(control, model);
                }
            }
            else
            {
                this.GetData(control, model);
            }

            return View(this.TemplateName, model);
        }

        private void GetData(string control, WidgetFinderModel model)
        {
            model.Controls = App.WorkWith().Pages().LocatedIn(PageLocation.Frontend)
                                .Where(p => p.Page != null)
                                .Get()
                                .SelectMany(x => x.Page.Controls)
                                .Where(x => !x.ObjectType.Contains("GridSystem"))
                                .Select(x => x.Caption)
                                .ToList().Distinct();

            model.SelectedControl = control;
            if (!String.IsNullOrEmpty(control))
            {
                model.FoundPages.AddRange(App.WorkWith().Pages().LocatedIn(PageLocation.Frontend)
                                    .Where(p => p.Page != null &&
                                        p.Page.Controls.Where(c => c.Caption == control).Count() > 0)
                                    .Get()
                                    .ToList());
            }
        }

        private WidgetFinderModel GetModel()
        {
            var model = new WidgetFinderModel();

            return model;
        }

        protected override void HandleUnknownAction(string actionName)
        {
            View(this.TemplateName, this.GetModel()).ExecuteResult(this.ControllerContext);
        }

        #region PROPERTIES
        public string TemplateName { get; set; } = "Default";
        public bool AllowUnauthenticated { get; set; } = false;
        #endregion
    }
}