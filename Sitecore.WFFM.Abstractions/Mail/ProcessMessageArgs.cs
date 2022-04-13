// Decompiled with JetBrains decompiler
// Type: Sitecore.WFFM.Abstractions.Mail.ProcessMessageArgs
// Assembly: Sitecore.WFFM.Abstractions, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 893E7AF0-E7C2-4A02-B47B-5687A98493FC
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.WFFM.Abstractions.dll

using Sitecore.Data;
using Sitecore.Web.UI.Sheer;
using Sitecore.WFFM.Abstractions.Actions;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Sitecore.WFFM.Abstractions.Mail
{
  public class ProcessMessageArgs : ClientPipelineArgs
  {
    public ProcessMessageArgs(
      ID formId,
      AdaptedResultList fields,
      MessageType messageType,
      IEmailAttributes emailAttributes)
    {
      this.FormID = formId;
      this.MessageType = messageType;
      this.Fields = fields;
      this.Attachments = new List<Attachment>();
      this.Data = (IDictionary<string, string>) new Dictionary<string, string>();
      this.MessageType = messageType;
      this.To = new StringBuilder();
      this.To.Append(emailAttributes.To.TrimEnd(',', ';').Replace(";", ","));
      this.From = emailAttributes.From.TrimEnd(',', ';').Replace(";", ",");
      this.CC = new StringBuilder();
      this.CC.Append(emailAttributes.CC.TrimEnd(',', ';').Replace(";", ","));
      this.BCC = new StringBuilder();
      this.BCC.Append(emailAttributes.BCC.TrimEnd(',', ';').Replace(";", ","));
      this.Mail = new StringBuilder();
      this.Mail.Append(emailAttributes.Mail);
      this.Subject = new StringBuilder();
      this.Subject.Append(emailAttributes.Subject);
      this.Host = emailAttributes.Host;
      this.Port = emailAttributes.Port;
    }

    public List<Attachment> Attachments { get; private set; }

    public StringBuilder BCC { get; }

    public StringBuilder CC { get; }

    public ICredentialsByHost Credentials { get; set; }

    public IDictionary<string, string> Data { get; private set; }

    public AdaptedResultList Fields { get; set; }

    public ID FormID { get; set; }

    public string From { get; set; }

    public string Host { get; set; }

    public bool IncludeAttachment { get; set; }

    public bool IsBodyHtml { get; set; }

    public StringBuilder Mail { get; }

    public MessageType MessageType { get; private set; }

    public int Port { get; set; }

    public string Recipient { get; set; }

    public string RecipientGateway { get; set; }

    public StringBuilder Subject { get; internal set; }

    public StringBuilder To { get; }

    public bool EnableSsl { get; set; }
  }
}
