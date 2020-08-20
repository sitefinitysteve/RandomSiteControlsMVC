using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Telerik.Sitefinity;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Abstractions.VirtualPath.Configuration;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Modules.Pages.Configuration;
using ServiceStack;
using ServiceStack.Formats;
using Telerik.Sitefinity.Publishing;
using Telerik.Sitefinity.Publishing.Pipes;
using Telerik.Sitefinity.Services;

namespace RandomSiteControlsMVC
{

    public static class Installer
    {
        /// <summary>
        /// This is the actual method that is called by ASP.NET even before application start. Sweet!
        /// </summary>
        public static void PreApplicationStart()
        {
            // With this method we subscribe for the Sitefinity Bootstrapper_Initialized event, which is fired after initialization of the Sitefinity application
            Bootstrapper.Initialized += (new EventHandler<ExecutedEventArgs>(Installer.Bootstrapper_Initialized));
        }

        public static void Bootstrapper_Initialized(object sender, Telerik.Sitefinity.Data.ExecutedEventArgs e)
        {
            Telerik.Sitefinity.Configuration.Config.RegisterSection<RandomSiteControlsMVC.Configuration.SitefinitySteveMvcConfig>();

            if (e.CommandName == "Bootstrapped")
            {
                //Register ServiceStack route
                SystemManager.RegisterServiceStackPlugin(new RandomSiteControlsMVC.Services.TwitterServicePlugin());

                //Add Tools
                InstallVirtualPaths(); // See that method for VIRTUAL PATHS installation code
                InstallLayouts();
            }
        }

        private static void InstallLayouts()
        {
            var sfconfig = Telerik.Sitefinity.Configuration.Config.Get<RandomSiteControlsMVC.Configuration.SitefinitySteveMvcConfig>();
            if (sfconfig.InstallWidgetsOnSiteInitalize)
            {
                ConfigManager manager = Config.GetManager();
                manager.Provider.SuppressSecurityChecks = true;
                var config = manager.GetSection<ToolboxesConfig>();
                var layouts = config.Toolboxes["PageLayouts"];
                foreach (var s in layouts.Sections)
                {
                    Debug.WriteLine(s); ;
                }

                var section = layouts
                                .Sections
                                .Where<ToolboxSection>(tb => tb.Name == "Controls")
                                .FirstOrDefault();

                if (section == null)
                {
                    section = new ToolboxSection(layouts.Sections)
                    {
                        Name = "Controls",
                        Title = "Controls",
                        Description = "Controls"
                    };
                    layouts.Sections.Add(section);

                    manager.SaveSection(config);
                }

                if (!section.Tools.Any<ToolboxItem>(e => e.Name == "TabStripMVC"))
                {
                    var tool = new ToolboxItem(section.Tools)
                    {
                        Name = "TabStripMVC",
                        Title = "TabStrip",
                        Description = "Renders the parent child layouts as a RadTabStrip",
                        ControlType = "Telerik.Sitefinity.Frontend.GridSystem.GridControl, Telerik.Sitefinity.Frontend",
                        CssClass = "sfL20_20_20_20_20",
                        LayoutTemplate = "~/SitefinitySteveMVC/RandomSiteControlsMVC.MVC.Views.TabStrip.Layouts.tabstrip.html"
                    };
                    section.Tools.Add(tool);
                }

                if (!section.Tools.Any<ToolboxItem>(e => e.Name == "TabMVC"))
                {
                    var tool = new ToolboxItem(section.Tools)
                    {
                        Name = "TabMVC",
                        Title = "Tab",
                        Description = "A tab for the tabstrip",
                        ControlType = "Telerik.Sitefinity.Frontend.GridSystem.GridControl, Telerik.Sitefinity.Frontend",
                        CssClass = "sfL100",
                        LayoutTemplate = "~/SitefinitySteveMVC/RandomSiteControlsMVC.MVC.Views.TabStrip.Layouts.tab.html"
                    };
                    section.Tools.Add(tool);
                }

                //if (!section.Tools.Any<ToolboxItem>(e => e.Name == "FancyBox"))
                //{
                //    var tool = new ToolboxItem(section.Tools)
                //    {
                //        Name = "FancyBox",
                //        Title = "FancyBox Popup",
                //        Description = "Build a popup from the Sitefinity UI",
                //        ControlType = "RandomSiteControls.FancyBox.FancyBoxLayout",
                //        CssClass = "sfsLayoutFancyBoxIcon"
                //    };
                //    section.Tools.Add(tool);
                //}

                manager.SaveSection(config);
                manager.Provider.SuppressSecurityChecks = false;
            }
        }

        private static void InstallVirtualPaths()
        {
            var sfconfig = Telerik.Sitefinity.Configuration.Config.Get<RandomSiteControlsMVC.Configuration.SitefinitySteveMvcConfig>();
            if (sfconfig.InstallVirtualPathsOnSiteInitalize)
            {
                SiteInitializer initializer = SiteInitializer.GetInitializer();
                var virtualPathConfig = initializer.Context.GetConfig<VirtualPathSettingsConfig>();

                string key = "~/SitefinitySteveMVC/*";
                if (!virtualPathConfig.VirtualPaths.ContainsKey(key))
                {
                    var newVirtualPathNode = new VirtualPathElement(virtualPathConfig.VirtualPaths)
                    {
                        VirtualPath = key,
                        ResolverName = "EmbeddedResourceResolver",
                        ResourceLocation = "RandomSiteControlsMVC"
                    };

                    var manager = Config.GetManager();
                    using (new ElevatedModeRegion(manager))
                    {
                        virtualPathConfig.VirtualPaths.Add(newVirtualPathNode);
                        manager.SaveSection(virtualPathConfig);
                    }
                }
            }
        }
    }

}
