using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Web;
using System.Web.UI;
using System.Web.WebPages;
using RandomSiteControlsMVC.Configuration;
using Telerik.Sitefinity.Configuration;

/// ########################
/// ## REGISTER THIS MODULE <modules> AS
/// <add name="RemoveHttpHeadersModule" type="RandomSiteControls.HttpModules.RemoveHttpHeadersModule, RandomSiteControls" />
///
/// ################################

namespace RandomSiteControlsMVC.HttpModules
{
    public class RemoveHttpHeadersModule : IHttpModule
    {
        public void Dispose()
        {
            //clean-up code here.
        }

        public void Init(HttpApplication context)
        {
            if (this.UserConfig.HttpHeaderModule.Enabled)
            {
                //context.EndRequest += new EventHandler(OnEndRequest);
                context.PreSendRequestHeaders += OnContextPreSendRequestHeaders;
                WebPageHttpHandler.DisableWebPagesResponseHeader = true;
            }
        }

        protected void OnContextPreSendRequestHeaders(object sender, EventArgs e)
        {
            if (System.Web.HttpContext.Current != null)
            {
                HttpResponse response = System.Web.HttpContext.Current.Response;
                if (response != null)
                {
                    if (!ControlExtensions.IsBackend())
                    {
                        var config = this.UserConfig;

                        var path = System.Web.HttpContext.Current.Request.Url.AbsolutePath;
                        string[] ignores = config.HttpHeaderModule.IgnorePaths.Split(',');

                        //Validate path
                        bool isOkay = true;

                        foreach (var p in ignores)
                        {
                            if (path.StartsWith(p.Trim()))
                            {
                                isOkay = false;
                                break;
                            }
                        }

                        if (isOkay) //okay to process
                        {
                            NameValueCollection headers = response.Headers;
                            if (headers != null)
                            {
                                if (config.HttpHeaderModule.XAspNetVersion)
                                    headers.Remove("X-AspNet-Version");

                                if (config.HttpHeaderModule.Server)
                                    headers.Remove("Server");

                                if (config.HttpHeaderModule.ETag)
                                    headers.Remove("ETag");

                                if (config.HttpHeaderModule.AddXFrameOptions)
                                {
                                    headers.Add("X-Frame-Options", config.HttpHeaderModule.XFrameOptionsMode);
                                }
                            }
                        }
                    }
                }
            }
        }
        /*
        protected void OnEndRequest(object sender, EventArgs e)
        {

        }
        */
        public SitefinitySteveMvcConfig UserConfig
        {
            get
            {
                return Config.Get<SitefinitySteveMvcConfig>();
            }
        }

    }
}
