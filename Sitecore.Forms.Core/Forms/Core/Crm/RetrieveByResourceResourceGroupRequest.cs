// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Core.Crm.RetrieveByResourceResourceGroupRequest
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
  [XmlType(Namespace = "http://schemas.microsoft.com/crm/2007/WebServices")]
  [DebuggerNonUserCode]
  [Serializable]
  public class RetrieveByResourceResourceGroupRequest : Request
  {
    private Guid resourceIdField;
    private QueryBase queryField;
    private bool returnDynamicEntitiesField;

    public Guid ResourceId
    {
      get => this.resourceIdField;
      set => this.resourceIdField = value;
    }

    public QueryBase Query
    {
      get => this.queryField;
      set => this.queryField = value;
    }

    [XmlAttribute]
    public bool ReturnDynamicEntities
    {
      get => this.returnDynamicEntitiesField;
      set => this.returnDynamicEntitiesField = value;
    }
  }
}
