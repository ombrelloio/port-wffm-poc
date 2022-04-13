// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Core.Crm.Metadata.AttributeMetadata
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
  [XmlInclude(typeof (LookupAttributeMetadata))]
  [XmlInclude(typeof (PicklistAttributeMetadata))]
  [XmlInclude(typeof (StateAttributeMetadata))]
  [XmlInclude(typeof (StatusAttributeMetadata))]
  [XmlInclude(typeof (IntegerAttributeMetadata))]
  [XmlInclude(typeof (FloatAttributeMetadata))]
  [XmlInclude(typeof (DecimalAttributeMetadata))]
  [XmlInclude(typeof (MoneyAttributeMetadata))]
  [XmlInclude(typeof (StringAttributeMetadata))]
  [XmlInclude(typeof (MemoAttributeMetadata))]
  [XmlInclude(typeof (BooleanAttributeMetadata))]
  [XmlInclude(typeof (DateTimeAttributeMetadata))]
  [GeneratedCode("wsdl", "2.0.50727.3038")]
  [DebuggerStepThrough]
  [DesignerCategory("code")]
  [XmlType(Namespace = "http://schemas.microsoft.com/crm/2007/WebServices")]
  [DebuggerNonUserCode]
  [Serializable]
  public class AttributeMetadata : CrmMetadata
  {
    private string aggregateOfField;
    private string attributeOfField;
    private CrmAttributeType attributeTypeField;
    private string calculationOfField;
    private object defaultValueField;
    private CrmLabel descriptionField;
    private CrmDisplayMasks displayMaskField;
    private CrmLabel displayNameField;
    private string entityLogicalNameField;
    private CrmBoolean isCustomFieldField;
    private string logicalNameField;
    private CrmAttributeRequiredLevel requiredLevelField;
    private string schemaNameField;
    private CrmBoolean validForCreateField;
    private CrmBoolean validForReadField;
    private CrmBoolean validForUpdateField;
    private string yomiOfField;

    public string AggregateOf
    {
      get => this.aggregateOfField;
      set => this.aggregateOfField = value;
    }

    public string AttributeOf
    {
      get => this.attributeOfField;
      set => this.attributeOfField = value;
    }

    public CrmAttributeType AttributeType
    {
      get => this.attributeTypeField;
      set => this.attributeTypeField = value;
    }

    public string CalculationOf
    {
      get => this.calculationOfField;
      set => this.calculationOfField = value;
    }

    public object DefaultValue
    {
      get => this.defaultValueField;
      set => this.defaultValueField = value;
    }

    public CrmLabel Description
    {
      get => this.descriptionField;
      set => this.descriptionField = value;
    }

    public CrmDisplayMasks DisplayMask
    {
      get => this.displayMaskField;
      set => this.displayMaskField = value;
    }

    public CrmLabel DisplayName
    {
      get => this.displayNameField;
      set => this.displayNameField = value;
    }

    public string EntityLogicalName
    {
      get => this.entityLogicalNameField;
      set => this.entityLogicalNameField = value;
    }

    public CrmBoolean IsCustomField
    {
      get => this.isCustomFieldField;
      set => this.isCustomFieldField = value;
    }

    public string LogicalName
    {
      get => this.logicalNameField;
      set => this.logicalNameField = value;
    }

    public CrmAttributeRequiredLevel RequiredLevel
    {
      get => this.requiredLevelField;
      set => this.requiredLevelField = value;
    }

    public string SchemaName
    {
      get => this.schemaNameField;
      set => this.schemaNameField = value;
    }

    public CrmBoolean ValidForCreate
    {
      get => this.validForCreateField;
      set => this.validForCreateField = value;
    }

    public CrmBoolean ValidForRead
    {
      get => this.validForReadField;
      set => this.validForReadField = value;
    }

    public CrmBoolean ValidForUpdate
    {
      get => this.validForUpdateField;
      set => this.validForUpdateField = value;
    }

    public string YomiOf
    {
      get => this.yomiOfField;
      set => this.yomiOfField = value;
    }
  }
}
