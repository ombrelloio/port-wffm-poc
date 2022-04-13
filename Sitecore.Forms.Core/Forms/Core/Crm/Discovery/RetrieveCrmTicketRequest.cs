// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Core.Crm.Discovery.RetrieveCrmTicketRequest
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Sitecore.Forms.Core.Crm.Discovery
{
  [GeneratedCode("wsdl", "2.0.50727.3038")]
  [DebuggerStepThrough]
  [DesignerCategory("code")]
  [XmlType(Namespace = "http://schemas.microsoft.com/crm/2007/CrmDiscoveryService")]
  [DebuggerNonUserCode]
  [Serializable]
  public class RetrieveCrmTicketRequest : Request
  {
    private string organizationNameField;
    private string passportTicketField;
    private string passwordField;
    private string userIdField;

    public string OrganizationName
    {
      get => this.organizationNameField;
      set => this.organizationNameField = value;
    }

    public string PassportTicket
    {
      get => this.passportTicketField;
      set => this.passportTicketField = value;
    }

    public string Password
    {
      get => this.passwordField;
      set => this.passwordField = value;
    }

    public string UserId
    {
      get => this.userIdField;
      set => this.userIdField = value;
    }
  }
}
