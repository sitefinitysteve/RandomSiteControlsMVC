using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomSiteControlsMVC.MVC.Models.TabStrip
{
    public class TabStripModel
    {
        public TabStripModel()
        {
            this.Tabs = new List<uib-tab>();
        }

        public string ClassName { get; set; }
        public string TabPosition { get; set; }
        public List<uib-tab> Tabs { get; set; }
    }
}
