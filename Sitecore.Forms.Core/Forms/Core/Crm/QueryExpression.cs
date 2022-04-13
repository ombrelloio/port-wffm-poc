// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Core.Crm.QueryExpression
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
  public class QueryExpression : QueryBase
  {
    private bool distinctField;
    private PagingInfo pageInfoField;
    private LinkEntity[] linkEntitiesField;
    private FilterExpression criteriaField;
    private OrderExpression[] ordersField;

    public bool Distinct
    {
      get => this.distinctField;
      set => this.distinctField = value;
    }

    public PagingInfo PageInfo
    {
      get => this.pageInfoField;
      set => this.pageInfoField = value;
    }

    [XmlArrayItem(IsNullable = false)]
    public LinkEntity[] LinkEntities
    {
      get => this.linkEntitiesField;
      set => this.linkEntitiesField = value;
    }

    public FilterExpression Criteria
    {
      get => this.criteriaField;
      set => this.criteriaField = value;
    }

    [XmlArrayItem("Order", IsNullable = false)]
    public OrderExpression[] Orders
    {
      get => this.ordersField;
      set => this.ordersField = value;
    }
  }
}
