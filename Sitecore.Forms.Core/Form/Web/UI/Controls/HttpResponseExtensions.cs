// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Web.UI.Controls.HttpResponseExtensions
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Diagnostics;
using System.Web;

namespace Sitecore.Form.Web.UI.Controls
{
  public static class HttpResponseExtensions
  {
    public static void ReturnOnly(this HttpResponse response, string responseText) => response.ReturnOnly(responseText, "application/json");

    public static void ReturnOnly(
      this HttpResponse response,
      string responseText,
      string responseType)
    {
      Assert.ArgumentNotNull((object) response, nameof (response));
      response.Clear();
      if (!string.IsNullOrEmpty(responseType))
        response.AddHeader("Content-type", responseType);
      response.Write(responseText);
      response.End();
    }
  }
}
