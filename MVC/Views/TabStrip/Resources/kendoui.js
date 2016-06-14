$(document).ready(function () {
    if (!$("body").hasClass(".sfPageEditor")) {

        $(".sfs-tabstrip").each(function () {
            var wrapper = $(this);
            //Find configurator
            var config = wrapper.find(".sfs-tabstrip-configurator[data-type='kendo']");

            if (config.length > 0) {
                ////Find content panels
                var panelwrapper = wrapper.find(".tab-panels .tab-pane:first-child");

                //Remove the wrapper nodes, kendo can't use it
                config.unwrap();
                panelwrapper.unwrap();

                //Initalize the tabstrip on sfs-tabstrip
                $(this).kendoTabStrip({
                    animation: false,
                    tabPosition: config.data("tab-position")
                });

                var className = config.data("classname");
                if(className !== ""){
                    wrapper.addClass(className);
                }

                //Show after initalized
                wrapper.removeClass("loading");
            }
        });
    }
});