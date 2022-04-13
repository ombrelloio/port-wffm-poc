// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.Submit.AuditSaveAction
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Diagnostics;
using Sitecore.Form.Core.Pipelines.AuditRender;
using Sitecore.Forms.Core.Data;
using Sitecore.Pipelines;
using Sitecore.WFFM.Actions.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sitecore.Form.Core.Submit
{
  [Serializable]
  public abstract class AuditSaveAction : WffmSaveAction
  {
    private readonly List<AuditEntry> updated = new List<AuditEntry>();
    private readonly List<AuditEntry> skipped = new List<AuditEntry>();
    private readonly List<string> messages = new List<string>();

    public void AuditMessage(string message)
    {
      Assert.ArgumentNotNullOrEmpty(message, nameof (message));
      this.messages.Add(message);
    }

    public void AuditUpdatedField(string takenFrom, string insertTo, string value) => this.AuditUpdatedField(new AuditEntry(takenFrom, insertTo, value));

    public void AuditSkippedField(string takenFrom, string insertTo, string value) => this.AuditSkippedField(new AuditEntry(takenFrom, insertTo, value));

    public void AuditUpdatedField(AuditEntry entry)
    {
      Assert.ArgumentNotNull((object) entry, nameof (entry));
      this.updated.Add(entry);
    }

    public void AuditSkippedField(AuditEntry entry)
    {
      Assert.ArgumentNotNull((object) entry, nameof (entry));
      this.skipped.Add(entry);
    }

    public string DumpAuditInfomration(string previousAudit)
    {
      if (this.CurrentForm == null)
        return previousAudit;
      AuditPipelineArgs auditPipelineArgs = new AuditPipelineArgs(this.CurrentForm, previousAudit ?? string.Empty, (IEnumerable<AuditEntry>) this.updated, (IEnumerable<AuditEntry>) this.skipped);
      auditPipelineArgs.Messages.AddRange(this.messages.Where<string>((Func<string, bool>) (m => !string.IsNullOrEmpty(m))));
      CorePipeline.Run("auditRender", (PipelineArgs) auditPipelineArgs);
      return auditPipelineArgs.Result ?? string.Empty;
    }

    public abstract FormItem CurrentForm { get; }
  }
}
