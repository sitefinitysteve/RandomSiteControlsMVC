(function ($) {
    angular.module('designer').requires.push('ngSanitize');
    angular.module('designer').requires.push('sfSelectors');

    angular.module('designer').controller('SimpleCtrl', ['$scope', 'propertyService', function ($scope, propertyService) {
        $scope.librarySelector = {
            selectedItemId: null,
            selectedItem: null
        };

        // whenever librarySelector.selectedItemId changes, bind the new value to the widget controller property 
        $scope.$watch('librarySelector.selectedItemId',
            function (newSelectedItemId, oldSelectedItemId) {
                console.log("newSelectedItemId", newSelectedItemId, oldSelectedItemId);
                if (newSelectedItemId !== oldSelectedItemId) {
                    if (newSelectedItemId) {
                        $scope.properties.SerializedSelectedItemId.PropertyValue = newSelectedItemId;
                    }
                }
            },
            true
        );

        // whenever librarySelector.selectedItem changes, bind the new value to the widget controller property
        $scope.$watch('librarySelector.selectedItem',
            function (newSelectedItem, oldSelectedItem) {
                console.log("newSelectedItem", newSelectedItem, oldSelectedItem);
                if (newSelectedItem !== oldSelectedItem) {
                    if (newSelectedItem) {
                        $scope.properties.SerializedSelectedItem.PropertyValue = JSON.stringify(newSelectedItem);
                    }
                }
            },
            true
        );


        propertyService.get()
            .then(function (data) {
                if (data) {
                    $scope.properties = propertyService.toAssociativeArray(data.Items);
                    console.log($scope.properties);
                    // copy the selected item id so that the selector can initialize with the selected items
                    var selectedItemId = $scope.properties.SerializedSelectedItemId.PropertyValue || null;
                    if (selectedItemId) {
                        $scope.librarySelector.selectedItemId = selectedItemId;
                    }
                }
            },
            function (data) {
                $scope.feedback.showError = true;
                if (data)
                    $scope.feedback.errorMessage = data.Detail;
            })
            .finally(function () {
                $scope.feedback.showLoadingIndicator = false;
            });
    }]);
})(jQuery);
