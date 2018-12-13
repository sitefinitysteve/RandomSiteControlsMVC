using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Sitefinity;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.DynamicModules;
using Telerik.Sitefinity.Data.Linq.Dynamic;
using Telerik.Sitefinity.Services.Search;
using Telerik.Sitefinity.Utilities.TypeConverters;
using ServiceStack.Text;
using ServiceStack;
using Telerik.Sitefinity.DynamicModules.Model;
using TweetSharp;
using Telerik.Microsoft.Practices.EnterpriseLibrary.Logging;

namespace RandomSiteControlsMVC.Services
{
    #region PLUGIN
    public class TwitterServicePlugin : IPlugin
    {
        /// <summary>
        /// Call this at SitefinityWebApp.App_Start.RegisterServiceStackRoutes
        /// SystemManager.RegisterServiceStackPlugin(new RandomSiteControlsMVC.Services.TwitterServicePlugin());
        /// </summary>
        public void Register(IAppHost appHost)
        {
            appHost.RegisterService(typeof(TwitterApiService));
        }
    }
    #endregion

    #region SERVICE
    public class TwitterApiService : Service
    {
        public readonly string _noKeysExceptionMessage = "Twitter API Keys not configured in Sitefinity";

        public object Get(HelloRequest request)
        {
            return "I am alive";
        }

        public object Get(TwitterHomeTimelineRequest request)
        {
            if (!this.IsSetup)
                throw new KeyNotFoundException(_noKeysExceptionMessage);

            List<TwitterStatus> tweets = new List<TwitterStatus>();
            var take = this.SanitizeTakeCount(request.Take);

            var cacheKey = "twitter-list-timeline-{0}".Arrange(take);

            try
            {
                if (!RSCUtil.Cache.Contains(cacheKey))
                {
                    var service = new TwitterService(RSCUtil.SfsConfig.Twitter.ConsumerKey, RSCUtil.SfsConfig.Twitter.ConsumerSecret);
                    service.AuthenticateWith(RSCUtil.SfsConfig.Twitter.AccessToken, RSCUtil.SfsConfig.Twitter.AccessTokenSecret);

                    tweets.AddRange(service.ListTweetsOnHomeTimeline(new ListTweetsOnHomeTimelineOptions() { Count = take }));

                    RSCUtil.AddToCache(tweets, cacheKey, TimeSpan.FromMinutes(RSCUtil.SfsConfig.Twitter.CacheTimeoutMinutes));
                }
                else
                {
                    tweets = (List<TwitterStatus>)RSCUtil.Cache[cacheKey];
                }
            }
            catch (Exception ex)
            {
                Logger.Writer.Write(ex);
            }

            return tweets;

        }
        public object Get(TwitterRetweetTimelineRequest request)
        {
            if (!this.IsSetup)
                throw new KeyNotFoundException(_noKeysExceptionMessage);

            List<TwitterStatus> tweets = new List<TwitterStatus>();
            var take = this.SanitizeTakeCount(request.Take);

            var cacheKey = "twitter-retweet-timeline-{0}".Arrange(take);

            try
            {
                if (!RSCUtil.Cache.Contains(cacheKey))
                {
                    var service = new TwitterService(RSCUtil.SfsConfig.Twitter.ConsumerKey, RSCUtil.SfsConfig.Twitter.ConsumerSecret);
                    service.AuthenticateWith(RSCUtil.SfsConfig.Twitter.AccessToken, RSCUtil.SfsConfig.Twitter.AccessTokenSecret);

                    tweets.AddRange(service.ListRetweetsOfMyTweets(new ListRetweetsOfMyTweetsOptions() { Count = take }));

                    RSCUtil.AddToCache(tweets, cacheKey, TimeSpan.FromMinutes(RSCUtil.SfsConfig.Twitter.CacheTimeoutMinutes));
                }
                else
                {
                    tweets = (List<TwitterStatus>)RSCUtil.Cache[cacheKey];
                }
            }
            catch (Exception ex)
            {
                Logger.Writer.Write(ex);
            }

            return tweets;

        }
        public object Get(TwitterUserTimelineRequest request)
        {
            if (!this.IsSetup)
                throw new KeyNotFoundException(_noKeysExceptionMessage);

            List<TwitterStatus> tweets = new List<TwitterStatus>();
            var take = this.SanitizeTakeCount(request.Take);

            var cacheKey = "twitter-list-user-timeline-{0}-{1}".Arrange(take, request.ScreenName);

