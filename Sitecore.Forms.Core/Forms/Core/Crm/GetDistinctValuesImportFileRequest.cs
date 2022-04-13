// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Core.Crm.GetDistinctValuesImportFileRequest
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
  public class GetDistinctValuesImportFileRequest : Request
  {
    private Guid importFileIdField;
    private int columnNumberField;
    private int pageNumberField;
    private int recordsPerPageField;

    public Guid ImportFileId
    {
      get => this.importFileIdField;
      set => this.importFileIdField = value;
    }

    public int columnNumber
    {
      get => this.columnNumberField;
      set => this.columnNumberField = value;
    }

    public int pageNumber
    {
      get => this.pageNumberField;
      set => this.pageNumberField = value;
    }

    public int recordsPerPage
    {
      get => this.recordsPerPageField;
      set => this.recordsPerPageField = value;
    }
  }
}
