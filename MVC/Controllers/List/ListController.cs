using System;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using Telerik.Sitefinity.Mvc;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Modules.Pages.Configuration;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers.Attributes;
using RandomSiteControlsMVC.MVC.Models.List;

namespace SitefinityWebApp.Mvc.Controllers
{
    [EnhanceViewEnginesAttribute]
    [ControllerToolboxItem(Name = "ListController_MVC", Title = "Content List", SectionName = ToolboxesConfig.ContentToolboxSectionName, CssClass = "sfAddContentLnk sfListitemsIcn sfMvcIcn")]
    public class ListController : Controller, ICustomWidgetVisualization
    {
        public ActionResult Index()
        {
            ListModel model = this.GetModel();

            return View(this.TemplateName, model);
        }

        private ListModel _model = null;
        private ListModel GetModel()
        {
            if(_model == null)
                _model = new ListModel(this.SerializedContent);

            return _model;
        }

        protected override void HandleUnknownAction(string actionName)
        {
            View(this.TemplateName, this.GetModel()).ExecuteResult(this.ControllerContext);
        }

        #region PROPERTIES
        private string _templateName = "Default";
        public string TemplateName
        {
            get { return _templateName; }
            set
            {
                _templateName = value;
            }
        }

        private string _serializedContent = "[]";
        public string SerializedContent
        {
            get { return _serializedContent; }
            set
            {
                _serializedContent = value;
            }
        }


        #endregion

        #region ICustomWidgetVisualization
        public bool IsEmpty
        {
            get
            {
                var model = this.GetModel();
                return model.Items.Count == 0;
            }
        }

        public string EmptyLinkText
        {
            get
            {
                return "Click to create list items";
            }
        }
        #endregion
    }
}