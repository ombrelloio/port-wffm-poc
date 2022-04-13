// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Shell.UI.Dialogs.ConfirmationMessage
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using System;

namespace Sitecore.Forms.Shell.UI.Dialogs
{
  [Serializable]
  public class ConfirmationMessage : BasePipelineMessage
  {
    private readonly string message;

    public ConfirmationMessage(string message) => this.message = message;

    protected override void ShowUI() => Context.ClientPage.ClientResponse.Confirm(this.message);
  }
}
