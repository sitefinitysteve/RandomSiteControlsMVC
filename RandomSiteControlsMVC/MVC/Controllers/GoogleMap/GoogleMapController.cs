using System;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using Telerik.Sitefinity.Mvc;
using RandomSiteControlsMVC.Mvc.Models;
using Telerik.Sitefinity.Modules.Pages.Configuration;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers.Attributes;
using Telerik.Sitefinity;

namespace RandomSiteControlsMVC.Mvc.Controllers
{
    [EnhanceViewEnginesAttribute]
    [ControllerToolboxItem(Name = "GoogleMap_MVC", Title = "Google Map", SectionName = ToolboxesConfig.ContentToolboxSectionName, CssClass = "sfSiteSelectorIcn sfMvcIcn")]
    public class GoogleMapController : Controller, ICustomWidgetVisualization
    {
        /// <summary>
        /// This is the default Action.
        /// </summary>
        public ActionResult Index()
        {
            var model = new GoogleMapModel()
            {
                HasApiKey = String.IsNullOrEmpty(RSCUtil.SfsConfig.GoogleMaps.ApiKey) ? false : true,
                Zoom = this.ZoomLevel.ToString(),
                Lat = this.Latitude.ToString(),
                Long = this.Longitude.ToString(),
                MapType = this.MapType,
                Description = this.LocationDescription,
                Height = this.Height,
                Width = this.Width
            };

            return View("Default", model);
        }

        protected override void HandleUnknownAction(string actionName)
        {
            View("Default").ExecuteResult(this.ControllerContext);
        }

        #region PROPERTIES
        private string _location = "Hamilton, ON";
        public string LocationAddress
        {
            get
            {
                return _location;
            }
            set
            {
                _location = value;
            }
        }

        private string _height = "300px";
        public new string Height
        {
            get
            {
                return _height;
            }
            set
            {
                _height = value;
            }
        }

        private string _width = "100%";
        public new string Width
        {
            get
            {
                return _width;
            }
            set
            {
                _width = value;
            }
        }

        private bool _hasSensor = false;
        public bool HasSensor
        {
            get
            {
                return _hasSensor;
            }
            set
            {
                _hasSensor = value;
            }
        }

        /// <summary>
        /// Google Option, Zoom level
        /// http://code.google.com/apis/maps/documentation/javascript/tutorial.html#MapOptions
        /// </summary>
        private int _zoomLevel = 10;
        public int ZoomLevel
        {
            get
            {
                return _zoomLevel;
            }
            set
            {
                _zoomLevel = value;
            }
        }

        /// <summary>
        /// Google Option, Latitude
        /// http://code.google.com/apis/maps/documentation/javascript/tutorial.html#MapOptions
        /// </summary>
        private string _lat = "43.26096391713925";
        public string Latitude
        {
            get
            {
                return _lat;
            }
            set
            {
                _lat = value;
            }
        }

        /// <summary>
        /// Google Option, Longitude
        /// http://code.google.com/apis/maps/documentation/javascript/tutorial.html#MapOptions
        /// </summary>
        private string _lang = "-79.91678237915039";
        public string Longitude
        {
            get
            {
                return _lang;
            }
            set
            {
                _lang = value;
            }
        }

        /// <summary>
        /// Marker Overlay
        /// http://code.google.com/apis/maps/documentation/javascript/overlays.html
        /// </summary>
        private string _desc = "My Location";
        public string LocationDescription
        {
            get
            {
                return _desc;
            }
            set
            {
                _desc = value;
            }
        }

        /// <summary>
        /// Google Option, Type
        /// http://code.google.com/apis/maps/documentation/javascript/tutorial.html#MapOptions
        /// </summary>
        private string _mapType = "ROADMAP";
        public string MapType
        {
            get
            {
                return _mapType;
            }
            set
            {
                _mapType = value;
            }
        }
        #endregion

        #region ICustomWidgetVisualization
        public bool IsEmpty
        {
            get
            {
                return String.IsNullOrEmpty(this.LocationAddress.Trim());
            }
        }

        public string EmptyLinkText
        {
            get
            {
                return "Missing Google Maps API Keys, please get an administrator to add your google maps API key to /Sitefinity/Administration/Settings/Advanced/SitefinitySteveMvc";
            }
        }
        #endregion
    }
}