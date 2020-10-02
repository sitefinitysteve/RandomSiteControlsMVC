"use strict";

$(document).ready(function () {
    $(".sfs-document-tree").each(function () {
        var mode = $(this).data("mode");
        var root = $(this).find(".root");

        if (mode === "TreeView") {
            renderDocumentTreeAsTree(root);
        } else {
            renderDocumentTreeAsPanel(root);
        }

        $(this).removeAttr("style");
    });

    function renderDocumentTreeAsTree(root) {
        var treeview = root.kendoTreeView({
            animation: false
        }).data("kendoTreeView");

        treeview.expand(".k-item");
    }

    function renderDocumentTreeAsPanel(root) {
        var panelbar = root.kendoPanelBar({
            animation: false
        }).data("kendoPanelBar");

        panelbar.expand(".k-item");
    }
});

