using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Telerik.Sitefinity.DynamicModules;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Model.ContentLinks;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Taxonomies.Model;
using Telerik.OpenAccess;
using Telerik.Sitefinity.Taxonomies;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Frontend.Mvc.Models;

namespace Telerik.Sitefinity
{
    public static class DynamicContentExtensions
    {
        /// <summary>
        /// Get a single image from a content link
        /// 🔥 From SitefinitySteve
        /// </summary>
        /// <returns>Telerik.Sitefinity.Libraries.Model.Image object</returns>
        public static Image GetImage(this DynamicContent item, string fieldName){
            var contentLinks = (ContentLink[])item.GetValue(fieldName);
            ContentLink imageContentLink = contentLinks.FirstOrDefault();

            return (imageContentLink == null) ? null : imageContentLink.ChildItemId.GetImage();
        }

        /// <summary>
        /// Gets the images from a content link array
        /// 🔥 From SitefinitySteve
        /// </summary>
        /// <returns>IQueryable Telerik.Sitefinity.Libraries.Model.Image</returns>
        public static IQueryable<Image> GetImages(this DynamicContent item, string fieldName)
        {
            var contentLinks = (ContentLink[])item.GetValue(fieldName);

            var images = from i in contentLinks
                         select i.ChildItemId.GetImage();
            
            return images.AsQueryable();
        }

        /// <summary>
        /// Get a single document from a content link
        /// 🔥 From SitefinitySteve
        /// </summary>
        /// <returns>Telerik.Sitefinity.Libraries.Model.Image object</returns>
        public static Document GetDocument(this DynamicContent item, string fieldName){
            var contentLinks = (ContentLink[])item.GetValue(fieldName);
            ContentLink docContentLink = contentLinks.FirstOrDefault();

            return (docContentLink == null) ? null : docContentLink.ChildItemId.GetDocument();
        }

        /// <summary>
        /// Gets the documents from a content link array
        /// 🔥 From SitefinitySteve
        /// </summary>
        /// <returns>IQueryable Telerik.Sitefinity.Libraries.Model.Image</returns>
        public static IQueryable<Document> GetDocuments(this DynamicContent item, string fieldName)
        {
            var contentLinks = (ContentLink[])item.GetValue(fieldName);

            var docs = from i in contentLinks
                         select i.ChildItemId.GetDocument();
            
            return docs.AsQueryable();
        }

        /// <summary>
        /// Get the Live Visible items
        /// 🔥 From SitefinitySteve
        /// </summary>
        /// <returns>IQueryable-DynamicContent-</returns>
        public static IQueryable<DynamicContent> Live(this IQueryable<DynamicContent> items){
            return items.Where(x => x.Status == GenericContent.Model.ContentLifecycleStatus.Live && x.Visible == true);
        }

        /// <summary>
        /// Get the Master Visible items
        /// 🔥 From SitefinitySteve
        /// </summary>
        /// <returns>IQueryable-DynamicContent-</returns>
        public static IQueryable<DynamicContent> Master(this IQueryable<DynamicContent> items)
        {
            return items.Where(x => x.Status == GenericContent.Model.ContentLifecycleStatus.Master);
        }

        /// <summary>
        /// Get the Temp Visible items
        /// 🔥 From SitefinitySteve
        /// </summary>
        /// <returns>IQueryable-DynamicContent-</returns>
        public static IQueryable<DynamicContent> Temp(this IQueryable<DynamicContent> items)
        {
            return items.Where(x => x.Status == GenericContent.Model.ContentLifecycleStatus.Temp);
        }

        /// <summary>
        /// Generic Taxon control, use GetCategories or GetTags for the defaults
        /// 🔥 From SitefinitySteve
        /// </summary>
        /// <returns>IQueryable-HierarchicalTaxon-</returns>
        public static List<HierarchicalTaxon> GetHierarchicalTaxons(this DynamicContent item, string fieldName, string taxonomyName)
        {
            var categories = item.GetValue<TrackedList<Guid>>(fieldName);

            TaxonomyManager manager = TaxonomyManager.GetManager();

            var taxonomyParent = manager.GetTaxonomies<HierarchicalTaxonomy>().SingleOrDefault(x => x.Name == taxonomyName);
            var taxons = taxonomyParent.Taxa.Where(x => categories.Contains(x.Id)).Select(x => (HierarchicalTaxon)x);

            return taxons.ToList();
        }

        /// <summary>
        /// Generic Taxon control, use GetCategories or GetTags for the defaults
        /// 🔥 From SitefinitySteve
        /// </summary>
        /// <returns>IQueryable-HierarchicalTaxon-</returns>
        public static List<Taxon> GetFlatTaxons(this DynamicContent item, string fieldName)
        {
            var categories = item.GetValue<TrackedList<Guid>>(fieldName);

            TaxonomyManager manager = TaxonomyManager.GetManager();

            var taxonomyParent = manager.GetTaxonomies<Taxonomy>().FirstOrDefault(x => x.Name == fieldName);

            var taxons = taxonomyParent.Taxa.Where(x => categories.Contains(x.Id)).Select(x => x);

            return taxons.ToList();
        }

