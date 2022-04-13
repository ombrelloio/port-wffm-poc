// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Core.Crm.Metadata.MetadataServiceRequest
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
  [XmlInclude(typeof (RetrieveTimestampRequest))]
  [XmlInclude(typeof (DeleteRelationshipRequest))]
  [XmlInclude(typeof (InsertOptionValueRequest))]
  [XmlInclude(typeof (CanManyToManyRequest))]
  [XmlInclude(typeof (CreateEntityRequest))]
  [XmlInclude(typeof (UpdateEntityRequest))]
  [XmlInclude(typeof (DeleteEntityRequest))]
  [XmlInclude(typeof (RetrieveEntityRequest))]
  [XmlInclude(typeof (CreateAttributeRequest))]
  [XmlInclude(typeof (UpdateAttributeRequest))]
  [XmlInclude(typeof (DeleteAttributeRequest))]
  [XmlInclude(typeof (RetrieveAttributeRequest))]
  [XmlInclude(typeof (CreateOneToManyRequest))]
  [XmlInclude(typeof (CreateManyToManyRequest))]
  [XmlInclude(typeof (RetrieveRelationshipRequest))]
  [XmlInclude(typeof (UpdateRelationshipRequest))]
  [XmlInclude(typeof (UpdateOptionValueRequest))]
  [XmlInclude(typeof (DeleteOptionValueRequest))]
  [XmlInclude(typeof (OrderOptionRequest))]
  [XmlInclude(typeof (RetrieveAllEntitiesRequest))]
  [XmlInclude(typeof (InsertStatusValueRequest))]
  [XmlInclude(typeof (CanBeReferencedRequest))]
  [XmlInclude(typeof (CanBeReferencingRequest))]
  [XmlInclude(typeof (GetValidReferencingEntitiesRequest))]
  [XmlInclude(typeof (GetValidReferencedEntitiesRequest))]
  [XmlInclude(typeof (GetValidManyToManyRequest))]
  [GeneratedCode("wsdl", "2.0.50727.3038")]
  [DebuggerStepThrough]
  [DesignerCategory("code")]
  [XmlType(Namespace = "http://schemas.microsoft.com/crm/2007/WebServices")]
  [DebuggerNonUserCode]
  [Serializable]
  public abstract class MetadataServiceRequest
  {
  }
}
