// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Core.Crm.ProposalParty
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Sitecore.Forms.Core.Crm
{
  [GeneratedCode("wsdl", "2.0.50727.3038")]
  [DebuggerStepThrough]
  [DesignerCategory("code")]
  [XmlType(Namespace = "http://schemas.microsoft.com/crm/2006/Scheduling")]
  [DebuggerNonUserCode]
  [Serializable]
  public class ProposalParty
  {
    private Guid resourceIdField;
    private Guid resourceSpecIdField;
    private string displayNameField;
    private string entityNameField;
    private double effortRequiredField;

    public Guid ResourceId
    {
      get => this.resourceIdField;
      set => this.resourceIdField = value;
    }

    public Guid ResourceSpecId
    {
      get => this.resourceSpecIdField;
      set => this.resourceSpecIdField = value;
    }

    public string DisplayName
    {
      get => this.displayNameField;
      set => this.displayNameField = value;
    }

    public string EntityName
    {
      get => this.entityNameField;
      set => this.entityNameField = value;
    }

    public double EffortRequired
    {
      get => this.effortRequiredField;
      set => this.effortRequiredField = value;
    }
  }
}
