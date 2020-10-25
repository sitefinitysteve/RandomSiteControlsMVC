using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Telerik.Sitefinity.Pages.Model;

namespace RandomSiteControlsMVC.MVC.Models.WidgetFinder
{
    public class WidgetGroup
    {
        public WidgetGroup()
        {

        }

        public WidgetGroup(string caption, string objectType, int count)
        {
            Caption = caption;
            ObjectType = objectType;
            this.Count = count;
            IsMVC = this.ObjectType.ToLower().Contains("controller");
        }

        public string EncodedCaption()
        {
            return Telerik.Sitefinity.Services.SystemManager.CurrentHttpContext.Server.HtmlEncode(this.Caption.Replace("+", "%2B"));
        }

        public string Caption { get; set; }
        public string ObjectType { get; set; }
        public int Count { get; set; }
        public bool IsMVC { get; set; }
    }
}