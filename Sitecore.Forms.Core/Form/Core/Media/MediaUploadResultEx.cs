// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.Media.MediaUploadResultEx
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Data.Items;
using Sitecore.Diagnostics;

namespace Sitecore.Form.Core.Media
{
  public class MediaUploadResultEx
  {
    private Item _item;
    private string _path;
    private string _validMediaPath;

    public Item Item
    {
      get => this._item;
      internal set
      {
        Assert.ArgumentNotNull((object) value, nameof (value));
        this._item = value;
      }
    }

    public string Path
    {
      get => this._path;
      internal set
      {
        Assert.ArgumentNotNull((object) value, nameof (value));
        this._path = value;
      }
    }

    public string ValidMediaPath
    {
      get => this._validMediaPath;
      internal set
      {
        Assert.ArgumentNotNull((object) value, nameof (value));
        this._validMediaPath = value;
      }
    }
  }
}
