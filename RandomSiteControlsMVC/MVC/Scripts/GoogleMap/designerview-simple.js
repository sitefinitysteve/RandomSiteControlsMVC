(function ($) {
    angular.module('designer').controller('SimpleCtrl', ['$scope', 'propertyService', function ($scope, propertyService) {
        $scope.validAddress = true;

        //Default
        $scope.properties = {
            MapType: "ROADMAP"
        };

        $scope.setActive = function (type) {
            $scope.properties.MapType.PropertyValue = type;
            loadMap();
        };

        $scope.isActive = function (type) {
            return type === $scope.properties.MapType.PropertyValue;
        };

        $scope.updateLocation = function () {
            console.log("Updating Location");
            setLocation();
        }

        $scope.updateZoom = function () {
            loadMap();
        }

        function setLocation() {
            var address = $scope.properties.LocationAddress.PropertyValue;
            var geocoder = new google.maps.Geocoder();

            if (geocoder) {
                geocoder.geocode({ 'address': address }, function (results, status) {
                    console.log("Geocode", results, status);
                    if (status === google.maps.GeocoderStatus.OK) {
                        $scope.validAddress = true;
                        var l = results[0].geometry.location;
                        var lat = l.lat();
                        var long = l.lng();


                        $scope.properties.Latitude.PropertyValue = lat;
                        $scope.properties.Longitude.PropertyValue = long;

                        loadMap();
                    } else {
                        $scope.validAddress = false;
                    }
                });
            }
        }

        function loadMap() {
            var zoom = parseInt($scope.properties.ZoomLevel.PropertyValue);
            var settings = {
                center: new google.maps.LatLng($scope.properties.Latitude.PropertyValue, $scope.properties.Longitude.PropertyValue),
                zoom: isNaN(zoom) ? 10 : zoom,
                mapTypeId: null
            };

            var type = $scope.properties.MapType.PropertyValue;
            switch (type) {
                case "ROADMAP":
                    settings.mapTypeId = google.maps.MapTypeId.ROADMAP;
                    break;
                case "SATELLITE":
                    settings.mapTypeId = google.maps.MapTypeId.SATELLITE;
                    break;
                case "HYBRID":
                    settings.mapTypeId = google.maps.MapTypeId.HYBRID;
                    break;
                case "TERRAIN":
                    settings.mapTypeId = google.maps.MapTypeId.TERRAIN;
                    break;
            }

            map = new google.maps.Map(document.getElementById("map_canvas_preview"), settings);


            //Add the overlay
            var marker = new google.maps.Marker({
                position: settings.center,
                title: $scope.properties.LocationAddress.PropertyValue
            });

            // To add the marker to the map, call setMap();
            marker.setMap(map);
        }


        propertyService.get()
            .then(function (data) {
                if (data) {
                    $scope.properties = propertyService.toAssociativeArray(data.Items);
                    loadMap();
                }
            },
            function (data) {
                $scope.feedback.showError = true;
                if (data)
                    $scope.feedback.errorMessage = data.Detail;
            })
            .then(function () {
                $scope.feedback.savingHandlers.push(function () {
                    var zoom = parseInt($scope.properties.ZoomLevel.PropertyValue);
                    if (zoom == "" || zoom == null || isNaN(zoom)) {
                        $scope.properties.ZoomLevel.PropertyValue = 20;
                    }

                });
            })
            .finally(function () {
                $scope.feedback.showLoadingIndicator = false;
            });
    }]);
})(jQuery);