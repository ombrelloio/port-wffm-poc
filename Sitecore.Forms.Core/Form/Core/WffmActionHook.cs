// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.WffmActionHook
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Eventing;
using Sitecore.Events.Hooks;
using System;

namespace Sitecore.Form.Core
{
  public class WffmActionHook : IHook
  {
    public void Initialize() => EventManager.Subscribe<WffmActionEvent>(new Action<WffmActionEvent>(WffmActionHandler.Run));
  }
}
