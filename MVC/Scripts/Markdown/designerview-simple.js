(function ($) {
    angular.module('designer').controller('SimpleCtrl', ['$scope', 'propertyService', function ($scope, propertyService) {

        $scope.myHTML = "";

        var preview = $("#preview");
        var converter = new showdown.Converter();
        $scope.$watch('properties.Markdown.PropertyValue', function (newValue, oldValue) {
            $scope.myHTML = converter.makeHtml(newValue);

            //I do not WANT to do this, I just am not sure how to bypass the security, please if you know PR it
            preview.html($scope.myHTML);
        }, true);

        propertyService.get()
            .then(function (data) {
                if (data) {
                    $scope.properties = propertyService.toAssociativeArray(data.Items);

                    $scope.myHTML = $scope.properties.Markdown.PropertyValue;
                }
            },
            function (data) {
                $scope.feedback.showError = true;
                if (data)
                    $scope.feedback.errorMessage = data.Detail;
            })
            .then(function () {
                $scope.feedback.savingHandlers.push(function () {
                });
            })
            .finally(function () {
                $scope.feedback.showLoadingIndicator = false;
            });
    }]);
})(jQuery);