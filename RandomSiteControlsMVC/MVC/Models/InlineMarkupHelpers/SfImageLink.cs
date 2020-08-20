using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Sitefinity.Frontend.Media.Mvc.Models.Image;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Modules.Libraries;

namespace RandomSiteControlsMVC.MVC.Models.InlineMarkupHelpers
{

    public class SfImageLink
    {
        public SfImageLink(string url, string title, string alttext)
        {
            foreach (var provider in LibrariesManager.ProvidersCollection)
            {
                var librariesManager = LibrariesManager.GetManager(provider.Name);

                if (librariesManager != null)
                {
                    //Find the image by title, alt text, AND make sure it's album urlname is in the url, that should do it
                    var image = librariesManager.GetImages().Where(x => x.Status == ContentLifecycleStatus.Live).FirstOrDefault(x => x.Title == title && x.AlternativeText == alttext && url.Contains(x.Album.UrlName));

                    if (image != null)
                    {
                        this.DataItem = image;
                        break;
                    }
                }
            }
        }

        #region Methods

        public bool FoundDataItem()
        {
            return DataItem == null ? false : true;
        }
        #endregion

        #region Properties
        public Telerik.Sitefinity.Libraries.Model.Image DataItem { get; set; }
        #endregion
    }
}
