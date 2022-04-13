// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.Pipelines.FormSubmit.SubmitSuccessArgs
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Forms.Core.Data;
using Sitecore.Web.UI.Sheer;

namespace Sitecore.Form.Core.Pipelines.FormSubmit
{
  public class SubmitSuccessArgs : ClientPipelineArgs
  {
    private FormItem form;

    public SubmitSuccessArgs(FormItem form) => this.form = form;

    public SubmitSuccessArgs(string message) => this.Result = message;

    public FormItem Form => this.form;
  }
}
