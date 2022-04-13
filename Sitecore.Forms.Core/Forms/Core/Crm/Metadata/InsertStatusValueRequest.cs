// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Core.Crm.Metadata.InsertStatusValueRequest
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Sitecore.Forms.Core.Crm.Metadata
{
  [GeneratedCode("wsdl", "2.0.50727.3038")]
  [DebuggerStepThrough]
  [DesignerCategory("code")]
  [XmlType(Namespace = "http://schemas.microsoft.com/crm/2007/WebServices")]
  [DebuggerNonUserCode]
  [Serializable]
  public class InsertStatusValueRequest : MetadataServiceRequest
  {
    private string attributeLogicalNameField;
    private string entityLogicalNameField;
    private CrmLabel labelField;
    private CrmNumber stateCodeField;
    private CrmNumber valueField;

    public string AttributeLogicalName
    {
      get => this.attributeLogicalNameField;
      set => this.attributeLogicalNameField = value;
    }

    public string EntityLogicalName
    {
      get => this.entityLogicalNameField;
      set => this.entityLogicalNameField = value;
    }

    public CrmLabel Label
    {
      get => this.labelField;
      set => this.labelField = value;
    }

    public CrmNumber StateCode
    {
      get => this.stateCodeField;
      set => this.stateCodeField = value;
    }

    public CrmNumber Value
    {
      get => this.valueField;
      set => this.valueField = value;
    }
  }
}
