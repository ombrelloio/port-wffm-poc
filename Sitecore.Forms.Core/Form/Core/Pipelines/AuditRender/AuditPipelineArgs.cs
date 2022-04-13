// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.Pipelines.AuditRender.AuditPipelineArgs
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Forms.Core.Data;
using Sitecore.Web.UI.Sheer;
using System.Collections.Generic;
using System.Text;

namespace Sitecore.Form.Core.Pipelines.AuditRender
{
  public class AuditPipelineArgs : ClientPipelineArgs
  {
    public AuditPipelineArgs(
      FormItem form,
      string previous,
      IEnumerable<AuditEntry> updated,
      IEnumerable<AuditEntry> skipped)
    {
      this.FormItem = form;
      this.Current = new StringBuilder();
      this.Previouse = previous ?? string.Empty;
      this.Updated = updated ?? (IEnumerable<AuditEntry>) new List<AuditEntry>();
      this.Skipped = skipped ?? (IEnumerable<AuditEntry>) new List<AuditEntry>();
      this.Messages = new List<string>();
    }

    public string Previouse { get; set; }

    public IEnumerable<AuditEntry> Updated { get; private set; }

    public IEnumerable<AuditEntry> Skipped { get; private set; }

    public StringBuilder Current { get; private set; }

    public List<string> Messages { get; private set; }

    public new string Result => string.Join(string.Empty, new string[2]
    {
      this.Current.ToString(),
      this.Previouse
    });

    public FormItem FormItem { get; private set; }
  }
}
