var $sfCodeMirrorHtmlEditor = null;

(function ($) {
    angular.module('designer').requires.push('expander', 'sfCodeArea', 'sfBootstrapPopover');

    angular.module('designer').controller('SimpleCtrl', ['$scope', 'propertyService', function ($scope, propertyService) {
        $('.sf-backend-wrp').addClass('modal-fluid');;
        
        //Set the max height
        setTimeout(function(){
            var elements = $(".CodeMirror-scroll > div, \
            .CodeMirror-scroll > div > div > div,\
            .CodeMirror-gutter");

            var requiredHeight = $(".modal-content").height() - 120;

            elements.height(requiredHeight)
            elements.css("max-height", requiredHeight);
            
            console.log("Resizing from " + currentHeight + " to " + requiredHeight);
        }, 500);


        propertyService.get()
            .then(function (data) {
                if (data && data.Items) {
                    $scope.properties = propertyService.toAssociativeArray(data.Items);
                }
            },
                function (errorData) {
                    $scope.feedback.showError = true;
                    if (errorData && errorData.data)
                        $scope.feedback.errorMessage = errorData.data.Detail;
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