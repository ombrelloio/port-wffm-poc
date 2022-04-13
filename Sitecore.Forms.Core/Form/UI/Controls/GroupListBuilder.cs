// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.UI.Controls.GroupListBuilder
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Form.Core.Configuration;
using Sitecore.Form.Core.ContentEditor.Data;
using Sitecore.Form.Core.Data;
using Sitecore.Form.Core.Utility;
using Sitecore.Reflection;
using Sitecore.Resources;
using Sitecore.Web.UI;
using Sitecore.Web.UI.HtmlControls;
using Sitecore.Web.UI.WebControls;
using Sitecore.Web.UI.XmlControls;
using Sitecore.WFFM.Abstractions.Actions;
using Sitecore.WFFM.Abstractions.ContentEditor;
using Sitecore.WFFM.Abstractions.Dependencies;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sitecore.Form.UI.Controls
{
  public class GroupListBuilder
  {
    private FormDefinition formDefinition;

    public GroupListBuilder()
    {
      this.GroupEmptyMessage = DependenciesManager.ResourceManager.GetString("NO_ACTIONS_SPECIFIED");
      this.ListItemsEmptyMessage = this.GroupEmptyMessage;
      this.GroupClass = "scContentControlLayout";
      this.ListItemClass = "";
      this.ReadonlyMode = true;
    }

    private void BuildGroup(GridPanel grid, GroupDefinition group)
    {
      XmlControl webControl = Resource.GetWebControl("GroupListField") as XmlControl;
      Assert.IsNotNull((object) webControl, "Resource GroupListField is not found");
      grid.Controls.Add((System.Web.UI.Control) webControl);
      Item obj1 = (Item) null;
      if (group != null)
        obj1 = StaticSettings.ContextDatabase.GetItem(group.ID);
      string str1 = "<span style=\"color:#999999\">" + this.GroupEmptyMessage + "</span>";
      webControl["EditMessage"] = (object) DependenciesManager.ResourceManager.Localize("EDIT");
      if (obj1 != null)
      {
        webControl["GroupClass"] = (object) this.GroupClass;
        if (!this.ReadonlyMode)
        {
          webControl["GroupClick"] = (object) this.GroupClick;
          webControl["Visible"] = (object) "display:block";
        }
        else
          webControl["Visible"] = (object) "display:none";
        Item obj2 = StaticSettings.ContextDatabase.GetItem(this.ActionRoot ?? StaticSettings.CommandsRoot);
        string key = obj2 != null ? obj2.DisplayName : group.DisplayName;
        string str2 = Images.GetImage(((Appearance) obj1.Appearance).Icon, 16, 16, "absmiddle", "0px 4px 0px 0px") + Translate.Text(key);
        webControl["GroupName"] = (object) str2;
        if (!string.IsNullOrEmpty(this.Description))
        {
          Border border1 = new Border();
          border1.Padding = "0 0 0 10";
          border1.Margin = "4 0 4 0";
          Border border2 = border1;
          border2.Attributes["class"] = "scwfmActionShortDesc";
          border2.Controls.Add((System.Web.UI.Control) new Sitecore.Web.UI.HtmlControls.Literal()
          {
            Text = DependenciesManager.ResourceManager.Localize(this.Description)
          });
          webControl.AddControl((System.Web.UI.Control) border2);
        }
        Border border3 = new Border();
        border3.Padding = "0 0 0 10px";
        Border border4 = border3;
        webControl.AddControl((System.Web.UI.Control) border4);
        bool flag = false;
        if (group.ListItems.Any<IListItemDefinition>())
        {
          Tracking tracking = (Tracking) null;
          if (!string.IsNullOrEmpty(this.TrackingXml))
            tracking = new Tracking(this.TrackingXml, StaticSettings.ContextDatabase);
          foreach (ListItemDefinition listItem in group.ListItems)
            flag |= this.BuildListItem(border4, listItem, tracking, this.FormDefinition);
        }
        if (flag | this.BuildSystemAction(border4))
          return;
        border4.Padding = "0 0 0 23px";
                Sitecore.Web.UI.HtmlControls.Literal literal1 = new Sitecore.Web.UI.HtmlControls.Literal();
        literal1.Text = "<a ";
        if (!this.ReadonlyMode)
        {
                    Sitecore.Web.UI.HtmlControls.Literal literal2 = literal1;
          literal2.Text = literal2.Text + "href='#' onclick=\"" + this.GroupClick + "\" ";
        }
        literal1.Text += string.Join("", new string[5]
        {
          "class=' ",
          this.ListItemClass,
          "' >",
          Translate.Text(this.ListItemsEmptyMessage),
          "</a>"
        });
        border4.Controls.Add((System.Web.UI.Control) literal1);
      }
      else
        ReflectionUtil.SetProperty((object) webControl, "GroupName", (object) str1);
    }

    private void BuildGroups(GridPanel grid, ListDefinition list)
    {
      if (list != null && list.Groups.Any<IGroupDefinition>())
      {
        foreach (GroupDefinition group in list.Groups)
          this.BuildGroup(grid, group);
      }
      else
        this.BuildGroup(grid, (GroupDefinition) null);
    }

    public void BuildGrid(Sitecore.Web.UI.HtmlControls.Control parent)
    {
      Assert.ArgumentNotNull((object) parent, nameof (parent));
      GridPanel grid = new GridPanel();
      parent.Controls.Add((System.Web.UI.Control) grid);
      grid.RenderAs = (RenderAs) 1;
      grid.Width = Unit.Parse("100%");
      grid.Attributes["Class"] = this.Class;
      grid.Attributes["id"] = ((object) ShortID.NewId()).ToString();
      ListDefinition list = ListDefinition.Parse(this.Value);
      if (!list.Groups.Any<IGroupDefinition>())
      {
        string str = this.ActionRoot ?? StaticSettings.CommandsRoot;
        Item obj = StaticSettings.ContextDatabase.GetItem(str);
        GroupDefinition groupDefinition = new GroupDefinition()
        {
          DisplayName = obj != null ? obj.Name : DependenciesManager.ResourceManager.Localize("ACTIONS_TITLE"),
          ID = str,
          OnClick = string.Empty
        };
        list.AddGroup((IGroupDefinition) groupDefinition);
      }
      this.BuildGroups(grid, list);
    }

    private bool BuildListItem(
      Border border,
      ListItemDefinition liDefinition,
      Tracking tracking,
      FormDefinition definition)
    {
      Item obj = StaticSettings.ContextDatabase.GetItem(liDefinition.ItemID);
      if (obj != null)
      {
        ActionItem actionItem = new ActionItem(obj);
        if (ActionUtil.GetActionState((IActionItem) actionItem, this.ItemId, tracking, definition) != ActionState.Hidden && ((object) actionItem.InnerItem.ParentID).ToString().ToLower() == this.ActionRoot)
        {
          this.BuildListItem(border, actionItem, liDefinition.Unicid, liDefinition);
          return true;
        }
      }
      return false;
    }

    private void BuildListItem(
      Border border,
      ActionItem item,
      string unicId,
      ListItemDefinition liDefinition)
    {
      string icon = item.Icon;
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append(Images.GetImage(icon, 16, 16, "absmiddle", "0px 4px 0px 0px"));
      stringBuilder.Append(liDefinition != null ? liDefinition.GetTitle() : item.DisplayName);
      Border border1 = new Border();
      border.Controls.Add((System.Web.UI.Control) border1);
      NameValueCollection parameters = new NameValueCollection();
      parameters.Add("ListItemName", stringBuilder.ToString());
      parameters.Add("ListItemClass", this.ListItemClass);
      if (!this.ReadonlyMode && !string.IsNullOrEmpty(item.Editor))
      {
        string str = string.Format("javascript:return scForm.postEvent(this,event,'forms:edititem(unicid={0},param={1})')", (object) unicId, (object) item.Editor);
        parameters.Add("ListItemClick", str);
      }
      border1.Controls.Add((System.Web.UI.Control) XmlResourceUtil.GetResourceControl("Forms.ListItem", parameters));
    }

    private bool BuildSystemAction(Border parent)
    {
      bool flag = false;
      if (!string.IsNullOrEmpty(this.SystemRoot))
      {
        Item obj = StaticSettings.ContextDatabase.GetItem(this.SystemRoot);
        if (obj != null)
        {
          Tracking tracking = (Tracking) null;
          if (!string.IsNullOrEmpty(this.TrackingXml))
            tracking = new Tracking(this.TrackingXml, StaticSettings.ContextDatabase);
          foreach (Item child in obj.Children)
          {
            ActionItem actionItem = new ActionItem(child);
            if (ActionUtil.GetActionState((IActionItem) actionItem, this.ItemId, tracking, this.FormDefinition) != ActionState.Hidden)
            {
              flag = true;
              this.BuildListItem(parent, actionItem, ((object) ShortID.NewId()).ToString(), (ListItemDefinition) null);
            }
          }
        }
      }
      return flag;
    }

    public string Class { get; set; }

    public string ID { get; set; }

    public string Value { get; set; }

    public string GroupClass { get; set; }

    public string ListItemClass { get; set; }

    public string ListItemsEmptyMessage { get; set; }

    public string GroupEmptyMessage { get; set; }

    public bool ReadonlyMode { get; set; }

    public string GroupClick { get; set; }

    public string ActionRoot { get; set; }

    public string SystemRoot { get; set; }

    public string TrackingXml { get; set; }

    public string ItemId { get; set; }

    public string Description { get; set; }

    public string Structure { get; set; }

    public FormDefinition FormDefinition
    {
      get
      {
        if (this.formDefinition == null && !string.IsNullOrEmpty(this.Structure))
          this.formDefinition = FormDefinition.Parse(this.Structure);
        return this.formDefinition;
      }
    }
  }
}
