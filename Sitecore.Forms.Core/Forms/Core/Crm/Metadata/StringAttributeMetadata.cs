// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Core.Crm.Metadata.StringAttributeMetadata
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
  public class StringAttributeMetadata : AttributeMetadata
  {
    private CrmNumber databaseLengthField;
    private CrmStringFormat formatField;
    private CrmImeMode imeModeField;
    private CrmNumber maxLengthField;

    public CrmNumber DatabaseLength
    {
      get => this.databaseLengthField;
      set => this.databaseLengthField = value;
    }

    public CrmStringFormat Format
    {
      get => this.formatField;
      set => this.formatField = value;
    }

    public CrmImeMode ImeMode
    {
      get => this.imeModeField;
      set => this.imeModeField = value;
    }

    public CrmNumber MaxLength
    {
      get => this.maxLengthField;
      set => this.maxLengthField = value;
    }
  }
}
