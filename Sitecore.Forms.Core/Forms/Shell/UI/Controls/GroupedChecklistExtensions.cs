// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Shell.UI.Controls.GroupedChecklistExtensions
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Collections;
using Sitecore.Diagnostics;
using Sitecore.Forms.Core.Data;
using Sitecore.WFFM.Abstractions.Dependencies;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace Sitecore.Forms.Shell.UI.Controls
{
  public static class GroupedChecklistExtensions
  {
    public static void Add(
      this GroupedChecklist list,
      ListField field,
      string formatPositiveMessage,
      string formatNegativeMessage)
    {
      Assert.ArgumentNotNull((object) list, nameof (list));
      Assert.ArgumentNotNull((object) field, nameof (field));
      Assert.ArgumentNotNullOrEmpty(formatPositiveMessage, nameof (formatPositiveMessage));
      Assert.ArgumentNotNullOrEmpty(formatNegativeMessage, nameof (formatNegativeMessage));
      if (field.IsEmpty)
        return;
      ListItem listItem = new ListItem(field.Field.FieldDisplayName, ((object) field.Field.ID.ToShortID()).ToString())
      {
        Attributes = {
          ["optgroup"] = "optgroup",
          ["class"] = "scWfmCheckboxOptGroup"
        }
      };
      listItem.Attributes["title"] = listItem.Text;
      list.Items.Add(listItem);
      foreach (Pair<string, string> pair in field.Values)
      {
        string text = Sitecore.StringExtensions.StringExtensions.FormatWith(pair.Part2.StartsWith("!") ? formatNegativeMessage : formatPositiveMessage, new object[1]
        {
          (object) pair.Part1
        });
        list.Items.Add(new ListItem(text, pair.Part2)
        {
          Attributes = {
            ["title"] = text,
            ["class"] = "scWfmCheckboxItem"
          }
        });
      }
    }

    public static void Add(this GroupedChecklist list, ListField field)
    {
      Assert.ArgumentNotNull((object) list, nameof (list));
      list.Add(field, DependenciesManager.ResourceManager.Localize("WHEN_IS_SELECTED"), DependenciesManager.ResourceManager.Localize("WHEN_IS_NOT_SELECTED"));
    }

    public static void AddRange(this GroupedChecklist list, IEnumerable<ListField> fields)
    {
      Assert.ArgumentNotNull((object) list, nameof (list));
      Assert.ArgumentNotNull((object) fields, nameof (fields));
      foreach (ListField field in fields)
      {
        if (field != null)
          list.Add(field);
      }
    }
  }
}
