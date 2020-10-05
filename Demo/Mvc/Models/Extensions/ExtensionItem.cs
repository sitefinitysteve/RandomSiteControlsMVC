using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SitefinityWebApp.Mvc.Models.Extensions
{
    public class ExtensionItem
    {
        public ExtensionItem(Member m)
        {
            
            var classStripper = m.Name.Replace("M:Telerik.Sitefinity.", "").Replace("Model.", "").Replace("Mvc.Helpers.", "").Replace("UserExtensions.", "").Replace("TaxonExtensions.", "").Split('.');
            this.Class = classStripper.GetValue(0).ToString();

            this.FixUpMethodName(m.Name.Replace($"M:Telerik.Sitefinity.{this.Class}.", "").Replace($"M:Telerik.Sitefinity.Model.{this.Class}.", "").Replace("Model.", "").Replace("Mvc.Helpers.", "").Replace("UserExtensions.", "").Replace("TaxonExtensions.", ""));

            this.Summary = m.Summary.Replace("\n","</br/>").Replace("🔥 From SitefinitySteve, from StackOverflow", "").Replace("🔥 From SitefinitySteve", "");
            this.ReturnType = m.Returns.Replace("(", "<").Replace(")", ">");
        }

        public void FixUpMethodName(string methodName)
        {
            var name = methodName;

            this.MethodName = name;
        }

        public string Class { get; set; }
        public string MethodName { get; set; }
        public string Summary { get; set; }
        public string ReturnType { get; set; }
    }
}