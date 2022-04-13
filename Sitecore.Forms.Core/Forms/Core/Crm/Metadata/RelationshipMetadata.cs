// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Core.Crm.Metadata.RelationshipMetadata
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
  [XmlInclude(typeof (ManyToManyMetadata))]
  [XmlInclude(typeof (OneToManyMetadata))]
  [GeneratedCode("wsdl", "2.0.50727.3038")]
  [DebuggerStepThrough]
  [DesignerCategory("code")]
  [XmlType(Namespace = "http://schemas.microsoft.com/crm/2007/WebServices")]
  [DebuggerNonUserCode]
  [Serializable]
  public class RelationshipMetadata : CrmMetadata
  {
    private CrmBoolean isCustomRelationshipField;
    private CrmBoolean isValidForAdvancedFindField;
    private EntityRelationshipType relationshipTypeField;
    private string schemaNameField;
    private SecurityTypes securityTypeField;

    public CrmBoolean IsCustomRelationship
    {
      get => this.isCustomRelationshipField;
      set => this.isCustomRelationshipField = value;
    }

    public CrmBoolean IsValidForAdvancedFind
    {
      get => this.isValidForAdvancedFindField;
      set => this.isValidForAdvancedFindField = value;
    }

    public EntityRelationshipType RelationshipType
    {
      get => this.relationshipTypeField;
      set => this.relationshipTypeField = value;
    }

    public string SchemaName
    {
      get => this.schemaNameField;
      set => this.schemaNameField = value;
    }

    public SecurityTypes SecurityType
    {
      get => this.securityTypeField;
      set => this.securityTypeField = value;
    }
  }
}
