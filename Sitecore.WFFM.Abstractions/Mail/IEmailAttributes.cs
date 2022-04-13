// Decompiled with JetBrains decompiler
// Type: Sitecore.WFFM.Abstractions.Mail.IEmailAttributes
// Assembly: Sitecore.WFFM.Abstractions, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 893E7AF0-E7C2-4A02-B47B-5687A98493FC
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.WFFM.Abstractions.dll

namespace Sitecore.WFFM.Abstractions.Mail
{
  public interface IEmailAttributes
  {
    string BCC { get; set; }

    string CC { get; set; }

    string From { get; set; }

    string LocalFrom { get; set; }

    string Host { get; set; }

    string Login { get; set; }

    string Mail { get; set; }

    string Password { get; set; }

    int Port { get; set; }

    string Subject { get; set; }

    string To { get; set; }

    bool EnableSsl { get; set; }

    MessageType MessageType { get; set; }

    string RecipientGateway { get; set; }

    string Recipient { get; set; }

    bool IsBodyHtml { get; set; }

    string FromPhone { get; set; }

    bool IsIncludeAttachments { get; set; }
  }
}
