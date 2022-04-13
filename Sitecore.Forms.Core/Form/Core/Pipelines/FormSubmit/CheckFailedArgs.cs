// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.Pipelines.FormSubmit.CheckFailedArgs
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Data;
using Sitecore.WFFM.Abstractions.Actions;
using System;
using System.Collections.Generic;

namespace Sitecore.Form.Core.Pipelines.FormSubmit
{
  public class CheckFailedArgs : SubmitFailedArgs
  {
    public CheckFailedArgs(
      ID formID,
      string actionFailed,
      ControlResult[] results,
      Exception innerException)
      : this(formID, ID.Parse(actionFailed), results, innerException)
    {
    }

    public CheckFailedArgs(
      ID formID,
      ID actionFailed,
      ControlResult[] results,
      Exception innerException)
      : base(formID, actionFailed, innerException)
    {
      this.Results = (IEnumerable<ControlResult>) results;
    }

    public IEnumerable<ControlResult> Results { get; private set; }
  }
}
