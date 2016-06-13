<%@ Control Language="C#" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="sf" Namespace="Telerik.Sitefinity.Web.UI.PublicControls.BrowseAndEdit" Assembly="Telerik.Sitefinity" %>
<%@ Register TagPrefix="sitefinity" Namespace="Telerik.Sitefinity.Web.UI" Assembly="Telerik.Sitefinity" %>

<sitefinity:ResourceLinks runat="server" UseEmbeddedThemes="true" Theme="Basic">
    <sitefinity:ResourceFile JavaScriptLibrary="JQuery"/>
    <sitefinity:ResourceFile Name="RandomSiteControls.Common.common.min.js" AssemblyInfo="RandomSiteControls.FancyBox.FancyBoxLayout, RandomSiteControls" Static="True" />
    <sitefinity:ResourceFile Name="RandomSiteControls.Common.Designer.min.css" AssemblyInfo="RandomSiteControls.FancyBox.FancyBoxLayout, RandomSiteControls" Static="True" />
    <sitefinity:ResourceFile JavaScriptLibrary="JQueryFancyBox"/>
    <%--<sitefinity:ResourceFile Name="RandomSiteControls.FancyBox.Resources.jquery.fancybox-1.3.8.min.js" AssemblyInfo="RandomSiteControls.FancyBox.FancyBoxLayout, RandomSiteControls" Static="True" />--%>
    <sitefinity:ResourceFile Name="Telerik.Sitefinity.Resources.Themes.Basic.Styles.fancybox.css" Static="true" />
    <sitefinity:ResourceFile Name="RandomSiteControls.FancyBox.Resources.fancybox-steve.min.css" AssemblyInfo="RandomSiteControls.FancyBox.FancyBoxLayout, RandomSiteControls" Static="True" />
    <sitefinity:ResourceFile Name="RandomSiteControls.FancyBox.Resources.fancybox-steve-layout.min.js" AssemblyInfo="RandomSiteControls.FancyBox.FancyBoxLayout, RandomSiteControls" Static="True" />
</sitefinity:ResourceLinks>

<div class="fancyboxWrapper">
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