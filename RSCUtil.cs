using RandomSiteControlsMVC.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using Telerik.Microsoft.Practices.EnterpriseLibrary.Caching;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Claims;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web;

namespace Telerik.Sitefinity
{
    public static class RSCUtil
    {

        /// <summary>
        /// Adds something to the Sitefinity Cache
        /// </summary>
        /// <param name="item">The object to cache</param>
        /// <param name="cacheKey">The key to store it as</param>
        /// <param name="length">For how long, example: TimeSpan.FromDays(30)</param>
        public static void AddToCache(object item, string cacheKey, TimeSpan length)
        {
            RSCUtil.Cache.Add(cacheKey,
            item,
            Telerik.Microsoft.Practices.EnterpriseLibrary.Caching.CacheItemPriority.Normal,
            null,
            new Telerik.Microsoft.Practices.EnterpriseLibrary.Caching.Expirations.AbsoluteTime(length));
        }

        /// <summary>
        /// Adds something to the Sitefinity Cache
        /// </summary>
        /// <param name="item">The object to cache</param>
        /// <param name="cacheKey">The key to store it as</param>
        /// <param name="minutes">Duration in minutes</param>
        public static void AddToCache(object item, string cacheKey, int minutes)
        {
            RSCUtil.AddToCache(item, cacheKey, TimeSpan.FromMinutes(minutes));
        }

        /// <summary>
        /// This is the code which will add a template to the Sitefinity Backend Template UI
        /// </summary>
        /// <param name="embeddedTemplatePath">Path without the Virtual Node, ex: RandomSiteControls.RssConsumer.RssConsumer.ascx</param>
        /// <param name="type">Type of the widget</param>
        /// <param name="name">Name</param>
        /// <param name="friendlyName">Easier to read name</param>
        /// <param name="areaName">Section Group</param>
        public static void AddWidgetTemplateToBackendHtmlEditor(string embeddedTemplatePath, Type type, string name, string friendlyName, string areaName)
        {
            var initalizer = SiteInitializer.GetInitializer();
            var manager = initalizer.PageManager;

            var existingTemplate = manager.GetPresentationItems<ControlPresentation>().Where(p =>
                    p.EmbeddedTemplateName == embeddedTemplatePath &&
                    p.ControlType == type.FullName &&
                    p.Name == name)
                    .SingleOrDefault();

            if (existingTemplate == null)
            {

                initalizer.RegisterControlTemplate(
                        embeddedTemplatePath,
                        type.FullName,
                        name,
                        null,
                        areaName,
                        "ASP_NET_TEMPLATE",
                        type.Assembly.FullName,
                        friendlyName
                        );

                initalizer.SaveChanges();
            }
        }

        public static void DoRedirect(string url)
        {
            if (!SystemManager.IsDesignMode && !SystemManager.IsPreviewMode)
                HttpContext.Current.Response.RedirectPermanent(url);
        }

        /// <summary>
        /// Gets the current page node and converts the pages to css classes, useful on the body element to style a specific page
        /// </summary>
        /// <returns></returns>
        public static string GetCurrentPageNodeAsClasses()
        {
            string css = String.Empty;
            var currentPage = SitefinitySiteMap.GetActualCurrentNode();
            if (currentPage != null)
            {
                css = " " + currentPage.Url.TrimStart('~', '/').Replace("'", "").Replace("<", "").Replace(">", "").Replace("\"", "").Replace("/", " ");
            }

            return css;
        }

        public static string GetSitefinityVersion()
        {
            SystemInfo info = new SystemInfo();

            return info.Version.ToString();
        }

        /// <summary>
        /// Come on dude...this node shouldn't be browsable, redirect up a few pages
        /// </summary>
        /// <param name="segmentsToRemove">Now many nodes are to be removed</param>
        /// <param name="pauseRedirect">Will not redirect but instead just output the url as a string</param>
        /// <returns></returns>
        public static string NopeNopeNopeRedirectToAParent(int segmentsToRemove = 1, bool pauseRedirect = false)
        {
            var url = HttpContext.Current.Request.Url;
            string host = "{0}://{1}".Arrange(url.Scheme, url.Host);
            string pages = String.Empty;

            if (!SystemManager.IsDesignMode && !SystemManager.IsPreviewMode && !RSCUtil.IsPublishing)
            {

                //Rebuild
                for (int i = 0; i < (url.Segments.Count() - segmentsToRemove); i++)
                {
                    pages += url.Segments[i];
                }

                var completedUrl = "{0}{1}".Arrange(host, pages).TrimEnd('/');

                if (!pauseRedirect)
                {
                    HttpContext.Current.Response.RedirectPermanent(completedUrl, true);
                }

                return completedUrl;
            }

            return "";
        }

        /// <summary>
        /// Instance of the Sitefinity Global Cache
        /// </summary>
        public static ICacheManager Cache
        {
            get
            {
                return SystemManager.GetCacheManager(CacheManagerInstance.Global);
            }
        }

        /// <summary>
        /// Detects the work workflow in the Url...assumes publishing
        /// </summary>
        public static bool IsPublishing
        {
            get
            {
                return HttpContext.Current.Request.Url.AbsoluteUri.ToLower().Contains("workflow");
            }
        }

        #region User Helpers
        public static bool IsBackendUser()
        {
            var identity = ClaimsManager.GetCurrentIdentity();
            return identity.IsBackendUser;
        }

        public static SitefinityProfile GetProfile(string nickname, string provider = "")
        {
            return UserProfileManager.GetManager(provider).GetUserProfiles().OfType<SitefinityProfile>().Where(x => x.Nickname == nickname).FirstOrDefault();
        }

        public static SitefinityProfile GetProfile(Guid userID, string provider = "")
        {
            var manager = UserProfileManager.GetManager(provider);
            var user = RSCUtil.GetUser(userID);
            if (user != null)
            {
                var profile = manager.GetUserProfile<SitefinityProfile>(RSCUtil.GetUser(userID));

                return profile;
            }
            else
            {
                return null;
            }
        }

        public static User GetUserByNickName(string nickname, string provider = "")
        {
            var profile = UserProfileManager.GetManager(provider).GetUserProfiles().OfType<SitefinityProfile>().Where(x => x.Nickname == nickname).FirstOrDefault();

            return (profile != null) ? profile.User : null;
        }

        public static string GetNickname(User user, string provider = "")
        {
            var profile = RSCUtil.GetProfile(user.Id);

            return (profile != null) ? profile.Nickname : String.Empty;
        }

        public static User GetUser(Guid id, string provider = "")
        {
            var user = UserManager.GetManager(provider).GetUser(id);

            return user;
        }

        public static Guid GetCurrentUserId()
        {
            var identity = ClaimsManager.GetCurrentIdentity();

            return identity.UserId;

        }

        public static User GetCurrentUser(string provider = "")
        {
            var identity = ClaimsManager.GetCurrentIdentity();

            var user = UserManager.GetManager(provider).GetUser(identity.UserId);

            return user;
        }

        public static SitefinityProfile GetCurrentUserProfile(string provider = "")
        {
            var profile = RSCUtil.GetProfile(RSCUtil.GetCurrentUser(provider).Id);

            return profile;
        }

        public static SitefinitySteveMvcConfig SfsConfig
        {
            get{
                return Config.Get<SitefinitySteveMvcConfig>();
            }
        }
        #endregion
    }
}
 