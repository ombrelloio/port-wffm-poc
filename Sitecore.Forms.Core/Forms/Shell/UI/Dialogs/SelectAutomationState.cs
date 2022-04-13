// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Shell.UI.Dialogs.SelectAutomationState
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Diagnostics;
using Sitecore.Forms.Shell.UI.Controls;
using Sitecore.Web.UI.HtmlControls;
using Sitecore.Web.UI.Pages;
using Sitecore.Web.UI.Sheer;
using Sitecore.Web.UI.XmlControls;
using Sitecore.WFFM.Abstractions.Dependencies;
using Sitecore.WFFM.Abstractions.Shared;
using System;
using System.Web.UI;

namespace Sitecore.Forms.Shell.UI.Dialogs
{
  public class SelectAutomationState : DialogForm
  {
    private readonly IResourceManager resourceManager;
    protected Sitecore.Web.UI.XmlControls.XmlControl Dialog;
    protected Literal SelectSate;
    protected AutomationStateList StateList;

    public SelectAutomationState()
      : this(DependenciesManager.ResourceManager)
    {
    }

    public SelectAutomationState(IResourceManager resourceManager)
    {
      Assert.IsNotNull((object) resourceManager, "Dependency resourceManager is null");
      this.resourceManager = resourceManager;
    }

    protected override void OnLoad(EventArgs e)
    {
      base.OnLoad(e);
      this.Localize();
    }

    protected virtual void Localize()
    {
      this.Dialog["Header"] = (object) this.resourceManager.Localize("SELECT_ENGAGEMENT_PLAN");
      this.Dialog["Text"] = (object) this.resourceManager.Localize("SELECT_AN_ENGAGEMENT_PLAN");
      this.SelectSate.Text = this.resourceManager.Localize("SELECT_STATE_AND_PLAN");
    }

    protected override void OnOK(object sender, EventArgs args)
    {
      if (string.IsNullOrEmpty(this.StateList.Value))
      {
        SheerResponse.Alert(this.resourceManager.Localize("SELECT_ENGAGEMENT_PLAN_YOU_WANT_USE"), Array.Empty<string>());
      }
      else
      {
        SheerResponse.SetDialogValue(this.StateList.Value);
        base.OnOK(sender, args);
      }
    }

    protected override void OnPreRender(EventArgs e)
    {
      ((Button) (Context.ClientPage).FindControl("OK")).KeyCode = string.Empty;
      base.OnPreRender(e);
    }
  }
}
