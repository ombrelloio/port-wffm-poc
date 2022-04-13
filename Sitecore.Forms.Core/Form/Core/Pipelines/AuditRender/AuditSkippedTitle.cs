// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.Pipelines.AuditRender.AuditSkippedTitle
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Diagnostics;
using Sitecore.WFFM.Abstractions.Dependencies;
using System.Linq;

namespace Sitecore.Form.Core.Pipelines.AuditRender
{
  public class AuditSkippedTitle
  {
    public void Process(AuditPipelineArgs args)
    {
      Assert.ArgumentNotNull((object) args, nameof (args));
      if (!args.Skipped.Any<AuditEntry>())
        return;
      args.Current.AppendLine(DependenciesManager.ResourceManager.Localize("AUDIT_NOT_OVERWRITE_START", args.FormItem != null ? args.FormItem.FormName : string.Empty));
    }
  }
}
