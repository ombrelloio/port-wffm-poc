// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Core.Crm.CancelSalesOrderRequest
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
  public class CancelSalesOrderRequest : Request
  {
    private BusinessEntity orderCloseField;
    private int statusField;

    public BusinessEntity OrderClose
    {
      get => this.orderCloseField;
      set => this.orderCloseField = value;
    }

    public int Status
    {
      get => this.statusField;
      set => this.statusField = value;
    }
  }
}
