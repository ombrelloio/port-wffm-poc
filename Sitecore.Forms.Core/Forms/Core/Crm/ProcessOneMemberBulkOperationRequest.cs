// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Core.Crm.ProcessOneMemberBulkOperationRequest
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
  public class ProcessOneMemberBulkOperationRequest : Request
  {
    private Guid bulkOperationIdField;
    private BusinessEntity entityField;
    private BulkOperationSource bulkOperationSourceField;

    public Guid BulkOperationId
    {
      get => this.bulkOperationIdField;
      set => this.bulkOperationIdField = value;
    }

    public BusinessEntity Entity
    {
      get => this.entityField;
      set => this.entityField = value;
    }

    public BulkOperationSource BulkOperationSource
    {
      get => this.bulkOperationSourceField;
      set => this.bulkOperationSourceField = value;
    }
  }
}
