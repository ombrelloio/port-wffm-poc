// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Core.Crm.SendTemplateRequest
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
  public class SendTemplateRequest : Request
  {
    private Guid templateIdField;
    private Moniker senderField;
    private string recipientTypeField;
    private Guid[] recipientIdsField;
    private string regardingTypeField;
    private Guid regardingIdField;

    public Guid TemplateId
    {
      get => this.templateIdField;
      set => this.templateIdField = value;
    }

    public Moniker Sender
    {
      get => this.senderField;
      set => this.senderField = value;
    }

    public string RecipientType
    {
      get => this.recipientTypeField;
      set => this.recipientTypeField = value;
    }

    public Guid[] RecipientIds
    {
      get => this.recipientIdsField;
      set => this.recipientIdsField = value;
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
  }
}