        /// <summary>
        /// Get the linked Categories
        /// 🔥 From SitefinitySteve
        /// </summary>
        /// <returns>IQueryable-HierarchicalTaxon-</returns>
        public static List<HierarchicalTaxon> GetCategories(this DynamicContent item)
        {
            var categories = item.GetValue<TrackedList<Guid>>("Category");

            TaxonomyManager manager = TaxonomyManager.GetManager();

            var taxonomyParent = manager.GetTaxonomy<HierarchicalTaxonomy>(TaxonomyManager.CategoriesTaxonomyId);

            var taxons = taxonomyParent.Taxa.Where(x => categories.Contains(x.Id)).Select(x => (HierarchicalTaxon)x);

            return taxons.ToList();
        }

        /// <summary>
        /// Get the linked Tags
        /// 🔥 From SitefinitySteve
        /// </summary>
        /// <returns>IQueryable-HierarchicalTaxon-</returns>
        public static List<Taxon> GetTags(this DynamicContent item)
        {
            var tags = item.GetValue<TrackedList<Guid>>("Tags");

            TaxonomyManager manager = TaxonomyManager.GetManager();

            var taxonomyParent = manager.GetTaxonomy<Taxonomy>(TaxonomyManager.TagsTaxonomyId);
            var taxons = taxonomyParent.Taxa.Where(x => tags.Contains(x.Id)).ToList();

            return taxons;
        }

        /// <summary>
        /// Returns the linked DynamicContent objects
        /// 🔥 From SitefinitySteve
        /// </summary>
        [Obsolete("In Sitefinity 7 you should be using the new Related Content Fields.  Use GetRelatedItems instead and migrate your fields type if nessesary - Steve")]
        public static IQueryable<DynamicContent> GetRelatedContentItems(this DynamicContent dataItem, string fieldName, string type)
        {
            var contentLinks = dataItem.GetValue<Guid[]>(fieldName);
            var items = contentLinks.GetDynamicContentItems(type);

            return items;
        }

        /// <summary>
        /// Returns the linked DynamicContent object
        /// 🔥 From SitefinitySteve
        /// </summary>
        [Obsolete("In Sitefinity 7 you should be using the new Related Content Fields.  Use GetRelatedItems instead and migrate your fields type if nessesary - Steve")]
        public static DynamicContent GetRelatedContentItem(this DynamicContent dataItem, string fieldName, string type)
        {
            var contentLink = dataItem.GetValue<Guid>(fieldName);

            return contentLink.GetDynamicContent(type);
        }

        /// <summary>
        /// Returns the Child Items of the current DataItem
        /// 🔥 From SitefinitySteve
        /// </summary>
        public static IQueryable<DynamicContent> GetChildren(this DynamicContent dataItem, Type type = null)
        {
            if (dataItem != null)
            {
                DynamicModuleManager dynamicModuleManager = DynamicModuleManager.GetManager();

                bool hasChildren = dynamicModuleManager.HasChildItems(dataItem);

                if (hasChildren)
                {
                    //dynamicModuleManager.LoadChildItemsHierarchy(dataItem);

                    var items = dynamicModuleManager.GetChildItems(dataItem, type);
                    return items;
                }
            }
            return new List<DynamicContent>().AsQueryable();
        }

        public static IQueryable<DynamicContent> GetChildren(this DynamicContent dataItem, string type)
        {
            return DynamicContentExtensions.GetChildren(dataItem, TypeResolutionService.ResolveType(type));
        }

        /// <summary>
        /// Returns the Child Items of the current DataItems Parent, so like every content item on the same level as THIS item
        /// 🔥 From SitefinitySteve
        /// </summary>
        public static IQueryable<DynamicContent> GetChildrenFromParent(this DynamicContent dataItem, Type type = null)
        {
            if (type == null)
                type = TypeResolutionService.ResolveType(dataItem.GetType().FullName);

            if (dataItem != null)
            {
                return DynamicContentExtensions.RetrieveDataThroughFiltering(type).Where(x => x.SystemParentItem.SystemParentId == dataItem.SystemParentId);
            }
            else{
                return new List<DynamicContent>().AsQueryable();
            }
        }

        private static IQueryable<DynamicContent> RetrieveDataThroughFiltering(Type type)
        {
            DynamicModuleManager dynamicModuleManager = DynamicModuleManager.GetManager();

            var myFilteredCollection = dynamicModuleManager.GetDataItems(type);
            return myFilteredCollection;
        }

        /// <summary>
        /// Convert to fully dynamic object, saves headaches of .GetValue
        /// </summary>
        public static ItemViewModel ToItemViewModel(this IDataItem item)
        {
            return new ItemViewModel(item);
        }

        /// <summary>
        /// Convert to fully dynamic object, saves headaches of .GetValue
        /// Alias to ToItemViewModel
        /// </summary>
        public static ItemViewModel ToModuleDynamicObject(this IDataItem item)
        {
            return item.ToItemViewModel();
        }
    }
}