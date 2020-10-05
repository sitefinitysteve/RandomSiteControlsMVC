using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Sitefinity.DynamicModules;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Claims;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity;
using Telerik.Sitefinity.Model;
using System.Text.RegularExpressions;

namespace Telerik.Sitefinity
{
    /// <summary>
    /// Thanks Stephen Pittman https://plus.google.com/+StephenPittmanAus/posts
    /// </summary>
    public class PublishingExtensions
    {
        /// <summary>
        /// Saves a dynamic content item without all the workflow bloat
        /// 🔥 From SitefinitySteve
        /// </summary>
        /// <returns>Telerik.Sitefinity.DynamicModules.Model.DynamicContent</returns>
        public static DynamicContent JustFingSaveIt(string type, Dictionary<string, object> fields, bool publishItem = false, string identifyingFieldName = "Title", bool requireUniqueIdentifyingField = true, bool createUniqueUrl = false, Guid ownerId = new Guid(), bool suppressSecurity = true, DateTime? publishOn = null)
        {
            //Create blank item
            var dynMgr = DynamicModuleManager.GetManager(DynamicModuleManager.GetDefaultProviderName());
            if (!type.StartsWith("Telerik.Sitefinity.DynamicTypes.Model.")){
                type = "Telerik.Sitefinity.DynamicTypes.Model.{0}".Arrange(type);
            }

            var itemType = TypeResolutionService.ResolveType(type);
            var item = dynMgr.CreateDataItem(itemType);

            //Set field data
            foreach (var field in fields)
            {
                item.SetValue(field.Key, field.Value);
            }

            //Title doesn't have to be unique
            var identifyingField = fields[identifyingFieldName].ToString();

            if (requireUniqueIdentifyingField && dynMgr.GetDataItems(itemType).Any(o => o.GetValue<string>(identifyingFieldName).ToLower() == identifyingField.ToLower()))
            {
                throw new ArgumentException(string.Format("Identifying field value is not unique: '{0}', value: '{1}'", identifyingFieldName, identifyingField));
            }

            //URL must be unique
            var url = Regex.Replace(identifyingField.ToLower(), @"[^\w\-\!\$\'\(\)\=\@\d_]+", "-");
            var itemExists = dynMgr.GetDataItems(itemType).Any(o => o.UrlName == url);

            if (itemExists && !createUniqueUrl)
            {
                throw new ArgumentException(string.Format("URL is not unique based on identifying field: '{0}', value: '{1}'", identifyingFieldName, identifyingField));
            }

            if (itemExists && createUniqueUrl)
            {
                //Create unique URL means append "-1" or "-2" etc... until unique
                var modifier = 1;

                while (dynMgr.GetDataItems(itemType).Any(o => o.UrlName == string.Format("{0}-{1}", url, modifier)))
                {
                    modifier++;
                }

                url = string.Format("{0}-{1}", url, modifier);
            }

            item.UrlName = url;

            //Owner - if no ID provided, use logged on user
            if (ownerId == Guid.Empty)
            {
                var currentUser = ClaimsManager.GetCurrentUserId();
                item.Owner = currentUser == null ? Guid.Empty : currentUser;
            }
            else
            {
                item.Owner = ownerId;
            }

            //Save the master item
            if (publishOn == null) {

                item.PublicationDate = DateTime.Now;
            }

            item.SetWorkflowStatus(dynMgr.Provider.ApplicationName, "Draft");

            if (suppressSecurity) {

                dynMgr.Provider.SuppressSecurityChecks = true;
            }
            dynMgr.SaveChanges();

            if (suppressSecurity) {

                dynMgr.Provider.SuppressSecurityChecks = false;
            }

            if (publishItem)
            {
                //We can now call the following to publish the item
                if (publishOn == null)
                {
                    var publishedItem = dynMgr.Lifecycle.Publish(item);
                }
                else
                {
                    var publishedItem = dynMgr.Lifecycle.PublishWithSpecificDate(item, publishOn.Value);
                }

                //You need to set appropriate workflow status
                item.SetWorkflowStatus(dynMgr.Provider.ApplicationName, "Published");

                //You need to call SaveChanges() in order for the items to be actually persisted to data store
                if (suppressSecurity) {
                    dynMgr.Provider.SuppressSecurityChecks = true;

                }

                dynMgr.SaveChanges();

                if (suppressSecurity) {
                    dynMgr.Provider.SuppressSecurityChecks = false;

                }
            }

            return item;
        }

    }

    public static class PageExtensions{
        /// <summary>
        /// From https://plus.google.com/109308138315717177456/posts/CK1cANseeKP
        /// 🔥 From SitefinitySteve
        /// </summary>
        /// <returns>void</returns>
        public static void UnpublishPage(this PageNode pageNode)
        {
            var pageManager = PageManager.GetManager();

            pageNode.ApprovalWorkflowState = "Unpublished";

            pageManager.UnpublishPage(pageNode.GetPageData());
            pageManager.SaveChanges();
        }
    }
}
