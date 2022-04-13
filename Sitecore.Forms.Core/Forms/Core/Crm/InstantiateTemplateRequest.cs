// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Core.Crm.InstantiateTemplateRequest
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
  [XmlType(Namespace = "http://schemas.microsoft.com/crm/2007/WebServices")]
  [DebuggerNonUserCode]
  [Serializable]
  public class InstantiateTemplateRequest : Request
  {
    private Guid templateIdField;
    private string objectTypeField;
    private Guid objectIdField;
    private bool returnDynamicEntitiesField;

    public Guid TemplateId
    {
      get => this.templateIdField;
      set => this.templateIdField = value;
    }

    public string ObjectType
    {
      get => this.objectTypeField;
      set => this.objectTypeField = value;
    }

    public Guid ObjectId
    {
      get => this.objectIdField;
      set => this.objectIdField = value;
    }

    [XmlAttribute]
    public bool ReturnDynamicEntities
    {
      get => this.returnDynamicEntitiesField;
      set => this.returnDynamicEntitiesField = value;
    }
  }
}
