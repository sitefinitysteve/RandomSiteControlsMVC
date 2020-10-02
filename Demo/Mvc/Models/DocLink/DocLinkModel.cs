using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SitefinityWebApp.Mvc.Models.DocLink
{
    public class DocLinkModel
    {
        public DocLinkModel()
        {

        }

        public DocLinkModel(string text, string link)
        {
            this.Text = text;
            this.Link = link;
        }

        public string Text { get; set; }
        public string Link { get; set; }
    }
}