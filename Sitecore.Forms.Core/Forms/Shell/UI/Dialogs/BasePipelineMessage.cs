// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Shell.UI.Dialogs.BasePipelineMessage
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Web.UI.Sheer;
using System;

namespace Sitecore.Forms.Shell.UI.Dialogs
{
  [Serializable]
  public abstract class BasePipelineMessage
  {
    private BasePipelineMessage.ExecuteCallback callback;

    public virtual void Execute(BasePipelineMessage.ExecuteCallback callback)
    {
      this.callback = callback;
      Context.ClientPage.Start((object) this, "Pipeline");
    }

    protected virtual void Pipeline(ClientPipelineArgs args)
    {
      if (!args.IsPostBack)
      {
        this.ShowUI();
        Context.ClientPage.ClientResponse.Redraw();
        args.WaitForPostBack();
      }
      else
      {
        if (!args.HasResult || this.callback == null)
          return;
        this.callback(args.Result);
      }
    }

    protected abstract void ShowUI();

    public delegate void ExecuteCallback(string result);
  }
}
