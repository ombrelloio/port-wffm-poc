// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Submit.CheckUserAction
// Assembly: Sitecore.Forms.Custom, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E620A323-75A8-42D2-B304-7093FE781621
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Custom.dll

using Sitecore.WFFM.Actions.Base;

namespace Sitecore.Form.Submit
{
  public abstract class CheckUserAction : WffmCheckAction
  {
    public string UserNameField { get; set; }

    public string DomainField { get; set; }

    public string FailedMessage { get; set; }
  }
}
