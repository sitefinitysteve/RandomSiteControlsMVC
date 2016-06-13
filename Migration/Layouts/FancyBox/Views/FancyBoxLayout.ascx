<%@ Control Language="C#" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="sf" Namespace="Telerik.Sitefinity.Web.UI.PublicControls.BrowseAndEdit" Assembly="Telerik.Sitefinity" %>
<%@ Register TagPrefix="sitefinity" Namespace="Telerik.Sitefinity.Web.UI" Assembly="Telerik.Sitefinity" %>

<style>
    .sfPageEditor .fancyboxWrapper{
        display: block !important;
    }
</style>

<div class="fancyboxWrapper" style="display:none">
    <!-- FancyBox Opener -->
    <div runat="server" class="sf_cols fancyOuterlayout">
        <div runat="server" class="sf_colsOut sf_1cols_1_100" data-placeholder-label="Popup Link">
            <div runat="server" class="sf_colsIn sf_1cols_1in_100">

            </div>
        </div>
    </div>
    <!-- FancyBox Content -->
    <div class="fancyBoxContainer">
        <div class="fancyPopupTitleContainer" style="visibility:hidden">
            <h3 class="fancyTitle">
                <span>Popup Container</span>
            </h3>
        </div>
        <div id="sfsFancyBoxInlineItem" class="sfsFancyBoxInlineItem">
            <div runat="server" class="sf_cols">
                <div runat="server" class="sf_colsOut sf_1cols_1_100" data-placeholder-label="Popup Content Zone">
                    <div runat="server" class="sf_colsIn sf_1cols_1in_100">

                    </div>
                </div>
            </div>
        </div>
    </div>
</div>