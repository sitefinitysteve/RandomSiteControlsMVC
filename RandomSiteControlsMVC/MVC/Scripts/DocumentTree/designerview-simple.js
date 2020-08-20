(function ($) {
    angular.module('designer').requires.push('sfSelectors');

    angular.module('designer').controller('SimpleCtrl', ['$scope', 'propertyService', function ($scope, propertyService) {
        //$('.modal-dialog').scope().size = 'lg';
        $scope.$watch('selectedLibraryId', function (newValue, oldValue) {
            if (newValue) {
                $scope.properties.LibraryId.PropertyValue = JSON.stringify(newValue);
            }
        });

        $scope.$watch('selectedLibrary', function (newValue, oldValue) {
            if (newValue) {
                $scope.properties.SelectedLibraryName.PropertyValue = JSON.stringify(newValue);
            }
        });

        propertyService.get()
            .then(function (data) {
                if (data) {
                    $scope.properties = propertyService.toAssociativeArray(data.Items);

                    $scope.selectedLibraryId = $.parseJSON($scope.properties.LibraryId.PropertyValue);
                    $scope.selectedLibrary = $.parseJSON($scope.properties.SelectedLibraryName.PropertyValue);
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
