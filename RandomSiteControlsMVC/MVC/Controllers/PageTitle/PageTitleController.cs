using System;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using Telerik.Sitefinity.Mvc;
using RandomSiteControlsMVC.Mvc.Models;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers.Attributes;

namespace RandomSiteControlsMVC.Mvc.Controllers
{
    [EnhanceViewEnginesAttribute]
    [ControllerToolboxItem(Name = "PageTitle_MVC", Title = "Page Title", SectionName = "ContentToolboxSection", CssClass= "sfSearchBoxIcn sfMvcIcn")]
    public class PageTitleController : Controller
    {
        public string Title { get; set; }
        public string SubTitle { get; set; }

        /// <summary>
        /// This is the default Action.
        /// </summary>
        public ActionResult Index(string title = "", string subTitle = "")
        {
            PageTitleModel model = GetModel(title, subTitle);

            return View("PageTitle", model);
        }

        public PageTitleModel GetModel(string title = "", string subTitle = "")
        {
            var model = new PageTitleModel();

            if (!String.IsNullOrEmpty(title))
            {
                this.Title = title;
            }

            if (!String.IsNullOrEmpty(subTitle)){
                this.SubTitle = subTitle;
            }

            if (String.IsNullOrEmpty(this.Title))
            {
                var currentNode = SiteMapBase.GetCurrentNode();
                if (currentNode == null)
                    model.Title = "Page Title";
                else
                    model.Title = SiteMapBase.GetCurrentNode().Title;
            }
            else
            {
                model.Title = this.Title;
            }


            model.SubTitle = this.SubTitle;
            return model;
        }

        protected override void HandleUnknownAction(string actionName)
        {
            View("PageTitle", this.GetModel()).ExecuteResult(this.ControllerContext);
        }
    }
}