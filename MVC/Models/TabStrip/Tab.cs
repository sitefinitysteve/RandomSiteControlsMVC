using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomSiteControlsMVC.MVC.Models.TabStrip
{
    public class Tab
    {
        public Tab()
        {
        }

        public Tab(string title, string cssClass = ""){
            this.Title = title;

            this.CssClass = cssClass;
        }

        public string Title { get; set; }
        public string CssClass { get; set; }
    }
}
