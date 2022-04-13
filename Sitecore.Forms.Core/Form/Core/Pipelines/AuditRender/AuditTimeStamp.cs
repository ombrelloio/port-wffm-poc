// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.Pipelines.AuditRender.AuditTimeStamp
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Diagnostics;
using System;
using System.Linq;

namespace Sitecore.Form.Core.Pipelines.AuditRender
{
  public class AuditTimeStamp
  {
    public void Process(AuditPipelineArgs args)
    {
      Assert.ArgumentNotNull((object) args, nameof (args));
      if (args.Updated.Count<AuditEntry>() <= 0 && args.Skipped.Count<AuditEntry>() <= 0 && args.Messages.Count <= 0)
        return;
      args.Current.AppendLine(string.Join(string.Empty, new string[2]
      {
        DateTime.UtcNow.ToString(),
        " : "
      }));
    }
  }
}
