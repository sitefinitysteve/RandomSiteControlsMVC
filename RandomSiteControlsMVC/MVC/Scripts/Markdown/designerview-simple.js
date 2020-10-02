(function ($) {
    angular.module('designer').controller('SimpleCtrl', ['$scope', 'propertyService', function ($scope, propertyService) {


        //Store preview
        //$scope.myHTML = "";

        var preview = $("#preview");
        var converter = new showdown.Converter();
        $scope.$watch('properties.Markdown.PropertyValue', function (newValue, oldValue) {
            //$scope.myHTML = converter.makeHtml(newValue);

            //I do not WANT to do this, I just am not sure how to include sanitize properly,
            //please if you know PR it, directive script is in the assembly already
            setMarkDownPreview(newValue, preview, converter);
        }, true);

        propertyService.get()
            .then(function (data) {
                //Fill the screen
                console.log("Making full screen");
                $('.sf-backend-wrp').addClass('modal-fluid');;

                if (data) {
                    $scope.properties = propertyService.toAssociativeArray(data.Items);

                    setMarkDownPreview($scope.properties.Markdown.PropertyValue, preview, converter);
                    //$scope.myHTML = $scope.properties.Markdown.PropertyValue;
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

    function setMarkDownPreview(markdown, preview, converter) {
        if (markdown !== null && markdown !== "") {
            var html = converter.makeHtml(markdown);
            preview.html(html);
        }
    }
})(jQuery);