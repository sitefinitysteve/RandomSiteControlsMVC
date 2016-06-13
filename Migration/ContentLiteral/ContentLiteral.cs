using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Web.UI;
using Telerik.Sitefinity.Web.UI;
using System.Web;
using Telerik.Microsoft.Practices.EnterpriseLibrary.Logging;
using System.Diagnostics;

namespace RandomSiteControls.ContentLiteral
{
    public class ContentLiteral : SimpleView
    {
        protected override void InitializeControls(GenericContainer container)
        {
            this.tabStripLabel.Text = HttpContext.Current.Server.HtmlEncode(this.Markup);

            if (!this.IsDesignMode())
            {
                //Hide in live mode
                this.Visible = false;
            }

            try
            {
                Logger.Writer.Write("Migrate: ContentLiteral: {0}".Arrange(HttpContext.Current.Request.Url.AbsoluteUri));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        private string _markup = String.Empty;
        public string Markup
        {
            get { return _markup; }
            set { _markup = value; }
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
                var path = "~/SitefinitySteveMVC/RandomSiteControlsMVC.Migration.ContentLiteral.ContentLiteral.ascx";
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
