(function ($) {
    angular.module('designer').controller('SimpleCtrl', ['$scope', 'propertyService', function ($scope, propertyService) {
        //Resize large
        $('.modal-dialog').scope().size = 'lg';

        //Store preview
        $scope.myHTML = "";

        var preview = $("#preview");
        var converter = new showdown.Converter();
        $scope.$watch('properties.Markdown.PropertyValue', function (newValue, oldValue) {
            $scope.myHTML = converter.makeHtml(newValue);

            //I do not WANT to do this, I just am not sure how to include sanitize properly,
            //please if you know PR it, directive script is in the assembly already
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