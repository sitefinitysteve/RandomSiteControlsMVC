<%@ Control Language="C#" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Assembly="Telerik.Sitefinity" Namespace="Telerik.Sitefinity.Web.UI" TagPrefix="sitefinity" %>

<sitefinity:ResourceLinks runat="server">
    <sitefinity:ResourceFile JavaScriptLibrary="JQuery"/>
    <sitefinity:ResourceFile Name="RandomSiteControls.Common.common.min.js" AssemblyInfo="RandomSiteControls.TabStrip.TabStripLayout, RandomSiteControls" Static="True" />
    <sitefinity:ResourceFile Name="RandomSiteControls.Common.Designer.min.css" AssemblyInfo="RandomSiteControls.TabStrip.TabStripLayout, RandomSiteControls" Static="True" />
    <sitefinity:ResourceFile Name="RandomSiteControls.TabStrip.Resources.TabStrip.min.css" AssemblyInfo="RandomSiteControls.TabStrip.TabStripLayout, RandomSiteControls" Static="True" />
    <sitefinity:ResourceFile Name="RandomSiteControls.TabStrip.Resources.TabStrip.min.js" AssemblyInfo="RandomSiteControls.TabStrip.TabStripLayout, RandomSiteControls" Static="True" />
</sitefinity:ResourceLinks>


<div class="tabStrip tabStripContainer vertical">
    <div runat="server" class="sf_cols">
        <div runat="server" class="sf_colsOut sf_2cols_1_25 tabConfigurator tabStripOuter">
            <div runat="server" class="sf_colsIn sf_2cols_1in_25 tabStripInner tabStripInner">
            </div>
        </div>
        <div runat="server" class="sf_colsOut sf_2cols_2_75 tabDesignContainer tab tabContainer tabRoot tabChildOuter" data-loaded="false">
            <div runat="server" class="sf_colsIn sf_2cols_2in_75 multiPageInner tabChildInner">
            </div>
        </div>
    </div>
</div>
