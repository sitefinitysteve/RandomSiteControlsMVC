using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomSiteControlsMVC.MVC.Models.DocumentTree
{
    public class DocumentTreeNode
    {
        public DocumentTreeNode()
        {
            this.Nodes = new List<DocumentTreeNode>();
        }

        public Guid Id { get; set; }
        public Guid? ParentId { get; set; }
        public bool Expanded { get; set; }
        public bool IsFolder { get; set; }
        public string Title { get; set; }
        public string Filename { get; set; }
        public string NavigateUrl { get; set; }
        public string Target { get; set; }
        public string CssClass { get; set; }
        public string ContentCssClass { get; set; }
        public string ImageUrl { get; set; }
        public string Extension { get; set; }

        public List<DocumentTreeNode> Nodes { get; set; }
    }
}
