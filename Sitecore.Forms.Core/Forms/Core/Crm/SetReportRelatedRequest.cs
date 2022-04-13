// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Core.Crm.SetReportRelatedRequest
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
  public class SetReportRelatedRequest : Request
  {
    private Guid reportIdField;
    private int[] entitiesField;
    private int[] categoriesField;
    private int[] visibilityField;

    public Guid ReportId
    {
      get => this.reportIdField;
      set => this.reportIdField = value;
    }

    public int[] Entities
    {
      get => this.entitiesField;
      set => this.entitiesField = value;
    }

    public int[] Categories
    {
      get => this.categoriesField;
      set => this.categoriesField = value;
    }

    public int[] Visibility
    {
      get => this.visibilityField;
      set => this.visibilityField = value;
    }
  }
}
