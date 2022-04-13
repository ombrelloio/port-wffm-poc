// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Core.Commands.SetSubmitActions
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Form.Core.Configuration;
using Sitecore.Form.Core.ContentEditor.Data;
using Sitecore.Forms.Core.Data;
using Sitecore.Globalization;
using Sitecore.Shell.Framework.Commands;
using Sitecore.Text;
using Sitecore.Web;
using Sitecore.Web.UI.Sheer;
using Sitecore.WFFM.Abstractions.Dependencies;
using System;
using System.Collections.Specialized;
using System.Web;

namespace Sitecore.Forms.Core.Commands
{
  [Serializable]
  public class SetSubmitActions : Command
  {
    public override void Execute(CommandContext context)
    {
      Assert.ArgumentNotNull((object) context, nameof (context));
      Error.AssertObject((object) context, nameof (context));
      if (context.Items.Length != 1)
        return;
      Item obj = context.Items[0];
      Context.ClientPage.Start((object) this, "Run", new NameValueCollection()
      {
        ["id"] = ((object) obj.ID).ToString(),
        ["db"] = obj.Database.Name,
        ["la"] = obj.Language.Name,
        ["mode"] = context.Parameters["mode"],
        ["root"] = context.Parameters["root"],
        ["system"] = context.Parameters["system"]
      });
    }

    protected void Run(ClientPipelineArgs args)
    {
      Assert.ArgumentNotNull((object) args.Parameters["id"], "id");
      Assert.ArgumentNotNull((object) args.Parameters["db"], "db");
      Item innerItem = Database.GetItem(new ItemUri(ID.Parse(args.Parameters["id"]), Language.Current, Sitecore.Data.Version.Latest, args.Parameters["db"]));
      if (!SheerResponse.CheckModified() || innerItem == null)
        return;
      if (args.IsPostBack)
      {
        UrlHandle urlHandle = UrlHandle.Get(new UrlString(args.Parameters["url"]));
        innerItem.Editing.BeginEdit();
        ((BaseItem) innerItem).Fields["__Tracking"].Value = urlHandle["tracking"];
        if (args.HasResult)
        {
          ListDefinition listDefinition = ListDefinition.Parse(args.Result == "-" ? string.Empty : args.Result);
          if (args.Parameters["mode"] == "save")
            ((BaseItem) innerItem).Fields[Sitecore.Form.Core.Configuration.FieldIDs.SaveActionsID].Value = listDefinition.ToXml();
          else
            ((BaseItem) innerItem).Fields[Sitecore.Form.Core.Configuration.FieldIDs.CheckActionsID].Value = listDefinition.ToXml();
        }
        innerItem.Editing.EndEdit();
      }
      else
      {
        string name = ((object) ID.NewID).ToString();
        UrlString urlString = new UrlString(UIUtil.GetUri("control:SubmitCommands.Editor"));
        FormItem formItem = new FormItem(innerItem);
        ListDefinition listDefinition = ListDefinition.Parse(args.Parameters["mode"] == "save" ? formItem.SaveActions : formItem.CheckActions);
        HttpContext.Current.Session.Add(name, (object) listDefinition);
        urlString.Append("definition", name);
        urlString.Add("id", args.Parameters["id"]);
        urlString.Add("db", args.Parameters["db"]);
        urlString.Add("la", args.Parameters["la"]);
        urlString.Append("root", args.Parameters["root"]);
        urlString.Append("system", args.Parameters["system"] ?? string.Empty);
        args.Parameters.Add("params", name);
        new UrlHandle()
        {
          ["title"] = DependenciesManager.ResourceManager.Localize(args.Parameters["mode"] == "save" ? "SELECT_SAVE_TITLE" : "SELECT_CHECK_TITLE"),
          ["desc"] = DependenciesManager.ResourceManager.Localize(args.Parameters["mode"] == "save" ? "SELECT_SAVE_DESC" : "SELECT_CHECK_DESC"),
          ["actions"] = DependenciesManager.ResourceManager.Localize(args.Parameters["mode"] == "save" ? "SAVE_ACTIONS" : "CHECK_ACTIONS"),
          ["addedactions"] = DependenciesManager.ResourceManager.Localize(args.Parameters["mode"] == "save" ? "ADDED_SAVE_ACTIONS" : "ADDED_CHECK_ACTIONS"),
          ["tracking"] = formItem.Tracking.ToString()
        }.Add(urlString);
        args.Parameters["url"] = ((object) urlString).ToString();
        Context.ClientPage.ClientResponse.ShowModalDialog(((object) urlString).ToString(), true);
        args.WaitForPostBack();
      }
    }
  }
}
