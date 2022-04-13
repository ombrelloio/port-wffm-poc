// Decompiled with JetBrains decompiler
// Type: Sitecore.WFFM.Abstractions.Shared.ILogger
// Assembly: Sitecore.WFFM.Abstractions, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 893E7AF0-E7C2-4A02-B47B-5687A98493FC
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.WFFM.Abstractions.dll

using Sitecore.WFFM.Abstractions.Dependencies;
using System;

namespace Sitecore.WFFM.Abstractions.Shared
{
  [DependencyPath("wffm/logger")]
  public interface ILogger
  {
    void Warn(string message, object owner);

    void Warn(string message, Exception exception, object owner);

    void Info(string message, object owner);

    void Audit(string message, object owner);

    bool IsNull(object obj, string name);

    void Log(string message, object owner, LogMessageType messageType);
  }
}
