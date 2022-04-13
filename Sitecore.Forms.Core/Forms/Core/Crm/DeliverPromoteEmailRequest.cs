// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Core.Crm.DeliverPromoteEmailRequest
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
  public class DeliverPromoteEmailRequest : Request
  {
    private Guid emailIdField;
    private string messageIdField;
    private string subjectField;
    private string fromField;
    private string toField;
    private string ccField;
    private string bccField;
    private CrmDateTime receivedOnField;
    private string submittedByField;
    private string importanceField;
    private string bodyField;
    private BusinessEntityCollection attachmentsField;

    public Guid EmailId
    {
      get => this.emailIdField;
      set => this.emailIdField = value;
    }

    public string MessageId
    {
      get => this.messageIdField;
      set => this.messageIdField = value;
    }

    public string Subject
    {
      get => this.subjectField;
      set => this.subjectField = value;
    }

    public string From
    {
      get => this.fromField;
      set => this.fromField = value;
    }

    public string To
    {
      get => this.toField;
      set => this.toField = value;
    }

    public string Cc
    {
      get => this.ccField;
      set => this.ccField = value;
    }

    public string Bcc
    {
      get => this.bccField;
      set => this.bccField = value;
    }

    public CrmDateTime ReceivedOn
    {
      get => this.receivedOnField;
      set => this.receivedOnField = value;
    }

    public string SubmittedBy
    {
      get => this.submittedByField;
      set => this.submittedByField = value;
    }

    public string Importance
    {
      get => this.importanceField;
      set => this.importanceField = value;
    }

    public string Body
    {
      get => this.bodyField;
      set => this.bodyField = value;
    }

    public BusinessEntityCollection Attachments
    {
      get => this.attachmentsField;
      set => this.attachmentsField = value;
    }
  }
}
