// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Core.Crm.SendBulkMailRequest
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
  public class SendBulkMailRequest : Request
  {
    private Moniker senderField;
    private Guid templateIdField;
    private string regardingTypeField;
    private Guid regardingIdField;
    private QueryBase queryField;

    public Moniker Sender
    {
      get => this.senderField;
      set => this.senderField = value;
    }

    public Guid TemplateId
    {
      get => this.templateIdField;
      set => this.templateIdField = value;
    }

    public string RegardingType
    {
      get => this.regardingTypeField;
      set => this.regardingTypeField = value;
    }

    public Guid RegardingId
    {
      get => this.regardingIdField;
      set => this.regardingIdField = value;
    }

    public QueryBase Query
    {
      get => this.queryField;
      set => this.queryField = value;
    }
  }
}
