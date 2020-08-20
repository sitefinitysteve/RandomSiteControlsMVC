using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Sitefinity.Modules.GenericContent;
using Telerik.Sitefinity.Web.Utilities;

namespace RandomSiteControlsMVC.MVC.Models.List
{
    public class ListModel
    {
        public ListModel(string items)
        {
            this.Items = new List<ListItem>();

            if (!String.IsNullOrEmpty(items))
            {
                this.Items.AddRange(ServiceStack.Text.JsonSerializer.DeserializeFromString<List<ListItem>>(items));

                //Resolve the links
                foreach (var i in this.Items)
                {
                    i.Content = LinkParser.ResolveLinks(i.Content, DynamicLinksParser.GetContentUrl, null, false);
                }
            }
        }

       public List<ListItem> Items { get; set; }
    }
}
