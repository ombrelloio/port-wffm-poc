// Decompiled with JetBrains decompiler
// Type: Sitecore.WFFM.Analytics.Core.IJson
// Assembly: Sitecore.WFFM.Analytics, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1C8CEE62-02B4-499C-A460-15C00E7B925E
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.WFFM.Analytics.dll

namespace Sitecore.WFFM.Analytics.Core
{
  public interface IJson
  {
    T DeserializeObject<T>(string value);

    bool TryDeserializeObject<T>(string value, out T t);
  }
}
