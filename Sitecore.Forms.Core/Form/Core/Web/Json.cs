// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.Web.Json
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Newtonsoft.Json;
using Sitecore.Diagnostics;
using System;

namespace Sitecore.Form.Core.Web
{
  public class Json
  {
    private static IJson instance;

    static Json() => Sitecore.Form.Core.Web.Json.Initialize();

    private Json()
    {
    }

    public static IJson Instance
    {
      get => Sitecore.Form.Core.Web.Json.instance;
      set
      {
        Assert.ArgumentNotNull((object) value, nameof (value));
        Sitecore.Form.Core.Web.Json.instance = value;
      }
    }

    private static void Initialize() => Sitecore.Form.Core.Web.Json.Instance = (IJson) new Sitecore.Form.Core.Web.Json.Newtonsoft35Converter();

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