            try
            {
                if (!RSCUtil.Cache.Contains(cacheKey))
                {
                    var service = new TwitterService(RSCUtil.SfsConfig.Twitter.ConsumerKey, RSCUtil.SfsConfig.Twitter.ConsumerSecret);
                    service.AuthenticateWith(RSCUtil.SfsConfig.Twitter.AccessToken, RSCUtil.SfsConfig.Twitter.AccessTokenSecret);

                    tweets.AddRange(service.ListTweetsOnUserTimeline(new ListTweetsOnUserTimelineOptions() {
                        Count = take,
                        ScreenName = request.ScreenName
                    }));

                    RSCUtil.AddToCache(tweets, cacheKey, TimeSpan.FromMinutes(RSCUtil.SfsConfig.Twitter.CacheTimeoutMinutes));
                }
                else
                {
                    tweets = (List<TwitterStatus>)RSCUtil.Cache[cacheKey];
                }
            }
            catch (Exception ex)
            {
                Logger.Writer.Write(ex);
            }


            return tweets;

        }
        public object Get(TwitterMentionsTimelineRequest request)
        {
            if (!this.IsSetup)
                throw new KeyNotFoundException(_noKeysExceptionMessage);

            List<TwitterStatus> tweets = new List<TwitterStatus>();
            var take = this.SanitizeTakeCount(request.Take);

            var cacheKey = "twitter-list-mentioningme-{0}".Arrange(take);

            try
            {
                if (!RSCUtil.Cache.Contains(cacheKey))
                {
                    var service = new TwitterService(RSCUtil.SfsConfig.Twitter.ConsumerKey, RSCUtil.SfsConfig.Twitter.ConsumerSecret);
                    service.AuthenticateWith(RSCUtil.SfsConfig.Twitter.AccessToken, RSCUtil.SfsConfig.Twitter.AccessTokenSecret);

                    tweets.AddRange(service.ListTweetsMentioningMe(new ListTweetsMentioningMeOptions() { Count = take }));

                    RSCUtil.AddToCache(tweets, cacheKey, TimeSpan.FromMinutes(RSCUtil.SfsConfig.Twitter.CacheTimeoutMinutes));
                }
                else
                {
                    tweets = (List<TwitterStatus>)RSCUtil.Cache[cacheKey];
                }
            }
            catch (Exception ex)
            {
                Logger.Writer.Write(ex);
            }

            return tweets;

        }

        private int SanitizeTakeCount(int take)
        {
            return take > RSCUtil.SfsConfig.Twitter.MaxCount ? RSCUtil.SfsConfig.Twitter.MaxCount : take;
        }

        public bool IsSetup
        {
            get
            {
                return !String.IsNullOrEmpty(RSCUtil.SfsConfig.Twitter.ConsumerKey) && !String.IsNullOrEmpty(RSCUtil.SfsConfig.Twitter.ConsumerSecret) && !String.IsNullOrEmpty(RSCUtil.SfsConfig.Twitter.AccessToken) && !String.IsNullOrEmpty(RSCUtil.SfsConfig.Twitter.AccessTokenSecret);
            }
        }
    }
    #endregion

    #region REQUEST
    [Route("/sfs/twitter/hello")]
    public class HelloRequest : IReturn<string>
    {
    }

    [Route("/sfs/twitter/list/home")]
    public class TwitterHomeTimelineRequest: IReturn<List<TwitterStatus>>
    {
        private int _take = 10;
        public int Take
        {
            get { return _take; }
            set
            {
                _take = value;
            }
        }        
    }

    [Route("/sfs/twitter/list/retweets")]
    public class TwitterRetweetTimelineRequest : IReturn<List<TwitterStatus>>
    {
        private int _take = 10;
        public int Take
        {
            get { return _take; }
            set
            {
                _take = value;
            }
        }
    }

    [Route("/sfs/twitter/list/user/{ScreenName}")]
    public class TwitterUserTimelineRequest : IReturn<List<TwitterStatus>>
    {
        public string ScreenName { get; set; }

        private int _take = 10;
        public int Take
        {
            get { return _take; }
            set
            {
                _take = value;
            }
        }
    }

    [Route("/sfs/twitter/list/mentions")]
    public class TwitterMentionsTimelineRequest : IReturn<List<TwitterStatus>>
    {
        private int _take = 10;
        public int Take
        {
            get { return _take; }
            set
            {
                _take = value;
            }
        }
    }
    #endregion
}