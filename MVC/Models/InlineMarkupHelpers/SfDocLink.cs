using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Sitefinity.Frontend.Media.Mvc.Models.Document;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Modules.Libraries;
using Telerik.Sitefinity.Modules.Libraries.Documents;

namespace RandomSiteControlsMVC.MVC.Models.InlineMarkupHelpers
{

    public class SfDocLink
    {
        public SfDocLink(string url)
        {
            url = url.Split('?').GetValue(0).ToString();
            var parts = url.TrimStart('/').Split('/');

            int extensionPosition = url.LastIndexOf(".");
            if (extensionPosition >= 0)
                url = url.Substring(0, extensionPosition);

            this.Type = parts.GetValue(0).ToString();
            this.Source = parts.GetValue(1).ToString();
            this.Library = parts.GetValue(2).ToString();
            this.UrlName = parts.GetValue(parts.Length - 1).ToString();

            this.ItemUrl = url;

            this.ResolveMediaItem();
        }

        #region Methods
        private void ResolveMediaItem()
        {
            var librariesManager = LibrariesManager.GetManager();

            var document = librariesManager.GetDocuments().FirstOrDefault(x => x.ItemDefaultUrl == this.ItemUrl);

            if (document != null)
            {
                this.DataItem = document;
            }
        }

        public bool FoundDataItem()
        {
            return DataItem == null ? false : true;
        }
        #endregion

        #region Properties

        public string Type { get; set; }
        public string Library { get; set; }
        public string Source { get; set; }
        public string UrlName { get; set; }
        public string ItemUrl { get; set; }
        public Document DataItem { get; set; }
        #endregion
    }
}
