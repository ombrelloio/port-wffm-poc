// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Core.Dependencies.DefaultImplWebUtil
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Form.Core.Utility;
using Sitecore.Web;
using Sitecore.WFFM.Abstractions.Utils;
using System;

namespace Sitecore.Forms.Core.Dependencies
{
  [Serializable]
  public class DefaultImplWebUtil : IWebUtil
  {
    public string GetTempFileName() => Sitecore.Form.Core.Utility.WebUtil.GetTempFileName();

    public string GetServerUrl() => Sitecore.Web.WebUtil.GetServerUrl();

    public string GetQueryString(string key, string defaultValue) => Sitecore.Web.WebUtil.GetQueryString(key, defaultValue);
  }
}
