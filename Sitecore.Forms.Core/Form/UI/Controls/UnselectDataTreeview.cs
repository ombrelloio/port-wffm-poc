// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.UI.Controls.UnselectDataTreeview
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Web.UI;
using Sitecore.Web.UI.HtmlControls;
using System.Collections;
using System.Web.UI;

namespace Sitecore.Form.UI.Controls
{
  internal class UnselectDataTreeview : DataTreeview
  {
    protected override System.Web.UI.Control Populate(System.Web.UI.Control control, DataContext dataContext)
    {
      Assert.ArgumentNotNull((object) control, nameof (control));
      Assert.ArgumentNotNull((object) dataContext, nameof (dataContext));
      if (!IsTrackingViewState && !ViewStateDisabler.IsActive)
        TrackViewState();
      Item obj1;
      Item obj2;
      Item[] selected;
      dataContext.GetState(out obj1, out obj2, out selected);
      if (obj1 != null)
      {
        SetViewStateString("Root", ((object) obj1.ID).ToString());
        var control1 = control;
        if (this.ShowRoot)
        {
          TreeNode treeNode = this.GetTreeNode(obj1, control);
          treeNode.Expanded = true;
          treeNode.Selected = false;
          control1 = treeNode;
        }
        string selectedIds = UnselectDataTreeview.GetSelectedIDs(selected);
        base.Populate(dataContext, control1, obj1, obj2, selectedIds);
      }
      return control;
    }

    protected override void Populate(
      DataContext dataContext,
      System.Web.UI.Control control,
      Item root,
      Item folder,
      string selectedIDs)
    {
      Assert.ArgumentNotNull((object) dataContext, nameof (dataContext));
      Assert.ArgumentNotNull((object) control, nameof (control));
      Assert.ArgumentNotNull((object) root, nameof (root));
      Assert.ArgumentNotNull((object) folder, nameof (folder));
      Assert.ArgumentNotNull((object) selectedIDs, nameof (selectedIDs));
      Sitecore.Context.ClientPage.ClientResponse.DisableOutput();
      try
      {
                System.Web.UI.Control control1 = null;
        Item obj = (Item) null;
        foreach (Item child in (CollectionBase) dataContext.GetChildren(root))
        {
          TreeNode treeNode = this.GetTreeNode(child, control);
          treeNode.Expandable = dataContext.HasChildren(child);
          if (dataContext.IsAncestorOf(child, folder))
          {
            obj = child;
            control1 = treeNode;
            treeNode.Selected = false;
            treeNode.Expanded = !treeNode.Selected;
          }
          if (selectedIDs.Length > 0)
            treeNode.Selected = selectedIDs.IndexOf(((object) child.ID).ToString()) >= 0;
        }
        if (obj == null || obj.ID == folder.ID)
          return;
        base.Populate(dataContext, control1, obj, folder, selectedIDs);
      }
      finally
      {
        Sitecore.Context.ClientPage.ClientResponse.EnableOutput();
      }
    }

    private static string GetSelectedIDs(Item[] selected)
    {
      Assert.ArgumentNotNull((object) selected, nameof (selected));
      string empty = string.Empty;
      foreach (Item obj in selected)
      {
        if (obj != null)
          empty += (string) (object) obj.ID;
      }
      return empty;
    }
  }
}
