<%@ Control Language="C#" %>

<asp:Panel ID="liveMode" runat="server">
    <div style="background-color:red; color: #FFF; padding: 20px;">
        Problem with tabstrip, please contact your site admin to edit this page
    </div>
</asp:Panel>
<asp:Panel ID="designMode" runat="server">
    <h3>DEPRECIATED TABSTRIP</h3>
    <div><asp:Label ID="tabStripLabel" runat="server" ForeColor="Red" /></div>
    <div>This is the tab content to migreate to the new tabstrip, DONT FORGET TO USE THE NEW LAYOUT AS WELL!</div>
</asp:Panel>