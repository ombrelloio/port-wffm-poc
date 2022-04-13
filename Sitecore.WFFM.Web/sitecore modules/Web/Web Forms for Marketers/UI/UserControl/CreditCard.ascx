<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CreditCard.ascx.cs"
    Inherits="Sitecore.Form.UI.UserControls.CreditCard" %>
<%@ Register TagPrefix="wfmcustom" Namespace="Sitecore.Form.Web.UI.Controls" Assembly="Sitecore.Forms.Custom" %>

<div>
    <asp:Panel ID="cardTypeBorder" CssClass="scfCreditCardBorder" runat="server">
        <wfmcustom:Label ID="cardTypeTitle" runat="server" CssClass="scfCreditCardLabel" AssociatedControlID="cardType"
            Text="Card type" />
            
        <asp:Panel ID="creditCardPanel" CssClass="scfCreditCardGeneralPanel" runat="server">
            <asp:DropDownList CssClass="scfCreditCardType cardType.1" ID="cardType" runat="server" >
            
            </asp:DropDownList>
            <asp:Label ID="cardTypeHelp" CssClass="scfCreditCardTextUsefulInfo" runat="server" Style="display:none"/>
        </asp:Panel>
            
    </asp:Panel>
</div>
<div>
    <asp:Panel ID="creditCard" CssClass="scfCreditCardBorder" runat="server">
        <wfmcustom:Label ID="cardNumberTitle" runat="server" CssClass="scfCreditCardLabel" AssociatedControlID="cardNumber"
            Text="Card number" />
        <asp:Panel ID="cardNumberPanel" CssClass="scfCreditCardGeneralPanel" runat="server">
            <asp:TextBox ID="cardNumber" CssClass="scfCreditCardTextBox scWfmPassword cardNumber.1" runat="server" />
            <asp:Label ID="cardNumberHelp" CssClass="scfCreditCardTextUsefulInfo" Style="display:none"
                runat="server" />
        </asp:Panel>
    </asp:Panel>
</div>
