using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Telerik.Sitefinity
{
    public static class MarkupExtensions
    {
        /// <summary>
        /// Renders the "invisible class if the string item is empty, so clearly
        /// you need to have .invisible{display:none;} in your css
        /// 🔥 From SitefinitySteve
        /// </summary>
        /// <returns>string</returns>
        public static string HideIfEmpty(this object text, bool renderSpaceBeforeClass = false)
        {
            if (String.IsNullOrEmpty(text.ToString()))
                return (renderSpaceBeforeClass) ? " invisible" : "invisible";
            else
                return "";
        }

        /// <summary>
        /// Checks to see if there is content, returns true or false, useful for Visible
        /// 🔥 From SitefinitySteve
        /// </summary>
        /// <returns>bool</returns>
        public static bool HasContent(this object text)
        {
            if (String.IsNullOrEmpty(text.ToString()))
                return false;
            else
                return true;
        }


        /// <summary>
        /// Renders passed text if item is NOT empty
        /// 🔥 From SitefinitySteve
        /// </summary>
        /// <returns>string</returns>
        public static string IfContentExists(this object text, object textToRender, object elseRender = null)
        {
            if (!String.IsNullOrEmpty(text.ToString()))
            {
                return textToRender.ToString();
            }
            else
            {
                return (elseRender == null) ? "" : elseRender.ToString();
            }
        }


        /// <summary>
        /// Renders passed text if TRUE
        /// 🔥 From SitefinitySteve
        /// </summary>
        /// <returns>string</returns>
        public static string IsTrue(this bool value, string textToRender, string elseRender = "")
        {
            if (value)
            {
                return textToRender;
            }
            else
            {
                return elseRender;
            }
        }

        /// <summary>
        /// Renders passed text if FALSE
        /// 🔥 From SitefinitySteve
        /// </summary>
        /// <returns>string</returns>
        public static string IsFalse(this bool value, string textToRender, string elseRender = "")
        {
            if (value)
            {
                return textToRender;
            }
            else
            {
                return elseRender;
            }
        }
    }
}


