// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Core.Crm.TargetRelatedDynamic
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
  public class TargetRelatedDynamic : TargetRelated
  {
    private string entity1NameField;
    private Guid entity1IdField;
    private string entity2NameField;
    private Guid entity2IdField;

    public string Entity1Name
    {
      get => this.entity1NameField;
      set => this.entity1NameField = value;
    }

    public Guid Entity1Id
    {
      get => this.entity1IdField;
      set => this.entity1IdField = value;
    }

    public string Entity2Name
    {
      get => this.entity2NameField;
      set => this.entity2NameField = value;
    }

    public Guid Entity2Id
    {
      get => this.entity2IdField;
      set => this.entity2IdField = value;
    }
  }
}
