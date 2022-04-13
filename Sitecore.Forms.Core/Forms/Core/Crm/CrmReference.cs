// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Core.Crm.CrmReference
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
  [XmlInclude(typeof (Owner))]
  [XmlInclude(typeof (Lookup))]
  [XmlInclude(typeof (Customer))]
  [GeneratedCode("wsdl", "2.0.50727.3038")]
  [DebuggerStepThrough]
  [DesignerCategory("code")]
  [XmlType(Namespace = "http://schemas.microsoft.com/crm/2006/WebServices")]
  [DebuggerNonUserCode]
  [Serializable]
  public abstract class CrmReference
  {
    private bool isNullField;
    private bool isNullFieldSpecified;
    private string nameField;
    private string typeField;
    private int dscField;
    private bool dscFieldSpecified;
    private Guid valueField;

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
    public string name
    {
      get => this.nameField;
      set => this.nameField = value;
    }

    [XmlAttribute]
    public string type
    {
      get => this.typeField;
      set => this.typeField = value;
    }

    [XmlAttribute]
    public int dsc
    {
      get => this.dscField;
      set => this.dscField = value;
    }

    [XmlIgnore]
    public bool dscSpecified
    {
      get => this.dscFieldSpecified;
      set => this.dscFieldSpecified = value;
    }

    [XmlText]
    public Guid Value
    {
      get => this.valueField;
      set => this.valueField = value;
    }
  }
}
