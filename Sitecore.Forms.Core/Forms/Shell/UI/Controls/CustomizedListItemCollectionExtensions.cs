// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Shell.UI.Controls.CustomizedListItemCollectionExtensions
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Form.Core.Configuration;
using Sitecore.Form.Core.ContentEditor.Data;
using Sitecore.Form.Core.Data;
using Sitecore.Form.Web.UI.Controls;
using Sitecore.Forms.Core.Data;
using Sitecore.WFFM.Abstractions.ContentEditor;
using Sitecore.WFFM.Abstractions.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;

namespace Sitecore.Forms.Shell.UI.Controls
{
  public static class CustomizedListItemCollectionExtensions
  {
    public static System.Web.UI.WebControls.ListItemCollection LoadItemsFromForm(
      this System.Web.UI.WebControls.ListItemCollection list,
      FormItem form)
    {
      return list.LoadItemsFromForm(form, "{0}");
    }

    public static System.Web.UI.WebControls.ListItemCollection LoadItemsFromForm(
      this System.Web.UI.WebControls.ListItemCollection list,
      FormItem form,
      string formatTextMessage)
    {
      return list.LoadItemsFromForm(form, formatTextMessage, string.Empty);
    }

    public static System.Web.UI.WebControls.ListItemCollection LoadItemsFromForm(
      this System.Web.UI.WebControls.ListItemCollection list,
      FormItem form,
      string formatTextMessage,
      string allowedTypes)
    {
      System.Web.UI.WebControls.ListItemCollection list1 = list;
      FormItem form1 = form;
      string formatTextMessage1 = formatTextMessage;
      string[] allowedTypes1;
      if (!string.IsNullOrEmpty(allowedTypes))
        allowedTypes1 = allowedTypes.Split('|');
      else
        allowedTypes1 = (string[]) null;
      return list1.LoadItemsFromForm(form1, formatTextMessage1, allowedTypes1);
    }

    public static System.Web.UI.WebControls.ListItemCollection LoadItemsFromForm(
      this System.Web.UI.WebControls.ListItemCollection list,
      FormItem form,
      string formatTextMessage,
      string[] allowedTypes)
    {
      if (form != null)
      {
        foreach (IFieldItem field1 in form.Fields)
        {
          IFieldItem field = field1;
          if (allowedTypes == null || allowedTypes.Length == 0 || allowedTypes[0] == "*" || ((IEnumerable<string>) allowedTypes).FirstOrDefault<string>((Func<string, bool>) (a => a == ((object) field.TypeID).ToString())) != null)
          {
            ListItem listItem = new ListItem(Sitecore.StringExtensions.StringExtensions.FormatWith(formatTextMessage, new object[1]
            {
              (object) field.FieldDisplayName
            }), ((object) field.ID).ToString());
            list.Add(listItem);
          }
        }
      }
      return list;
    }

    public static System.Web.UI.WebControls.ListItemCollection LoadItemsFromActions(
      this System.Web.UI.WebControls.ListItemCollection list,
      string actionXml,
      Func<ActionItem, string, bool> func)
    {
      Assert.ArgumentNotNull((object) actionXml, nameof (actionXml));
      foreach (IListItemDefinition listItem in ListDefinition.Parse(actionXml).Groups.First<IGroupDefinition>().ListItems)
      {
        Item obj = StaticSettings.ContextDatabase.GetItem(listItem.ItemID);
        if (obj != null && func(new ActionItem(obj), listItem.Unicid))
          list.AddItem(string.Join(string.Empty, new string[2]
          {
            "$",
            listItem.GetTitle()
          }), listItem.Unicid);
      }
      return list;
    }

    public static System.Web.UI.WebControls.ListItemCollection EnableFieldTypes(
      this System.Web.UI.WebControls.ListItemCollection list,
      string allowedTypes)
    {
      Assert.ArgumentNotNull((object) list, nameof (list));
      if (!string.IsNullOrEmpty(allowedTypes))
      {
        if (string.IsNullOrEmpty(allowedTypes) || allowedTypes == "*")
        {
          list.EnableEach((Func<ListItem, bool>) (i => true));
          return list;
        }
        string[] parts = allowedTypes.Split('|');
        list.EnableEach((Func<ListItem, bool>) (i => StaticSettings.ContextDatabase.GetItem(i.Value) == null || ((IEnumerable<string>) parts).Contains<string>(((BaseItem) StaticSettings.ContextDatabase.GetItem(i.Value))[Sitecore.Form.Core.Configuration.FieldIDs.FieldLinkTypeID])));
      }
      return list;
    }
  }
}
