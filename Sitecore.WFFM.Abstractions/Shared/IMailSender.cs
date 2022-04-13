// Decompiled with JetBrains decompiler
// Type: Sitecore.WFFM.Abstractions.Shared.IMailSender
// Assembly: Sitecore.WFFM.Abstractions, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 893E7AF0-E7C2-4A02-B47B-5687A98493FC
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.WFFM.Abstractions.dll

using Sitecore.Data;
using Sitecore.WFFM.Abstractions.Actions;
using Sitecore.WFFM.Abstractions.Mail;

namespace Sitecore.WFFM.Abstractions.Shared
{
  public interface IMailSender
  {
    void SendMail(
      IEmailAttributes emailAttributes,
      ID formId,
      AdaptedResultList fields,
      object[] data);

    void SendMail(IEmailAttributes emailAttributes);

    void SendMailWithGlobalParameters(IEmailAttributes emailAttributes);
  }
}
