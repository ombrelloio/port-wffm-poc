// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Core.Crm.Metadata.ManyToManyMetadata
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
  public class ManyToManyMetadata : RelationshipMetadata
  {
    private CrmAssociatedMenuBehavior entity1AssociatedMenuBehaviorField;
    private CrmAssociatedMenuGroup entity1AssociatedMenuGroupField;
    private CrmLabel entity1AssociatedMenuLabelField;
    private CrmNumber entity1AssociatedMenuOrderField;
    private string entity1IntersectAttributeField;
    private string entity1LogicalNameField;
    private CrmAssociatedMenuBehavior entity2AssociatedMenuBehaviorField;
    private CrmAssociatedMenuGroup entity2AssociatedMenuGroupField;
    private CrmLabel entity2AssociatedMenuLabelField;
    private CrmNumber entity2AssociatedMenuOrderField;
    private string entity2IntersectAttributeField;
    private string entity2LogicalNameField;
    private string intersectEntityNameField;

    public CrmAssociatedMenuBehavior Entity1AssociatedMenuBehavior
    {
      get => this.entity1AssociatedMenuBehaviorField;
      set => this.entity1AssociatedMenuBehaviorField = value;
    }

    public CrmAssociatedMenuGroup Entity1AssociatedMenuGroup
    {
      get => this.entity1AssociatedMenuGroupField;
      set => this.entity1AssociatedMenuGroupField = value;
    }

    public CrmLabel Entity1AssociatedMenuLabel
    {
      get => this.entity1AssociatedMenuLabelField;
      set => this.entity1AssociatedMenuLabelField = value;
    }

    public CrmNumber Entity1AssociatedMenuOrder
    {
      get => this.entity1AssociatedMenuOrderField;
      set => this.entity1AssociatedMenuOrderField = value;
    }

    public string Entity1IntersectAttribute
    {
      get => this.entity1IntersectAttributeField;
      set => this.entity1IntersectAttributeField = value;
    }

    public string Entity1LogicalName
    {
      get => this.entity1LogicalNameField;
      set => this.entity1LogicalNameField = value;
    }

    public CrmAssociatedMenuBehavior Entity2AssociatedMenuBehavior
    {
      get => this.entity2AssociatedMenuBehaviorField;
      set => this.entity2AssociatedMenuBehaviorField = value;
    }

    public CrmAssociatedMenuGroup Entity2AssociatedMenuGroup
    {
      get => this.entity2AssociatedMenuGroupField;
      set => this.entity2AssociatedMenuGroupField = value;
    }

    public CrmLabel Entity2AssociatedMenuLabel
    {
      get => this.entity2AssociatedMenuLabelField;
      set => this.entity2AssociatedMenuLabelField = value;
    }

    public CrmNumber Entity2AssociatedMenuOrder
    {
      get => this.entity2AssociatedMenuOrderField;
      set => this.entity2AssociatedMenuOrderField = value;
    }

    public string Entity2IntersectAttribute
    {
      get => this.entity2IntersectAttributeField;
      set => this.entity2IntersectAttributeField = value;
    }

    public string Entity2LogicalName
    {
      get => this.entity2LogicalNameField;
      set => this.entity2LogicalNameField = value;
    }

    public string IntersectEntityName
    {
      get => this.intersectEntityNameField;
      set => this.intersectEntityNameField = value;
    }
  }
}
