using RandomSiteControlsMVC.MVC.Models.Twitter;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers.Attributes;
using Telerik.Sitefinity.Mvc;
using TweetSharp;
using Telerik.Sitefinity.Publishing.Twitter;
using System.Diagnostics;
using Telerik.Sitefinity;
using Telerik.Sitefinity.Web.UI;
using Telerik.Microsoft.Practices.EnterpriseLibrary.Logging;

namespace RandomSiteControlsMVC.MVC.Controllers
{
    public enum TwitterModeEnum
    {
        UserTimeline,
        HomeTimeline,
        MentioningMe
    }

    [EnhanceViewEnginesAttribute]
    [ControllerToolboxItem(Name = "Twitter_SFS_MVC", Title = "Twitter", SectionName = "Social", CssClass = "sfTwitterFeedIcn sfMvcIcn")]
    public class TwitterController : Controller, ICustomWidgetVisualization
    {
        public ActionResult Index()
        {
            var model = this.GetModel();

            return View(this.Template, model);
        }

        private TwitterModel GetModel()
        {
            return new TwitterModel();
        }


        protected override void HandleUnknownAction(string actionName)
        {
            View(this.Template, this.GetModel()).ExecuteResult(this.ControllerContext);
        }

        private string _template = "Default";
        public string Template
        {
            get { return _template; }
            set
            {
                _template = value;
            }
        }

        private int _cacheTimeout = 15;
        public int CacheTimeoutMins
        {
            get { return _cacheTimeout; }
            set
            {
                _cacheTimeout = value;
            }
        }
        

        public bool IsEmpty
        {
            get
            {
                return RSCUtil.SfsConfig.Twitter.ConsumerKey.IsNullOrEmpty() && RSCUtil.SfsConfig.Twitter.ConsumerSecret.IsNullOrEmpty() && RSCUtil.SfsConfig.Twitter.AccessToken.IsNullOrEmpty() && RSCUtil.SfsConfig.Twitter.AccessTokenSecret.IsNullOrEmpty();
            }
        }

        public string EmptyLinkText => "Missing Twitter API Keys, please set in the backend /Sitefinity/Administration/Settings/Advanced/SitefinitySteveMvc";
    }
}
