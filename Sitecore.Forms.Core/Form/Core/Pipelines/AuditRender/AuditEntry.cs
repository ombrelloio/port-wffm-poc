// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.Pipelines.AuditRender.AuditEntry
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Diagnostics;
using System;

namespace Sitecore.Form.Core.Pipelines.AuditRender
{
  [Serializable]
  public class AuditEntry
  {
    public AuditEntry(string takenFrom, string insertTo, string value)
    {
      Assert.ArgumentNotNullOrEmpty(takenFrom, nameof (takenFrom));
      Assert.ArgumentNotNull((object) value, nameof (value));
      Assert.ArgumentNotNullOrEmpty(insertTo, nameof (insertTo));
      this.TakenFrom = takenFrom;
      this.Value = value;
      this.InsertTo = insertTo;
    }

    public string TakenFrom { get; private set; }

    public string Value { get; private set; }

    public string InsertTo { get; private set; }

    public string FormatMessage { get; set; }
  }
}
