using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Sitefinity.Pages.Model;

namespace RandomSiteControlsMVC.MVC.Models.WidgetFinder
{
    public class WidgetFinderModel
    {
        public WidgetFinderModel()
        {
            this.FoundPages = new List<PageNode>();
        }

        public string SelectedControl { get; set; }
        public List<WidgetGroup> Controls { get; set; } = new List<WidgetGroup>();
        public List<PageNode> FoundPages { get; set; }
    }
}