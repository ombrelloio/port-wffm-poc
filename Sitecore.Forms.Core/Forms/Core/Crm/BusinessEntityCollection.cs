// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Core.Crm.BusinessEntityCollection
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
  [XmlType(Namespace = "http://schemas.microsoft.com/crm/2006/WebServices")]
  [DebuggerNonUserCode]
  [Serializable]
  public class BusinessEntityCollection
  {
    private BusinessEntity[] businessEntitiesField;
    private string entityNameField;
    private bool moreRecordsField;
    private string pagingCookieField;
    private string versionField;

    [XmlArrayItem(IsNullable = false)]
    public BusinessEntity[] BusinessEntities
    {
      get => this.businessEntitiesField;
      set => this.businessEntitiesField = value;
    }

    [XmlAttribute]
    public string EntityName
    {
      get => this.entityNameField;
      set => this.entityNameField = value;
    }

    [XmlAttribute]
    public bool MoreRecords
    {
      get => this.moreRecordsField;
      set => this.moreRecordsField = value;
    }

    [XmlAttribute]
    public string PagingCookie
    {
      get => this.pagingCookieField;
      set => this.pagingCookieField = value;
    }

    [XmlAttribute]
    public string Version
    {
      get => this.versionField;
      set => this.versionField = value;
    }
  }
}
