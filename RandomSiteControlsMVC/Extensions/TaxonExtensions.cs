using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using Telerik.Sitefinity.Taxonomies;
using Telerik.Sitefinity.Taxonomies.Model;
using Telerik.Sitefinity.DynamicModules.Model;


namespace Telerik.Sitefinity.Taxonomies.Model
{
    public static class TaxonExtensions
    {
        /// <summary>
        /// Gets a taxon attribute value
        /// 🔥 From SitefinitySteve
        /// </summary>
        /// <typeparam name="T">Type of the data</typeparam>
        /// <param name="currentTaxon">This taxon</param>
        /// <param name="key">Dictionary key the data is stored as</param>
        /// <param name="isJson">If the data is a complex object stored as JSON make this true to return the deserialized object</param>
        /// <param name="defaultValue">If the item doesn't exist, it will initalize to this</param>
        /// <param name="createIfDoesntExist">If the item isnt in the attribute collection, create it</param>
        /// <returns></returns>
        public static T GetValue<T>(this Taxon currentTaxon, string key, bool createIfDoesntExist = false, bool isJson = false)
        {
            bool keyExists = currentTaxon.Attributes.ContainsKey(key);

            if (!keyExists)
            {
                if (createIfDoesntExist)
                {
                    var value = (default(T) == null) ? "null" : default(T).ToString();

                    currentTaxon.Attributes.Add(key, value);
                }
            }
            else{
                if (isJson)
                {
                    return ServiceStack.Text.JsonSerializer.DeserializeFromString<T>(currentTaxon.Attributes[key]);
                }
                else
                {

                    return (T)TypeDescriptor.GetConverter(typeof(T)).ConvertFromInvariantString(currentTaxon.Attributes[key]);
                }
            }

            return default(T);
        }

        /// <summary>
        /// Saves an attribute to a taxon.  Do not forget to call SaveChanges on your TaxonomyManager.
        /// 🔥 From SitefinitySteve
        /// </summary>
        /// <param name="currentTaxon">This taxon</param>
        /// <param name="key">Dictionary Key to store the value as</param>
        /// <param name="value">Data to store</param>
        /// <param name="asJSON">If it's a complex object, convert to JSON</param>
        public static void SetValue(this Taxon currentTaxon, string key, object value, bool isJson = false)
        {
            string data = (isJson) ? ServiceStack.Text.JsonSerializer.SerializeToString(value) : value.ToString();

            if (data.Length >= 255)
                throw new ArgumentOutOfRangeException("Data length exceeded, Max 255");

            if (!currentTaxon.Attributes.ContainsKey(key))
            {
                currentTaxon.Attributes.Add(key, data);
            }else{
                currentTaxon.Attributes[key] = data;
            }
        }

        /// <summary>
        /// To Impliment Later
        /// 🔥 From SitefinitySteve
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsSimpleType(
            this Type type)
                    {
                        return
                        type.IsValueType ||
                        type.IsPrimitive ||
                        new Type[] {
                            typeof(String),
                            typeof(Decimal),
                            typeof(DateTime),
                            typeof(DateTimeOffset),
                            typeof(TimeSpan),
                            typeof(Guid)
            }.Contains(type) ||
            Convert.GetTypeCode(type) != TypeCode.Object;
        }

        /// <summary>
        /// Checks for a key
        /// 🔥 From SitefinitySteve
        /// </summary>
        /// <param name="currentTaxon">This taxon</param>
        /// <param name="key">Key to find</param>
        /// <returns></returns>
        public static bool HasAttribute(this Taxon currentTaxon, string key){
            return currentTaxon.Attributes.ContainsKey(key);
        }
    }
}