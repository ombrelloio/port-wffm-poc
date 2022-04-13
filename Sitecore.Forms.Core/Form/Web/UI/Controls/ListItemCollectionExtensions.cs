// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Web.UI.Controls.ListItemCollectionExtensions
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace Sitecore.Form.Web.UI.Controls
{
  public static class ListItemCollectionExtensions
  {
    public static IEnumerable<ListItem> Where(
      this System.Web.UI.WebControls.ListItemCollection list,
      Func<ListItem, bool> condition)
    {
      Assert.ArgumentNotNull((object) list, nameof (list));
      List<ListItem> listItemList = new List<ListItem>();
      foreach (ListItem listItem in list)
      {
        if (condition(listItem))
          listItemList.Add(listItem);
      }
      return (IEnumerable<ListItem>) listItemList;
    }

    public static void ForEach(this System.Web.UI.WebControls.ListItemCollection collection, Action<ListItem> action)
    {
      foreach (ListItem listItem in collection)
        action(listItem);
    }

    public static ListItem FirstOrDefault(
      this System.Web.UI.WebControls.ListItemCollection collection,
      Func<ListItem, bool> func)
    {
      foreach (ListItem listItem in collection)
      {
        if (func(listItem))
          return listItem;
      }
      return (ListItem) null;
    }

    public static ListItem FindByValue(
      this System.Web.UI.WebControls.ListItemCollection collection,
      string value,
      bool ignoreCase)
    {
      return !ignoreCase ? collection.FindByValue(value) : collection.FirstOrDefault((Func<ListItem, bool>) (i => string.Compare(i.Value, value, true) == 0));
    }

    public static ListItem FindByText(
      this System.Web.UI.WebControls.ListItemCollection collection,
      string text,
      bool ignoreCase)
    {
      return !ignoreCase ? collection.FindByText(text) : collection.FirstOrDefault((Func<ListItem, bool>) (i => string.Compare(i.Text, text, true) == 0));
    }

    public static System.Web.UI.WebControls.ListItemCollection AddEmptyChoice(
      this System.Web.UI.WebControls.ListItemCollection list)
    {
      ListItem listItem = new ListItem(string.Empty, string.Empty);
      list.Insert(0, listItem);
      return list;
    }

    public static void Disable(this System.Web.UI.WebControls.ListItemCollection list, int itemIndex)
    {
      if (list.Count <= itemIndex)
        return;
      list[itemIndex].Enabled = false;
    }

    public static void Enable(this System.Web.UI.WebControls.ListItemCollection list, int itemIndex)
    {
      if (list.Count <= itemIndex)
        return;
      list[itemIndex].Enabled = true;
    }

    public static System.Web.UI.WebControls.ListItemCollection DisableAll(
      this System.Web.UI.WebControls.ListItemCollection list)
    {
      list.ForEach((Action<ListItem>) (i => i.Enabled = false));
      return list;
    }

    public static System.Web.UI.WebControls.ListItemCollection EnableEach(
      this System.Web.UI.WebControls.ListItemCollection list,
      string values)
    {
      return list.EnableEach(values.Split('|'));
    }

    public static System.Web.UI.WebControls.ListItemCollection EnableEach(
      this System.Web.UI.WebControls.ListItemCollection list,
      string[] allowedItems)
    {
      if (allowedItems != null && allowedItems.Length != 0)
      {
        foreach (string allowedItem in allowedItems)
        {
          string value = allowedItem;
          list.EnableEach((Func<ListItem, bool>) (i => string.Compare(i.Value, value, true) == 0 || allowedItems[0] == "*"));
        }
      }
      return list;
    }

    public static System.Web.UI.WebControls.ListItemCollection EnableEach(
      this System.Web.UI.WebControls.ListItemCollection list,
      Func<ListItem, bool> func)
    {
      foreach (ListItem listItem in list)
      {
        if (func(listItem))
          listItem.Enabled = true;
      }
      return list;
    }

    public static void AddItems(this System.Web.UI.WebControls.ListItemCollection list, string[] values)
    {
      foreach (string str in values)
        list.AddItem(str);
    }

    public static System.Web.UI.WebControls.ListItemCollection AddItem(
      this System.Web.UI.WebControls.ListItemCollection list,
      string value)
    {
      return list.AddItem(value, value);
    }

    public static System.Web.UI.WebControls.ListItemCollection AddItem(
      this System.Web.UI.WebControls.ListItemCollection list,
      string text,
      string value)
    {
      if (!string.IsNullOrEmpty(value))
      {
        ListItem listItem = new ListItem(text, value);
        list.Add(listItem);
      }
      return list;
    }

    public static System.Web.UI.WebControls.ListItemCollection Insert(
      this System.Web.UI.WebControls.ListItemCollection list,
      int position,
      string value)
    {
      if (!string.IsNullOrEmpty(value))
      {
        ListItem listItem = new ListItem(value, value);
        list.Insert(position, listItem);
      }
      return list;
    }
  }
}
