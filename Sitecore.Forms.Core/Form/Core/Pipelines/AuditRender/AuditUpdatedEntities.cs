// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.Pipelines.AuditRender.AuditUpdatedEntities
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Diagnostics;
using Sitecore.WFFM.Abstractions.Dependencies;

namespace Sitecore.Form.Core.Pipelines.AuditRender
{
  public class AuditUpdatedEntities
  {
    public void Process(AuditPipelineArgs args)
    {
      Assert.ArgumentNotNull((object) args, nameof (args));
      foreach (AuditEntry auditEntry in args.Updated)
      {
        if (string.IsNullOrEmpty(auditEntry.FormatMessage))
          args.Current.AppendLine(DependenciesManager.ResourceManager.Localize("AUDIT_NEW_ENTRY", auditEntry.InsertTo, auditEntry.TakenFrom, auditEntry.Value ?? string.Empty));
        else
          args.Current.AppendLine(Sitecore.StringExtensions.StringExtensions.FormatWith(auditEntry.FormatMessage, new object[3]
          {
            (object) auditEntry.InsertTo,
            (object) auditEntry.TakenFrom,
            (object) (auditEntry.Value ?? string.Empty)
          }));
      }
    }
  }
}
