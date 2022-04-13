// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Core.Crm.MergeRequest
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
  public class MergeRequest : Request
  {
    private TargetMerge targetField;
    private Guid subordinateIdField;
    private BusinessEntity updateContentField;
    private bool performParentingChecksField;

    public TargetMerge Target
    {
      get => this.targetField;
      set => this.targetField = value;
    }

    public Guid SubordinateId
    {
      get => this.subordinateIdField;
      set => this.subordinateIdField = value;
    }

    public BusinessEntity UpdateContent
    {
      get => this.updateContentField;
      set => this.updateContentField = value;
    }

    public bool PerformParentingChecks
    {
      get => this.performParentingChecksField;
      set => this.performParentingChecksField = value;
    }
  }
}
