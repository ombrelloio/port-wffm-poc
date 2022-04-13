// Decompiled with JetBrains decompiler
// Type: Sitecore.WFFM.Abstractions.Mail.EmailAttributes
// Assembly: Sitecore.WFFM.Abstractions, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 893E7AF0-E7C2-4A02-B47B-5687A98493FC
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.WFFM.Abstractions.dll

namespace Sitecore.WFFM.Abstractions.Mail
{
  public class EmailAttributes : IEmailAttributes
  {
    private string fromAddress;

    public EmailAttributes(
      string emailFrom,
      string host,
      string login,
      string password,
      string port)
    {
      this.To = "example@mail.com";
      this.BCC = string.Empty;
      this.CC = string.Empty;
      this.Subject = "notification";
      this.Mail = string.Empty;
      this.From = string.IsNullOrEmpty(emailFrom) ? "example@mail.com" : emailFrom;
      this.Port = 25;
      if (string.IsNullOrEmpty(host))
        return;
      this.Host = host;
      int result;
      if (int.TryParse(port, out result))
        this.Port = result;
      if (string.IsNullOrEmpty(login))
        return;
      this.Login = login;
      this.Password = password;
    }

    public string BCC { get; set; }

    public string CC { get; set; }

    public string From
    {
      get => !string.IsNullOrEmpty(this.LocalFrom) ? this.LocalFrom : this.fromAddress;
      set => this.fromAddress = value;
    }

    public string LocalFrom { get; set; }

    public string Host { get; set; }

    public string Login { get; set; }

    public string Mail { get; set; }

    public string Password { get; set; }

    public int Port { get; set; }

    public string Subject { get; set; }

    public string To { get; set; }

    public bool EnableSsl { get; set; }

    public MessageType MessageType { get; set; }

    public string RecipientGateway { get; set; }

    public string Recipient { get; set; }

    public bool IsBodyHtml { get; set; }

    public string FromPhone { get; set; }

    public bool IsIncludeAttachments { get; set; }
  }
}
