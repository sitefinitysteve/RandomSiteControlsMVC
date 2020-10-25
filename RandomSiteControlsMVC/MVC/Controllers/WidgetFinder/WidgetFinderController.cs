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
using System.Diagnostics;
using RandomSiteControlsMVC.MVC.Models.WidgetFinder;

namespace SitefinityWebApp.Mvc.Controllers
{
    [EnhanceViewEnginesAttribute]
    [ControllerToolboxItem(Name = "WidgetFinderEAMVC", Title = "Widget Finder EA", SectionName = ToolboxesConfig.ContentToolboxSectionName, CssClass = "sfSearchResultIcn sfMvcIcn")]
    public class WidgetFinderController : Controller
    {
        public ActionResult Index(string control, string objecttype)
        {
            WidgetFinderModel model = GetModel();

            if (this.AllowUnauthenticated)
            {
                var pageManager = App.WorkWith().Pages().GetManager();
                using (var elevatedModeRegion = new ElevatedModeRegion(pageManager))
                {
                    this.GetData(control, objecttype, model);
                }
            }
            else
            {
                this.GetData(control, objecttype, model);
            }

            return View(this.TemplateName, model);
        }

        private void GetData(string control, string objecttype, WidgetFinderModel model)
        {
            model.Controls.AddRange(App.WorkWith().Pages().LocatedIn(PageLocation.Frontend)
                                .Where(p => p.Page != null)
                                .Get()
                                .SelectMany(x => x.Page.Controls)
                                .Where(x => !x.ObjectType.Contains("GridSystem"))
                                .GroupBy(c => new
                                {
                                    c.Caption,
                                    c.ObjectType,
                                }
                                ).Select(x => new WidgetGroup(x.Key.Caption, x.Key.ObjectType, x.Count())));



            model.SelectedControl = control;
            if (!String.IsNullOrEmpty(control))
            {
                var deEncodedControl = Telerik.Sitefinity.Services.SystemManager.CurrentHttpContext.Server.HtmlDecode(control.Replace("%2B", "+"));

                model.FoundPages.AddRange(App.WorkWith().Pages().LocatedIn(PageLocation.Frontend)
                                    .Where(p => p.Page != null &&
                                        p.Page.Controls.Where(c => c.Caption == deEncodedControl && c.ObjectType == objecttype).Count() > 0)
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
        public string TemplateName { get; set; } = "WidgetFinder";
        public bool AllowUnauthenticated { get; set; } = false;
        #endregion
    }
}