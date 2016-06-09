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
            this.Tabs = new List<Tab>();
        }

        public string TabPosition { get; set; }
        public List<Tab> Tabs { get; set; }
    }
}
