// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.Utility.SoapExceptionExtensions
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using System.Web.Services.Protocols;

namespace Sitecore.Form.Core.Utility
{
  public static class SoapExceptionExtensions
  {
    public static string GetFormatedMessage(this SoapException exception) => Sitecore.StringExtensions.StringExtensions.FormatWith("{0}: {1}", new object[2]
    {
      (object) exception.Detail.LastChild.SelectSingleNode("code").InnerText,
      (object) exception.Detail.LastChild.SelectSingleNode("description").InnerText
    });
  }
}
