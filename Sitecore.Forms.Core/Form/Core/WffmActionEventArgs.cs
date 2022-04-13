// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.WffmActionEventArgs
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Events;
using System;

namespace Sitecore.Form.Core
{
  public class WffmActionEventArgs : EventArgs, IPassNativeEventArgs
  {
    public WffmActionEvent RemoteEvent { get; private set; }

    public WffmActionEventArgs(WffmActionEvent remoteEvent) => this.RemoteEvent = remoteEvent;
  }
}
