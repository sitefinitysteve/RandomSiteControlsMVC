var require = { paths: { 'vs': '/adminapp/assets/js/monaco-editor/vs/' } };

(function ($) {
    angular.module('designer').controller('SimpleCtrl', ['$scope', 'propertyService', function ($scope, propertyService) {
        //Fill the screen
        $('.sf-backend-wrp').addClass('modal-fluid');;

        var contentHtmlEditor;

        $scope.waitForMonaco = function () {
            if (typeof monaco !== "undefined") {
                //variable exists, do what you want
                //Resize editor zone
                $scope.resizeMonacoEditorWindow();

                contentHtmlEditor = monaco.editor.create(document.getElementById('content-html-container'), {
                    language: 'html',
                    autoIndent: true
                });

                if ($scope.properties) {
                    contentHtmlEditor.setValue($scope.properties.HtmlContent.PropertyValue);
                }

                $(window).resize(function () {
                    $scope.resizeMonacoEditorWindow();
                });
            }
            else {
                setTimeout($scope.waitForMonaco, 250);
            }
        }

        $scope.resizeMonacoEditorWindow = function () {
            //Resize editor zone
            var height = $("#viewsPlaceholder").closest(".modal-body").height();
            $("#content-html-container").height(height);
            console.log("Resizing editor: " + height);
        }

        if (typeof monaco === "undefined") {
            addMonacoStyleSheet("/adminapp/assets/js/monaco-editor/vs/editor/editor.main.css", "vs/editor/editor.main");
            addMonacoScript("/adminapp/assets/js/monaco-editor/vs/editor/editor.main.js");
            addMonacoScript("/adminapp/assets/js/monaco-editor/vs/loader.js");
        }

        $scope.waitForMonaco();

        propertyService.get()
            .then(function (data) {
                if (data) {
                    $scope.properties = propertyService.toAssociativeArray(data.Items);

                    if (contentHtmlEditor) {
                        contentHtmlEditor.setValue($scope.properties.HtmlContent.PropertyValue);
                    }
                }
            },
                function (data) {
                    $scope.feedback.showError = true;
                    if (data)
                        $scope.feedback.errorMessage = data.Detail;
                })
            .then(function () {
                $scope.feedback.savingHandlers.push(function () {
                    $scope.properties.HtmlContent.PropertyValue = contentHtmlEditor.getValue();
                });
            })
            .finally(function () {
                $scope.feedback.showLoadingIndicator = false;
            });
    }]);
})(jQuery);

function addMonacoScript(src) {
    var s = document.createElement('script');
    s.setAttribute('src', src);
    document.body.appendChild(s);
}

function addMonacoStyleSheet(src, dataname) {
    var s = document.createElement('link');
    s.setAttribute('href', src);
    s.setAttribute('rel', 'stylesheet');
    if (dataname) {
        s.setAttribute('data-name', dataname);
    }
    document.head.appendChild(s);
}