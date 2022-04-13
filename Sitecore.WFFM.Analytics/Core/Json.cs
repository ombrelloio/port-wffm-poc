// Decompiled with JetBrains decompiler
// Type: Sitecore.WFFM.Analytics.Core.Json
// Assembly: Sitecore.WFFM.Analytics, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1C8CEE62-02B4-499C-A460-15C00E7B925E
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.WFFM.Analytics.dll

using Newtonsoft.Json;
using Sitecore.Diagnostics;
using System;

namespace Sitecore.WFFM.Analytics.Core
{
  public class Json
  {
    private static IJson instance;

    static Json() => Sitecore.WFFM.Analytics.Core.Json.Initialize();

    private Json()
    {
    }

    public static IJson Instance
    {
      get => Sitecore.WFFM.Analytics.Core.Json.instance;
      set
      {
        Assert.ArgumentNotNull((object) value, nameof (value));
        Sitecore.WFFM.Analytics.Core.Json.instance = value;
      }
    }

    private static void Initialize() => Sitecore.WFFM.Analytics.Core.Json.Instance = (IJson) new Sitecore.WFFM.Analytics.Core.Json.Newtonsoft35Converter();

    private class Newtonsoft35Converter : IJson
    {
      public string NaN;

      public Newtonsoft35Converter() => this.NaN = (string) JsonConvert.NaN;

      public T DeserializeObject<T>(string value) => JsonConvert.DeserializeObject<T>(value);

      public bool TryDeserializeObject<T>(string value, out T t)
      {
        t = default (T);
        try
        {
          t = this.DeserializeObject<T>(value);
          return true;
        }
        catch (Exception ex)
        {
          return false;
        }
      }
    }
  }
}
