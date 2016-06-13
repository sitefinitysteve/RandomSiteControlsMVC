using System;
using System.Linq;
using Telerik.Sitefinity.Web.UI;

namespace RandomSiteControls.FancyBox {
    public class FancyBoxLayout : LayoutControl
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

        public string CustomTempalate = "~/SitefinitySteveMVC/RandomSiteControls.FancyBox.Views.FancyBoxLayout.ascx";
        #endregion
    }
}
