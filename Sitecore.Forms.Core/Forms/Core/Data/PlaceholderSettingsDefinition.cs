// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Core.Data.PlaceholderSettingsDefinition
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Form.Core.Configuration;
using Sitecore.Security.Accounts;
using Sitecore.Text;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sitecore.Forms.Core.Data
{
  public class PlaceholderSettingsDefinition : CustomItemBase
  {
    public PlaceholderSettingsDefinition(Item item)
      : base(item)
    {
    }

    public List<string> AllowedControls => new List<string>((IEnumerable<string>) ((BaseItem) this.InnerItem).Fields[Sitecore.Form.Core.Configuration.FieldIDs.AllowedControl].Value.Split('|'));

    public static string[] GetPlaceholdersNameWithAllowedControl(
      string controlID,
      bool onlyForCanWrite)
    {
      Item[] withAllowedControl = PlaceholderSettingsDefinition.GetPlaceholdersWithAllowedControl(controlID);
      List<string> stringList = new List<string>();
      if (withAllowedControl != null)
        stringList.AddRange(((IEnumerable<Item>) withAllowedControl).Where<Item>((Func<Item, bool>) (item => !onlyForCanWrite || item.Security.CanWrite((Account) Context.User))).Select<Item, string>((Func<Item, string>) (item => item.Name.ToLower())));
      return stringList.ToArray();
    }

    public static Item[] GetPlaceholdersWithAllowedControl(string controlID)
    {
      Item obj = StaticSettings.ContextDatabase.GetItem((ID) ItemIDs.PlaceholderSettingsRoot);
      Assert.IsNotNull((object) obj, "placeholders root");
      return obj.Axes.SelectItems(string.Format(".//*[contains(@Allowed Controls, '{0}')]", (object) controlID));
    }

    public static Item[] GetPlaceholdersWithAllowedControl(ID controlID) => PlaceholderSettingsDefinition.GetPlaceholdersWithAllowedControl(((object) controlID).ToString());

    public void AddControl(string id)
    {
      if (this.InnerItem == null)
        return;
      ListString listString = new ListString(((BaseItem) this.InnerItem)["Allowed Controls"]);
      if (!listString.Contains(id))
        listString.Add(id);
      this.InnerItem.Editing.BeginEdit();
      ((BaseItem) this.InnerItem)["Allowed Controls"] = ((object) listString).ToString();
      this.InnerItem.Editing.EndEdit();
    }

    public void RemoveControl(string id)
    {
      if (this.InnerItem == null)
        return;
      ListString listString = new ListString(((BaseItem) this.InnerItem).Fields[Sitecore.Form.Core.Configuration.FieldIDs.AllowedControl].Value);
      while (listString.Contains(id))
        listString.Remove(id);
      this.InnerItem.Editing.BeginEdit();
      ((BaseItem) this.InnerItem)["Allowed Controls"] = ((object) listString).ToString();
      this.InnerItem.Editing.EndEdit();
    }
  }
}
