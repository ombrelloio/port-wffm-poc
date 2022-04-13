// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Core.Crm.RollupRequest
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
  public class RollupRequest : Request
  {
    private TargetRollup targetField;
    private RollupType rollupTypeField;
    private bool returnDynamicEntitiesField;

    public TargetRollup Target
    {
      get => this.targetField;
      set => this.targetField = value;
    }

    public RollupType RollupType
    {
      get => this.rollupTypeField;
      set => this.rollupTypeField = value;
    }

    [XmlAttribute]
    public bool ReturnDynamicEntities
    {
      get => this.returnDynamicEntitiesField;
      set => this.returnDynamicEntitiesField = value;
    }
  }
}
