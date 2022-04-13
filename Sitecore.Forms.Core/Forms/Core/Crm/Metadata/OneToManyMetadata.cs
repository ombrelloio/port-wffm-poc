// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Core.Crm.Metadata.OneToManyMetadata
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
  public class OneToManyMetadata : RelationshipMetadata
  {
    private CrmAssociatedMenuBehavior associatedMenuBehaviorField;
    private CrmAssociatedMenuGroup associatedMenuGroupField;
    private CrmLabel associatedMenuLabelField;
    private CrmNumber associatedMenuOrderField;
    private CrmCascadeType cascadeAssignField;
    private CrmCascadeType cascadeDeleteField;
    private CrmCascadeType cascadeMergeField;
    private CrmCascadeType cascadeReparentField;
    private CrmCascadeType cascadeShareField;
    private CrmCascadeType cascadeUnshareField;
    private string referencedAttributeField;
    private string referencedEntityField;
    private string referencingAttributeField;
    private string referencingEntityField;

    public CrmAssociatedMenuBehavior AssociatedMenuBehavior
    {
      get => this.associatedMenuBehaviorField;
      set => this.associatedMenuBehaviorField = value;
    }

    public CrmAssociatedMenuGroup AssociatedMenuGroup
    {
      get => this.associatedMenuGroupField;
      set => this.associatedMenuGroupField = value;
    }

    public CrmLabel AssociatedMenuLabel
    {
      get => this.associatedMenuLabelField;
      set => this.associatedMenuLabelField = value;
    }

    public CrmNumber AssociatedMenuOrder
    {
      get => this.associatedMenuOrderField;
      set => this.associatedMenuOrderField = value;
    }

    public CrmCascadeType CascadeAssign
    {
      get => this.cascadeAssignField;
      set => this.cascadeAssignField = value;
    }

    public CrmCascadeType CascadeDelete
    {
      get => this.cascadeDeleteField;
      set => this.cascadeDeleteField = value;
    }

    public CrmCascadeType CascadeMerge
    {
      get => this.cascadeMergeField;
      set => this.cascadeMergeField = value;
    }

    public CrmCascadeType CascadeReparent
    {
      get => this.cascadeReparentField;
      set => this.cascadeReparentField = value;
    }

    public CrmCascadeType CascadeShare
    {
      get => this.cascadeShareField;
      set => this.cascadeShareField = value;
    }

    public CrmCascadeType CascadeUnshare
    {
      get => this.cascadeUnshareField;
      set => this.cascadeUnshareField = value;
    }

    public string ReferencedAttribute
    {
      get => this.referencedAttributeField;
      set => this.referencedAttributeField = value;
    }

    public string ReferencedEntity
    {
      get => this.referencedEntityField;
      set => this.referencedEntityField = value;
    }

    public string ReferencingAttribute
    {
      get => this.referencingAttributeField;
      set => this.referencingAttributeField = value;
    }

    public string ReferencingEntity
    {
      get => this.referencingEntityField;
      set => this.referencingEntityField = value;
    }
  }
}
