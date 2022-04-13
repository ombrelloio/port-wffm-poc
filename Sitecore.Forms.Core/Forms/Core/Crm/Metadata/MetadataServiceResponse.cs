// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Core.Crm.Metadata.MetadataServiceResponse
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
  [XmlInclude(typeof (OrderOptionResponse))]
  [XmlInclude(typeof (UpdateRelationshipResponse))]
  [XmlInclude(typeof (RetrieveTimestampResponse))]
  [XmlInclude(typeof (CreateEntityResponse))]
  [XmlInclude(typeof (UpdateEntityResponse))]
  [XmlInclude(typeof (DeleteEntityResponse))]
  [XmlInclude(typeof (RetrieveEntityResponse))]
  [XmlInclude(typeof (CreateAttributeResponse))]
  [XmlInclude(typeof (UpdateAttributeResponse))]
  [XmlInclude(typeof (DeleteAttributeResponse))]
  [XmlInclude(typeof (RetrieveAttributeResponse))]
  [XmlInclude(typeof (CreateOneToManyResponse))]
  [XmlInclude(typeof (CreateManyToManyResponse))]
  [XmlInclude(typeof (RetrieveRelationshipResponse))]
  [XmlInclude(typeof (CanBeReferencedResponse))]
  [XmlInclude(typeof (DeleteRelationshipResponse))]
  [XmlInclude(typeof (UpdateOptionValueResponse))]
  [XmlInclude(typeof (DeleteOptionValueResponse))]
  [XmlInclude(typeof (RetrieveAllEntitiesResponse))]
  [XmlInclude(typeof (InsertStatusValueResponse))]
  [XmlInclude(typeof (InsertOptionValueResponse))]
  [XmlInclude(typeof (CanBeReferencingResponse))]
  [XmlInclude(typeof (GetValidReferencingEntitiesResponse))]
  [XmlInclude(typeof (GetValidReferencedEntitiesResponse))]
  [XmlInclude(typeof (GetValidManyToManyResponse))]
  [XmlInclude(typeof (CanManyToManyResponse))]
  [GeneratedCode("wsdl", "2.0.50727.3038")]
  [DebuggerStepThrough]
  [DesignerCategory("code")]
  [XmlType(Namespace = "http://schemas.microsoft.com/crm/2007/WebServices")]
  [DebuggerNonUserCode]
  [Serializable]
  public abstract class MetadataServiceResponse
  {
  }
}
