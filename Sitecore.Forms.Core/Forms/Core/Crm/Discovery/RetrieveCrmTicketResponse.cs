// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Core.Crm.Discovery.RetrieveCrmTicketResponse
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
  public class RetrieveCrmTicketResponse : Response
  {
    private string crmTicketField;
    private string expirationDateField;
    private OrganizationDetail organizationDetailField;

    public string CrmTicket
    {
      get => this.crmTicketField;
      set => this.crmTicketField = value;
    }

    public string ExpirationDate
    {
      get => this.expirationDateField;
      set => this.expirationDateField = value;
    }

    public OrganizationDetail OrganizationDetail
    {
      get => this.organizationDetailField;
      set => this.organizationDetailField = value;
    }
  }
}
