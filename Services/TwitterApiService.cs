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
using System.Net;

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

        public List<TwitterStatus> Get(TwitterHomeTimelineRequest request)
        {
            if (!this.IsSetup)
                throw new KeyNotFoundException(_noKeysExceptionMessage);

            List<TwitterStatus> tweets = new List<TwitterStatus>();
            var take = this.SanitizeTakeCount(request.Take);

            var cacheKey = "twitter-list-timeline-{0}".Arrange(take);

            if (!RSCUtil.Cache.Contains(cacheKey))
            {
                TwitterService service = this.GetService();
                var result = service.ListTweetsOnHomeTimeline(new ListTweetsOnHomeTimelineOptions() { Count = take });
                this.HandleTwitterStatusCallback(tweets, cacheKey, service, result);
            }
            else
            {
                tweets = (List<TwitterStatus>)RSCUtil.Cache[cacheKey];
            }
            
            
            return tweets;
        }


        public List<TwitterStatus> Get(TwitterRetweetTimelineRequest request)
        {
            if (!this.IsSetup)
                throw new KeyNotFoundException(_noKeysExceptionMessage);

            List<TwitterStatus> tweets = new List<TwitterStatus>();
            var take = this.SanitizeTakeCount(request.Take);

            var cacheKey = "twitter-retweet-timeline-{0}".Arrange(take);

            if (!RSCUtil.Cache.Contains(cacheKey))
            {
                TwitterService service = this.GetService();
                var result = service.ListRetweetsOfMyTweets(new ListRetweetsOfMyTweetsOptions() { Count = take });
                this.HandleTwitterStatusCallback(tweets, cacheKey, service, result);
            }
            else
            {
                tweets = (List<TwitterStatus>)RSCUtil.Cache[cacheKey];
            }

            return tweets;

        }
        public List<TwitterStatus> Get(TwitterUserTimelineRequest request)
        {
            if (!this.IsSetup)
                throw new KeyNotFoundException(_noKeysExceptionMessage);

            List<TwitterStatus> tweets = new List<TwitterStatus>();
            var take = this.SanitizeTakeCount(request.Take);

            var cacheKey = "twitter-list-user-timeline-{0}-{1}".Arrange(take, request.ScreenName);

            if (!RSCUtil.Cache.Contains(cacheKey))
            {
                TwitterService service = this.GetService();
                var result = service.ListTweetsOnUserTimeline(new ListTweetsOnUserTimelineOptions()
                {
                    Count = take,
                    ScreenName = request.ScreenName
                });
                this.HandleTwitterStatusCallback(tweets, cacheKey, service, result);
            }
            else
            {
                tweets = (List<TwitterStatus>)RSCUtil.Cache[cacheKey];
            }

            return tweets;

        }
        public List<TwitterStatus> Get(TwitterMentionsTimelineRequest request)
        {
            if (!this.IsSetup)
                throw new KeyNotFoundException(_noKeysExceptionMessage);

            List<TwitterStatus> tweets = new List<TwitterStatus>();
            var take = this.SanitizeTakeCount(request.Take);

            var cacheKey = "twitter-list-mentioningme-{0}".Arrange(take);

            if (!RSCUtil.Cache.Contains(cacheKey))
            {
                TwitterService service = this.GetService();
                var result = service.ListTweetsMentioningMe(new ListTweetsMentioningMeOptions() { Count = take });
                this.HandleTwitterStatusCallback(tweets, cacheKey, service, result);
            }
            else
            {
                tweets = (List<TwitterStatus>)RSCUtil.Cache[cacheKey];
            }

            return tweets;

        }

        public List<TwitterStatus> Get(TwitterTweetRequest request)
        {
            if (!this.IsSetup)
                throw new KeyNotFoundException(_noKeysExceptionMessage);

            List<TwitterStatus> tweets = new List<TwitterStatus>();

            var cacheKey = "twitter-tweet-{0}".Arrange(request.Id);

            if (!RSCUtil.Cache.Contains(cacheKey))
            {
                TwitterService service = this.GetService();
                var result = service.GetTweet(new GetTweetOptions() { Id = request.Id });

                this.HandleTwitterStatusCallback(tweets, cacheKey, service, new List<TwitterStatus>() { result });
            }
            else
            {
                tweets = (List<TwitterStatus>)RSCUtil.Cache[cacheKey];
            }

            return tweets;

        }

        #region HELPERS

        private void HandleTwitterStatusCallback(List<TwitterStatus> tweets, string cacheKey, TwitterService service, IEnumerable<TwitterStatus> result)
        {
            if (service.Response.StatusCode == HttpStatusCode.OK)
            {
                tweets.AddRange(result);

                RSCUtil.AddToCache(tweets, cacheKey, TimeSpan.FromMinutes(RSCUtil.SfsConfig.Twitter.CacheTimeoutMinutes));
            }
            else
            {
                Logger.Writer.Write(service.Response.Error.Message);
                throw new UnauthorizedAccessException(service.Response.Error.Message);
            }
        }

        private TwitterService GetService()
        {
            var service = new TwitterService(RSCUtil.SfsConfig.Twitter.ConsumerKey, RSCUtil.SfsConfig.Twitter.ConsumerSecret);
            service.AuthenticateWith(RSCUtil.SfsConfig.Twitter.AccessToken, RSCUtil.SfsConfig.Twitter.AccessTokenSecret);
            return service;
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
        #endregion
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

    [Route("/sfs/twitter/tweet/{id}")]
    public class TwitterTweetRequest : IReturn<List<TwitterStatus>>
    {
        public Int64 Id { get; set; }
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