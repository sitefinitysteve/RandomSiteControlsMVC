using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Serialization;
using Telerik.Sitefinity;

namespace SitefinityWebApp.Mvc.Models.Extensions
{
    public class ExtensionsModel
    {
        public ExtensionsModel()
        {
            var cacheKey = "extensions";
            //XML
            try
            {
                if (RSCUtil.Cache.Contains(cacheKey))
                {
                    this.Documentation = RSCUtil.Cache[cacheKey] as Doc;
                }
                else
                {

                    XmlDocument doc = new XmlDocument();
                    var fs = new FileStream(Telerik.Sitefinity.Services.SystemManager.CurrentHttpContext.Server.MapPath("~/rsc-build-docs.xml"), FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                    doc.Load(fs);
                    var serializer = new XmlSerializer(typeof(Doc), new XmlRootAttribute("doc"));
                    StringReader stringReader = new StringReader(doc.InnerXml);
                    this.Documentation = (Doc)serializer.Deserialize(stringReader);
                }

                this.Extensions = this.Documentation.Members.Member.Where(x => x.Name.Contains("Extensions") && !x.Name.Contains("T:")).Select(x => new ExtensionItem(x)).ToList();

                this.Classes = this.Extensions.GroupBy(x => x.Class).OrderBy(x => x.Key);
            }
            catch (Exception ex)
            {

            }
        }

        public Doc Documentation { get; set; }
        public List<ExtensionItem> Extensions { get; set; }
        public IOrderedEnumerable<IGrouping<string, ExtensionItem>> Classes { get; set; }
    }
}