// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Core.Crm.Discovery.OrganizationEndpoint
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
  public class OrganizationEndpoint
  {
    private int authenticationTypeField;
    private string crmMetadataServiceUrlField;
    private string crmServiceUrlField;
    private string webApplicationUrlField;

    public int AuthenticationType
    {
      get => this.authenticationTypeField;
      set => this.authenticationTypeField = value;
    }

    public string CrmMetadataServiceUrl
    {
      get => this.crmMetadataServiceUrlField;
      set => this.crmMetadataServiceUrlField = value;
    }

    public string CrmServiceUrl
    {
      get => this.crmServiceUrlField;
      set => this.crmServiceUrlField = value;
    }

    public string WebApplicationUrl
    {
      get => this.webApplicationUrlField;
      set => this.webApplicationUrlField = value;
    }
  }
}
