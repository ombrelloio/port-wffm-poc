// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Core.Crm.Metadata.MoneyAttributeMetadata
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
  public class MoneyAttributeMetadata : AttributeMetadata
  {
    private CrmImeMode imeModeField;
    private CrmDouble maxValueField;
    private CrmDouble minValueField;
    private CrmNumber precisionField;
    private CrmNumber precisionSourceField;

    public CrmImeMode ImeMode
    {
      get => this.imeModeField;
      set => this.imeModeField = value;
    }

    public CrmDouble MaxValue
    {
      get => this.maxValueField;
      set => this.maxValueField = value;
    }

    public CrmDouble MinValue
    {
      get => this.minValueField;
      set => this.minValueField = value;
    }

    public CrmNumber Precision
    {
      get => this.precisionField;
      set => this.precisionField = value;
    }

    public CrmNumber PrecisionSource
    {
      get => this.precisionSourceField;
      set => this.precisionSourceField = value;
    }
  }
}
