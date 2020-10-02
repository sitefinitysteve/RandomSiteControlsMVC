using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Telerik.Microsoft.Practices.EnterpriseLibrary.Logging;
using Telerik.Sitefinity.DynamicModules;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Modules.Libraries;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.SitefinityExceptions;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Data.Linq.Dynamic;

namespace Telerik.Sitefinity
{
    public static class GuidExtensions
    {
        /// <summary>
        /// Returns an Image object from a Guid
        /// 🔥 From SitefinitySteve
        /// </summary>
        public static Image GetImage(this Guid imageId)
        {
            LibrariesManager manager = LibrariesManager.GetManager();
            try{
                return (imageId != Guid.Empty) ? manager.GetImage(imageId) : null;
            }catch(ItemNotFoundException iex){
                Logger.Writer.Write(iex);
                return null;
            }
        }

        /// <summary>
        /// Returns an Document object from a Guid
        /// 🔥 From SitefinitySteve
        /// </summary>
        public static Document GetDocument(this Guid documentId)
        {
            LibrariesManager manager = LibrariesManager.GetManager();
            try
            {
                return (documentId != Guid.Empty) ? manager.GetDocument(documentId) : null;
            }
            catch (ItemNotFoundException iex)
            {
                Logger.Writer.Write(iex);
                return null;
            }
        }

        /// <summary>
        /// Returns an Album object from a Guid
        /// 🔥 From SitefinitySteve
        /// </summary>
        public static Album GetAlbum(this Guid albumID)
        {
            LibrariesManager manager = LibrariesManager.GetManager();
            return (albumID != Guid.Empty) ? manager.GetAlbum(albumID) : null;
        }

        /// <summary>
        /// Returns a PageNode object from a Guid
        /// 🔥 From SitefinitySteve
        /// </summary>
        public static PageNode GetPage(this Guid pageId)
        {
            PageManager manager = PageManager.GetManager();
            return (pageId != Guid.Empty) ? manager.GetPageNode(pageId) : null;
        }

        /// <summary>
        /// Returns a PageNode object from a Guid
        /// 🔥 From SitefinitySteve
        /// </summary>
        public static PageData GetPageData(this Guid pageId)
        {
            PageManager manager = PageManager.GetManager();
            return (pageId != Guid.Empty) ? manager.GetPageData(pageId) : null;
        }

        /// <summary>
        /// Converts the Guid[] type to the DynamicContent objects
        /// 🔥 From SitefinitySteve
        /// </summary>
        public static IQueryable<DynamicContent> GetDynamicContentItems(this Guid[] contentLinks, string type)
        {
            DynamicModuleManager manager = DynamicModuleManager.GetManager();
            var contentType = TypeResolutionService.ResolveType(type);

            var items = manager.GetDataItems(contentType).Where(x => contentLinks.Contains(x.Id));

            return items;
        }

        /// <summary>
        /// Converts the Guid[] type to the DynamicContent objects
        /// 🔥 From SitefinitySteve
        /// </summary>
        public static DynamicContent GetDynamicContent(this Guid contentLink, string type)
        {
            DynamicModuleManager manager = DynamicModuleManager.GetManager();
            var contentType = TypeResolutionService.ResolveType(type);

            var item = manager.GetDataItem(contentType, contentLink);

            return item;
        }

        /// <summary>
        /// Converts the Guid[] type to the DynamicContent objects
        /// 🔥 From SitefinitySteve
        /// </summary>
        public static IQueryable<DynamicContent> GetDynamicContentItemsWithTags(this Guid[] tagIds, string type, FilterOperatorEnum filterOperator)
        {
            return tagIds.GetDynamicContentItemsWithTaxa(type, filterOperator, "Tags");
        }

        /// <summary>
        /// Converts the Guid[] type to the DynamicContent objects
        /// 🔥 From SitefinitySteve
        /// </summary>
        public static IQueryable<DynamicContent> GetDynamicContentItemsWithCategories(this Guid[] tagIds, string type, FilterOperatorEnum filterOperator)
        {
            return tagIds.GetDynamicContentItemsWithTaxa(type, filterOperator, "Category");
        }

        /// <summary>
        /// Converts the Guid[] type to the DynamicContent objects
        /// 🔥 From SitefinitySteve
        /// </summary>
        public static IQueryable<DynamicContent> GetDynamicContentItemsWithTaxa(this Guid[] tagIds, string type, FilterOperatorEnum filterOperator, string taxonName)
        {
            var expression = tagIds.GenerateFilterExpression(taxonName, filterOperator);

            DynamicModuleManager manager = DynamicModuleManager.GetManager();
            var contentType = TypeResolutionService.ResolveType(type);

            return manager.GetDataItems(contentType).Where(expression);
        }

        public enum FilterOperatorEnum
		{
			AND,
			OR
		}

        /// <summary>
        /// Turns a guid array into a dynamic filter expression that openaccess can use
        /// 🔥 From SitefinitySteve
        /// </summary>
        public static string GenerateFilterExpression(this Guid[] elements, string fieldname, FilterOperatorEnum decision, bool wrapInBraces = true)
		{
			string filter = String.Empty;

			foreach (var c in elements)
			{
				filter += "{0}.Contains(\"{{{1}}}\") {2} ".Arrange(fieldname, c, decision.ToString());
			}

			if (String.IsNullOrEmpty(filter))
				return filter;
			else
			{
				//Strip the end operator
				filter = filter.Substring(0, filter.Length - (decision.ToString().Length + 2));
				return (wrapInBraces) ? " (" + filter + ") " : filter;
			}
		}

        #region OBSOLETE
        /// <summary>
        /// Converts the Guid[] type to the DynamicContent objects
        /// 🔥 From SitefinitySteve
        /// </summary>
        [Obsolete("Use GetDynamicContentItems", true)]
        public static IQueryable<DynamicContent> GetContentLinks(this Guid[] contentLinks, string type)
        {
            return null;
        }

        /// <summary>
        /// Converts the Guid[] type to the DynamicContent objects
        /// 🔥 From SitefinitySteve
        /// </summary>
        /// [Obsolete("Use GetDynamicContent", true)]
        public static DynamicContent GetContentLink(this Guid contentLink, string type)
        {
            return null;
        }
        #endregion
    }
}