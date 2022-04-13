// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Core.Dependencies.SitecoreItemWrapper
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.WFFM.Abstractions.Data;
using System;

namespace Sitecore.Forms.Core.Dependencies
{
  [Serializable]
  public class SitecoreItemWrapper : ISitecoreItem
  {
    private readonly Item item;

    public SitecoreItemWrapper(Item item)
    {
      Assert.ArgumentNotNull((object) item, nameof (item));
      this.item = item;
    }

    public string Name
    {
      get => this.item.Name;
      set => this.item.Name = value;
    }
  }
}
