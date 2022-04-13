// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Shell.UI.Dialogs.SuccessEditor
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Form.Core.Utility;
using Sitecore.Web;
using Sitecore.Web.UI.HtmlControls;
using Sitecore.Web.UI.Pages;
using Sitecore.Web.UI.Sheer;
using Sitecore.Web.UI.XmlControls;
using Sitecore.WFFM.Abstractions.Dependencies;
using Sitecore.WFFM.Abstractions.Shared;
using System;
using System.Collections.Specialized;

namespace Sitecore.Forms.Shell.UI.Dialogs
{
  public class SuccessEditor : DialogForm
  {
    private readonly IResourceManager resourceManager;
    protected DataTreeview ItemLister;
    protected DataContext ItemDataContext;
    protected Memo SuccessMessage;
    protected Radiobutton MessageMode;
    protected Radiobutton PageMode;
    protected XmlControl Dialog;

    public SuccessEditor()
      : this(DependenciesManager.ResourceManager)
    {
    }

    public SuccessEditor(IResourceManager resourceManager)
    {
      Assert.IsNotNull((object) resourceManager, "Dependency resourceManager is null");
      this.resourceManager = resourceManager;
    }

    protected override void OnLoad(EventArgs e)
    {
      base.OnLoad(e);
      if (!Context.ClientPage.IsEvent)
      {
        UrlHandle urlHandle = UrlHandle.Get();
        ((Control) this.SuccessMessage).Value = StringUtil.GetString(new string[1]
        {
          urlHandle["message"]
        });
        string str = urlHandle["page"];
        if (!string.IsNullOrEmpty(str))
          this.ItemDataContext.DefaultItem = str;
        bool flag = MainUtil.GetBool(urlHandle["choice"], false);
        this.PageMode.Checked = flag;
        this.MessageMode.Checked = !flag;
        this.OnChangeMode();
        this.Localize();
      }
      ((Treeview) this.ItemLister).OnDblClick += OnOK;
    }

    protected virtual void Localize()
    {
      this.Dialog["Header"] = (object) this.resourceManager.Localize("SUCCESS");
      this.Dialog["Text"] = (object) this.resourceManager.Localize("SELECT_PAGE_OR_CREATE_SUCCESS_MESSAGE");
      this.PageMode.Header = this.resourceManager.Localize("SUCCESS_PAGE_CAPTION");
      this.MessageMode.Header = this.resourceManager.Localize("SUCCESS_MESSAGE_CAPTION");
    }

    protected override void OnOK(object sender, EventArgs args)
    {
      NameValueCollection values = new NameValueCollection(3);
      Item selectionItem1 = this.ItemLister.GetSelectionItem();
      if (this.PageMode.Checked)
      {
        if (selectionItem1 == null)
        {
          SheerResponse.Alert(this.resourceManager.Localize("CHOOSE_PAGE"), Array.Empty<string>());
          return;
        }
        if (((BaseItem) selectionItem1).Fields["__renderings"] == null || string.IsNullOrEmpty(((BaseItem) selectionItem1).Fields["__renderings"].Value))
        {
          Context.ClientPage.ClientResponse.Alert(this.resourceManager.Localize("ITEM_HAS_NO_LAYOUT"));
          return;
        }
      }
      if (string.IsNullOrEmpty(((Control) this.SuccessMessage).Value) && !this.PageMode.Checked)
      {
        SheerResponse.Alert(this.resourceManager.Localize("TYPE_SUCCESS_MESSAGE"), Array.Empty<string>());
      }
      else
      {
        Item selectionItem2 = this.ItemLister.GetSelectionItem();
        values.Add("message", ((Control) this.SuccessMessage).Value);
        values.Add("page", selectionItem2 != null ? ((object) selectionItem2.ID).ToString() : string.Empty);
        values.Add("choice", this.PageMode.Checked ? "1" : "0");
        SheerResponse.SetDialogValue(ParametersUtil.NameValueCollectionToXml(values));
        base.OnOK(sender, args);
      }
    }

    protected void OnChangeMode()
    {
      if (this.PageMode.Checked)
      {
        ((Control) this.ItemLister).Disabled = false;
        ((Control) this.SuccessMessage).Disabled = true;
      }
      else
      {
        ((Control) this.ItemLister).Disabled = true;
        ((Control) this.SuccessMessage).Disabled = false;
      }
    }
  }
}
