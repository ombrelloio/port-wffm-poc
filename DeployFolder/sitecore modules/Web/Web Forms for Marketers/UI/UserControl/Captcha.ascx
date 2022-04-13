<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Captcha.ascx.cs" Inherits="Sitecore.Form.UI.UserControls.Captcha" %>
<%@ Register TagPrefix="wfmcore" Namespace="Sitecore.Form.Web.UI.Controls" Assembly="Sitecore.Forms.Core" %>
<%@ Register TagPrefix="wfmcustom" Namespace="Sitecore.Form.Web.UI.Controls" Assembly="Sitecore.Forms.Custom" %>

<div class="scfCaptchTop">
    <asp:Panel ID="captchaCodeBorder" CssClass="scfCaptchaBorder" runat="server">
        <span class="scfCaptchaLabel">&nbsp;</span>
        <asp:Panel ID="Panel1" CssClass="scfCaptchaGeneralPanel" runat="server">
            <wfmcustom:Label ID="captchaCodeText" runat="server" CssClass="scfCaptchaLabel" AssociatedControlID="captchaCode"
                Text=" " />
            <asp:Panel ID="captchaCodePanel" CssClass="scfCaptchaLimitGeneralPanel" runat="server">
                <wfmcore:Captcha ID="captchaCode" runat="server">
                    <CaptchaImage CaptchaBackgroundNoise="None" CaptchaFontWarping="None" CaptchaLineNoise="None" />
                    <CaptchaPlayButton />
                    <CaptchaRefreshButton />
                </wfmcore:Captcha>
            </asp:Panel>
        </asp:Panel>
    </asp:Panel>
</div>
<div>
    <asp:Panel ID="captchaTextBorder" CssClass="scfCaptchaBorder" runat="server">
        <span class="scfCaptchaLabel">&nbsp;</span>
        <asp:Panel ID="captchaTextPanel" CssClass="scfCaptchaGeneralPanel" runat="server">
            <wfmcustom:Label ID="captchaTextTitle" runat="server" CssClass="scfCaptchaLabelText"
                AssociatedControlID="captchaText" Text=" " />
            <asp:Panel ID="captchLimitTextPanel" CssClass="scfCaptchaLimitGeneralPanel" runat="server">
                <asp:Panel ID="captchStrongTextPanel" CssClass="scfCaptchStrongTextPanel" runat="server">
                    <asp:TextBox ID="captchaText" CssClass="scfCaptchaTextBox scWfmPassword" runat="server" />
                    <asp:Label ID="captchaTextHelp" CssClass="scfCaptchaUsefulInfo" runat="server" Style="display:none"/>
                </asp:Panel>
            </asp:Panel>
        </asp:Panel>
    </asp:Panel>
</div>
