// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Shell.UI.Controls.GallerySubmitActions
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Configuration;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Form.Core.Configuration;
using Sitecore.Form.Core.Utility;
using Sitecore.Globalization;
using Sitecore.Shell.Applications.ContentManager.Galleries;
using Sitecore.Web;
using Sitecore.Web.UI.HtmlControls;
using Sitecore.Web.UI.Sheer;
using Sitecore.WFFM.Abstractions.Actions;
using Sitecore.WFFM.Abstractions.Dependencies;
using System;
using System.Collections.Specialized;
using System.Web.UI;

namespace Sitecore.Forms.Shell.UI.Controls
{
  public class GallerySubmitActions : GalleryForm
  {
    protected Scrollbox SubmitActions;
    protected GalleryMenu Options;
    private readonly IActionExecutor actionExecutor;

    public GallerySubmitActions()
      : this(DependenciesManager.ActionExecutor)
    {
    }

    public GallerySubmitActions(IActionExecutor actionExecutor) => this.actionExecutor = actionExecutor;

    private static Item GetCurrentForm()
    {
      string queryString1 = Sitecore.Web.WebUtil.GetQueryString("db");
      string queryString2 = Sitecore.Web.WebUtil.GetQueryString("id");
      Language language = Language.Parse(Sitecore.Web.WebUtil.GetQueryString("la"));
      Sitecore.Data.Version version = Sitecore.Data.Version.Parse(Sitecore.Web.WebUtil.GetQueryString("vs"));
      Database database = Factory.GetDatabase(queryString1);
      Assert.IsNotNull((object) database, queryString1);
      return database.GetItem(queryString2, language, version);
    }

    public override void HandleMessage(Message message)
    {
      Assert.ArgumentNotNull((object) message, nameof (message));
      this.Invoke(message, true);
    }

    protected override void OnLoad(EventArgs e)
    {
      Assert.ArgumentNotNull((object) e, nameof (e));
      base.OnLoad(e);
      if (Context.ClientPage.IsEvent)
        return;
      Item currentForm = GallerySubmitActions.GetCurrentForm();
      if (currentForm == null)
        return;
      foreach (IActionItem action in this.actionExecutor.GetActions(currentForm))
        Context.ClientPage.AddControl(this.SubmitActions, XmlResourceUtil.GetResourceControl("Gallery.SubmitAction.Option", new NameValueCollection()
        {
          {
            "Icon",
            action.Icon
          },
          {
            "Header",
            action.Name
          },
          {
            "Description",
            action.Description
          },
          {
            "Click",
            action.Editor
          },
          {
            "ClassName",
            "scMenuPanelItem"
          }
        }));
      Context.ClientPage.AddControl(this.Options, new GalleryMenuLine());
      Item obj = Factory.GetDatabase("core").GetItem(StaticSettings.SubmitActionsMenu);
      if (obj != null)
        ((Menu) this.Options).AddFromDataSource(obj, string.Empty);
      Context.ClientPage.AddControl(this.Options, new GalleryMenuLine());
    }
  }
}
