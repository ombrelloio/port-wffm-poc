// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.Pipelines.FormSubmit.SubmitFailedArgs
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Data;
using Sitecore.Web.UI.Sheer;
using System;

namespace Sitecore.Form.Core.Pipelines.FormSubmit
{
  public class SubmitFailedArgs : ClientPipelineArgs
  {
    protected SubmitFailedArgs(ID formID)
      : this(formID, (Exception) null)
    {
    }

    protected SubmitFailedArgs(ID formID, Exception innerException)
      : this(formID, ID.Null, string.Empty, innerException)
    {
    }

    public SubmitFailedArgs(ID formID, ID actionFailed, Exception innerException)
      : this(formID, actionFailed, innerException.Message, innerException)
    {
      this.FormID = formID;
      this.InnerException = innerException;
      this.ActionFailed = actionFailed;
    }

    public SubmitFailedArgs(
      ID formID,
      ID actionFailed,
      string errorMessage,
      Exception innerException)
    {
      this.FormID = formID;
      this.ActionFailed = actionFailed;
      this.ErrorMessage = errorMessage;
      this.InnerException = innerException;
    }

    public ID FormID { get; protected set; }

    public virtual ID ActionFailed { get; protected set; }

    public virtual string ErrorMessage { get; set; }

    public Exception InnerException { get; protected set; }

    public string Database { get; set; }

    public bool UseDefaultMessage { get; set; }
  }
}
