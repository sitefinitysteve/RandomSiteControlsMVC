(function ($) {
    angular.module('designer').requires.push('sfCodeArea');

    angular.module('designer').controller('SimpleCtrl', ['$scope', 'propertyService', function ($scope, propertyService) {
        //Fill the screen
        $('.sf-backend-wrp').addClass('modal-fluid');;

        setTimeout(function () {
            var myCodeMirror = $('.CodeMirror')[0].CodeMirror;

            //Resize editor
            var modalHeight = $(".modal-body").outerHeight() + "px"; //Bit of padding
            $(".CodeMirror-scroll").attr("style", "min-height: " + modalHeight );

            myCodeMirror.refresh();
        }, 200);

        propertyService.get()
            .then(function (data) {
                if (data) {
                    $scope.properties = propertyService.toAssociativeArray(data.Items);
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