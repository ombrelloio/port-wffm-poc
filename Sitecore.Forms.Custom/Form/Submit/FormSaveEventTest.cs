// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Submit.FormSaveEventTest
// Assembly: Sitecore.Forms.Custom, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E620A323-75A8-42D2-B304-7093FE781621
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Custom.dll

using Sitecore.Events;
using System;

namespace Sitecore.Form.Submit
{
  public class FormSaveEventTest
  {
    protected void OnFormSave(object sender, EventArgs args)
    {
      if (!(args is SitecoreEventArgs sitecoreEventArgs))
        return;
      object parameter1 = sitecoreEventArgs.Parameters[0];
      object parameter2 = sitecoreEventArgs.Parameters[1];
    }
  }
}
