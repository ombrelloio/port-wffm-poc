﻿// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Core.Crm.Discovery.OrganizationExtendedDetail
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
  public class OrganizationExtendedDetail
  {
    private OrganizationEndpoint[] endpointsField;
    private string friendlyNameField;
    private Guid organizationIdField;
    private string organizationNameField;

    public OrganizationEndpoint[] Endpoints
    {
      get => this.endpointsField;
      set => this.endpointsField = value;
    }

    public string FriendlyName
    {
      get => this.friendlyNameField;
      set => this.friendlyNameField = value;
    }

    public Guid OrganizationId
    {
      get => this.organizationIdField;
      set => this.organizationIdField = value;
    }

    public string OrganizationName
    {
      get => this.organizationNameField;
      set => this.organizationNameField = value;
    }
  }
}
