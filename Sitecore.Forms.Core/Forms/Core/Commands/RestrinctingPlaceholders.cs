// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Core.Commands.RestrinctingPlaceholders
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Form.Core.Configuration;
using Sitecore.Form.Core.Utility;
using Sitecore.Forms.Core.Data;
using Sitecore.Shell.Framework.Commands;
using Sitecore.Text;
using Sitecore.Web;
using Sitecore.Web.UI.Sheer;
using Sitecore.WFFM.Abstractions.Dependencies;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sitecore.Forms.Core.Commands
{
  [Serializable]
  public class RestrinctingPlaceholders : Command
  {
    public override void Execute(CommandContext context) => Context.ClientPage.Start((object) this, "Run");

    protected void Run(ClientPipelineArgs args)
    {
      Item obj = StaticSettings.ContextDatabase.GetItem(IDs.SettingsRoot);
      Assert.ArgumentNotNull((object) obj, "item");
      if (args.IsPostBack)
      {
        if (!args.HasResult)
          return;
        string oldValue = RestrinctingPlaceholders.GetValue();
        obj.Editing.BeginEdit();
        ((BaseItem) obj).Fields[FormIDs.ModuleSettingsID].Value = args.Result;
        obj.Editing.EndEdit();
        this.UpdateAllowedRenderings(oldValue, args.Result);
        Log.Audit((object) this, "Set the following restricting placeholders: {0}", new string[1]
        {
          AuditFormatter.FormatItem(obj)
        });
      }
      else
      {
        UrlString urlString = new UrlString(UIUtil.GetUri("control:Forms.CustomizeTreeListDialog"));
        new UrlHandle()
        {
          ["value"] = RestrinctingPlaceholders.GetValue(),
          ["source"] = Sitecore.WFFM.Abstractions.Constants.Core.Constants.RestrinctingPlaceholders,
          ["language"] = obj.Language.Name,
          ["includetemplatesforselection"] = "Placeholder",
          ["includetemplatesfordisplay"] = "Placeholder Settings Folder,Placeholder",
          ["includeitemsfordisplay"] = string.Empty,
          ["excludetemplatesforselection"] = "Placeholder Settings Folder",
          ["icon"] = "Business/32x32/table_selection_block.png",
          ["title"] = DependenciesManager.ResourceManager.Localize("RESTRINCTING_PLACEHOLDERS"),
          ["text"] = DependenciesManager.ResourceManager.Localize("RESTRINCTING_PLACEHOLDERS_TEXT")
        }.Add(urlString);
        SheerResponse.ShowModalDialog(((object) urlString).ToString(), "800px", "500px", string.Empty, true);
        args.WaitForPostBack();
      }
    }

    protected void UpdateAllowedRenderings(string oldValue, string newValue)
    {
      List<string> stringList1 = new List<string>((IEnumerable<string>) oldValue.Split('|'));
      List<string> stringList2 = new List<string>((IEnumerable<string>) newValue.Split('|'));
      foreach (string str in stringList1)
      {
        if (!stringList2.Contains(str))
        {
          Item obj = StaticSettings.ContextDatabase.GetItem(str);
          if (obj != null)
          {
            PlaceholderSettingsDefinition settingsDefinition = new PlaceholderSettingsDefinition(obj);
            settingsDefinition.RemoveControl(((object) IDs.FormInterpreterID).ToString());
            settingsDefinition.RemoveControl(((object) IDs.FormMvcInterpreterID).ToString());
          }
        }
      }
      foreach (string str in stringList2)
      {
        if (!stringList1.Contains(str))
        {
          Item obj = StaticSettings.ContextDatabase.GetItem(str);
          if (obj != null)
          {
            PlaceholderSettingsDefinition settingsDefinition = new PlaceholderSettingsDefinition(obj);
            settingsDefinition.AddControl(((object) IDs.FormInterpreterID).ToString());
            settingsDefinition.AddControl(((object) IDs.FormMvcInterpreterID).ToString());
          }
        }
      }
    }

    private static string GetValue()
    {
      List<Item> list = new List<Item>();
      Item[] withAllowedControl1 = PlaceholderSettingsDefinition.GetPlaceholdersWithAllowedControl(IDs.FormInterpreterID);
      Item[] withAllowedControl2 = PlaceholderSettingsDefinition.GetPlaceholdersWithAllowedControl(IDs.FormMvcInterpreterID);
      if (withAllowedControl1 != null)
        list = ((IEnumerable<Item>) withAllowedControl1).ToList<Item>();
      if (withAllowedControl2 != null)
      {
        foreach (Item obj in ((IEnumerable<Item>) withAllowedControl2).Where<Item>((Func<Item, bool>) (item => ((IEnumerable<Item>) list).All<Item>((Func<Item, bool>) (x => x.ID!=item.ID)))))
          list.Add(obj);
      }
      return string.Join("|", Sitecore.Form.Core.Utility.Utils.ToStringArray(list.ToArray()));
    }
  }
}
