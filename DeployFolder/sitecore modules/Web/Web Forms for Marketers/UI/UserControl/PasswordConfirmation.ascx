<%@ Control Language="C#" AutoEventWireup="true" Inherits="Sitecore.Form.UI.UserControls.PasswordConfirmation" %>
<%@ Register TagPrefix="wfmcustom" Namespace="Sitecore.Form.Web.UI.Controls" Assembly="Sitecore.Forms.Custom" %>
<div>
  <asp:Panel ID="passwordBorder" CssClass="scfConfirmPasswordBorder" runat="server">
    <wfmcustom:Label ID="passwordTitle" AssociatedControlID="password" runat="server" CssClass="scfConfirmPasswordLabel"
      Text="Password" />
    <asp:Panel ID="passwordPanel" CssClass="scfConfirmPasswordGeneralPanel" runat="server">
      <asp:TextBox TextMode="Password" MaxLength="256" AutoCompleteType="Disabled" CssClass="scfConfirmPasswordTextBox password.1"
        ID="password" runat="server">            
      </asp:TextBox>
      <asp:Label ID="passwordHelp" CssClass="scfConfirmPasswordUsefulInfo" Style="display:none"
        runat="server" />
    </asp:Panel>
  </asp:Panel>
</div>
<div>
  <asp:Panel ID="confirmationBorder" CssClass="scfConfirmPasswordBorder" runat="server">
    <wfmcustom:Label ID="confirmationTitle" runat="server" CssClass="scfConfirmPasswordLabel"
      AssociatedControlID="confirmation" Text="Confirmation" />
    <asp:Panel ID="confirmationPanel" CssClass="scfConfirmPasswordGeneralPanel" runat="server">
      <asp:TextBox ID="confirmation" TextMode="Password" MaxLength="256" CssClass="scfConfirmPasswordTextBox confirmation.1"
        runat="server" AutoCompleteType="Disabled" />
      <asp:Label ID="confimationHelp" CssClass="scfConfirmPasswordUsefulInfo" Style="display:none"
        runat="server" />
    </asp:Panel>
  </asp:Panel>
</div>
