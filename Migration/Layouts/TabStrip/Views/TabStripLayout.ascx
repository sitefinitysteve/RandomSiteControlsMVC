<%@ Control Language="C#" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Assembly="Telerik.Sitefinity" Namespace="Telerik.Sitefinity.Web.UI" TagPrefix="sitefinity" %>

<sitefinity:ResourceLinks runat="server" UseEmbeddedThemes="true" Theme="Basic">
    <sitefinity:ResourceFile JavaScriptLibrary="JQuery"/>
    <sitefinity:ResourceFile Name="RandomSiteControls.Common.common.min.js" AssemblyInfo="RandomSiteControls.TabStrip.TabStripLayout, RandomSiteControls" Static="True" />
    <sitefinity:ResourceFile Name="RandomSiteControls.Common.Designer.min.css" AssemblyInfo="RandomSiteControls.TabStrip.TabStripLayout, RandomSiteControls" Static="True" />
    <sitefinity:ResourceFile Name="RandomSiteControls.TabStrip.Resources.TabStrip.min.css" AssemblyInfo="RandomSiteControls.TabStrip.TabStripLayout, RandomSiteControls" Static="True" />
    <sitefinity:ResourceFile Name="RandomSiteControls.TabStrip.Resources.TabStrip.min.js" AssemblyInfo="RandomSiteControls.TabStrip.TabStripLayout, RandomSiteControls" Static="True" />
</sitefinity:ResourceLinks>


<div class="tabStrip tabStripContainer horizontal">
    <div runat="server" class="sf_cols tabConfigurator">
        <div runat="server" class="sf_colsOut sf_1cols_1_100 tabStripOuter" data-placeholder-label="Drop in the tabstrip widget">
            <div runat="server" class="sf_colsIn sf_1cols_1in_100 tabStripInner tabStripInner">

            </div>
        </div>
    </div>

    <div class="tabDesignContainer" data-loaded="false">
        <div runat="server" class="sf_cols tab tabContainer tabRoot">
            <div runat="server" class="sf_colsOut sf_1cols_1_100 tabChildOuter" data-placeholder-label="Each layout control in here represents a tab">
                <div runat="server" class="sf_colsIn sf_1cols_1in_100 multiPageInner tabChildInner">

                </div>
            </div>
        </div>
    </div>
</div>
