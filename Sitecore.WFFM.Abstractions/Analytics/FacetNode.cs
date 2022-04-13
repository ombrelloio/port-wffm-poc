// Decompiled with JetBrains decompiler
// Type: Sitecore.WFFM.Abstractions.Analytics.FacetNode
// Assembly: Sitecore.WFFM.Abstractions, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 893E7AF0-E7C2-4A02-B47B-5687A98493FC
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.WFFM.Abstractions.dll

using Sitecore.Diagnostics;

namespace Sitecore.WFFM.Abstractions.Analytics
{
  public class FacetNode
  {
    public FacetNode(string key, string path, string value)
    {
      Assert.ArgumentNotNullOrEmpty(key, nameof (key));
      Assert.ArgumentNotNullOrEmpty(path, nameof (path));
      Assert.ArgumentNotNullOrEmpty(value, nameof (value));
      this.Path = path;
      this.Key = key;
      this.Value = value;
    }

    public string Value { get; private set; }

    public string Path { get; private set; }

    public string Key { get; private set; }
  }
}
