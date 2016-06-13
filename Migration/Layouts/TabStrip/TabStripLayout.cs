using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.HtmlControls;
using Telerik.Sitefinity.Web.UI;
using System.Web.UI;
using System.Diagnostics;
using System.Web.UI.WebControls;

namespace RandomSiteControls.TabStrip {
    public class TabStripLayout : LayoutControl
    {
        protected override void CreateChildControls() {
            base.CreateChildControls();
        }

        public override string AssemblyInfo {
            get {
                return GetType().ToString();
            }
            set {
                base.AssemblyInfo = value;
            }
        }

        #region TEMPLATE
        public override string Layout {
            get {
                /*return this.CustomTempalate;*/
                var layout = this.ViewState["Layout"] as string;
                if (string.IsNullOrEmpty(layout)) {
                    layout = this.CustomTempalate;
                }
                return layout;
            }
        }

        public string CustomTempalate = "~/SitefinitySteveMVC/RandomSiteControls.TabStrip.Views.TabStripLayout.ascx";
        #endregion
    }
}
