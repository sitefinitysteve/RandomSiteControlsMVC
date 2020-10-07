using SitefinityWebApp.Mvc.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomSiteControlsMVC.MVC.Models.PrintTo
{
    public class PrintToModel
    {
        public string ElementId { get; set; }
        public string ButtonText { get; set; }
        public string Filename { get; set; }
        public PrintModeEnum Mode { get; set; }
        public PrintPaperSizeEnum PaperSize {get;set;}
        public string MarginTop { get; set; }
        public string MarginLeft { get; set; }
        public string MarginBottom { get; set; }
        public string MarginRight { get; set; }
        public string Multipage { get; set; }
        public string Landscape { get; set; }
    }
}
