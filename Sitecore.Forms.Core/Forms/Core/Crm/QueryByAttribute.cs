// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Core.Crm.QueryByAttribute
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
  [XmlType(Namespace = "http://schemas.microsoft.com/crm/2006/Query")]
  [DebuggerNonUserCode]
  [Serializable]
  public class QueryByAttribute : QueryBase
  {
    private string[] attributesField;
    private object[] valuesField;
    private PagingInfo pageInfoField;
    private OrderExpression[] ordersField;

    [XmlArrayItem("Attribute", IsNullable = false)]
    public string[] Attributes
    {
      get => this.attributesField;
      set => this.attributesField = value;
    }

    [XmlArrayItem("Value")]
    public object[] Values
    {
      get => this.valuesField;
      set => this.valuesField = value;
    }

    public PagingInfo PageInfo
    {
      get => this.pageInfoField;
      set => this.pageInfoField = value;
    }

    [XmlArrayItem("Order", IsNullable = false)]
    public OrderExpression[] Orders
    {
      get => this.ordersField;
      set => this.ordersField = value;
    }
  }
}
