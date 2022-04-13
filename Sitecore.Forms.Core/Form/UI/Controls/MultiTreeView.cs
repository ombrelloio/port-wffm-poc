// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.UI.Controls.MultiTreeView
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Data.Items;
using Sitecore.Web.UI.HtmlControls;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI;

namespace Sitecore.Form.UI.Controls
{
  public class MultiTreeView : Sitecore.Web.UI.HtmlControls.Control
  {
    public static readonly string ShortDescriptionField = "{9541E67D-CE8C-4225-803D-33F7F29F09EF}";

    public event EventHandler SelectedChange;

    protected override void OnLoad(EventArgs e)
    {
      base.OnLoad(e);
      if (Sitecore.Context.ClientPage.IsEvent)
        return;
      foreach (KeyValuePair<string, string> root in this.Roots)
      {
        DataContext dataContext = this.AddDataContext(root.Key);
        Controls.Add(dataContext);
        bool flag = this.IsDataContextEmpty(dataContext, dataContext.CurrentItem);
        if (this.IsFullPath && !flag)
          Controls.Add(MultiTreeView.AddTitle(dataContext, root.Key, root.Value));
        var control = this.AddTree(dataContext);
        if (flag)
          control.Visible = false;
        Controls.Add(control);
      }
    }

    protected void DoubleClick(string id)
    {
      if (string.IsNullOrEmpty(id))
        return;
      this.InvokeDbClick(new ItemDoubleClickedEventArgs(id));
    }

    protected void Select(string id)
    {
      if (FindControl(id) is DataContext control2)
      {
        if (FindControl(ID + control2.ID + "_treeview") is UnselectDataTreeview control3)
          this.Selected = ((Treeview) control3).Selected == null || ((Treeview) control3).Selected.Length == 0 ? (string) null : (((Treeview) control3).Selected[0] as DataTreeNode).ItemID;
      }
      else
        this.Selected = (string) null;
      DataContext[] dataContexts = this.GetDataContexts(id);
      UnselectDataTreeview[] treeViews = this.GetTreeViews(dataContexts);
      this.ClearSelection(dataContexts, treeViews);
      this.RaiseSelectedChange();
    }

    private void RaiseSelectedChange()
    {
      if (this.SelectedChange == null)
        return;
      this.SelectedChange((object) this, EventArgs.Empty);
    }

    private DataContext[] GetDataContexts(string notIncludeContext)
    {
      List<DataContext> dataContextList = new List<DataContext>();
      foreach (KeyValuePair<string, string> root in this.Roots)
      {
        string id = ID + "dataContext" + root.Key;
        if (id != notIncludeContext && FindControl(id) is DataContext control2)
          dataContextList.Add(control2);
      }
      return dataContextList.ToArray();
    }

    private UnselectDataTreeview[] GetTreeViews(DataContext[] dataContexts)
    {
      List<UnselectDataTreeview> unselectDataTreeviewList = new List<UnselectDataTreeview>();
      foreach (var dataContext in dataContexts)
      {
        if (FindControl(ID + dataContext.ID + "_treeview") is UnselectDataTreeview control1)
          unselectDataTreeviewList.Add(control1);
      }
      return unselectDataTreeviewList.ToArray();
    }

    private void ClearSelection(DataContext[] dataContexts, UnselectDataTreeview[] treeviews)
    {
      foreach (DataContext dataContext in dataContexts)
        dataContext.ClearSelected();
      foreach (Treeview treeview in treeviews)
        treeview.ClearSelection();
    }

    private DataTreeview AddTree(DataContext dataContext)
    {
      UnselectDataTreeview unselectDataTreeview = new UnselectDataTreeview();
      unselectDataTreeview.DataContext = dataContext.ID;
      unselectDataTreeview.ID = ID + dataContext.ID + "_treeview";
      unselectDataTreeview.AllowDragging = false;
      ((Treeview) unselectDataTreeview).Click = ID + ".Select(\"" + dataContext.ID + "\")";
      ((Treeview) unselectDataTreeview).DblClick = ID + ".DoubleClick(\"" + dataContext.ID + "\")";
      unselectDataTreeview.AutoUpdateDataContext = false;
      unselectDataTreeview.ShowRoot = true;
      return (DataTreeview) unselectDataTreeview;
    }

    private DataContext AddDataContext(string root)
    {
      DataContext dataContext = new DataContext();
      dataContext.DataViewName = this.DataViewName;
      dataContext.Root = root;
      dataContext.Filter = this.Filter;
      dataContext.ID = ID + "dataContext" + root;
      return dataContext;
    }

    private static Border AddTitle(DataContext dataContext, string name, string desc)
    {
      Item obj = dataContext.GetItem(name);
      Border border = new Border();
      border.Class = "scTitleTree";
      Literal literal = new Literal();
      string str = !string.IsNullOrEmpty(((BaseItem) obj).Fields[MultiTreeView.ShortDescriptionField].Value) ? ((BaseItem) obj).Fields[MultiTreeView.ShortDescriptionField].Value : desc ?? string.Empty;
      literal.Text = !string.IsNullOrEmpty(str) ? str : (obj.Parent != null ? obj.Parent.Paths.FullPath : "/");
      border.Controls.Add(literal);
      return border;
    }

    private bool IsDataContextEmpty(DataContext context, Item item)
    {
      foreach (Item child in (CollectionBase) context.GetChildren(item))
      {
        if (((object) child.TemplateID).ToString() == this.TemplateID || !this.IsDataContextEmpty(context, child))
          return false;
      }
      return true;
    }

    private void InvokeDbClick(ItemDoubleClickedEventArgs e)
    {
      EventHandler<ItemDoubleClickedEventArgs> dbClick = this.DbClick;
      if (dbClick == null)
        return;
      dbClick((object) this, e);
    }

    public Dictionary<string, string> Roots
    {
      get => ((Component) this).ServerProperties[nameof (Roots)] as Dictionary<string, string>;
      set => ((Component) this).ServerProperties[nameof (Roots)] = (object) value;
    }

    public string Filter
    {
      get => GetViewStateString(nameof (Filter));
      set => SetViewStateString(nameof (Filter), value);
    }

    public bool IsFullPath
    {
      get => GetViewStateBool(nameof (IsFullPath));
      set => SetViewStateBool(nameof (IsFullPath), value);
    }

    public string TemplateID
    {
      get => GetViewStateString(nameof (TemplateID));
      set => SetViewStateString(nameof (TemplateID), value);
    }

    public string DataViewName
    {
      get => GetViewStateString(nameof (DataViewName));
      set => SetViewStateString(nameof (DataViewName), value);
    }

    public string Selected
    {
      get => GetViewStateString("SelectedID", (string) null);
      set => SetViewStateString("SelectedID", value);
    }

    public event EventHandler<ItemDoubleClickedEventArgs> DbClick;
  }
}
