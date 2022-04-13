// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Core.Crm.Metadata.EntityMetadata
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
  public class EntityMetadata : CrmMetadata
  {
    private AttributeMetadata[] attributesField;
    private CrmBoolean canTriggerWorkflowField;
    private CrmLabel descriptionField;
    private CrmLabel displayCollectionNameField;
    private CrmLabel displayNameField;
    private CrmBoolean duplicateDetectionField;
    private CrmBoolean isActivityField;
    private CrmBoolean isAvailableOfflineField;
    private CrmBoolean isChildEntityField;
    private CrmBoolean isCustomEntityField;
    private CrmBoolean isCustomizableField;
    private CrmBoolean isImportableField;
    private CrmBoolean isIntersectField;
    private CrmBoolean isMailMergeEnabledField;
    private CrmBoolean isValidForAdvancedFindField;
    private string logicalNameField;
    private ManyToManyMetadata[] manyToManyRelationshipsField;
    private OneToManyMetadata[] manyToOneRelationshipsField;
    private CrmNumber objectTypeCodeField;
    private OneToManyMetadata[] oneToManyRelationshipsField;
    private CrmOwnershipTypes ownershipTypeField;
    private string primaryFieldField;
    private string primaryKeyField;
    private SecurityPrivilegeMetadata[] privilegesField;
    private string reportViewNameField;
    private string schemaNameField;
    private CrmNumber workflowSupportField;

    [XmlArrayItem("Attribute", IsNullable = false)]
    public AttributeMetadata[] Attributes
    {
      get => this.attributesField;
      set => this.attributesField = value;
    }

    public CrmBoolean CanTriggerWorkflow
    {
      get => this.canTriggerWorkflowField;
      set => this.canTriggerWorkflowField = value;
    }

    public CrmLabel Description
    {
      get => this.descriptionField;
      set => this.descriptionField = value;
    }

    public CrmLabel DisplayCollectionName
    {
      get => this.displayCollectionNameField;
      set => this.displayCollectionNameField = value;
    }

    public CrmLabel DisplayName
    {
      get => this.displayNameField;
      set => this.displayNameField = value;
    }

    public CrmBoolean DuplicateDetection
    {
      get => this.duplicateDetectionField;
      set => this.duplicateDetectionField = value;
    }

    public CrmBoolean IsActivity
    {
      get => this.isActivityField;
      set => this.isActivityField = value;
    }

    public CrmBoolean IsAvailableOffline
    {
      get => this.isAvailableOfflineField;
      set => this.isAvailableOfflineField = value;
    }

    public CrmBoolean IsChildEntity
    {
      get => this.isChildEntityField;
      set => this.isChildEntityField = value;
    }

    public CrmBoolean IsCustomEntity
    {
      get => this.isCustomEntityField;
      set => this.isCustomEntityField = value;
    }

    public CrmBoolean IsCustomizable
    {
      get => this.isCustomizableField;
      set => this.isCustomizableField = value;
    }

    public CrmBoolean IsImportable
    {
      get => this.isImportableField;
      set => this.isImportableField = value;
    }

    public CrmBoolean IsIntersect
    {
      get => this.isIntersectField;
      set => this.isIntersectField = value;
    }

    public CrmBoolean IsMailMergeEnabled
    {
      get => this.isMailMergeEnabledField;
      set => this.isMailMergeEnabledField = value;
    }

    public CrmBoolean IsValidForAdvancedFind
    {
      get => this.isValidForAdvancedFindField;
      set => this.isValidForAdvancedFindField = value;
    }

    public string LogicalName
    {
      get => this.logicalNameField;
      set => this.logicalNameField = value;
    }

    [XmlArrayItem("Intersect", IsNullable = false)]
    public ManyToManyMetadata[] ManyToManyRelationships
    {
      get => this.manyToManyRelationshipsField;
      set => this.manyToManyRelationshipsField = value;
    }

    [XmlArrayItem("From", IsNullable = false)]
    public OneToManyMetadata[] ManyToOneRelationships
    {
      get => this.manyToOneRelationshipsField;
      set => this.manyToOneRelationshipsField = value;
    }

    public CrmNumber ObjectTypeCode
    {
      get => this.objectTypeCodeField;
      set => this.objectTypeCodeField = value;
    }

    [XmlArrayItem("To", IsNullable = false)]
    public OneToManyMetadata[] OneToManyRelationships
    {
      get => this.oneToManyRelationshipsField;
      set => this.oneToManyRelationshipsField = value;
    }

    public CrmOwnershipTypes OwnershipType
    {
      get => this.ownershipTypeField;
      set => this.ownershipTypeField = value;
    }

    public string PrimaryField
    {
      get => this.primaryFieldField;
      set => this.primaryFieldField = value;
    }

    public string PrimaryKey
    {
      get => this.primaryKeyField;
      set => this.primaryKeyField = value;
    }

    [XmlArrayItem("Privilege", IsNullable = false)]
    public SecurityPrivilegeMetadata[] Privileges
    {
      get => this.privilegesField;
      set => this.privilegesField = value;
    }

    public string ReportViewName
    {
      get => this.reportViewNameField;
      set => this.reportViewNameField = value;
    }

    public string SchemaName
    {
      get => this.schemaNameField;
      set => this.schemaNameField = value;
    }

    public CrmNumber WorkflowSupport
    {
      get => this.workflowSupportField;
      set => this.workflowSupportField = value;
    }
  }
}
