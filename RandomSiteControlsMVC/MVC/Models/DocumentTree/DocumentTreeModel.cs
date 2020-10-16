using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomSiteControlsMVC.MVC.Models.DocumentTree
{
    public class DocumentTreeModel
    {
        public DocumentTreeModel() {
            this.Nodes = new List<DocumentTreeNode>();
        }

        public bool Animated { get; set; }
        public int ExpandLevelDepth {get;set;}
        public bool RenderParent { get; set; }
        public List<DocumentTreeNode> Nodes { get; set; }
    }
}
