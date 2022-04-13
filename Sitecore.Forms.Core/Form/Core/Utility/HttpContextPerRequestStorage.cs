// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.Utility.HttpContextPerRequestStorage
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Diagnostics;
using Sitecore.WFFM.Abstractions.Shared;
using System.Web;

namespace Sitecore.Form.Core.Utility
{
  public class HttpContextPerRequestStorage : IPerRequestStorage
  {
    public object Get(string key)
    {
      Assert.ArgumentNotNull((object) key, nameof (key));
      return HttpContext.Current.Items[(object) key];
    }

    public void Put(string key, object value)
    {
      Assert.ArgumentNotNull((object) key, nameof (key));
      Assert.ArgumentNotNull(value, nameof (value));
      HttpContext.Current.Items[(object) key] = value;
    }
  }
}
