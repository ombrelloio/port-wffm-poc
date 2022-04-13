// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Core.Wrappers.JobContextWrapper
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Diagnostics;
using Sitecore.Jobs.AsyncUI;
using System;

namespace Sitecore.Forms.Core.Wrappers
{
  [Serializable]
  public class JobContextWrapper : IJobContext
  {
    public object SendMessage(string message)
    {
      Assert.ArgumentNotNull((object) message, nameof (message));
      return JobContext.SendMessage(message);
    }
  }
}
