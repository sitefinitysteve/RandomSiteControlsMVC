"use strict";

$(document).ready(function () {
    sfsReloadGoogleMaps();
});

function sfsReloadGoogleMaps() {
    if (typeof google !== "undefined") {
        var mapIndex = 1;
        $(".googlemap-widget").each(function () {
            var wrapper = $(this);
            var mapId = "googlemap-" + mapIndex;
            var mapcanvas = wrapper.find(".map-canvas");

            var mapType = null;
            switch (wrapper.data("maptype")) {
                case "ROADMAP":
                    mapType = google.maps.MapTypeId.ROADMAP;
                    break;
                case "SATELLITE":
                    mapType = google.maps.MapTypeId.SATELLITE;
                    break;
                case "HYBRID":
                    mapType = google.maps.MapTypeId.HYBRID;
                    break;
                case "TERRAIN":
                    mapType = google.maps.MapTypeId.TERRAIN;
                    break;
            }

            var settings = {
                center: new google.maps.LatLng(wrapper.data("lat"), wrapper.data("long")),
                zoom: wrapper.data("zoom"),
                mapTypeId: mapType
            };

            var mymap = new google.maps.Map(mapcanvas.get()[0], settings);

            //Add the overlay
            var marker = new google.maps.Marker({
                position: settings.center,
                title: wrapper.data("description")
            });

            // To add the marker to the map, call setMap();
            marker.setMap(mymap);

            mapIndex = mapIndex + 1;
        });
    }
}

