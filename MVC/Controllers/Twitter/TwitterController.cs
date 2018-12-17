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
        MentioningMe,
        SpecificTweet
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
            var model = new TwitterModel(this.Mode, this.Count, this.ScreenName, this.TweetId);

            model.CssClass = this.CssClass;

            if(model.Tweets.Count == 0)
            {
                //Try again
                model = new TwitterModel(this.Mode, this.Count, this.ScreenName, this.TweetId);
            }

            return model;
        }


        protected override void HandleUnknownAction(string actionName)
        {
            View(this.Template, this.GetModel()).ExecuteResult(this.ControllerContext);
        }


        
        private TwitterModeEnum _mode = TwitterModeEnum.HomeTimeline;
        public TwitterModeEnum Mode
        {
            get { return _mode; }
            set
            {
                _mode = value;
            }
        }

        private int _count = 10;
        public int Count
        {
            get { return _count; }
            set
            {
                _count = value;
            }
        }

        private string _screenName;
        public string ScreenName
        {
            get { return _screenName; }
            set
            {
                _screenName = value;
            }
        }

        private string _template = "Tweets";
        public string Template
        {
            get { return _template; }
            set
            {
                _template = value;
            }
        }

        private Int64 _tweetId = -1;
        public Int64 TweetId
        {
            get { return _tweetId; }
            set
            {
                _tweetId = value;
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

        private string _cssClass;
        public string CssClass
        {
            get { return _cssClass; }
            set
            {
                _cssClass = value;
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
