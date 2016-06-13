using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Web.UI;
using Telerik.Sitefinity.Web.UI;

namespace RandomSiteControls
{
    public class ScriptStyle : SimpleView
    {
        protected override void InitializeControls(GenericContainer container)
        {
            StringBuilder result = new StringBuilder();

            if(!String.IsNullOrEmpty(this.JavaScriptText))
                result.AppendLine("JavaScriptText: " + this.JavaScriptText + "<br/><br/>");

            if (!String.IsNullOrEmpty(this.CssText))
                result.AppendLine("CssText: " + this.CssText + "<br/><br/>");

            if(!String.IsNullOrEmpty(this.ExternalHeaderLinks) && this.ExternalHeaderLinks != "[]")
                result.AppendLine("ExternalHeaderLinks: " + this.ExternalHeaderLinks + "<br/><br/>");

            if (!String.IsNullOrEmpty(this.ExternalInPlaceLinks) && this.ExternalInPlaceLinks != "[]")
                result.AppendLine("ExternalInPlaceLinks: " + this.ExternalInPlaceLinks + "<br/><br/>");

            if (!String.IsNullOrEmpty(this.ExternalFooterLinks) && this.ExternalFooterLinks != "[]")
                result.AppendLine("ExternalFooterLinks: " + this.ExternalFooterLinks);

            this.tabStripLabel.Text = result.ToString();

            if (!this.IsDesignMode())
            {
                //Hide in live mode
                this.Visible = false;
            }
        }

        #region PROPERTIES
        public string JavaScriptText { get; set; }
        public string CssText { get; set; }

        private string _externalHeaderLinks = "[]";
        public string ExternalHeaderLinks
        {
            get { return _externalHeaderLinks; }
            set { _externalHeaderLinks = value; }
        }

        private string _externalInPlaceLinks = "[]";
        public string ExternalInPlaceLinks
        {
            get { return _externalInPlaceLinks; }
            set { _externalInPlaceLinks = value; }
        }

        private string _externalFooterLinks = "[]";
        public string ExternalFooterLinks
        {
            get { return _externalFooterLinks; }
            set { _externalFooterLinks = value; }
        }
        #endregion

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
                var path = "~/SitefinitySteveMVC/RandomSiteControlsMVC.Migration.ScriptStyle.ScriptStyle.ascx";
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
