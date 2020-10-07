using RandomSiteControlsMVC.MVC.Models.PrintTo;
using System;
using System.Web.Mvc;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers.Attributes;
using Telerik.Sitefinity.Modules.Pages.Configuration;
using Telerik.Sitefinity.Mvc;
using Telerik.Sitefinity.Web.UI;

namespace SitefinityWebApp.Mvc.Controllers
{
    public enum PrintModeEnum
    {
        Element,
        Container
    }
    
    public enum PrintPaperSizeEnum
    {
        Auto,
        A4,
        Letter,
        Legal,
        Executive, 
        Folio,
        Tabloid
    }

    [EnhanceViewEnginesAttribute]
    [ControllerToolboxItem(Name = "PrintToControllerMVC", Title = "Print", SectionName = ToolboxesConfig.ContentToolboxSectionName, CssClass = "sfDownloadLinkIcn sfMvcIcn")]
    public class PrintToController : Controller, ICustomWidgetVisualization
    {
        public ActionResult Index()
        {
            var model = this.GetModel();

            return View(this.TemplateName, model);
        }

        private PrintToModel GetModel()
        {
            var model = new PrintToModel()
            {
                ElementId = this.ElementId,
                ButtonText = this.ButtonText,
                Filename = this.Filename,
                Mode = this.Mode,
                PaperSize = this.PaperSize,
                MarginTop = this.MarginTop,
                MarginLeft = this.MarginLeft,
                MarginBottom = this.MarginBottom,
                MarginRight = this.MarginRight,
                Multipage = this.Multipage.ToString().ToLower(),
                Landscape = this.Landscape.ToString().ToLower()
            };

            return model;
        }

        protected override void HandleUnknownAction(string actionName)
        {
            View(this.TemplateName, this.GetModel()).ExecuteResult(this.ControllerContext);
        }

        #region PROPERTIES
        public string ElementId { get; set; }
        public string Filename { get; set; } = "Document1";
        public string ButtonText { get; set; } = "Print";
        public string TemplateName { get; set; } = "Default";
        public PrintModeEnum Mode { get; set; } = PrintModeEnum.Container;
        public PrintPaperSizeEnum PaperSize { get; set; } = PrintPaperSizeEnum.Auto;
        public string MarginTop { get; set; } = "1cm";
        public string MarginLeft { get; set; } = "1cm";
        public string MarginBottom { get; set; } = "1cm";
        public string MarginRight { get; set; } = "1cm";
        public bool Multipage { get; set; } = true;
        public bool Landscape { get; set; } = false;
        #endregion

        #region ICustomWidgetVisualization
        public bool IsEmpty
        {
            get
            {
                return String.IsNullOrEmpty(this.ElementId);
            }
        }

        public string EmptyLinkText
        {
            get
            {
                return "Select which element to print";
            }
        }
        #endregion
    }
}