using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Telerik.Sitefinity.Taxonomies;
using Telerik.Sitefinity.Taxonomies.Model;
using Telerik.Sitefinity.DynamicModules.Model;


namespace Telerik.Sitefinity.Taxonomies.Model
{
    public static class HierarchicalTaxonExtensions
    {
        public enum HierarchicalTaxonCompareType
        {
            Equals,
            StartsWith,
            EndsWith,
            Contains
        }


        /// <summary>
        /// Gets the Root of a HierarchicalTaxon
        /// 🔥 From SitefinitySteve
        /// </summary>
        /// <param name="currentTaxon">This Taxon</param>
        /// <returns>Root Taxon</returns>
        public static HierarchicalTaxon GetRootTaxon(this HierarchicalTaxon currentTaxon)
        {
            if (currentTaxon.Parent != null)
            {
                var parent = currentTaxon.Parent;

                while (parent.HasParent())
                {
                    parent = parent.Parent;
                }

                return parent;
            }
            else
                return currentTaxon;
        }

        /// <summary>
        /// Gets a list of the parent Taxon items
        /// 🔥 From SitefinitySteve
        /// </summary>
        /// <param name="currentTaxon">Current Taxon</param>
        /// <returns>List of HierarchicalTaxons linked to this item.  0 index is the closest parent.</returns>
        public static List<HierarchicalTaxon> GetParentTaxa(this HierarchicalTaxon currentTaxon)
        {
            List<HierarchicalTaxon> taxa = new List<HierarchicalTaxon>();

            if (currentTaxon.Parent != null)
            {
                var parent = currentTaxon.Parent;
                taxa.Add(parent);

                while (parent.HasParent())
                {
                    parent = parent.Parent;
                    taxa.Add(parent);
                }
            }

            return taxa;
        }

        /// <summary>
        /// Searches from Parent to parent to match the taxon name you need, returns the first match
        /// 🔥 From SitefinitySteve
        /// </summary>
        /// <param name="currentTaxon">Current Node</param>
        /// <param name="textToFind">Text to locate</param>
        /// <param name="type">Compare type</param>
        /// <param name="isCaseSensitive">Case Sensitive check, doesn't apply to Contains</param>
        /// <returns>Null if nothing found</returns>
        public static HierarchicalTaxon GetFirstParentTaxon(this HierarchicalTaxon currentTaxon, string textToFind, HierarchicalTaxonCompareType type, bool isCaseSensitive)
        {
            bool found = false;
            HierarchicalTaxon foundTaxon = null;
            var compareType = (isCaseSensitive) ? StringComparison.CurrentCulture : StringComparison.CurrentCultureIgnoreCase;

            //Check this one, TODO: consolidate this and the one below
            if (type == HierarchicalTaxonCompareType.StartsWith)
            {
                found = currentTaxon.Title.StartsWith(textToFind, compareType);
            }
            else if (type == HierarchicalTaxonCompareType.EndsWith)
            {
                found = currentTaxon.Title.EndsWith(textToFind, compareType);
            }
            else if (type == HierarchicalTaxonCompareType.Equals)
            {
                found = currentTaxon.Title.Equals(textToFind, compareType);
            }
            else if (type == HierarchicalTaxonCompareType.Contains)
            {
                found = currentTaxon.Title.Contains(textToFind);
            }

            if (found)
                return currentTaxon;


            if (currentTaxon.Parent != null)
            {
                while (currentTaxon != null)
                {

                    switch (type)
                    {
                        case HierarchicalTaxonCompareType.StartsWith:
                            found = currentTaxon.Title.StartsWith(textToFind, compareType);
                            break;
                        case HierarchicalTaxonCompareType.EndsWith:
                            found = currentTaxon.Title.EndsWith(textToFind, compareType);
                            break;
                        case HierarchicalTaxonCompareType.Contains:
                            found = currentTaxon.Title.Contains(textToFind);
                            break;
                        case HierarchicalTaxonCompareType.Equals:
                            found = currentTaxon.Title.Equals(textToFind, compareType);
                            break;
                    }

                    if (found)
                    {
                        foundTaxon = currentTaxon;
                        break;
                    }
                    else
                    {
                        currentTaxon = currentTaxon.Parent;
                    }
                }
            }

            return foundTaxon;
        }

        /// <summary>
        /// Flattens out a taxon tree to a list
        /// 🔥 From SitefinitySteve
        /// </summary>
        public static IEnumerable<HierarchicalTaxon> FlattenHierarchy(this HierarchicalTaxon parent)
        {
            if (parent != null)
            {
                foreach (HierarchicalTaxon control in parent.Subtaxa)
                {
                    yield return control;
                    foreach (HierarchicalTaxon descendant in control.FlattenHierarchy())
                    {
                        yield return descendant;
                    }
                }
            }
        }

        /// <summary>
        /// Lets us know if a Hierarchical taxon object has a parent object
        /// 🔥 From SitefinitySteve
        /// </summary>
        /// <param name="currentTaxon"></param>
        /// <returns></returns>
        private static bool HasParent(this HierarchicalTaxon currentTaxon)
        {
            return (currentTaxon.Parent == null) ? false : true;
        }

        /// <summary>
        /// Flattens out the taxons to a delimited string
        /// 🔥 From SitefinitySteve
        /// </summary>
        public static string FlattenToString(this IEnumerable<HierarchicalTaxon> items, char seperator = ',', bool appendSpace = true)
        {
            string result = String.Empty;
            foreach (HierarchicalTaxon t in items)
            {
                result += String.Format("{0}{1}{2}", t.Title, seperator, (appendSpace) ? " " : "");
            }

            return result.Trim().TrimEnd(seperator);
        }
    }
}