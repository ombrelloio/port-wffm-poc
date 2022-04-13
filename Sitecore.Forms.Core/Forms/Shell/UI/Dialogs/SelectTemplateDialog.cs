// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Shell.UI.Dialogs.SelectTemplateDialog
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Web;
using Sitecore.Web.UI.HtmlControls;
using Sitecore.Web.UI.Pages;
using Sitecore.Web.UI.Sheer;
using Sitecore.Web.UI.WebControls;
using Sitecore.Web.UI.XmlControls;
using Sitecore.WFFM.Abstractions.Dependencies;
using System;

namespace Sitecore.Forms.Shell.UI.Dialogs
{
  public class SelectTemplateDialog : DialogForm
  {
    protected XmlControl Dialog;
    protected DataContext TemplatesDataContext;
    protected TreeviewEx TemplateLister;

    protected override void OnLoad(EventArgs e)
    {
      Assert.ArgumentNotNull((object) e, nameof (e));
      base.OnLoad(e);
      if (Context.ClientPage.IsEvent)
        return;
      this.Localize();
      Context.ClientPage.ServerProperties["id"] = (object) WebUtil.GetQueryString("id");
      TemplateItem templateItem = (TemplateItem) null;
      if (!string.IsNullOrEmpty(this.TemplateID))
      {
        ID id;
        templateItem = !ID.TryParse(this.TemplateID, out id) ? Context.ContentDatabase.GetTemplate(this.TemplateID) : Context.ContentDatabase.GetTemplate(id);
      }
      if (templateItem == null)
        return;
      this.TemplatesDataContext.SetFolder(((CustomItemBase) templateItem).InnerItem.Uri);
    }

    protected virtual void Localize()
    {
      this.Dialog["Header"] = (object) DependenciesManager.ResourceManager.Localize("SELECT_TEMPLATE_CAPTION");
      this.Dialog["Text"] = (object) DependenciesManager.ResourceManager.Localize("SELECT_TEMPLATE_THAT_YOU_WANT");
    }

    protected override void OnOK(object sender, EventArgs args)
    {
      Item selectionItem = this.TemplateLister.GetSelectionItem();
      if (selectionItem == null || (selectionItem.TemplateID != (ID) TemplateIDs.Template))
      {
        SheerResponse.Alert(DependenciesManager.ResourceManager.Localize("SELECT_TEMPLATE"), new string[0]);
      }
      else
      {
        SheerResponse.SetDialogValue(((object) selectionItem.ID).ToString());
        base.OnOK(sender, args);
      }
    }

    public string TemplateID => WebUtil.GetQueryString("id");
  }
}
