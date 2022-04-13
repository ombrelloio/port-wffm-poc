// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Core.Crm.CrmAuthenticationToken
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Web.Services.Protocols;
using System.Xml.Serialization;

namespace Sitecore.Forms.Core.Crm
{
  [GeneratedCode("wsdl", "2.0.50727.3038")]
  [DebuggerStepThrough]
  [DesignerCategory("code")]
  [XmlType(Namespace = "http://schemas.microsoft.com/crm/2007/CoreTypes")]
  [XmlRoot(IsNullable = true, Namespace = "http://schemas.microsoft.com/crm/2007/WebServices")]
  [DebuggerNonUserCode]
  [Serializable]
  public class CrmAuthenticationToken : SoapHeader
  {
    private int authenticationTypeField;
    private string crmTicketField;
    private string organizationNameField;
    private Guid callerIdField;

    public int AuthenticationType
    {
      get => this.authenticationTypeField;
      set => this.authenticationTypeField = value;
    }

    public string CrmTicket
    {
      get => this.crmTicketField;
      set => this.crmTicketField = value;
    }

    public string OrganizationName
    {
      get => this.organizationNameField;
      set => this.organizationNameField = value;
    }

    public Guid CallerId
    {
      get => this.callerIdField;
      set => this.callerIdField = value;
    }
  }
}
