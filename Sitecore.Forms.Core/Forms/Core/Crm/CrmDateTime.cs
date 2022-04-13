// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Core.Crm.CrmDateTime
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
  public class CrmDateTime
  {
    private bool isNullField;
    private bool isNullFieldSpecified;
    private string dateField;
    private string timeField;
    private string valueField;

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
    public string date
    {
      get => this.dateField;
      set => this.dateField = value;
    }

    [XmlAttribute]
    public string time
    {
      get => this.timeField;
      set => this.timeField = value;
    }

    [XmlText]
    public string Value
    {
      get => this.valueField;
      set => this.valueField = value;
    }
  }
}
