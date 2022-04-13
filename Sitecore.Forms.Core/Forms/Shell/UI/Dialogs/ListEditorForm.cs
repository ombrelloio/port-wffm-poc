// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Shell.UI.Dialogs.ListEditorForm
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Form.Core.Configuration;
using Sitecore.Form.Core.Utility;
using Sitecore.Shell.Applications.Dialogs.ItemLister;
using Sitecore.Web;
using Sitecore.Web.UI.HtmlControls;
using Sitecore.Web.UI.Pages;
using Sitecore.Web.UI.Sheer;
using Sitecore.WFFM.Abstractions.Dependencies;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sitecore.Forms.Shell.UI.Dialogs
{
  public class ListEditorForm : DialogForm
  {
    protected Sitecore.Web.UI.HtmlControls.Button Add;
    protected Listbox List;
    protected Border ListBorder;
    protected Edit ListEdit;
    protected Radiobutton ListMode;
    protected Sitecore.Web.UI.HtmlControls.Button Remove;
    protected Border SourceBorder;
    protected Sitecore.Web.UI.HtmlControls.Button SourceButton;
    protected Edit SourceEdit;
    protected Radiobutton SourceMode;

    protected override void OnLoad(EventArgs e)
    {
      Assert.ArgumentNotNull((object) e, nameof (e));
      base.OnLoad(e);
      if (Context.ClientPage.IsEvent)
        return;
      ((WebControl) this.Remove).Attributes.Add("disabled", "1");
      ((WebControl) this.Add).Attributes.Add("disabled", "1");
      ((Input) this.SourceEdit).ReadOnly = true;
      this.InitState();
    }

    protected void Add_Click()
    {
      string str = ((Sitecore.Web.UI.HtmlControls.Control) this.ListEdit).Value;
      if (str.Length == 0)
        SheerResponse.Alert(DependenciesManager.ResourceManager.Localize("EMPTY_INPUT"), new string[0]);
      else if (!this.Contains(str))
      {
        var listItem = new Sitecore.Web.UI.HtmlControls.ListItem();
        ((Sitecore.Web.UI.HtmlControls.Control) this.List).Controls.Add((Sitecore.Web.UI.HtmlControls.Control) listItem);
        listItem.Header = ((Sitecore.Web.UI.HtmlControls.Control) this.ListEdit).Value;
        ((Sitecore.Web.UI.HtmlControls.Control) listItem).Value = listItem.Header;
        ((Sitecore.Web.UI.HtmlControls.Control) listItem).ID = ((object) ShortID.NewId()).ToString();
        SheerResponse.SetOuterHtml("List", (Sitecore.Web.UI.HtmlControls.Control) this.List);
        SheerResponse.SetOuterHtml(((Sitecore.Web.UI.HtmlControls.Control) this.Add).ID, (Sitecore.Web.UI.HtmlControls.Control) this.Add);
        ((Sitecore.Web.UI.HtmlControls.Control) this.ListEdit).Value = string.Empty;
      }
      else
        SheerResponse.Alert(string.Format(DependenciesManager.ResourceManager.Localize("CONTAINS_VALUE"), (object) str), new string[0]);
    }

    protected void Remove_Click()
    {
      if (this.List.Selected == null)
        SheerResponse.Alert(DependenciesManager.ResourceManager.Localize("SELECT_VALUE"), new string[0]);
      foreach (Sitecore.Web.UI.HtmlControls.Control control in this.List.Selected)
        ((Sitecore.Web.UI.HtmlControls.Control) this.List).Controls.Remove(control);
      if (((Sitecore.Web.UI.HtmlControls.Control) this.List).Controls.Count == 0)
        SheerResponse.SetOuterHtml(((Sitecore.Web.UI.HtmlControls.Control) this.Remove).ID, (Sitecore.Web.UI.HtmlControls.Control) this.Remove);
      else if (((Sitecore.Web.UI.HtmlControls.Control) this.List).Controls[0] is Sitecore.Web.UI.HtmlControls.ListItem control2)
        control2.Selected = true;
      SheerResponse.SetOuterHtml(((Sitecore.Web.UI.HtmlControls.Control) this.List).ID, (Sitecore.Web.UI.HtmlControls.Control) this.List);
    }

    protected override void OnOK(object sender, EventArgs args)
    {
      string str;
      if (this.SourceMode.Checked)
      {
        str = StaticSettings.SourceMarker + ((Sitecore.Web.UI.HtmlControls.Control) this.SourceEdit).Value;
      }
      else
      {
        System.Collections.Generic.List<string> stringList = new System.Collections.Generic.List<string>();
        foreach (Sitecore.Web.UI.HtmlControls.ListItem listItem in this.List.Items)
          stringList.Add(listItem.Header);
        str = ParametersUtil.StringArrayToXml((IEnumerable<string>) stringList, "item");
      }
      SheerResponse.SetDialogValue(str == string.Empty ? "-" : str);
      base.OnOK(sender, args);
    }

    protected virtual void OnSetSourceMode()
    {
      ((Sitecore.Web.UI.HtmlControls.Control) this.SourceBorder).Disabled = false;
      ((Sitecore.Web.UI.HtmlControls.Control) this.ListBorder).Disabled = true;
      ((Input) this.ListEdit).ReadOnly = true;
      ((Sitecore.Web.UI.HtmlControls.Control) this.SourceButton).Disabled = false;
      this.UpdateSections();
    }

    protected virtual void OnSetListMode()
    {
      ((Sitecore.Web.UI.HtmlControls.Control) this.SourceBorder).Disabled = true;
      ((Sitecore.Web.UI.HtmlControls.Control) this.ListBorder).Disabled = false;
      ((Input) this.ListEdit).ReadOnly = false;
      ((Sitecore.Web.UI.HtmlControls.Control) this.SourceButton).Disabled = true;
      this.UpdateSections();
    }

    [HandleMessage("source:edit", true)]
    protected void BrowseSource(ClientPipelineArgs args)
    {
      Assert.ArgumentNotNull((object) args, nameof (args));
      if (!args.IsPostBack)
      {
        SelectItemOptions selectItemOptions = new SelectItemOptions();
        string source = this.Source;
        if (string.IsNullOrEmpty(source))
          source = ((object) ItemIDs.RootID).ToString();
        selectItemOptions.Root = StaticSettings.ContextDatabase.GetItem(((object) ItemIDs.RootID).ToString());
        selectItemOptions.SelectedItem = StaticSettings.ContextDatabase.SelectSingleItem(source);
        selectItemOptions.Icon = "Imaging/16x16/layer_blend.png";
        selectItemOptions.Title = DependenciesManager.ResourceManager.Localize("SOURCE_TITLE");
        selectItemOptions.Text = DependenciesManager.ResourceManager.Localize("SOURCE_DESC");
        selectItemOptions.ButtonText = DependenciesManager.ResourceManager.Localize("SELECT");
        SheerResponse.ShowModalDialog(((object) selectItemOptions.ToUrlString()).ToString(), true);
        args.WaitForPostBack();
      }
      else
      {
        if (!args.HasResult)
          return;
        Item obj = StaticSettings.ContextDatabase.GetItem(args.Result);
        if (obj == null)
          return;
        ((Sitecore.Web.UI.HtmlControls.Control) this.SourceEdit).Value = obj.Paths.FullPath;
      }
    }

    private bool Contains(string item)
    {
      foreach (object control in ((Sitecore.Web.UI.HtmlControls.Control) this.List).Controls)
      {
        if (control is Sitecore.Web.UI.HtmlControls.ListItem && ((Sitecore.Web.UI.HtmlControls.Control) (control as Sitecore.Web.UI.HtmlControls.ListItem)).Value == item)
          return true;
      }
      return false;
    }

    private void UpdateSections()
    {
      SheerResponse.SetOuterHtml(((Sitecore.Web.UI.HtmlControls.Control) this.SourceBorder).ID, (Sitecore.Web.UI.HtmlControls.Control) this.SourceBorder);
      SheerResponse.SetOuterHtml(((Sitecore.Web.UI.HtmlControls.Control) this.ListBorder).ID, (Sitecore.Web.UI.HtmlControls.Control) this.ListBorder);
    }

    private void InitState()
    {
      if (this.Source == null)
      {
        ((Sitecore.Web.UI.HtmlControls.Control) this.SourceEdit).Value = "/sitecore/content/Home";
        IEnumerable<string> items = this.Items;
        ((Sitecore.Web.UI.HtmlControls.Control) this.List).Controls.Clear();
        foreach (string str in items)
        {
          if (str != string.Empty)
          {
            var listItem = new Sitecore.Web.UI.HtmlControls.ListItem();
            ((Sitecore.Web.UI.HtmlControls.Control) this.List).Controls.Add((Sitecore.Web.UI.HtmlControls.Control) listItem);
            listItem.Header = str;
            ((Sitecore.Web.UI.HtmlControls.Control) listItem).Value = str;
            ((Sitecore.Web.UI.HtmlControls.Control) listItem).ID = ((object) ShortID.NewId()).ToString();
          }
        }
        this.ListMode.Checked = true;
        this.OnSetListMode();
      }
      else
      {
        ((Sitecore.Web.UI.HtmlControls.Control) this.SourceEdit).Value = this.Source;
        this.SourceMode.Checked = true;
        this.OnSetSourceMode();
      }
    }

    public string Value => Sitecore.Web.WebUtil.GetQueryString("value");

    public IEnumerable<string> Items => ParametersUtil.XmlToStringArray(this.Value);

    public string Source => this.Value.ToLower().StartsWith(StaticSettings.SourceMarker) ? this.Value.Substring(StaticSettings.SourceMarker.Length, this.Value.Length - StaticSettings.SourceMarker.Length) : (string) null;

    public string TargetID => Sitecore.Web.WebUtil.GetQueryString("target");
  }
}
