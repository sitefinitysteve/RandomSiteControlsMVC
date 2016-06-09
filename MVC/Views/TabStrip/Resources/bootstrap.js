$(document).ready(function () {
    if (!$("body").hasClass(".sfPageEditor")) {
        var panelCounter = 0;
        $(".sfs-tabstrip").each(function () {
            var wrapper = $(this);
            //Find configurator
            var config = wrapper.find(".sfs-tabstrip-configurator[data-type='bootstrap']");
            if (config.length > 0) {
                //Get tabs
                var tabs = config.find("li[role='presentation']")

                //Find content zone
                var panelwrapper = wrapper.find(".tab-panels");

                //Find panels
                var panels = panelwrapper.find(".sfs-tabitem");

                //Loop through each tab
                tabs.each(function (index) {
                    if (index <= panels.length - 1) { //Can only find matching panels if they exist
                        var tab = $(this);
                        var tabId = "sfs-tab-panel-" + panelCounter;

                        //Give the tab the anchorlink
                        var link = $(tab.children()[0]);
                        link.attr("href", "#" + tabId);

                        //Give the panel the ID to link to
                        var panel = $(panels[index]);
                        panel.attr("id", tabId);

                        if (tab.hasClass("active")) {
                            panel.addClass("active");
                        }
                    }

                    panelCounter = panelCounter + 1;
                });

                //Show after initalized
                wrapper.removeClass("loading");
            }
        });
    }
});