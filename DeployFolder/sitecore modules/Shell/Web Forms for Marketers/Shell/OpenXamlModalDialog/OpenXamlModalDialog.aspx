<%@ Page Language="C#" AutoEventWireup="true" Inherits="Sitecore.Shell.Web.UI.SecurePage" %>
<%@ Register Assembly="Sitecore.Kernel" Namespace="Sitecore.Web.UI.HtmlControls" TagPrefix="sc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">

<html>
<head runat="server">
  <title>Sitecore</title>
</head>
<body style="background:transparent; height: 100%">
  <form id="OpenApplicationForm" runat="server">

    <sc:CodeBeside ID="ApplicationForm" runat="server" Type="Sitecore.Forms.Shell.UI.Dialogs.OpenXamlModalDialog,Sitecore.Forms.Core" />

    <sc:Frame ID="ApplicationFrame" runat="server" />

  </form>
</body>
</html>