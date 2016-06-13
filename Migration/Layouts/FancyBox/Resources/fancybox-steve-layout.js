//Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageFancyLoaded);


$(document).ready(function () {
    //Only apply fancybox if we're not in the designer
    if (sfs_IsInDesignMode() == false) {
        //Wrap the opener in an anchor tag
        $('.fancyOuterlayout').each(function () {
            $(this).wrap('<a class="fancyboxOpenLink" href="#sfsFancyBoxInlineItem0" />');
        });
         
        //Fix up ALL fancyboxes so the IDs are unique
        var i = 0;
        $('.fancyboxWrapper').each(function () {
            var newID = 'sfsFancyBoxInlineItem' + i++;
            $(this).find('a.fancyboxOpenLink:first').attr('href', '#' + newID); //Fix the Link
            $(this).find('.sfsFancyBoxInlineItem:first').attr('id', newID); //Fix the Opener Div
        });
         
        $(".fancyboxOpenLink").fancybox({
            'onStart': function (selectedArray, selectedIndex, selectedOpts) {
                // see https://code.google.com/p/fancybox/issues/detail?id=100
                // store iframe src in _src attribute for use next time fancybox is opened
                var video_id = $(selectedArray[selectedIndex]).attr('href');
                $(video_id + ' iframe').each(function () {
                    if (typeof $(this).attr('_src') === "undefined")
                        $(this).attr('_src', $(this).attr('src'));
                    else
                        $(this).attr('src', $(this).attr('_src'));
                });
            },
            'onComplete': function (selectedArray, selectedIndex, selectedOpts) {
                
            },
            'onClose': function (selectedArray, selectedIndex, selectedOpts) {
                
            },
            'onCleanup': function (selectedArray, selectedIndex, selectedOpts) {
                
            }
        }); //Fancybox the bits

        //Remove Headers
        $(".fancyPopupTitleContainer").remove();
    }
});
