// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Shell.UI.Controls.TreePickerEx
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Web.UI;
using Sitecore.Web.UI.HtmlControls;
using Sitecore.Web.UI.Sheer;
using Sitecore.WFFM.Abstractions.Dependencies;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sitecore.Forms.Shell.UI.Controls
{
  public class TreePickerEx : TreePicker
  {
    protected override void DropDown()
    {
      DataContext subControl = Sitecore.Context.ClientPage.FindSubControl((this).DataContext) as DataContext;
      if (!string.IsNullOrEmpty((this).Value))
        subControl.Folder = (this).Value;
      var hiddenHolder = UIUtil.GetHiddenHolder(this);
      DataTreeNode dataTreeNode = (DataTreeNode) null;
      Scrollbox scrollbox1 = new Scrollbox();
      (scrollbox1).Width = (Unit) 300;
      (scrollbox1).Height = (Unit) 400;
      (scrollbox1).Padding = "0";
      (scrollbox1).Border = "1px solid black";
      Scrollbox scrollbox2 = scrollbox1;
      Sitecore.Context.ClientPage.AddControl(hiddenHolder, scrollbox2);
      DataTreeview dataTreeview1 = new DataTreeview();
      (dataTreeview1).DataContext = (this).DataContext;
      (dataTreeview1).ID = (this).ID + "_treeview";
      dataTreeview1.AllowDragging = false;
      DataTreeview dataTreeview2 = dataTreeview1;
      if (this.AllowNone)
      {
        dataTreeNode = new DataTreeNode();
        Sitecore.Context.ClientPage.AddControl(dataTreeview2, dataTreeNode);
        (dataTreeNode).ID = (this).ID + "_none";
        ((HeaderedItemsControl) dataTreeNode).Header = Sitecore.StringExtensions.StringExtensions.FormatWith("[{0}]", new object[1]
        {
          (object) DependenciesManager.ResourceManager.Localize("NONE")
        });
        (dataTreeNode).Expandable = false;
        (dataTreeNode).Expanded = false;
        (dataTreeNode).Value = "none";
        (dataTreeNode).Icon = "Applications/16x16/forbidden.png";
      }
      Sitecore.Context.ClientPage.AddControl(scrollbox2, dataTreeview2);
      (dataTreeview2).Width = new Unit(100.0, UnitType.Percentage);
      ((Treeview) dataTreeview2).Click = (this).ID + ".Select";
      (dataTreeview2).DataContext = (this).DataContext;
      if (string.IsNullOrEmpty((this).Value) || (this).Value == subControl.Root)
      {
        ((Treeview) dataTreeview2).ClearSelection();
        if (dataTreeNode != null)
          (dataTreeNode).Selected = true;
      }
      ((HeaderedItemsControl) (dataTreeview2).Controls[this.AllowNone ? 2 : 1]).Header = this.NullName;
      SheerResponse.ShowPopup((this).ID, "below-right", scrollbox2);
    }

    protected override string GetDisplayValue()
    {
      if ((this).Value == (Sitecore.Context.ClientPage.FindSubControl((this).DataContext) as DataContext).Root || string.IsNullOrEmpty((this).Value))
        return this.NullName;
      string[] strArray = base.GetDisplayValue().Split('/');
      return strArray[strArray.Length - 1];
    }

    protected new void Select()
    {
      DataTreeview subControl1 = Sitecore.Context.ClientPage.FindSubControl((this).ID + "_treeview") as DataTreeview;
      Assert.IsNotNull((object) subControl1, typeof (DataTreeview));
      Item selectionItem = subControl1.GetSelectionItem();
      if (selectionItem != null)
      {
        DataContext subControl2 = Sitecore.Context.ClientPage.FindSubControl((this).DataContext) as DataContext;
        if (selectionItem.ID == subControl2.GetItem(subControl2.Root).ID)
          (this).Value = this.NullName;
        else
          (this).Value = ((object) selectionItem.ID).ToString();
      }
      SheerResponse.SetAttribute((this).ID + "_edit", "value", GetDisplayValue());
      this.DoChanged();
    }

    protected override void Click()
    {
      if ((this).Disabled)
        return;
      DropDown();
    }

    protected virtual string NullName => DependenciesManager.ResourceManager.Localize("CHOOSE_EXISTING_GOAL");
  }
}
