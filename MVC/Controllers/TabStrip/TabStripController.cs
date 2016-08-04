using System;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using System.Web;
using Telerik.Sitefinity.Mvc;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Modules.Pages.Configuration;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers.Attributes;
using RandomSiteControlsMVC.MVC.Models.TabStrip;
using Telerik.Sitefinity;
using Telerik.Sitefinity.Services;
using System.Collections.Generic;

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
            model.TabPosition = this.TabPosition;
            model.ClassName = this.ClassName;

            //Load Saved tabs
            var tabs = this.DeserializeTabs();

            //Check for no tabs
            if (tabs.Count == 0)
            {
                model.Tabs.Add(new Tab("Tab1", true));
                model.Tabs.Add(new Tab("Tab2"));
                model.Tabs.Add(new Tab("Tab3"));
            }
            else
            {
                if (!String.IsNullOrEmpty(this.QuerystringKey))
                {
                    //Check for set querystring key
                    if (Request.QueryString[this.QuerystringKey] != null)
                    {
                        var value = Request.QueryString[this.QuerystringKey];

                        //Okay the key is there, now we need to look for a value
                        var luckyTab = tabs.FirstOrDefault(x => x.QuerystringValue == value);
                        if (luckyTab != null)
                        {
                            //Deselect all the existing tabs
                            tabs.Select(c => { c.Selected = false; return c; }).ToList();

                            luckyTab.Selected = true;
                        }
                    }
                }

                //Check for selection
                if (tabs.Count(x => x.Selected) == 0)
                {
                    //Select the first one
                    tabs[0].Selected = true;
                }

                model.Tabs.AddRange(tabs);
            }

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

        string _tabs = "[]";
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

        string _className = String.Empty;
        public string ClassName
        {
            get { return _className; }
            set
            {
                _className = value;
            }
        }

        string _querystringKey = String.Empty;
        public string QuerystringKey
        {
            get { return _querystringKey; }
            set
            {
                _querystringKey = value;
            }
        }

        public string CurrentTheme
        {
            get { return RSCUtil.SfsConfig.TabstripTheme; }
        }

        #region AdditionalProperties
        string _tabPosition = "top";
        public string TabPosition
        {
            get { return _tabPosition; }
            set
            {
                _tabPosition = value;
            }
        }

        #endregion

        private List<uib-tab> DeserializeTabs()
        {
            return ServiceStack.Text.JsonSerializer.DeserializeFromString<List<uib-tab>>(this.SerializedTabs);
        }

    }
}