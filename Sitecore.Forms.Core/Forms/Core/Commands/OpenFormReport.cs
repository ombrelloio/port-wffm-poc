// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Core.Commands.OpenFormReport
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Shell.Framework.Commands;
using Sitecore.Text;
using Sitecore.Web.UI.Sheer;
using Sitecore.WFFM.Abstractions.Dependencies;
using Sitecore.WFFM.Abstractions.Shared;
using Sitecore.WFFM.Abstractions.Wrappers.UI;
using System;
using System.Collections.Specialized;
using System.Runtime.Serialization;

namespace Sitecore.Forms.Core.Commands
{
  [Serializable]
  public class OpenFormReport : Command
  {
    [NonSerialized]
    private ClientPageWrapper clientPage;
    [NonSerialized]
    private SheerResponseWrapper sheerResponse;
    [NonSerialized]
    private IItemRepository itemRepository;

    public OpenFormReport()
      : this(DependenciesManager.Resolve<IItemRepository>(), DependenciesManager.Resolve<ClientPageWrapper>(), DependenciesManager.Resolve<SheerResponseWrapper>())
    {
    }

    public OpenFormReport(
      IItemRepository itemRepository,
      ClientPageWrapper clientPage,
      SheerResponseWrapper sheerResponse)
    {
      this.initialize(itemRepository, clientPage, sheerResponse);
    }

    private void initialize(
      IItemRepository itemRepository,
      ClientPageWrapper clientPage,
      SheerResponseWrapper sheerResponse)
    {
      Assert.ArgumentNotNull((object) itemRepository, nameof (itemRepository));
      Assert.ArgumentNotNull((object) clientPage, nameof (clientPage));
      Assert.ArgumentNotNull((object) sheerResponse, nameof (sheerResponse));
      this.clientPage = clientPage;
      this.sheerResponse = sheerResponse;
      this.itemRepository = itemRepository;
    }

    public override void Execute(CommandContext context)
    {
      Assert.ArgumentNotNull((object) context, nameof (context));
      NameValueCollection nameValueCollection = new NameValueCollection();
      ClientPipelineArgs args = new ClientPipelineArgs(nameValueCollection);
      Item obj = context.Items.Length == 1 ? context.Items[0] : (context.Parameters["id"] != null ? this.itemRepository.GetItem(context.Parameters["id"]) : (Item) null);
      if (obj != null)
      {
        nameValueCollection.Add("itemId", ((object) obj.ID).ToString());
        args.Parameters.Add("needconfirmation", "false");
      }
      this.clientPage.Start((object) this, "Run", args);
    }

    public override string GetClick(CommandContext context, string click)
    {
      Assert.ArgumentNotNull((object) context, nameof (context));
      Assert.ArgumentNotNull((object) click, nameof (click));
      string click1 = base.GetClick(context, click);
      if (!this.IsFormInContext(context))
        return click1;
      Item obj = context.Items[0];
      return click1.Replace("$itm", ((object) obj.ID).ToString()).Replace("$la", obj.Language.Name).Replace("$ver", obj.Version.Number.ToString()).Replace("$da", obj.Database.Name);
    }

    protected void Run(ClientPipelineArgs args)
    {
      Assert.ArgumentNotNull((object) args, nameof (args));
      if (this.clientPage == null || this.sheerResponse == null || this.itemRepository == null)
        this.initialize(DependenciesManager.Resolve<IItemRepository>(), DependenciesManager.Resolve<ClientPageWrapper>(), DependenciesManager.Resolve<SheerResponseWrapper>());
      if (args.Parameters["needconfirmation"] == "false" || args.IsPostBack)
        this.OpenReportApp(args);
      else
        this.OpenSelectFormDialog(args);
    }

    private void OpenSelectFormDialog(ClientPipelineArgs args)
    {
      Assert.ArgumentNotNull((object) args, nameof (args));
      this.sheerResponse.ShowModalDialog(((object) new UrlString(UIUtil.GetUri("control:Forms.SelectForm"))
      {
        Parameters = {
          {
            "analytics_enabled",
            "1"
          }
        }
      }).ToString(), true);
      args.WaitForPostBack();
    }

    private void OpenReportApp(ClientPipelineArgs args)
    {
      Assert.ArgumentNotNull((object) args, nameof (args));
      string script;
      if (args.HasResult)
      {
        ItemUri itemUri = ItemUri.Parse(args.Result);
        if (itemUri == (ItemUri) null)
          return;
        script = string.Format("(function(){{ var d = document.createElement('div'); d.setAttribute('onclick', '{0}'); document.querySelector(\"body\").appendChild(d); d.click(); d.remove(); }})()", (object) this.GetOpenAppScript(itemUri.ItemID.ToGuid().ToString()));
      }
      else
      {
        string parameter = args.Parameters["itemId"];
        if (parameter == null)
          return;
        script = this.GetOpenAppScript(parameter);
      }
      this.clientPage.ClientResponse.Eval(script);
    }

    private string GetOpenAppScript(string itemId)
    {
      Assert.ArgumentNotNull((object) itemId, nameof (itemId));
      UrlString urlString = new UrlString("/sitecore/client/Applications/WFFM/Pages/FormReport");
      urlString.Add(nameof (itemId), itemId);
      return string.Format("window.open(\"{0}\", \"_blank\")", (object) urlString);
    }

    private bool IsFormInContext(CommandContext context)
    {
      Assert.ArgumentNotNull((object) context, nameof (context));
      return context.Items.Length == 1 && context.Items[0].TemplateName == "Form";
    }

    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
    }
  }
}
