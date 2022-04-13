// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Shell.UI.Dialogs.CampaignEditor
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Configuration;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Form.Core.Data;
using Sitecore.Marketing.Definitions;
using Sitecore.Marketing.Definitions.Campaigns;
using Sitecore.Text;
using Sitecore.Web;
using Sitecore.Web.UI;
using Sitecore.Web.UI.HtmlControls;
using Sitecore.Web.UI.Pages;
using Sitecore.Web.UI.Sheer;
using Sitecore.Web.UI.XmlControls;
using Sitecore.WFFM.Abstractions.Dependencies;
using Sitecore.WFFM.Abstractions.Shared;
using System;
using System.Linq;

namespace Sitecore.Forms.Shell.UI.Dialogs
{
  public class CampaignEditor : DialogForm
  {
    private readonly IResourceManager resourceManager;
    protected DataTreeview ItemLister;
    protected DataContext ItemDataContext;
    protected Literal SlectCampaignEventLiteral;
    protected XmlControl Dialog;

    public CampaignEditor()
      : this(DependenciesManager.ResourceManager)
    {
    }

    public CampaignEditor(IResourceManager resourceManager)
    {
      Assert.IsNotNull((object) resourceManager, "Dependency resourceManager is null");
      this.resourceManager = resourceManager;
    }

    protected override void OnLoad(EventArgs e)
    {
      base.OnLoad(e);
      if (!Context.ClientPage.IsEvent)
      {
        this.Localize();
        UrlHandle urlHandle = UrlHandle.Get(new UrlString(WebUtil.GetRawUrl()), "hdl", false);
        if (urlHandle != null)
        {
          this.TrackingXml = urlHandle["tracking"];
          ICampaignActivityDefinition activityDefinition = new Tracking(this.TrackingXml, Factory.GetDatabase(((WebControl) this.ItemDataContext).Database)).Campaigns.FirstOrDefault<ICampaignActivityDefinition>();
          if (activityDefinition != null)
            this.ItemDataContext.DefaultItem = ((IDefinition) activityDefinition).Id.ToString();
        }
      }
      ((Treeview) this.ItemLister).OnDblClick += OnOK;
    }

    protected virtual void Localize()
    {
      this.Dialog["Header"] = (object) this.resourceManager.Localize("SELECT_CAMPAIGN_HEADER");
      this.Dialog["Text"] = (object) this.resourceManager.Localize("SELECT_CAMPAIGN_EVENT_TO_ASSOCIATE_WITH_FORM");
      this.SlectCampaignEventLiteral.Text = this.resourceManager.Localize("SELECT_CAMPAIGN_EVENT_COLON");
    }

    protected override void OnOK(object sender, EventArgs args)
    {
      Item selectionItem = this.ItemLister.GetSelectionItem();
      if (selectionItem == null)
        SheerResponse.Alert(this.resourceManager.Localize("SELECT_CAMPAIGN") + ".", Array.Empty<string>());
      else if (selectionItem.TemplateName != "Campaign")
      {
        SheerResponse.Alert(this.resourceManager.Localize("SELECT_CAMPAIGN") + ".", Array.Empty<string>());
      }
      else
      {
        Tracking tracking = new Tracking(this.TrackingXml, Factory.GetDatabase(((WebControl) this.ItemDataContext).Database));
        tracking.SetCampaignEvent(selectionItem.ID.Guid);
        UrlString urlString = new UrlString(WebUtil.GetRawUrl());
        UrlHandle urlHandle = UrlHandle.Get(urlString, "hdl", false);
        urlHandle["tracking"] = tracking.ToString();
        urlHandle.Add(urlString);
        base.OnOK(sender, args);
      }
    }

    public string TrackingXml
    {
      get => (string) Context.ClientPage.ServerProperties["trackingxml"];
      set => Context.ClientPage.ServerProperties["trackingxml"] = (object) value;
    }

    public string HandleName => WebUtil.GetQueryString("handleName");
  }
}
