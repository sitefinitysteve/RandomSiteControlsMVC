(function ($) {
    angular.module('designer').controller('SimpleCtrl', ['$scope', 'propertyService', function ($scope, propertyService) {
        //$('.modal-dialog').scope().size = 'lg';

        $scope.tabstrip = {
            tabs: []
        };

        $scope.currentTheme = "";
        $scope.origin = window.location.origin;

        $scope.tabstrip.selectedIndex = 0;

        if($scope.tabstrip.tabs.length > 0)
            $scope.tabstrip.selectedTab = $scope.tabstrip.tabs[0];

        $scope.onAddTab = function (title) {
            var newBlankTab = {
                Title: (title === null || title === "") ? "New Tab" : title,
                Selected: ($scope.tabstrip.selectedTab == null) ? true : false, //No selected means this is the first tab! WOOHOO
                QuerystringValue: "",
                Editing: true
            };

            //Add tab
            $scope.tabstrip.tabs.push(newBlankTab);

            //Remove current editing state for the current tab
            if ($scope.tabstrip.selectedTab) {
                $scope.tabstrip.selectedTab.Editing = false;
            }

            //Make the new tab the default
            $scope.tabstrip.selectedTab = newBlankTab;
        }

        $scope.onSelectTab = function (tab, index) {
            //Remove selection from other tabs
            for (var i = 0; i < $scope.tabstrip.tabs.length; i++) {
                    $scope.tabstrip.tabs[i].Editing = false;
            }

            //Select this magical tab
            tab.Editing = true;
            $scope.tabstrip.selectedTab = tab;
            $scope.tabstrip.selectedIndex = index;
        }

        $scope.onChangeSelected = function () {
            //Remove selection from other tabs
            for (var i = 0; i < $scope.tabstrip.tabs.length; i++) {
                if ($scope.tabstrip.tabs[i].Title != $scope.tabstrip.selectedTab.Title) {
                    $scope.tabstrip.tabs[i].Selected = false;
                }
            }
        }

        $scope.onDeleteTab = function () {
            var index = $scope.tabstrip.selectedIndex;
            $scope.tabstrip.tabs.splice(index, 1);

            //Select the first item
            $scope.onSelectTab($scope.tabstrip.tabs[0], 0);
        }

        propertyService.get()
            .then(function (data) {
                if (data) {
                    $scope.properties = propertyService.toAssociativeArray(data.Items);
                    var savedTabs = JSON.parse($scope.properties.SerializedTabs.PropertyValue);

                    if (savedTabs.length > 0) {
                        for (var i = 0; i < savedTabs.length; i++) {
                            savedTabs[i].Editing = (i == 0) ? true : false; //Set the first one to be edit
                            $scope.tabstrip.tabs.push(savedTabs[i]);
                        }
                    }
                    else {
                        //Create the defaults
                        $scope.onAddTab("Tab1");
                        $scope.onAddTab("Tab2");
                        $scope.onAddTab("Tab3");
                    }

                    $scope.onSelectTab($scope.tabstrip.tabs[0], 0)
                }
            },
            function (data) {
                $scope.feedback.showError = true;
                if (data)
                    $scope.feedback.errorMessage = data.Detail;
            })
            .then(function () {
                $scope.feedback.savingHandlers.push(function () {
                    var tabsJSON = JSON.stringify($scope.tabstrip.tabs);
                    $scope.properties.SerializedTabs.PropertyValue = tabsJSON;
                });
            })
            .finally(function () {
                $scope.feedback.showLoadingIndicator = false;
            });
    }]);
})(jQuery);