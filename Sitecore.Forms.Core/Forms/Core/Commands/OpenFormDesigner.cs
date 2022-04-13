// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Core.Commands.OpenFormDesigner
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Form.Core.Configuration;
using Sitecore.Forms.Core.Commands.FormDesigner;
using Sitecore.Forms.Core.Data;
using Sitecore.Shell.Framework;
using Sitecore.Shell.Framework.Commands;
using Sitecore.Text;
using Sitecore.Web;
using Sitecore.Web.UI.Sheer;
using Sitecore.Web.UI.WebControls;
using Sitecore.Web.UI.XamlSharp.Continuations;
using Sitecore.WFFM.Abstractions.Dependencies;
using System;
using System.Collections.Specialized;

namespace Sitecore.Forms.Core.Commands
{
  [Serializable]
  public class OpenFormDesigner : Command, ISupportsContinuation
  {
    public override void Execute(CommandContext context)
    {
      Assert.ArgumentNotNull((object) context, nameof (context));
      Error.AssertObject((object) context, nameof (context));
      if (context.Items.Length != 0)
        this.RunDesigner(context.Items[0]);
      else if (ContinuationManager.Current != null)
        ContinuationManager.Current.Start((ISupportsContinuation) this, "SelectForm");
      else
        Context.ClientPage.Start((object) this, "SelectForm");
    }

    protected void Run(ClientPipelineArgs args)
    {
      Assert.ArgumentNotNull((object) args, nameof (args));
      if (args.IsPostBack)
        return;
      if ((Settings.OpenFormDesignerAsModalDialog || WebUtil.GetSessionString(StaticSettings.Mode) == StaticSettings.DesignMode ? 1 : (WebUtil.GetQueryString("mode") == StaticSettings.DesignMode ? 1 : 0)) != 0)
      {
        FormItem form = FormItem.GetForm(args.Parameters["formId"]);
        if (form == null)
          return;
        NameValueCollection urlParameters = new NameValueCollection();
        urlParameters["formid"] = StringUtil.GetString(new string[1]
        {
          args.Parameters["formid"]
        });
        urlParameters["db"] = StringUtil.GetString(new string[1]
        {
          args.Parameters["db"]
        });
        urlParameters["la"] = StringUtil.GetString(new string[1]
        {
          args.Parameters["la"]
        });
        urlParameters["vs"] = StringUtil.GetString(new string[1]
        {
          args.Parameters["vs"]
        });
        ID id;
        if (args.Parameters["referenceId"] != null && ID.TryParse(args.Parameters["referenceId"], out id))
          urlParameters["referenceId"] = StringUtil.GetString((object) new ID[1]
          {
            id
          });
        if (WebUtil.GetQueryString("mode") == StaticSettings.DesignMode)
          urlParameters["mode"] = StaticSettings.DesignMode;
        new FormDialog(form, DependenciesManager.ResourceManager).ShowModalDialog(urlParameters);
      }
      else
      {
        Windows.RunApplication(Path.FormDesignerApplication, ((object) new UrlString()
        {
          ["formid"] = StringUtil.GetString(new string[1]
          {
            args.Parameters["formid"]
          }),
          ["db"] = StringUtil.GetString(new string[1]
          {
            args.Parameters["db"]
          }),
          ["la"] = StringUtil.GetString(new string[1]
          {
            args.Parameters["la"]
          }),
          ["vs"] = StringUtil.GetString(new string[1]
          {
            args.Parameters["vs"]
          })
        }).ToString());
        args.WaitForPostBack();
      }
    }

    protected void RunDesigner(Item item)
    {
      NameValueCollection nameValueCollection = new NameValueCollection();
      nameValueCollection["formid"] = ((object) item.ID).ToString();
      nameValueCollection["la"] = ((object) item.Language).ToString();
      nameValueCollection["vs"] = ((object) item.Version).ToString();
      nameValueCollection["db"] = item.Database.Name;
      if (ContinuationManager.Current != null)
        ContinuationManager.Current.Start((ISupportsContinuation) this, "Run", new ClientPipelineArgs(nameValueCollection));
      else
        Context.ClientPage.Start((object) this, "Run", nameValueCollection);
    }

    protected void SelectForm(ClientPipelineArgs args)
    {
      Assert.ArgumentNotNull((object) args, nameof (args));
      if (args.IsPostBack)
      {
        if (!args.HasResult)
          return;
        this.RunDesigner(Database.GetItem(ItemUri.Parse(args.Result)));
      }
      else
      {
        SheerResponse.ShowModalDialog(((object) new UrlString(UIUtil.GetUri("control:Forms.SelectForm"))).ToString(), true);
        args.WaitForPostBack();
      }
    }
  }
}
