using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Pages.Model;

namespace SitefinityWebApp.Mvc.Models.SideNav
{
    public class SideNavModel
    {
        public SideNavModel()
        {
            PageManager pageManager = PageManager.GetManager();
            this.ComponentPages = pageManager.GetPageNodes().Where(x => x.ParentId == new Guid("1f6ac3e8-7385-4c49-8a3e-51997e1d03bf"));
            this.ExtensionsPage = pageManager.GetPageNode(new Guid("8863bbd2-4e72-463d-8261-f95a4c66b339"));
        }

        public PageNode ExtensionsPage { get; set; }
        public IQueryable<PageNode> ComponentPages { get; set; }
    }
}