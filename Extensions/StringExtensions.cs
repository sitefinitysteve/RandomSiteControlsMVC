using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Telerik.Sitefinity
{
    public static class StringExtensions
    {
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