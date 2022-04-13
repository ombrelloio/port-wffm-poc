// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Core.Pipelines.ProcessMessage
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Links;
using Sitecore.WFFM.Abstractions.Actions;
using Sitecore.WFFM.Abstractions.Data;
using Sitecore.WFFM.Abstractions.Dependencies;
using Sitecore.WFFM.Abstractions.Mail;
using Sitecore.WFFM.Abstractions.Shared;
using Sitecore.WFFM.Abstractions.Utils;
using System;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace Sitecore.Forms.Core.Pipelines
{
  public class ProcessMessage
  {
    private readonly string srcReplacer;
    private readonly string shortHrefReplacer;
    private readonly string shortHrefMediaReplacer;
    private readonly string hrefReplacer;

    public IItemRepository ItemRepository { get; set; }

    public IFieldProvider FieldProvider { get; set; }

    public ProcessMessage()
      : this(DependenciesManager.WebUtil)
    {
    }

    public ProcessMessage(IWebUtil webUtil)
    {
      Assert.IsNotNull((object) webUtil, nameof (webUtil));
      this.srcReplacer = string.Join(string.Empty, new string[3]
      {
        "src=\"",
        webUtil.GetServerUrl(),
        "/~"
      });
      this.shortHrefReplacer = string.Join(string.Empty, new string[3]
      {
        "href=\"",
        webUtil.GetServerUrl(),
        "/"
      });
      this.shortHrefMediaReplacer = string.Join(string.Empty, new string[3]
      {
        "href=\"",
        webUtil.GetServerUrl(),
        "/~/"
      });
      this.hrefReplacer = this.shortHrefReplacer + "~";
    }

    public void ExpandLinks(ProcessMessageArgs args)
    {
      string str = LinkManager.ExpandDynamicLinks(args.Mail.ToString());
      args.Mail.Remove(0, args.Mail.Length);
      args.Mail.Append(str);
    }

    public void ExpandTokens(ProcessMessageArgs args)
    {
      Assert.IsNotNull((object) this.ItemRepository, "ItemRepository");
      Assert.IsNotNull((object) this.FieldProvider, "FieldProvider");
      foreach (AdaptedControlResult field in args.Fields)
      {
        IFieldItem fieldItem = this.ItemRepository.CreateFieldItem(this.ItemRepository.GetItem(field.FieldID));
        string str1 = field.Value;
        string str2 = Regex.Replace(Regex.Replace(Regex.Replace(this.FieldProvider.GetAdaptedValue(field.FieldID, str1), "src=\"/sitecore/shell/themes/standard/~", this.srcReplacer), "href=\"/sitecore/shell/themes/standard/~", this.hrefReplacer), "on\\w*=\".*?\"", string.Empty);
        if (args.MessageType == MessageType.Sms)
        {
          args.Mail.Replace(Sitecore.StringExtensions.StringExtensions.FormatWith("[{0}]", new object[1]
          {
            (object) fieldItem.FieldDisplayName
          }), str2);
          args.Mail.Replace(Sitecore.StringExtensions.StringExtensions.FormatWith("[{0}]", new object[1]
          {
            (object) fieldItem.Name
          }), str2);
        }
        else
        {
          if (!string.IsNullOrEmpty(field.Parameters) && args.IsBodyHtml)
          {
            if (field.Parameters.StartsWith("multipleline"))
              str2 = str2.Replace(Environment.NewLine, "<br/>");
            if (field.Parameters.StartsWith("secure") && field.Parameters.Contains("<schidden>"))
              str2 = Regex.Replace(str2, "\\d", "*");
          }
          string input = args.Mail.ToString();
          if (Regex.IsMatch(input, "\\[<label id=\"" + (object) fieldItem.ID + "\">[^<]+?</label>]"))
            input = Regex.Replace(input, "\\[<label id=\"" + (object) fieldItem.ID + "\">[^<]+?</label>]", str2);
          if (Regex.IsMatch(input, "\\[<label id=\"" + (object) fieldItem.ID + "\" renderfield=\"Value\">[^<]+?</label>]"))
            input = Regex.Replace(input, "\\[<label id=\"" + (object) fieldItem.ID + "\" renderfield=\"Value\">[^<]+?</label>]", field.Value);
          if (Regex.IsMatch(input, "\\[<label id=\"" + (object) fieldItem.ID + "\" renderfield=\"Text\">[^<]+?</label>]"))
            input = Regex.Replace(input, "\\[<label id=\"" + (object) fieldItem.ID + "\" renderfield=\"Text\">[^<]+?</label>]", str2);
          string str3 = input.Replace(((object) fieldItem.ID).ToString(), str2);
          args.Mail.Clear().Append(str3);
        }
        args.From = args.From.Replace("[" + (object) fieldItem.ID + "]", str2);
        args.From = args.From.Replace(((object) fieldItem.ID).ToString(), str2);
        args.To.Replace(string.Join(string.Empty, new string[3]
        {
          "[",
          ((object) fieldItem.ID).ToString(),
          "]"
        }), str2);
        args.To.Replace(string.Join(string.Empty, new string[1]
        {
          ((object) fieldItem.ID).ToString()
        }), str2);
        args.CC.Replace(string.Join(string.Empty, new string[3]
        {
          "[",
          ((object) fieldItem.ID).ToString(),
          "]"
        }), str2);
        args.CC.Replace(string.Join(string.Empty, new string[1]
        {
          ((object) fieldItem.ID).ToString()
        }), str2);
        args.Subject.Replace(string.Join(string.Empty, new string[3]
        {
          "[",
          ((object) fieldItem.ID).ToString(),
          "]"
        }), str2);
        args.From = args.From.Replace("[" + fieldItem.FieldDisplayName + "]", str2);
        args.To.Replace(string.Join(string.Empty, new string[3]
        {
          "[",
          fieldItem.FieldDisplayName,
          "]"
        }), str2);
        args.CC.Replace(string.Join(string.Empty, new string[3]
        {
          "[",
          fieldItem.FieldDisplayName,
          "]"
        }), str2);
        args.Subject.Replace(string.Join(string.Empty, new string[3]
        {
          "[",
          fieldItem.FieldDisplayName,
          "]"
        }), str2);
        args.From = args.From.Replace("[" + field.FieldName + "]", str2);
        args.To.Replace(string.Join(string.Empty, new string[3]
        {
          "[",
          field.FieldName,
          "]"
        }), str2);
        args.CC.Replace(string.Join(string.Empty, new string[3]
        {
          "[",
          field.FieldName,
          "]"
        }), str2);
        args.Subject.Replace(string.Join(string.Empty, new string[3]
        {
          "[",
          field.FieldName,
          "]"
        }), str2);
      }
    }

    public void AddHostToItemLink(ProcessMessageArgs args) => args.Mail.Replace("href=\"/", this.shortHrefReplacer);

    public void AddHostToMediaItem(ProcessMessageArgs args) => args.Mail.Replace("href=\"~/", this.shortHrefMediaReplacer);

    public void AddAttachments(ProcessMessageArgs args)
    {
      Assert.IsNotNull((object) this.ItemRepository, "ItemRepository");
      if (!args.IncludeAttachment)
        return;
      foreach (AdaptedControlResult field in args.Fields)
      {
        if (!string.IsNullOrEmpty(field.Parameters) && field.Parameters.StartsWith("medialink") && !string.IsNullOrEmpty(field.Value))
        {
          ItemUri uri = ItemUri.Parse(field.Value);
          if ((uri != (ItemUri) null))
          {
            Item obj = this.ItemRepository.GetItem(uri);
            if (obj != null)
            {
              MediaItem mediaItem = new MediaItem(obj);
              args.Attachments.Add(new Attachment(mediaItem.GetMediaStream(), string.Join(".", new string[2]
              {
                ((CustomItemBase) mediaItem).Name,
                mediaItem.Extension
              }), mediaItem.MimeType));
            }
          }
        }
      }
    }

    public void BuildToFromRecipient(ProcessMessageArgs args)
    {
      if (string.IsNullOrEmpty(args.Recipient) || string.IsNullOrEmpty(args.RecipientGateway))
        return;
      if (args.To.Length > 0)
        args.To.Remove(0, args.To.Length);
      args.To.Append(args.Fields.GetValueByFieldID(args.Recipient)).Append(args.RecipientGateway);
    }

    public void SendEmail(ProcessMessageArgs args)
    {
      SmtpClient smtpClient = new SmtpClient(args.Host)
      {
        EnableSsl = args.EnableSsl
      };
      if (args.Port != 0)
        smtpClient.Port = args.Port;
      smtpClient.Credentials = args.Credentials;
      smtpClient.Send(this.GetMail(args));
    }

    private MailMessage GetMail(ProcessMessageArgs args)
    {
      if (!Regex.Match(args.From, "^\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*$").Success)
        throw new Exception("The email message was not sent.The email address specified in the \"From\" parameter is not valid.");
      MailMessage mail = new MailMessage(args.From.Replace(";", ","), args.To.Replace(";", ",").ToString(), args.Subject.ToString(), args.Mail.ToString())
      {
        IsBodyHtml = args.IsBodyHtml
      };
      if (args.CC.Length > 0)
      {
        char[] chArray = new char[1]{ ',' };
        foreach (string address in args.CC.Replace(";", ",").ToString().Split(chArray))
          mail.CC.Add(new MailAddress(address));
      }
      if (args.BCC.Length > 0)
      {
        char[] chArray = new char[1]{ ',' };
        foreach (string address in args.BCC.Replace(";", ",").ToString().Split(chArray))
          mail.Bcc.Add(new MailAddress(address));
      }
      args.Attachments.ForEach((Action<Attachment>) (attachment => mail.Attachments.Add(attachment)));
      return mail;
    }
  }
}
