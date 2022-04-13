// Decompiled with JetBrains decompiler
// Type: Sitecore.WFFM.Actions.SaveActions.SendMessage
// Assembly: Sitecore.WFFM.Actions, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6607DF78-1371-42DC-8675-A986B5291018
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.WFFM.Actions.dll

using Sitecore.Data;
using Sitecore.Diagnostics;
using Sitecore.WFFM.Abstractions.Actions;
using Sitecore.WFFM.Abstractions.Mail;
using Sitecore.WFFM.Abstractions.Shared;
using Sitecore.WFFM.Actions.Base;

namespace Sitecore.WFFM.Actions.SaveActions
{
  public class SendMessage : WffmSaveAction, IEmailAttributes
  {
    private readonly IMailSender mailSender;
    private readonly IEmailAttributes emailAttributes;

    public SendMessage(ISettings settings, IMailSender mailSender)
    {
      Assert.IsNotNull((object) settings, nameof (settings));
      Assert.IsNotNull((object) mailSender, nameof (mailSender));
      this.mailSender = mailSender;
      this.emailAttributes = (IEmailAttributes) new EmailAttributes(settings.EmailFromAddress, settings.MailServer, settings.MailServerUserName, settings.MailServerPassword, settings.MailServerPort);
    }

    public string BCC
    {
      get => this.emailAttributes.BCC;
      set => this.emailAttributes.BCC = value;
    }

    public string CC
    {
      get => this.emailAttributes.CC;
      set => this.emailAttributes.CC = value;
    }

    public string From
    {
      get => this.emailAttributes.From;
      set => this.emailAttributes.From = value;
    }

    public string LocalFrom
    {
      get => this.emailAttributes.LocalFrom;
      set => this.emailAttributes.LocalFrom = value;
    }

    public string Host
    {
      get => this.emailAttributes.Host;
      set => this.emailAttributes.Host = value;
    }

    public string Login
    {
      get => this.emailAttributes.Login;
      set => this.emailAttributes.Login = value;
    }

    public string Mail
    {
      get => this.emailAttributes.Mail;
      set => this.emailAttributes.Mail = value;
    }

    public string Password
    {
      get => this.emailAttributes.Password;
      set => this.emailAttributes.Password = value;
    }

    public int Port
    {
      get => this.emailAttributes.Port;
      set => this.emailAttributes.Port = value;
    }

    public string Subject
    {
      get => this.emailAttributes.Subject;
      set => this.emailAttributes.Subject = value;
    }

    public string To
    {
      get => this.emailAttributes.To;
      set => this.emailAttributes.To = value;
    }

    public bool EnableSsl
    {
      get => this.emailAttributes.EnableSsl;
      set => this.emailAttributes.EnableSsl = value;
    }

    public MessageType MessageType
    {
      get => this.emailAttributes.MessageType;
      set => this.emailAttributes.MessageType = value;
    }

    public string RecipientGateway
    {
      get => this.emailAttributes.RecipientGateway;
      set => this.emailAttributes.RecipientGateway = value;
    }

    public string Recipient
    {
      get => this.emailAttributes.Recipient;
      set => this.emailAttributes.Recipient = value;
    }

    public bool IsBodyHtml
    {
      get => this.emailAttributes.IsBodyHtml;
      set => this.emailAttributes.IsBodyHtml = value;
    }

    public string FromPhone
    {
      get => this.emailAttributes.FromPhone;
      set => this.emailAttributes.FromPhone = value;
    }

    public bool IsIncludeAttachments
    {
      get => this.emailAttributes.IsIncludeAttachments;
      set => this.emailAttributes.IsIncludeAttachments = value;
    }

    public override void Execute(
      ID formId,
      AdaptedResultList adaptedFields,
      ActionCallContext actionCallContext = null,
      params object[] data)
    {
      this.mailSender.SendMail(this.emailAttributes, formId, adaptedFields, data);
    }
  }
}
