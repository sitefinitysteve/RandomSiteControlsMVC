using System;

using System;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using Telerik.Sitefinity.Mvc;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Modules.Pages.Configuration;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers.Attributes;
using RandomSiteControlsMVC.MVC.Models.TabStrip;
using Telerik.Sitefinity;
using Telerik.Sitefinity.Services;

namespace SitefinityWebApp.Mvc.Controllers
{
    [EnhanceViewEnginesAttribute]
    [ControllerToolboxItem(Name = "TabStripMVC", Title = "Tabstrip", SectionName = ToolboxesConfig.ContentToolboxSectionName, CssClass = "sfFormsIcn sfMvcIcn")]
    public class TabStripController : Controller
    {
        /// <summary>
        /// This is the default Action.
        /// </summary>
        public ActionResult Index()
        {
            var model = new TabStripModel();

            model.Tabs.Add(new Tab("Tab1", true));
            model.Tabs.Add(new Tab("Tab2"));
            model.Tabs.Add(new Tab("Tab3"));

            var themeName = RSCUtil.SfsConfig.TabstripTheme;
            if (themeName != "Bootstrap" || themeName != "Kendo")
            {
                return View("Bootstrap", model);
            }
            else
            {
                //Load default
                return View(themeName, model);
            }
        }

        string _tabs = String.Empty;
        public string Tabs
        {
            get { return _tabs; }
            set
            {
                _tabs = value;
            }
        }
    }
}