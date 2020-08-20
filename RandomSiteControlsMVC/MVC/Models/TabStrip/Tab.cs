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

        public Tab(string title, bool selected = false, string cssClass = ""){
            this.Title = title;
            this.Selected = selected;
            this.Editing = false;
            this.CssClass = cssClass;
        }

        public string Title { get; set; }
        public bool Selected { get; set; }
        public bool Editing { get; set; }
        public string QuerystringValue { get; set; }
        public string CssClass { get; set; }
    }
}
