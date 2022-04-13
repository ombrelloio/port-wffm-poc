// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Core.Dependencies.DefaultImplLogger
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Diagnostics;
using Sitecore.WFFM.Abstractions;
using Sitecore.WFFM.Abstractions.Shared;
using System;

namespace Sitecore.Forms.Core.Dependencies
{
  [Serializable]
  public class DefaultImplLogger : ILogger
  {
    private const string LogPrefix = "[WFFM] ";

    public virtual void Warn(string message, object owner) => this.Log(message, owner, LogMessageType.Warn);

    public virtual void Warn(string message, Exception exception, object owner) => Sitecore.Diagnostics.Log.Warn("[WFFM] " + message, exception, owner ?? (object) this);

    public virtual void Info(string message, object owner) => this.Log(message, owner, LogMessageType.Info);

    public virtual void Audit(string message, object owner) => this.Log(message, owner, LogMessageType.Audit);

    public virtual bool IsNull(object obj, string name)
    {
      Assert.ArgumentNotNullOrEmpty(name, nameof (name));
      if (obj != null)
        return false;
      this.Log(string.Format("{0}  is not initialized", (object) name), (object) this, LogMessageType.Warn);
      return true;
    }

    public void Log(string message, object owner, LogMessageType messageType)
    {
      Action<string, object> action = new Action<string, object>(Sitecore.Diagnostics.Log.Warn);
      switch (messageType)
      {
        case LogMessageType.Info:
          action = new Action<string, object>(Sitecore.Diagnostics.Log.Info);
          break;
        case LogMessageType.Audit:
          action = new Action<string, object>(Sitecore.Diagnostics.Log.Audit);
          break;
        case LogMessageType.Error:
          action = new Action<string, object>(Sitecore.Diagnostics.Log.Error);
          break;
      }
      action("[WFFM] " + message, owner ?? (object) this);
    }
  }
}
