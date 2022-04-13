// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Core.Commands.InsertForm
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Configuration;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Form.Core.Configuration;
using Sitecore.Security.Accounts;
using Sitecore.Shell.Framework.Commands;
using Sitecore.Text;
using Sitecore.Web;
using Sitecore.Web.UI.Sheer;
using Sitecore.Web.UI.XamlSharp.Continuations;
using Sitecore.WFFM.Abstractions.Dependencies;
using System;
using System.Collections.Specialized;

namespace Sitecore.Forms.Core.Commands
{
  [Serializable]
  public class InsertForm : Command, ISupportsContinuation
  {
    public static readonly string messageError = string.Empty;

    public override void Execute(CommandContext context)
    {
      Assert.ArgumentNotNull((object) context, nameof (context));
      NameValueCollection parameters = new NameValueCollection();
      this.SetNonEmptyContextParam(parameters, context, "placeholder");
      this.SetNonEmptyContextParam(parameters, context, "url");
      if (context.Parameters["mode"] == StaticSettings.DesignMode)
      {
        parameters["deviceid"] = ShortID.Decode(WebUtil.GetFormValue("scDeviceID"));
        parameters["id"] = ShortID.Decode(WebUtil.GetFormValue("scItemID"));
        parameters["db"] = Client.ContentDatabase.Name;
        parameters["la"] = Client.Site.Language;
        parameters["vs"] = string.Empty;
        parameters["mode"] = StaticSettings.DesignMode;
        Context.ClientPage.Start((object) this, "Run", parameters);
      }
      else
      {
        if (context.Items.Length != 1)
          return;
        Item obj = context.Items[0];
        parameters["id"] = ((object) obj.ID).ToString();
        parameters["la"] = obj.Language.Name;
        parameters["vs"] = obj.Version.Number.ToString();
        parameters["db"] = obj.Database.Name;
        this.SetNonEmptyContextParam(parameters, context, "mode");
        Context.ClientPage.Start((object) this, "Run", parameters);
      }
    }

    public override CommandState QueryState(CommandContext context)
    {
      Assert.ArgumentNotNull((object) context, nameof (context));
      if (context.Items.Length != 0)
      {
        Item obj = context.Items[0];
        if (!obj.Security.CanWrite((Account) Context.User) || obj.Appearance.ReadOnly)
          return (CommandState) 2;
      }
      return base.QueryState(context);
    }

    protected void Run(ClientPipelineArgs args)
    {
      Assert.ArgumentNotNull((object) args, nameof (args));
      if (!SheerResponse.CheckModified())
        return;
      if (!args.IsPostBack)
      {
        this.ShowWizard(args);
      }
      else
      {
        if (!args.HasResult)
          return;
        CommandManager.GetCommand("forms:designer").Execute(new CommandContext(Database.GetItem(ItemUri.Parse(args.Result))));
      }
    }

    private void ShowWizard(ClientPipelineArgs args)
    {
      Assert.ArgumentNotNull((object) args, nameof (args));
      string parameter1 = args.Parameters["id"];
      string parameter2 = args.Parameters["db"];
      Database database = Factory.GetDatabase(parameter2);
      if (string.IsNullOrEmpty(((BaseItem) database.GetItem(parameter1))["__renderings"]))
      {
        Context.ClientPage.ClientResponse.Alert(DependenciesManager.ResourceManager.Localize("HAS_NO_LAYOUT"));
      }
      else
      {
        UrlString url = new UrlString(UIUtil.GetUri("control:Forms.InsertFormWizard"));
        url.Add("id", parameter1);
        url.Add("db", parameter2);
        url.Add("la", args.Parameters["la"]);
        url.Add("vs", args.Parameters["vs"]);
        this.AddNonEmptyUrlParam(url, "placeholder", args.Parameters["placeholder"]);
        this.AddNonEmptyUrlParam(url, "mode", args.Parameters["mode"]);
        this.AddNonEmptyUrlParam(url, "deviceid", this.GetDevice(args, database));
        Context.ClientPage.ClientResponse.ShowModalDialog(((object) url).ToString(), true);
        args.WaitForPostBack();
      }
    }

    private string GetDevice(ClientPipelineArgs args, Database database)
    {
      Assert.ArgumentNotNull((object) args, nameof (args));
      Assert.ArgumentNotNull((object) database, nameof (database));
      string parameter1 = args.Parameters["deviceid"];
      if (!string.IsNullOrEmpty(parameter1))
        return parameter1;
      string parameter2 = args.Parameters["url"];
      if (string.IsNullOrEmpty(parameter2))
        return string.Empty;
      UrlString urlString = new UrlString(parameter2);
      return this.FindDevice(database, "p=" + urlString.Parameters["p"]);
    }

    private string FindDevice(Database database, string queryparam)
    {
      Assert.ArgumentNotNull((object) database, nameof (database));
      Assert.ArgumentNotNull((object) queryparam, nameof (queryparam));
      Item obj = database.GetItem((ID) ItemIDs.DevicesRoot);
      if (obj == null)
        return string.Empty;
      foreach (Item child in obj.Children)
      {
        if (((BaseItem) child)[(ID) DeviceFieldIDs.QueryString] == queryparam)
          return ((object) child.ID).ToString();
      }
      return string.Empty;
    }

    private void AddNonEmptyUrlParam(UrlString url, string name, string value)
    {
      Assert.ArgumentNotNull((object) url, nameof (url));
      Assert.ArgumentNotNull((object) name, nameof (name));
      if (string.IsNullOrEmpty(value))
        return;
      url.Add(name, value);
    }

    private void SetNonEmptyContextParam(
      NameValueCollection parameters,
      CommandContext context,
      string name)
    {
      Assert.ArgumentNotNull((object) parameters, nameof (parameters));
      Assert.ArgumentNotNull((object) context, nameof (context));
      Assert.ArgumentNotNull((object) name, nameof (name));
      string parameter = context.Parameters[name];
      if (string.IsNullOrEmpty(parameter))
        return;
      parameters[name] = parameter;
    }
  }
}
