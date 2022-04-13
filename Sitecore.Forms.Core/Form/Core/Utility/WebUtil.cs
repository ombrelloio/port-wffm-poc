// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.Utility.WebUtil
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Configuration;
using Sitecore.Diagnostics;
using System;
using System.Collections.Specialized;
using System.IO;
using System.Web;
using System.Web.UI;

namespace Sitecore.Form.Core.Utility
{
  public class WebUtil
  {
    public static Control CreateUserControl(Page systemPage, string reference) => (systemPage ?? WebUtil.GetPage())?.LoadControl(reference);

    public static void ExecuteForAllControls(Control container, Action<Control> action)
    {
      action(container);
      foreach (Control control in container.Controls)
        WebUtil.ExecuteForAllControls(control, action);
    }

    public static Control FindFirstOrDefault(Control container, Func<Control, bool> func)
    {
      Control control1 = (Control) null;
      if (func(container))
        return container;
      foreach (Control control2 in container.Controls)
      {
        control1 = WebUtil.FindFirstOrDefault(control2, func);
        if (control1 != null)
          break;
      }
      return control1;
    }

    public static Page GetPage() => HttpContext.Current != null && HttpContext.Current.Handler != null && HttpContext.Current.Handler is Page ? (Page) HttpContext.Current.Handler : new Page();

    public static T GetParent<T>(Control control) where T : Control
    {
      Assert.ArgumentNotNull((object) control, nameof (control));
      Control parent = control.Parent;
      while (true)
      {
        switch (parent)
        {
          case null:
          case T _:
            goto label_3;
          default:
            parent = parent.Parent;
            continue;
        }
      }
label_3:
      return (T) parent;
    }

    public static Control GetPostBackEventHandler(Page page)
    {
      Assert.ArgumentNotNull((object) page, nameof (page));
      foreach (string id in (NameObjectCollectionBase) page.Request.Params)
      {
        if (!string.IsNullOrEmpty(id))
        {
          Control control = page.FindControl(id);
          if (control != null && control is IPostBackEventHandler)
            return control;
        }
      }
      return (Control) null;
    }

    public static string GetTempFileName()
    {
      string path;
      do
      {
        path = Path.Combine(MainUtil.MapPath(Settings.TempFolderPath), Path.GetRandomFileName());
      }
      while (File.Exists(path));
      return path;
    }
  }
}
