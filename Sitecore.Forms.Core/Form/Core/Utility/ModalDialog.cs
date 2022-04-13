// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.Utility.ModalDialog
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Text;
using Sitecore.Web.UI.Sheer;

namespace Sitecore.Form.Core.Utility
{
  public class ModalDialog
  {
    public static void Show(UrlString url) => ModalDialog.Show(url, string.Empty);

    public static void Show(UrlString url, string query)
    {
      url.Append(query);
      if (!string.IsNullOrEmpty(url["w"]) && !string.IsNullOrEmpty(url["h"]))
        SheerResponse.ShowModalDialog(((object) url).ToString(), url["w"], url["h"], "", true);
      else
        SheerResponse.ShowModalDialog(((object) url).ToString(), true);
    }
  }
}
