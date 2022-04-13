// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Core.Crm.SdkMessageProcessingStepImageRegistration
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
  public class SdkMessageProcessingStepImageRegistration
  {
    private string messagePropertyNameField;
    private string[] attributesField;
    private string entityAliasField;
    private int imageTypeField;

    public string MessagePropertyName
    {
      get => this.messagePropertyNameField;
      set => this.messagePropertyNameField = value;
    }

    public string[] Attributes
    {
      get => this.attributesField;
      set => this.attributesField = value;
    }

    public string EntityAlias
    {
      get => this.entityAliasField;
      set => this.entityAliasField = value;
    }

    public int ImageType
    {
      get => this.imageTypeField;
      set => this.imageTypeField = value;
    }
  }
}
