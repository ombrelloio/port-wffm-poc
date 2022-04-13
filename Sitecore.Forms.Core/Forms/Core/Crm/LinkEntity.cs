// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Core.Crm.LinkEntity
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
  public class LinkEntity
  {
    private string linkFromAttributeNameField;
    private string linkFromEntityNameField;
    private string linkToEntityNameField;
    private string linkToAttributeNameField;
    private JoinOperator joinOperatorField;
    private FilterExpression linkCriteriaField;
    private LinkEntity[] linkEntitiesField;

    public string LinkFromAttributeName
    {
      get => this.linkFromAttributeNameField;
      set => this.linkFromAttributeNameField = value;
    }

    public string LinkFromEntityName
    {
      get => this.linkFromEntityNameField;
      set => this.linkFromEntityNameField = value;
    }

    public string LinkToEntityName
    {
      get => this.linkToEntityNameField;
      set => this.linkToEntityNameField = value;
    }

    public string LinkToAttributeName
    {
      get => this.linkToAttributeNameField;
      set => this.linkToAttributeNameField = value;
    }

    public JoinOperator JoinOperator
    {
      get => this.joinOperatorField;
      set => this.joinOperatorField = value;
    }

    public FilterExpression LinkCriteria
    {
      get => this.linkCriteriaField;
      set => this.linkCriteriaField = value;
    }

    [XmlArrayItem(IsNullable = false)]
    public LinkEntity[] LinkEntities
    {
      get => this.linkEntitiesField;
      set => this.linkEntitiesField = value;
    }
  }
}
