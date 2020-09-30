using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SitefinityWebApp.Mvc.Models.HeaderTitle
{
    public class HeaderTitleModel
    {
        public HeaderTitleModel()
        {

        }

        public HeaderTitleModel(string title, string cssClass)
        {
            this.Title = title;
            this.CssClass = cssClass;
        }

        public string Title { get; set; }
        public string CssClass { get; set; }
    }
}