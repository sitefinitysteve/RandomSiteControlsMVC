(function ($) {
    angular.module('designer').requires.push('sfSelectors');

    angular.module('designer').controller('SimpleCtrl', ['$scope', 'propertyService', function ($scope, propertyService) {
        // initialize the news selector model
        $scope.newsSelector = {
            selectedItemId: [],
            selectedItem: []
        };


        // whenever newsSelector.selectedItemId changes, bind the new value to the widget controller property 
        $scope.$watch(newsSelector.selectedItemId,
            'newsSelector.selectedItemId',
            function (newSelectedItemId, oldSelectedItemId) {
                if (newSelectedItemId !== oldSelectedItemId) {
                    if (newSelectedItemId) {
                        $scope.properties.SerializedSelectedItemId.PropertyValue = newSelectedItemId;
                    }
                }
            },
            true
        );

        // whenever newsSelector.selectedItem changes, bind the new value to the widget controller property
        $scope.$watch(
            'newsSelector.selectedItem',
            function (newSelectedItem, oldSelectedItem) {
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

                    // copy the selected item id so that the selector can initialize with the selected items
                    var selectedItemId = $scope.properties.SerializedSelectedItemId.PropertyValue || null;
                    if (selectedItemId) {
                        $scope.newsSelector.selectedItemId = selectedItemId;
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
