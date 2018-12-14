using System;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using Telerik.Sitefinity.Mvc;
using System.Linq;
using Telerik.Sitefinity.Publishing;
using Telerik.Sitefinity.Publishing.Model;
using RandomSiteControlsMVC.MVC.Controllers;
using TweetSharp;
using RandomSiteControlsMVC.Services;

namespace RandomSiteControlsMVC.MVC.Models.Twitter
{
    public class TwitterModel
    {
        public TwitterModel(TwitterModeEnum mode, int count, string screenName)
        {
            this.Mode = mode;

            this.Tweets = new List<TwitterStatus>();
            var service = new TwitterApiService();

            this.Error = false;

            try
            {
                switch (mode)
                {
                    case TwitterModeEnum.HomeTimeline:
                        this.Tweets.AddRange(service.Get(new TwitterHomeTimelineRequest() { Take = count + 1 }));
                        break;
                    case TwitterModeEnum.MentioningMe:
                        this.Tweets.AddRange(service.Get(new TwitterMentionsTimelineRequest() { Take = count }));
                        break;
                    case TwitterModeEnum.UserTimeline:
                        this.Tweets.AddRange(service.Get(new TwitterUserTimelineRequest() { ScreenName = screenName, Take = count }));
                        break;
                }
            }catch(Exception ex)
            {
                this.Error = true;
                this.ErrorMessage = ex.Message;
            }
        }

        public string CssClass { get; set; }
        public TwitterModeEnum Mode { get; set; }
        public List<TwitterStatus> Tweets { get; set; }
        public bool Error { get; set; }
        public string ErrorMessage { get; set; }
    }
}
