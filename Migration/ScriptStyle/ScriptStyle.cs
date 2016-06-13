using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.ControlDesign;
using ServiceStack;
using ServiceStack.Text;
using Telerik.Microsoft.Practices.EnterpriseLibrary.Logging;

namespace RandomSiteControls
{
    public class ScriptStyleContainer : Control, INamingContainer
    {
        public ScriptStyleContainer()
        {

        }
    }

    [ParseChildren(true)]
    public class ScriptStyle : SimpleView, ICustomWidgetVisualization
    {
        /// </remarks>
        protected override void InitializeControls(GenericContainer container)
        {

            if (this.IsDesignMode())
            {
                designModePanel.Visible = true;
            }


            if (this.style != null || !String.IsNullOrEmpty(this.CssText))
            {
                string cssText = String.Empty;

                if (this.style != null)
                {
                    PlaceHolder cssPlaceHolder = new PlaceHolder();
                    this.style.InstantiateIn(cssPlaceHolder);
                    cssText = RenderControl(cssPlaceHolder);
                }
                else
                {
                    cssText = this.CssText;
                }

                bool containsWrappers = (cssText.Contains("<style")) ? true : false;

                if (!containsWrappers)
                    cssText = "<style type='text/css'>" + cssText + "</style>";

                this.Page.Header.Controls.Add(new LiteralControl(cssText));
            }

            try
            {
                Logger.Writer.Write("Migrate: ScriptStyle: {0}".Arrange(HttpContext.Current.Request.Url.AbsoluteUri));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

        }

        #region Properties
        [PersistenceMode(PersistenceMode.InnerProperty),
        TemplateContainer(typeof(ScriptStyleContainer))]
        public ITemplate script { get; set; }

        [PersistenceMode(PersistenceMode.InnerProperty),
         TemplateContainer(typeof(ScriptStyleContainer))]
        public ITemplate style { get; set; }

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


        private string _description = "";
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        private bool _allowScriptsInDesignMode = true;
        public bool AllowScriptsInDesignMode
        {
            get { return _allowScriptsInDesignMode; }
            set { _allowScriptsInDesignMode = value; }
        }

        private EmbedPosition _literalEmbedPosition = EmbedPosition.BeforeBodyEndTag;
        public EmbedPosition ScriptEmbedPosition
        {
            get
            {
                if (_literalEmbedPosition == null)
                    _literalEmbedPosition = new EmbedPosition();
                return _literalEmbedPosition;
            }
            set { _literalEmbedPosition = value; }
        }
        #endregion

        #region Layouts
        /// <summary>
        /// Gets the layout template path
        /// </summary>
        public override string LayoutTemplatePath
        {
            get
            {
                return ScriptStyle.layoutTemplatePath;
            }
        }

        /// <summary>
        /// Gets the layout template name
        /// </summary>
        protected override string LayoutTemplateName
        {
            get
            {
                return String.Empty;
            }
        }
        #endregion

        #region Control References
        protected virtual Literal descriptionLiteral
        {
            get { return this.Container.GetControl<Literal>("descriptionLiteral", true); }
        }

        protected virtual Panel designModePanel
        {
        	get
        	{
        		return this.Container.GetControl<Panel>("designModePanel", true);
        	}
        }
        #endregion

#region Methods
/// <summary>
/// Renders all the controls out to a string
/// </summary>
/// <param name="control"></param>
/// <returns></returns>
private string RenderControl(Control control)
        {
            StringBuilder sb = new StringBuilder();
            using (StringWriter stringWriter = new StringWriter(sb))
            {
                using (HtmlTextWriter textWriter = new HtmlTextWriter(stringWriter))
                {
                    control.RenderControl(textWriter);
                }
            }

            return sb.ToString();
        }

        /// <summary>
        /// Gets the is empty.
        /// </summary>
        /// <value>The is empty.</value>
        public bool IsEmpty
        {
            get
            {
                bool isEmpty = false;
                isEmpty = (String.IsNullOrEmpty(this.JavaScriptText) &&
                            String.IsNullOrEmpty(this.CssText) &&
                            String.IsNullOrEmpty(this.Description) &&
                            this.ExternalHeaderLinks.Trim() == "[]" &&
                            this.ExternalInPlaceLinks.Trim() == "[]" &&
                            this.ExternalFooterLinks.Trim() == "[]");

                return isEmpty;
            }
        }

        /// <summary>
        /// Gets the empty link text.
        /// </summary>
        /// <value>The empty link text.</value>
        public string EmptyLinkText
        {
            get
            {
                return @"<i class='icon-code pull-right icon-large'></i>It&#39;s Time to Code";
            }
        }
        #endregion

        #region Events
        protected override void OnPreRender(EventArgs e)
        {
            #region External Files
            this.RenderExternal(this.ExternalHeaderLinks, EmbedPosition.Head);
            this.RenderExternal(this.ExternalInPlaceLinks, EmbedPosition.InPlace);
            this.RenderExternal(this.ExternalFooterLinks, EmbedPosition.BeforeBodyEndTag);

            #endregion

            #region Templates
            Debug.WriteLine("Rendering Script Templates for " + this.ID);
            if (this.script != null || !String.IsNullOrEmpty(this.JavaScriptText))
            {
                string scriptText = String.Empty;

                if (this.script != null)
                {
                    PlaceHolder javascriptPlaceHolder = new PlaceHolder();
                    this.script.InstantiateIn(javascriptPlaceHolder);
                    scriptText = RenderControl(javascriptPlaceHolder);
                }
                else
                {
                    scriptText = this.JavaScriptText;
                }

                bool containsWrappers = (scriptText.Contains("<script")) ? true : false;

                if (!containsWrappers)
                    scriptText = "<script type='text/javascript'>" + scriptText + "</script>";


                bool canRender = true;

                //Check to see if the script can indeed render in design mode
                if (!this.AllowScriptsInDesignMode && this.IsDesignMode() && !this.IsPreviewMode())
                    canRender = false;

                if (canRender)
                {
                    RenderScriptOrStyle(scriptText, this.ScriptEmbedPosition);
                }
                //this.Page.ClientScript.RegisterStartupScript(typeof(ScriptStyle), this.ClientID, scriptText, false);
            }
            #endregion


        }

        /// <summary>
        /// Renders the external.
        /// </summary>
        /// <param name="externalHeaderLinks">The external header links.</param>
        private void RenderExternal(string externalHeaderLinks, EmbedPosition position)
        {
            var links = ServiceStack.Text.JsonSerializer.DeserializeFromString<List<ScriptStyleLink>>(externalHeaderLinks);

            if (links.Count > 0)
            {
                //Create the context item
                if (HttpContext.Current.Items["RandomSiteControlsScriptStyleLinks"] == null)
                    HttpContext.Current.Items.Add("RandomSiteControlsScriptStyleLinks", new List<ScriptStyleLink>());

                List<ScriptStyleLink> globalLinks = (List<ScriptStyleLink>) HttpContext.Current.Items["RandomSiteControlsScriptStyleLinks"];

                foreach (var link in links)
                {
                    //Check to see if it's already on the page
                    bool exists = (globalLinks.Count(x => x.LinkType == link.LinkType && x.Url == link.Url) == 0) ? false : true;

                    if (!exists)
                    {
                        //Prep the objects
                        string linkFormat = "";
                        if (link.LinkType == "script")
                            linkFormat = String.Format("<script src='{0}' type='text/javascript' {1}></script>", link.Url, link.LoadType);
                        else
                            linkFormat = String.Format("<link href='{0}' type='text/css' rel='stylesheet' />", link.Url);

                        this.RenderScriptOrStyle(linkFormat, position);

                        //Add to the global context
                        globalLinks.Add(link);
                    }
                }

                //Save the global object back
                HttpContext.Current.Items["RandomSiteControlsScriptStyleLinks"] = globalLinks;
            }
        }

        public void RenderScriptOrStyle(string script, EmbedPosition position)
        {
            Literal literal = new Literal();
            literal.Text = script;

            if (position == EmbedPosition.Head)
            {
                this.Page.Header.Controls.Add(literal);
                return;
            }
            else if (position == EmbedPosition.InPlace)
            {
                this.Controls.Add(literal);
                return;
            }
            else if (position == EmbedPosition.BeforeBodyEndTag)
            {
                var hashCodeKey = script.GetHashCode().ToString().Replace("-", "");

                if (!this.Page.ClientScript.IsStartupScriptRegistered(hashCodeKey))
                {
                    Debug.WriteLine("Script not registered with hash " + hashCodeKey);
                    this.Page.ClientScript.RegisterStartupScript(typeof(ScriptStyle), hashCodeKey, script, false);
                }
                return;
            }
        }

        protected override void Render(HtmlTextWriter writer)
        {
            //Hide in live mode, show in design
            if (this.IsDesignMode() && !this.IsPreviewMode())
            {
                this.Visible = true;

                if (!String.IsNullOrEmpty(this.Description))
                {
                    string markup = "";
                    string html = String.Format("<div class='scriptstyle-description-wrapper'>{0}<span class='description'>{1}</span></div>", markup, this.Description);

                    writer.Write(html);
                }

            }
            else
            {
                this.RenderContents(writer);

                if (this.ScriptEmbedPosition == EmbedPosition.InPlace || this.ExternalInPlaceLinks.Trim() != "[]")
                    this.Visible = true;
                else
                    this.Visible = false;
            }
        }
        #endregion

        #region Private members & constants
        public static readonly string layoutTemplatePath = "~/SitefinitySteveMVC/RandomSiteControlsMVC.Migration.ScriptStyle.ScriptStyle.ascx";
        #endregion
    }
    public enum EmbedPosition
    {
        Head,
        InPlace,
        BeforeBodyEndTag
    }

    public class ScriptStyleLink
    {
        public string Url { get; set; }
        public string LinkType { get; set; }
        public string LoadType { get; set; }
    }
}
