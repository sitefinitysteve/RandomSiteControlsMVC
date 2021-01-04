using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Telerik.Sitefinity
{ 
    public static class StringExtensions
    {
        /// <summary>
        /// Capitalize the first letter of a string 
        /// 🔥 From SitefinitySteve
        /// </summary>
        /// <returns>string</returns>
        public static string Capitalize(this string s)
        {
            // Check for empty string.
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }
            // Return char and concat substring.
            return char.ToUpper(s[0]) + s.Substring(1);
        }

        /// <summary>
        /// Check if a string contains any of the passed in words
        /// 🔥 From SitefinitySteve
        /// </summary>
        /// <param name="str">String to check</param>
        /// <param name="values">Words passed in</param>
        /// <returns>bool</returns>
        public static bool ContainsAny(this string str, params string[] values)
        {
            if (!string.IsNullOrEmpty(str) || values.Length > 0)
            {
                foreach (string value in values)
                {
                    if (str.ToLower().Contains(value.ToLower()))
                        return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Checks to see if a string contains all of the passed in values
        /// 🔥 From SitefinitySteve
        /// </summary>
        /// <param name="value"></param>
        /// <param name="values"></param>
        /// <returns>bool</returns>
        public static bool ContainsAll(this string value, params string[] values)
        {
            foreach (string one in values)
            {
                if (!value.ToLower().Contains(one.ToLower()))
                {
                    return false;
                }
            }
            return true;
        }
    }
}