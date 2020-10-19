var monacoTries = 0;

$(document).ready(function () {
    if (typeof monaco === "undefined") {
        addMonacoStyleSheet(window.location.origin + "/adminapp/assets/js/monaco-editor/vs/editor/editor.main.css", "vs/editor/editor.main");
        addMonacoScript(window.location.origin + "/adminapp/assets/js/monaco-editor/vs/loader.js");
        addMonacoScript(window.location.origin + "/adminapp/assets/js/monaco-editor/vs/editor/editor.main.nls.js");
        addMonacoScript(window.location.origin + "/adminapp/assets/js/monaco-editor/vs/editor/editor.main.js", "vs/editor/editor.main");
        
        
    }
    
    waitForMonaco();
    require(['vs/editor/editor.main'], function () {
        console.log(`editor loaded`);
        debugger;
    });
});

function waitForMonaco() {
    monacoTries++;
    
    if (typeof monaco !== "undefined") {
        debugger;
        //variable exists, do what you want
        //Resize editor zone
        resizeMonacoEditorWindow();
        debugger;
        contentHtmlEditor = monaco.editor.create(document.getElementById('content-html-container'), {
            language: 'html',
            autoIndent: true
        });

        if ($scope.properties) {
            //contentHtmlEditor.setValue($scope.properties.HtmlContent.PropertyValue);
        }

        $(window).resize(function () {
            resizeMonacoEditorWindow();
        });
    }
    else {
        if (monacoTries < 50) {
            setTimeout(waitForMonaco, 250);
        } else {
            console.log("Cancelling looking for monaco");
        }
    }
}

function addMonacoScript(src, dataname) {
    console.log("Adding Script", src);
    var s = document.createElement('script');
    s.setAttribute('src', src);
    if (dataname) {
        s.setAttribute('data-name', dataname);
    }
    document.body.appendChild(s);
}

function addMonacoStyleSheet(src, dataname) {
    console.log("Adding Stylesheet", src);
    var s = document.createElement('link');
    s.setAttribute('href', src);
    s.setAttribute('rel', 'stylesheet');
    if (dataname) {
        s.setAttribute('data-name', dataname);
    }
    document.head.appendChild(s);
}

function resizeMonacoEditorWindow() {
   /* //Resize editor zone
    var height = $("#viewsPlaceholder").closest(".modal-body").height();
    $("#content-html-container").height(height);
    console.log("Resizing editor: " + height);*/
}