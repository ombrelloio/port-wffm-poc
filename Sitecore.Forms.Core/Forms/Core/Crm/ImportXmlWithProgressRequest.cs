// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Core.Crm.ImportXmlWithProgressRequest
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
  public class ImportXmlWithProgressRequest : Request
  {
    private string parameterXmlField;
    private string customizationXmlField;
    private Guid importJobIdField;

    public string ParameterXml
    {
      get => this.parameterXmlField;
      set => this.parameterXmlField = value;
    }

    public string CustomizationXml
    {
      get => this.customizationXmlField;
      set => this.customizationXmlField = value;
    }

    public Guid ImportJobId
    {
      get => this.importJobIdField;
      set => this.importJobIdField = value;
    }
  }
}
