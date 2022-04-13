// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Core.Crm.Metadata.RetrieveRelationshipRequest
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
  public class RetrieveRelationshipRequest : MetadataServiceRequest
  {
    private Guid metadataIdField;
    private string nameField;
    private bool retrieveAsIfPublishedField;

    public Guid MetadataId
    {
      get => this.metadataIdField;
      set => this.metadataIdField = value;
    }

    public string Name
    {
      get => this.nameField;
      set => this.nameField = value;
    }

    public bool RetrieveAsIfPublished
    {
      get => this.retrieveAsIfPublishedField;
      set => this.retrieveAsIfPublishedField = value;
    }
  }
}
