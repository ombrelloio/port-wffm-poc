// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Core.Crm.SearchByKeywordsKbArticleRequest
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
  public class SearchByKeywordsKbArticleRequest : Request
  {
    private string searchTextField;
    private Guid subjectIdField;
    private ColumnSetBase columnSetField;
    private bool returnDynamicEntitiesField;

    public string SearchText
    {
      get => this.searchTextField;
      set => this.searchTextField = value;
    }

    public Guid SubjectId
    {
      get => this.subjectIdField;
      set => this.subjectIdField = value;
    }

    public ColumnSetBase ColumnSet
    {
      get => this.columnSetField;
      set => this.columnSetField = value;
    }

    [XmlAttribute]
    public bool ReturnDynamicEntities
    {
      get => this.returnDynamicEntitiesField;
      set => this.returnDynamicEntitiesField = value;
    }
  }
}
