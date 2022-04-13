// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.Pipelines.FormSubmit.SubmittedFormFailuresArgs
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Data;
using Sitecore.WFFM.Abstractions.Actions;
using System.Collections.Generic;
using System.Linq;

namespace Sitecore.Form.Core.Pipelines.FormSubmit
{
  public class SubmittedFormFailuresArgs : SubmitFailedArgs
  {
    public SubmittedFormFailuresArgs(ID formID, IEnumerable<ExecuteResult.Failure> failures)
      : base(formID)
    {
      this.Failures = failures.ToArray<ExecuteResult.Failure>();
    }

    public ExecuteResult.Failure[] Failures { get; private set; }

    public override string ErrorMessage
    {
      get => ((IEnumerable<ExecuteResult.Failure>) this.Failures).FirstOrDefault<ExecuteResult.Failure>().ErrorMessage;
      set
      {
      }
    }

    public override ID ActionFailed
    {
      get
      {
        string failedAction = ((IEnumerable<ExecuteResult.Failure>) this.Failures).FirstOrDefault<ExecuteResult.Failure>().FailedAction;
        ID id;
        return !string.IsNullOrEmpty(failedAction) && ID.TryParse(failedAction, out id) ? id : ID.Null;
      }
      protected set
      {
      }
    }
  }
}
