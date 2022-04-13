// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Core.Crm.BulkDeleteRequest
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
  public class BulkDeleteRequest : Request
  {
    private QueryBase[] querySetField;
    private string jobNameField;
    private bool sendEmailNotificationField;
    private Guid[] toRecipientsField;
    private Guid[] cCRecipientsField;
    private string recurrencePatternField;
    private CrmDateTime startDateTimeField;

    public QueryBase[] QuerySet
    {
      get => this.querySetField;
      set => this.querySetField = value;
    }

    public string JobName
    {
      get => this.jobNameField;
      set => this.jobNameField = value;
    }

    public bool SendEmailNotification
    {
      get => this.sendEmailNotificationField;
      set => this.sendEmailNotificationField = value;
    }

    public Guid[] ToRecipients
    {
      get => this.toRecipientsField;
      set => this.toRecipientsField = value;
    }

    public Guid[] CCRecipients
    {
      get => this.cCRecipientsField;
      set => this.cCRecipientsField = value;
    }

    public string RecurrencePattern
    {
      get => this.recurrencePatternField;
      set => this.recurrencePatternField = value;
    }

    public CrmDateTime StartDateTime
    {
      get => this.startDateTimeField;
      set => this.startDateTimeField = value;
    }
  }
}
