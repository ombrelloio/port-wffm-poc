// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.Data.FormSubmitException
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Diagnostics;
using Sitecore.WFFM.Abstractions.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web.Services.Protocols;
using System.Xml;

namespace Sitecore.Form.Core.Data
{
  [Serializable]
  public class FormSubmitException : SoapException
  {
    public FormSubmitException(IEnumerable<ExecuteResult.Failure> failures)
    {
      Assert.ArgumentNotNull((object) failures, nameof (failures));
      this.Failures = failures;
    }

    public FormSubmitException(string message, string actionFailed, Exception innerException)
      : base(message, new XmlQualifiedName("wfm"), innerException)
    {
      Assert.ArgumentNotNullOrEmpty(message, nameof (message));
      Assert.ArgumentNotNullOrEmpty(actionFailed, nameof (actionFailed));
      this.Failures = (IEnumerable<ExecuteResult.Failure>) new List<ExecuteResult.Failure>()
      {
        new ExecuteResult.Failure()
        {
          ErrorMessage = message,
          FailedAction = actionFailed,
          IsCustom = false,
          StackTrace = string.Empty
        }
      };
    }

    public FormSubmitException(string message, string actionFailed)
      : this(message, actionFailed, (Exception) null)
    {
    }

    protected FormSubmitException()
    {
    }

    protected FormSubmitException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
      this.Failures = (IEnumerable<ExecuteResult.Failure>) (ExecuteResult.Failure[]) info.GetValue(nameof (Failures), typeof (ExecuteResult.Failure[]));
    }

    public string ActionFailed => this.Failures.FirstOrDefault<ExecuteResult.Failure>().FailedAction;

    public IEnumerable<ExecuteResult.Failure> Failures { get; private set; }

    public new string Message => this.Failures.FirstOrDefault<ExecuteResult.Failure>().ErrorMessage;

    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      base.GetObjectData(info, context);
      info.AddValue("Failures", (object) this.Failures.ToArray<ExecuteResult.Failure>(), typeof (ExecuteResult.Failure[]));
    }
  }
}
