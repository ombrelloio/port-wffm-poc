// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Shell.UI.Dialogs.ClientDialogs
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using System;

namespace Sitecore.Forms.Shell.UI.Dialogs
{
  [Serializable]
  public static class ClientDialogs
  {
    public static void Confirmation(string message, BasePipelineMessage.ExecuteCallback callback) => new ConfirmationMessage(message).Execute(callback);
  }
}
