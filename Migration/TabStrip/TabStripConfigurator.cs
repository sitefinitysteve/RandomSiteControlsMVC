using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Web.UI;
using Telerik.Sitefinity.Web.UI;

namespace RandomSiteControls.TabStrip
{
    public class TabStripConfigurator : SimpleView
    {
        protected override void InitializeControls(GenericContainer container)
        {
            this.tabStripLabel.Text = this.TabNames;

            if (!this.IsDesignMode())
            {
                //Hide in live mode
                this.Visible = false;
            }
        }

        private string _tabNames = "[{'Text':'Tab 1','Image':'','NavigateUrl':'', 'CssClass':'', 'Key':''},{'Text':'Tab 2','Image':'','NavigateUrl':'', 'CssClass':'', 'Key':''},{'Text':'Tab 3','Image':'','NavigateUrl':'', 'CssClass':'', 'Key':''}]";
        public string TabNames
        {
            get
            {
                return _tabNames;
            }
            set
            {
                _tabNames = value;
            }
        }

        protected override string LayoutTemplateName
        {
            get
            {
                //return "RandomSiteControls.TabStrip.Views.TabStripConfigurator.ascx";
                return null;
            }
        }

        public override string LayoutTemplatePath
        {
            get
            {
                var path = "~/SitefinitySteveMVC/RandomSiteControlsMVC.Migration.TabStrip.TabStripConfigurator.ascx";
                return path;
            }
            set
            {
                base.LayoutTemplatePath = value;
            }
        }
        protected virtual Label tabStripLabel
        {
            get
            {
                return this.Container.GetControl<Label>("tabStripLabel", true);
            }
        }
    }
}
