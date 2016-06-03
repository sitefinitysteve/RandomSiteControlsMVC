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

namespace SitefinityWebApp.Mvc.Controllers
{
    [EnhanceViewEnginesAttribute]
    [ControllerToolboxItem(Name = "TabStripMVC", Title = "Tabstrip", SectionName = ToolboxesConfig.ContentToolboxSectionName, CssClass = "sfFormsIcn sfMvcIcn")]
    public class TabStripController : Controller, ICustomWidgetVisualization
    {
        /// <summary>
        /// This is the default Action.
        /// </summary>
        public ActionResult Index()
        {
            var model = new TabStripModel();

            return View("Default", model);
        }

        string _tabs;
        public string Tabs
        {
            get { return _tabs; }
            set
            {
                _tabs = value;
            }
        }

        public bool IsEmpty
        {
            get
            {
                return String.IsNullOrEmpty(this.Tabs.Trim());
            }
        }

        public string EmptyLinkText
        {
            get
            {
                return "Click to add content";
            }
        }
    }
}