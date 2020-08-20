using System;
using System.Linq;

namespace RandomSiteControlsMVC.Mvc.Models
{
    public class GoogleMapModel
    {
        public bool HasApiKey { get; set; }
        public string Zoom { get; set; }
        public string Lat { get; set; }
        public string Long { get; set; }
        public string MapType { get; set; }
        public string Description { get; set; }

        public string Width { get; set; }
        public string Height { get; set; }
    }
}