// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Core.Crm.Metadata.CrmMetadata
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
  [XmlInclude(typeof (EntityMetadata))]
  [XmlInclude(typeof (AttributeMetadata))]
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
  [XmlInclude(typeof (RelationshipMetadata))]
  [XmlInclude(typeof (ManyToManyMetadata))]
  [XmlInclude(typeof (OneToManyMetadata))]
  [GeneratedCode("wsdl", "2.0.50727.3038")]
  [DebuggerStepThrough]
  [DesignerCategory("code")]
  [XmlType(Namespace = "http://schemas.microsoft.com/crm/2007/WebServices")]
  [DebuggerNonUserCode]
  [Serializable]
  public abstract class CrmMetadata
  {
    private Key metadataIdField;

    public Key MetadataId
    {
      get => this.metadataIdField;
      set => this.metadataIdField = value;
    }
  }
}
