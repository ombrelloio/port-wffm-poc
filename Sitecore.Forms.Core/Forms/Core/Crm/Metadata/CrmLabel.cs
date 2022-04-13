// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Core.Crm.Metadata.CrmLabel
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
  public class CrmLabel
  {
    private LocLabel[] locLabelsField;
    private LocLabel userLocLabelField;

    [XmlArrayItem(IsNullable = false)]
    public LocLabel[] LocLabels
    {
      get => this.locLabelsField;
      set => this.locLabelsField = value;
    }

    public LocLabel UserLocLabel
    {
      get => this.userLocLabelField;
      set => this.userLocLabelField = value;
    }
  }
}
