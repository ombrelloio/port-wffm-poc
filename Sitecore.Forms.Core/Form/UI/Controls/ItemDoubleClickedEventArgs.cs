// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.UI.Controls.ItemDoubleClickedEventArgs
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Diagnostics;
using System;

namespace Sitecore.Form.UI.Controls
{
  public class ItemDoubleClickedEventArgs : EventArgs
  {
    public ItemDoubleClickedEventArgs(string id)
    {
      Assert.ArgumentNotNullOrEmpty(id, nameof (id));
      this.ItemID = id;
    }

    public string ItemID { get; private set; }
  }
}
