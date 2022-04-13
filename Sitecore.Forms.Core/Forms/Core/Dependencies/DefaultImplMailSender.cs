// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Core.Dependencies.DefaultImplMailSender
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Data;
using Sitecore.Diagnostics;
using Sitecore.Form.Core.Configuration;
using Sitecore.Form.Core.Utility;
using Sitecore.Pipelines;
using Sitecore.WFFM.Abstractions.Actions;
using Sitecore.WFFM.Abstractions.Mail;
using Sitecore.WFFM.Abstractions.Shared;
using System;
using System.Net;

namespace Sitecore.Forms.Core.Dependencies
{
  [Serializable]
  public class DefaultImplMailSender : IMailSender
  {
    private readonly IItemRepository itemRepository;

    public DefaultImplMailSender(IItemRepository itemRepository)
    {
      Assert.ArgumentNotNull((object) itemRepository, nameof (itemRepository));
      this.itemRepository = itemRepository;
    }

    public void SendMail(IEmailAttributes emailAttributes) => this.SendMail(emailAttributes, ID.Null, (AdaptedResultList) null, (object[]) null);

    public void SendMailWithGlobalParameters(IEmailAttributes emailAttributes)
    {
      IActionItem action = this.itemRepository.CreateAction(FormIDs.SendEmailActionID);
      ReflectionUtils.SetXmlProperties((object) emailAttributes, action.GlobalParameters, true);
      this.SendMail(emailAttributes, ID.Null, (AdaptedResultList) null, (object[]) null);
    }

    public void SendMail(
      IEmailAttributes emailAttributes,
      ID formId,
      AdaptedResultList fields,
      object[] data)
    {
      ProcessMessageArgs processMessageArgs = new ProcessMessageArgs(formId, fields, MessageType.Email, emailAttributes)
      {
        IncludeAttachment = emailAttributes.IsIncludeAttachments,
        Recipient = emailAttributes.Recipient,
        RecipientGateway = emailAttributes.RecipientGateway,
        IsBodyHtml = emailAttributes.IsBodyHtml,
        EnableSsl = emailAttributes.EnableSsl,
        Credentials = this.GetCredentials(emailAttributes.Login, emailAttributes.Password)
      };
      processMessageArgs.Data.Add("FromPhone", emailAttributes.FromPhone ?? string.Empty);
      CorePipeline.Run("processMessage", (PipelineArgs) processMessageArgs);
    }

    protected ICredentialsByHost GetCredentials(string login, string password)
    {
      ICredentialsByHost credentialsByHost = (ICredentialsByHost) null;
      string[] strArray1;
      if (!string.IsNullOrEmpty(login))
        strArray1 = login.Split('\\');
      else
        strArray1 = new string[1]{ string.Empty };
      string[] strArray2 = strArray1;
      if (strArray2.Length != 0 && !string.IsNullOrEmpty(strArray2[0]))
        credentialsByHost = strArray2.Length != 2 || string.IsNullOrEmpty(strArray2[1]) ? (ICredentialsByHost) new NetworkCredential(strArray2[0], password) : (ICredentialsByHost) new NetworkCredential(strArray2[1], password, strArray2[0]);
      return credentialsByHost;
    }
  }
}
