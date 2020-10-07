"use strict";

$(document).ready(function () {
    $(".print-button").each(function () {
        var button = $(this);
        button.on("click", function () {
            var elementId = $(this).data("elementid");
            var filename = $(this).data("filename");
            var mode = $(this).data("mode");
            var papersize = $(this).data("papersize");
            var multipage = $(this).data("multipage");
            var landscape = $(this).data("landscape");

            var mt = $(this).data("margintop");
            var ml = $(this).data("marginleft");
            var mb = $(this).data("marginbottom");
            var mr = $(this).data("marginright");

            console.log("Printing", elementId, filename, mode, papersize);

            var selector = null;

            if (mode == "Container") {
                selector = $(this).closest(".sf_colsIn");

                //Hide the print button, dont want it in here
                button.hide();
            } else {
                selector = $(elementId);
            }

            if (papersize == "Auto") {
                papersize = "auto";
            }

            //add printable class
            selector.addClass("exporting-print-job");

            var options = {
                paperSize: papersize,
                margin: { left: ml, top: mt, right: mr, bottom: mb },
                multipage: multipage,
                landscape: landscape
            };

            console.log(options);

            kendo.drawing.drawDOM(selector).then(function (group) {
                // Render the result as a PDF file
                return kendo.drawing.exportPDF(group, options);
            }).done(function (data) {
                // Save the PDF file
                kendo.saveAs({
                    dataURI: data,
                    fileName: filename + ".pdf",
                    proxyURL: "/RestApi/utility/proxy/save"
                });

                button.show();
                selector.removeClass("exporting-print-job");
            });
        });
    });
});

