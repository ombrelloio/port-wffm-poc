// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Core.Crm.CrmDecimal
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
  [XmlType(Namespace = "http://schemas.microsoft.com/crm/2006/WebServices")]
  [DebuggerNonUserCode]
  [Serializable]
  public class CrmDecimal
  {
    private bool isNullField;
    private bool isNullFieldSpecified;
    private string formattedvalueField;
    private Decimal valueField;

    [XmlAttribute]
    public bool IsNull
    {
      get => this.isNullField;
      set => this.isNullField = value;
    }

    [XmlIgnore]
    public bool IsNullSpecified
    {
      get => this.isNullFieldSpecified;
      set => this.isNullFieldSpecified = value;
    }

    [XmlAttribute]
    public string formattedvalue
    {
      get => this.formattedvalueField;
      set => this.formattedvalueField = value;
    }

    [XmlText]
    public Decimal Value
    {
      get => this.valueField;
      set => this.valueField = value;
    }
  }
}
