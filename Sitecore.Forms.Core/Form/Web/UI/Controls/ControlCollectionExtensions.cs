// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Web.UI.Controls.ControlCollectionExtensions
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Diagnostics;
using System;
using System.Web.UI;

namespace Sitecore.Form.Web.UI.Controls
{
  public static class ControlCollectionExtensions
  {
    public static Control FirstOrDefault(
      this ControlCollection collection,
      Func<Control, bool> func,
      bool deep)
    {
      Assert.ArgumentNotNull((object) collection, nameof (collection));
      Control control1 = (Control) null;
      foreach (Control control2 in collection)
      {
        if (func(control2))
          return control2;
        if (deep)
          control1 = control2.Controls.FirstOrDefault(func, true);
        if (control1 != null)
          return control1;
      }
      return (Control) null;
    }

    public static Control FirstOrDefault(
      this ControlCollection collection,
      Func<Control, bool> func)
    {
      return collection.FirstOrDefault(func, false);
    }

    public static Control LastOrDefault(
      this ControlCollection collection,
      Func<Control, bool> func)
    {
      Assert.ArgumentNotNull((object) collection, nameof (collection));
      for (int count = collection.Count; count > 0; --count)
      {
        if (func(collection[count - 1]))
          return collection[count - 1];
      }
      return (Control) null;
    }

    public static void ForEach(this ControlCollection collection, Action<Control> action)
    {
      Assert.ArgumentNotNull((object) collection, nameof (collection));
      foreach (Control control in collection)
        action(control);
    }
  }
}
