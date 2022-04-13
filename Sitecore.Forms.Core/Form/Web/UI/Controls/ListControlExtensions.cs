// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Web.UI.Controls.ListControlExtensions
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace Sitecore.Form.Web.UI.Controls
{
  public static class ListControlExtensions
  {
    public static void Select(this System.Web.UI.WebControls.ListControl list, Func<ListItem, bool> condition)
    {
      Assert.ArgumentNotNull((object) list, nameof (list));
      foreach (ListItem listItem in list.Items.Where(condition))
        listItem.Selected = true;
    }

    public static void Select(this System.Web.UI.WebControls.ListControl list, string value) => list.Select(value, true);

    public static void Select(this System.Web.UI.WebControls.ListControl list, string value, bool useTextIfNoValue)
    {
      ListItem listItem = list.Items.FindByValue(value, true) ?? list.Items.FindByText(value, true);
      list.SelectedIndex = listItem != null ? list.Items.IndexOf(listItem) : -1;
    }

    public static void SelectRange(this System.Web.UI.WebControls.ListControl list, string values)
    {
      if (string.IsNullOrEmpty(values))
        return;
      list.SelectRange((IEnumerable<string>) values.Split('|'));
    }

    public static string GetEnabledSelectedValue(this System.Web.UI.WebControls.ListControl list)
    {
      Assert.ArgumentNotNull((object) list, nameof (list));
      ListItem listItem = list.Items.FirstOrDefault((Func<ListItem, bool>) (i => i.Selected && i.Enabled));
      return listItem != null ? listItem.Value : string.Empty;
    }

    public static void SelectRange(this System.Web.UI.WebControls.ListControl list, IEnumerable<string> values)
    {
      Assert.ArgumentNotNull((object) list, nameof (list));
      foreach (string str in values)
      {
        if (!string.IsNullOrEmpty(str))
        {
          ListItem byValue = list.Items.FindByValue(str);
          if (byValue != null && byValue.Enabled)
            byValue.Selected = true;
        }
      }
    }

    public static IEnumerable<ListItem> GetSelectedItems(this System.Web.UI.WebControls.ListControl list) => list.Items.Where((Func<ListItem, bool>) (i => i.Selected));

    public static void ClearSelection(this System.Web.UI.WebControls.ListControl list)
    {
      Assert.ArgumentNotNull((object) list, nameof (list));
      list.Items.ForEach((Action<ListItem>) (i => i.Selected = false));
    }
  }
}
