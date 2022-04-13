// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Shell.UI.Dialogs.SelectForm
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Form.Core.Configuration;
using Sitecore.Form.Core.Utility;
using Sitecore.Form.UI.Controls;
using Sitecore.Web.UI.HtmlControls;
using Sitecore.Web.UI.Pages;
using Sitecore.Web.UI.Sheer;
using Sitecore.Web.UI.XmlControls;
using Sitecore.WFFM.Abstractions.Dependencies;
using System;
using System.Text;
using System.Web.UI;

namespace Sitecore.Forms.Shell.UI.Dialogs
{
  public class SelectForm : DialogForm
  {
    protected Scrollbox ExistingForms;
    protected MultiTreeView multiTree;
    protected XmlControl Dialog;
    private const string MultiID = "sfMultiTree";

    protected override void OnLoad(EventArgs e)
    {
      base.OnLoad(e);
      if (!Context.ClientPage.IsEvent)
      {
        this.Localize();
        MultiTreeView multiTreeView = new MultiTreeView();
        multiTreeView.Roots = Sitecore.Form.Core.Utility.Utils.GetFormRoots();
        multiTreeView.Filter = "Contains('{C0A68A37-3C0A-4EEB-8F84-76A7DF7C840E},{A87A00B1-E6DB-45AB-8B54-636FEC3B5523},{FFB1DA32-2764-47DB-83B0-95B843546A7E}', @@templateid)";
        (multiTreeView).ID = "sfMultiTree";
        multiTreeView.DataViewName = "Master";
        multiTreeView.IsFullPath = true;
        multiTreeView.TemplateID = ((object) IDs.FormTemplateID).ToString();
        this.multiTree = multiTreeView;
        (this.ExistingForms).Controls.Add(this.multiTree);
      }
      else
        this.multiTree = (this.ExistingForms).FindControl("sfMultiTree") as MultiTreeView;
      if (this.multiTree == null)
        return;
      if (!string.IsNullOrEmpty((Context.ClientPage).Request.Params.Get("analytics_enabled")))
        this.multiTree.Filter += " AND not(contains(@__Tracking, 'ignore=\"1\"'))";
      this.multiTree.DbClick += new EventHandler<ItemDoubleClickedEventArgs>(this.OnDbClick);
    }

    protected virtual void OnDbClick(object sender, ItemDoubleClickedEventArgs e)
    {
      Assert.ArgumentNotNull((object) e, nameof (e));
      base.OnOK(sender, (EventArgs) e);
    }

    protected virtual void Localize()
    {
      this.Dialog["Header"] = (object) DependenciesManager.ResourceManager.Localize("SELECT_FORM");
      this.Dialog["Text"] = (object) DependenciesManager.ResourceManager.Localize("SELECT_FORM_THAT_YOU_WANT_TO_USE");
    }

    protected override void OnOK(object sender, EventArgs args)
    {
      Item obj = StaticSettings.GlobalFormsRoot.Database.GetItem(this.multiTree.Selected, Context.ContentLanguage);
      if (obj == null)
        Context.ClientPage.ClientResponse.Alert(DependenciesManager.ResourceManager.Localize("PLEASE_SELECT_FORM"));
      else if ((obj.TemplateID!=IDs.FormTemplateID))
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.AppendFormat("'{0}' ", (object) obj.Name);
        stringBuilder.Append(DependenciesManager.ResourceManager.Localize("IS_NOT_FORM"));
        Context.ClientPage.ClientResponse.Alert(stringBuilder.ToString());
      }
      else
      {
        SheerResponse.SetDialogValue(((object) obj.Uri).ToString());
        base.OnOK(sender, args);
      }
    }
  }
}
