// Decompiled with JetBrains decompiler
// Type: Sitecore.WFFM.Actions.SaveActions.TellAFriend
// Assembly: Sitecore.WFFM.Actions, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6607DF78-1371-42DC-8675-A986B5291018
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.WFFM.Actions.dll

using Sitecore.WFFM.Abstractions.Shared;

namespace Sitecore.WFFM.Actions.SaveActions
{
  public class TellAFriend : SendMessage
  {
    public TellAFriend(ISettings settings, IMailSender mailSender)
      : base(settings, mailSender)
    {
    }
  }
}
