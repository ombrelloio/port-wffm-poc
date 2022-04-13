// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Core.Crm.LogFailureBulkOperationRequest
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
  public class LogFailureBulkOperationRequest : Request
  {
    private Guid bulkOperationIdField;
    private Guid regardingObjectIdField;
    private int regardingObjectTypeCodeField;
    private int errorCodeField;
    private string messageField;
    private string additionalInfoField;

    public Guid BulkOperationId
    {
      get => this.bulkOperationIdField;
      set => this.bulkOperationIdField = value;
    }

    public Guid RegardingObjectId
    {
      get => this.regardingObjectIdField;
      set => this.regardingObjectIdField = value;
    }

    public int RegardingObjectTypeCode
    {
      get => this.regardingObjectTypeCodeField;
      set => this.regardingObjectTypeCodeField = value;
    }

    public int ErrorCode
    {
      get => this.errorCodeField;
      set => this.errorCodeField = value;
    }

    public string Message
    {
      get => this.messageField;
      set => this.messageField = value;
    }

    public string AdditionalInfo
    {
      get => this.additionalInfoField;
      set => this.additionalInfoField = value;
    }
  }
}
