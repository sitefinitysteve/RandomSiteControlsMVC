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

            var themeName = String.Empty;
            if (!String.IsNullOrEmpty(ThemeOverride))
            {
                themeName = this.ThemeOverride;
            }
            else
            {
                themeName = RSCUtil.SfsConfig.TabstripTheme;

                //Fix bad names
                if (themeName == "KendoUI")
                    themeName = "Kendo";
            }

            return View(themeName, model);
        }

        string _tabs = String.Empty;
        public string SerializedTabs
        {
            get { return _tabs; }
            set
            {
                _tabs = value;
            }
        }

        string _theme = String.Empty;
        public string ThemeOverride
        {
            get { return _theme; }
            set
            {
                _theme = value;
            }
        }
    }
}