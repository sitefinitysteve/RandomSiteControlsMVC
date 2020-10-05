using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using Telerik.Sitefinity.Data.Metadata;
using Telerik.Sitefinity.Taxonomies;
using Telerik.Sitefinity.Taxonomies.Model;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.Data;

namespace Telerik.Sitefinity.Model
{
    public static class GeneralExtensions
    {
        /// <summary>
        /// Gets an objects associated manager
        /// 🔥 From SitefinitySteve
        /// </summary>
        /// <returns>IManager</returns>
        public static IManager GetContentManager(this object item){
        	return ManagerBase.GetMappedManager(item.GetType());
        }

        /// <summary>
        /// Validates a DynamicContent item has a field defined, thx Stacey Schlenker for the update\testing
        /// </summary>
        /// <returns>bool</returns>
        public static bool HasValue(this DynamicContent dataItem, string field)
        {
            return App.WorkWith().DynamicData().Type(dataItem.GetType()).Get().Fields.Any(f => f.FieldName == field);
        }
    }
}