// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Shell.UI.Dialogs.SelectPage
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Form.Core.Data;
using Sitecore.Form.Core.Utility;
using Sitecore.Web;
using Sitecore.Web.UI.HtmlControls;
using Sitecore.Web.UI.Pages;
using Sitecore.Web.UI.Sheer;
using Sitecore.Web.UI.XmlControls;
using Sitecore.WFFM.Abstractions.Dependencies;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sitecore.Forms.Shell.UI.Dialogs
{
  public class SelectPage : DialogForm
  {
    protected DataContext ItemDataContext;
    protected DataTreeview ItemTreeView;
    protected XmlControl Dialog;
    protected Combobox PlaceholderList;
    protected GenericControl PageHolder;
    protected GenericControl CurrentPlaceholder;

    public string Page => Sitecore.Web.WebUtil.GetQueryString("page", string.Empty);

    public string Placeholder => Sitecore.Web.WebUtil.GetQueryString("placeholder", string.Empty);

    protected override void OnLoad(EventArgs e)
    {
      base.OnLoad(e);
      if (!Context.ClientPage.IsEvent)
      {
        this.Localize();
        if (!string.IsNullOrEmpty(this.Page) && ID.IsID(this.Page))
          this.ItemDataContext.DefaultItem = this.Page;
        ((WebControl) this.CurrentPlaceholder).Attributes["value"] = this.Placeholder;
        this.OnNodeSelected((object) this, EventArgs.Empty);
      }
      ((Treeview) this.ItemTreeView).OnClick += new EventHandler(this.OnNodeSelected);
    }

    private void OnNodeSelected(object sender, EventArgs e)
    {
      if (this.ItemDataContext.CurrentItem == null)
        return;
      string str = ((object) this.ItemDataContext.CurrentItem.ID).ToString();
      if (!(((System.Web.UI.Page) Context.ClientPage).Request.Form["PageHolder"] != str))
        return;
      if (!string.IsNullOrEmpty(((BaseItem) this.ItemDataContext.CurrentItem).Fields["__renderings"].Value))
      {
        string placeholderNamesUrl = PlaceholderManager.GetOnlyPlaceholderNamesUrl(Sitecore.Form.Core.Utility.ItemUtil.GetItemUrl(this.ItemDataContext.CurrentItem));
        if (!Context.ClientPage.IsEvent)
        {
          ((WebControl) this.PageHolder).Attributes["value"] = str;
          ((WebControl) this.PlaceholderList).Attributes["page"] = placeholderNamesUrl;
        }
        else
        {
          SheerResponse.SetAttribute("PageHolder", "value", str);
          SheerResponse.SetAttribute((this.PlaceholderList).ID, "page", placeholderNamesUrl);
          SheerResponse.Eval("$('StarLabel').style.display = 'none';$('LoadImage').style.display = 'block';Sitecore.Wfm.PlaceholderManager.getPlaceholders($$('.PlaceholderList')[0].readAttribute('page'), ExpandOptions, ClearOptions)");
        }
      }
      else
        SheerResponse.Eval("ClearOptions()");
    }

    protected virtual void Localize()
    {
      this.Dialog["Header"] = (object) DependenciesManager.ResourceManager.Localize("SELECT_FORM_DISPLAY_PAGE");
      this.Dialog["Text"] = (object) DependenciesManager.ResourceManager.Localize("SELECT_FORM_THAT_FORM_WILL_BE_DISPLAYED");
    }

    protected override void OnOK(object sender, EventArgs args)
    {
      Item selectionItem = this.ItemTreeView.GetSelectionItem();
      if (selectionItem == null)
        SheerResponse.Alert(DependenciesManager.ResourceManager.Localize("CHOOSE_PAGE"), Array.Empty<string>());
      else if (((BaseItem) selectionItem).Fields["__renderings"] == null || string.IsNullOrEmpty(((BaseItem) selectionItem).Fields["__renderings"].Value))
        SheerResponse.Alert(DependenciesManager.ResourceManager.Localize("ITEM_HAS_NO_LAYOUT"), Array.Empty<string>());
      else if (string.IsNullOrEmpty((this.PlaceholderList).Value))
      {
        SheerResponse.Alert(DependenciesManager.ResourceManager.Localize("SELECT_PLACEHOLDER_YOU_WANT_USE"), Array.Empty<string>());
      }
      else
      {
        SheerResponse.SetDialogValue(string.Join("/", new string[2]
        {
          ((object) selectionItem.ID).ToString(),
          (this.PlaceholderList).Value
        }));
        SheerResponse.SetModified(false);
        base.OnOK(sender, args);
      }
    }
  }
}
