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

namespace RandomSiteControlsMVC.Services
{
    #region PLUGIN
    public class UtilityServiceServicePlugin : IPlugin
    {
        /// <summary>
        /// Call this at SitefinityWebApp.App_Start.RegisterServiceStackRoutes
        /// SystemManager.RegisterServiceStackPlugin(new Medportal.Services.Utility.UtilityServiceServicePlugin());
        /// </summary>
        public void Register(IAppHost appHost)
        {
            appHost.RegisterService(typeof(UtilityServiceService));
        }
    }
    #endregion

    #region SERVICE
    public class UtilityServiceService : IService
    {
        public object Get(YoRequest request)
        {
            return "Sup bro";
        }

        public object Post(SaveRequest request)
        {
            var data = Convert.FromBase64String(request.Base64);

            var result = new HttpResult(data, request.ContentType);

            result.Headers.Add("Content-Disposition", "attachment;filename={0};".Arrange(request.Filename));

            return result;
        }
    }
    #endregion

    #region REQUEST
    [Route("/utility/hello")]
    public class YoRequest : IReturn<string>
    {
    }

    [Route("/utility/proxy/save")]
    public class SaveRequest : IReturn<HttpResult>
    {
        public string ContentType { get; set; }
        public string Base64 { get; set; }
        public string Filename { get; set; }
    }
    #endregion
}