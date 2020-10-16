$(document).ready(function () {
    $(".sfs-document-tree").each(function () {
        var root = $(this).find(".root").first();
        var mode = $(this).data("mode");
        
        if (mode == "treeview") {
            try {
             /*   var treeview = root.kendoTreeView({
                    animation: false
                }).data("kendoTreeView");

                treeview.expand(".k-item");*/
            } catch{
                console.error("Likely multiple instances of jQuery are preventing kendoUI from loading, can't initalize treeview")
            }
        }

        $(this).removeAttr("style");
    });
});