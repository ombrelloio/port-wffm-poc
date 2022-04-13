// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.Web.IJson
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

namespace Sitecore.Form.Core.Web
{
  public interface IJson
  {
    T DeserializeObject<T>(string value);

    bool TryDeserializeObject<T>(string value, out T t);
  }
}
