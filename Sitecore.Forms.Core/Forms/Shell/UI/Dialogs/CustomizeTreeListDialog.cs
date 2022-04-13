// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Shell.UI.Dialogs.CustomizeTreeListDialog
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Diagnostics;
using Sitecore.Form.Core.Utility;
using Sitecore.Shell.Applications.ContentEditor;
using Sitecore.Web;
using Sitecore.Web.UI.HtmlControls;
using Sitecore.Web.UI.Pages;
using Sitecore.Web.UI.Sheer;
using Sitecore.Web.UI.WebControls;
using Sitecore.Web.UI.XmlControls;
using System;
using System.Web.UI;

namespace Sitecore.Forms.Shell.UI.Dialogs
{
  public class CustomizeTreeListDialog : DialogForm
  {
    protected XmlControl Dialog;
    protected TreeList TreeList;
    private string dataViewName;
    private bool showRoot;

    protected override void OnLoad(EventArgs e)
    {
      Assert.ArgumentNotNull((object) e, nameof (e));
      base.OnLoad(e);
      if (Context.ClientPage.IsEvent)
        return;
      UrlHandle handle = UrlHandle.Get();
      this.TreeList.Source = handle["source"];
      this.TreeList.SetValue(StringUtil.GetString(new string[1]
      {
        handle["value"]
      }));
      this.TreeList.IncludeTemplatesForDisplay = handle["includetemplatesfordisplay"] ?? string.Empty;
      this.TreeList.IncludeTemplatesForSelection = handle["includetemplatesforselection"] ?? string.Empty;
      this.TreeList.IncludeItemsForDisplay = handle["includeitemsfordisplay"] ?? string.Empty;
      this.TreeList.ExcludeTemplatesForDisplay = handle["excludetemplatesfordisplay"] ?? string.Empty;
      this.TreeList.ExcludeTemplatesForSelection = handle["excludetemplatesforselection"] ?? string.Empty;
      this.TreeList.ExcludeItemsForDisplay = handle["excludeitemsfordisplay"] ?? string.Empty;
      this.dataViewName = handle["dataviewname"];
      this.showRoot = MainUtil.GetBool(handle["showroot"], true);
      (Context.ClientPage).Page.LoadComplete += new EventHandler(this.OnLoadComplete);
      this.TreeList.ReadOnly = MainUtil.GetBool(handle["readonly"], false);
      this.TreeList.AllowMultipleSelection = MainUtil.GetBool(handle["allowmultipleselection"], false);
      if (!string.IsNullOrEmpty(handle["databasename"]))
        this.TreeList.DatabaseName = handle["databasename"];
      this.Localize(handle);
    }

    private void OnLoadComplete(object sender, EventArgs e)
    {
      DataContext firstOrDefault1 = (DataContext)Sitecore.Form.Core.Utility.WebUtil.FindFirstOrDefault(this.TreeList, c => c is DataContext);
      if (firstOrDefault1 != null)
      {
        firstOrDefault1.DataViewName = string.IsNullOrEmpty(this.dataViewName) ? "Master" : this.dataViewName;
        firstOrDefault1.ShowRoot = this.showRoot;
      }
      TreeviewEx firstOrDefault2 = (TreeviewEx)Sitecore.Form.Core.Utility.WebUtil.FindFirstOrDefault(this.TreeList, c => c is TreeviewEx);
      if (firstOrDefault2 == null)
        return;
      firstOrDefault2.ShowRoot = this.showRoot;
    }

    protected virtual void Localize(UrlHandle handle)
    {
      this.Dialog["Header"] = (object) (handle["title"] ?? string.Empty);
      this.Dialog["text"] = (object) (handle["text"] ?? string.Empty);
      this.Dialog["icon"] = (object) (handle["icon"] ?? string.Empty);
    }

    protected override void OnOK(object sender, EventArgs args)
    {
      Assert.ArgumentNotNull(sender, nameof (sender));
      Assert.ArgumentNotNull((object) args, nameof (args));
      string str = this.TreeList.GetValue();
      if (string.IsNullOrEmpty(str))
        str = "-";
      SheerResponse.SetDialogValue(str);
      base.OnOK(sender, args);
    }
  }
}
