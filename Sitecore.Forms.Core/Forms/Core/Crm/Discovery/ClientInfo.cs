// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Core.Crm.Discovery.ClientInfo
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
  public class ClientInfo
  {
    private ClientTypes clientTypeField;
    private string crmVersionField;
    private int languageCodeField;
    private string officeVersionField;
    private Guid organizationIdField;
    private string oSVersionField;
    private Guid[] patchIdsField;
    private Guid userIdField;

    public ClientTypes ClientType
    {
      get => this.clientTypeField;
      set => this.clientTypeField = value;
    }

    public string CrmVersion
    {
      get => this.crmVersionField;
      set => this.crmVersionField = value;
    }

    public int LanguageCode
    {
      get => this.languageCodeField;
      set => this.languageCodeField = value;
    }

    public string OfficeVersion
    {
      get => this.officeVersionField;
      set => this.officeVersionField = value;
    }

    public Guid OrganizationId
    {
      get => this.organizationIdField;
      set => this.organizationIdField = value;
    }

    public string OSVersion
    {
      get => this.oSVersionField;
      set => this.oSVersionField = value;
    }

    public Guid[] PatchIds
    {
      get => this.patchIdsField;
      set => this.patchIdsField = value;
    }

    public Guid UserId
    {
      get => this.userIdField;
      set => this.userIdField = value;
    }
  }
}
