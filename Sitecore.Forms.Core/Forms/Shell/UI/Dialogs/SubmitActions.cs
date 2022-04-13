// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Shell.UI.Dialogs.SubmitActions
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Configuration;
using Sitecore.ContentSearch.Utilities;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Form.Core.Configuration;
using Sitecore.Form.Core.ContentEditor.Data;
using Sitecore.Form.Core.Data;
using Sitecore.Form.Core.Utility;
using Sitecore.Forms.Core.Data;
using Sitecore.Globalization;
using Sitecore.Resources;
using Sitecore.Text;
using Sitecore.Web;
using Sitecore.Web.UI.HtmlControls;
using Sitecore.Web.UI.Pages;
using Sitecore.Web.UI.Sheer;
using Sitecore.Web.UI.XmlControls;
using Sitecore.WFFM.Abstractions.Actions;
using Sitecore.WFFM.Abstractions.ContentEditor;
using Sitecore.WFFM.Abstractions.Dependencies;
using Sitecore.WFFM.Abstractions.Shared;
using System;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sitecore.Forms.Shell.UI.Dialogs
{
  public class SubmitActions : DialogForm
  {
    protected Scrollbox AddedCommands;
    protected TreePicker Commands;
    protected DataContext CommandsDataContext;
    protected Sitecore.Web.UI.HtmlControls.Button AddButton;
    protected Sitecore.Web.UI.HtmlControls.Button Edit;
    protected Sitecore.Web.UI.HtmlControls.Button Remove;
    protected Sitecore.Web.UI.HtmlControls.Button MoveDown;
    protected Sitecore.Web.UI.HtmlControls.Button MoveUp;
    protected XmlControl Dialog;
    protected Sitecore.Web.UI.HtmlControls.Literal ActionsLablel;
    protected Sitecore.Web.UI.HtmlControls.Literal AddedActionsLablel;
    protected Tab ActionsTab;
    protected Tab MessagesTab;
    protected Border MessagesBorder;
    private const string FailedMessageKey = "failed_message";
    private readonly IItemRepository itemRepository;
    private Database database;
    private FormDefinition formDefintion;
    private Tracking tracking;
    private FormItem formItem;

    public SubmitActions()
      : this(DependenciesManager.Resolve<IItemRepository>())
    {
    }

    public SubmitActions(IItemRepository itemRepository)
    {
      Assert.ArgumentNotNull((object) itemRepository, nameof (itemRepository));
      this.itemRepository = itemRepository;
    }

    protected override void OnLoad(EventArgs e)
    {
      base.OnLoad(e);
      if (Context.ClientPage.IsEvent)
        return;
      this.Localize();
      UrlHandle urlHandle = UrlHandle.Get(new UrlString(Sitecore.Web.WebUtil.GetRawUrl()), "hdl", false);
      this.Dialog["Header"] = (object) StringUtil.GetString(new string[2]
      {
        urlHandle["title"],
        "Select Actions."
      });
      this.Dialog["Text"] = (object) StringUtil.GetString(new string[2]
      {
        urlHandle["desc"],
        string.Empty
      });
      this.ActionsLablel.Text = StringUtil.GetString(new string[2]
      {
        urlHandle["actions"],
        "Actions"
      });
      this.AddedActionsLablel.Text = StringUtil.GetString(new string[2]
      {
        urlHandle["addedactions"],
        "Added Actions:"
      });
      ((HeaderedItemsControl) this.AddButton).Header = DependenciesManager.ResourceManager.Localize("ADD");
      ((HeaderedItemsControl) this.Edit).Header = DependenciesManager.ResourceManager.Localize("EDIT");
      ((HeaderedItemsControl) this.Remove).Header = DependenciesManager.ResourceManager.Localize("REMOVE");
      ((HeaderedItemsControl) this.MoveUp).Header = DependenciesManager.ResourceManager.Localize("MOVE_UP");
      ((HeaderedItemsControl) this.MoveDown).Header = DependenciesManager.ResourceManager.Localize("MOVE_DOWN");
      this.TrackingXml = urlHandle["tracking"];
      this.Structure = urlHandle["structure"];
      this.CommandsDataContext.Root = this.Root ?? this.CommandsDataContext.Root;
      this.CommandsDataContext.Filter += " AND not(contains('', @Assembly) AND contains('', @Factory Object Name))";
      this.Dialog["Icon"] = (object) ((Appearance) this.CommandsDataContext.GetRoot().Appearance).Icon;
      this.Definition = this.ServerDefinition;
      this.Refresh(this.Definition);
      this.UpdateButtonsState(false);
      this.UpdateMessagesList(false);
    }

    private void Localize()
    {
      ((HeaderedItemsControl) this.ActionsTab).Header = DependenciesManager.ResourceManager.Localize("ACTIONS_TITLE");
      ((HeaderedItemsControl) this.MessagesTab).Header = DependenciesManager.ResourceManager.Localize("FAILURE_MESSAGES");
    }

    protected override void OnOK(object sender, EventArgs args)
    {
      this.SaveFailureMessages();
      string str = this.Definition.ToXml();
      if (str.Length == 0)
        str = "-";
      UrlString urlString = new UrlString(Sitecore.Web.WebUtil.GetRawUrl());
      UrlHandle urlHandle = UrlHandle.Get(urlString, "hdl", false);
      urlHandle["tracking"] = this.TrackingXml;
      urlHandle.Add(urlString);
      SheerResponse.SetDialogValue(str);
      base.OnOK(sender, args);
    }

    private void SaveFailureMessages()
    {
      if (!this.Definition.Groups.Any<IGroupDefinition>())
        return;
      EnumerableExtensions.ForEach<IListItemDefinition>(this.Definition.Groups.First<IGroupDefinition>().ListItems, (System.Action<IListItemDefinition>) (i => i.SetFailedMessageForLanguage(this.GetMessage(i.Unicid), this.CurrentLanguage)));
    }

    private string GetMessage(string uniqId) => Context.ClientPage.ClientRequest.Form["failed_message" + uniqId];

    protected void OnCommandClick(string index, string isSystem)
    {
      int result;
      Assert.IsTrue(int.TryParse(index, out result), "index parameter is not int type");
      Assert.IsTrue(result >= 0 && result < ((Sitecore.Web.UI.HtmlControls.Control) this.AddedCommands).Controls.Count, "index parameter is not int type");
      this.SelectedIndex = result;
      bool isSystem1 = MainUtil.GetBool(isSystem, false);
      ListDefinition definition = this.Definition;
      string hiddenPostfix = ((WebControl) ((Sitecore.Web.UI.HtmlControls.Control) this.AddedCommands).Controls[this.SelectedIndex].Controls[0]).Attributes["class"].Contains(" scwfmHidden") ? " scwfmHidden" : string.Empty;
      Context.ClientPage.ClientResponse.SetAttribute(StringUtil.GetString(this.Controls[this.SelectedIndex]), "class", string.Join(string.Empty, new string[2]
      {
        definition.Groups.First<IGroupDefinition>().ListItems.Count<IListItemDefinition>() > this.SelectedIndex ? "scFbCommand" : "scFbSystemCommand",
        hiddenPostfix
      }));
      this.UpdateButtonsState(isSystem1);
      this.DeselectCommandsExceptGiven(result, hiddenPostfix);
      Context.ClientPage.ClientResponse.SetAttribute(StringUtil.GetString(this.Controls[this.SelectedIndex]), "class", string.Join(string.Empty, new string[2]
      {
        isSystem1 ? "scFbSystemSelectedCommand" : "scFbSelectedCommand",
        hiddenPostfix
      }));
    }

    private void DeselectCommandsExceptGiven(int givenCommandIndex, string hiddenPostfix)
    {
      for (int index = 0; index < ((Sitecore.Web.UI.HtmlControls.Control) this.AddedCommands).Controls.Count; ++index)
      {
        if (index != givenCommandIndex)
        {
          string attribute = ((WebControl) ((Sitecore.Web.UI.HtmlControls.Control) this.AddedCommands).Controls[index].Controls[0]).Attributes["class"];
          string str = string.Empty;
          if (attribute.Contains("scFbCommand") || attribute.Contains("scFbSelectedCommand"))
            str = "scFbCommand";
          else if (attribute.Contains("scFbSystemCommand") || attribute.Contains("scFbSystemSelectedCommand"))
            str = "scFbSystemCommand";
          if (!string.IsNullOrEmpty(str))
            Context.ClientPage.ClientResponse.SetAttribute(StringUtil.GetString(this.Controls[index]), "class", str + hiddenPostfix);
        }
      }
    }

    private void UpdateMessagesList(bool refreshClient)
    {
      ((Sitecore.Web.UI.HtmlControls.Control) this.MessagesBorder).Controls.Clear();
      if (this.Definition.Groups.Any<IGroupDefinition>())
        EnumerableExtensions.ForEach<IListItemDefinition>(this.Definition.Groups.First<IGroupDefinition>().ListItems, (System.Action<IListItemDefinition>) (i =>
        {
          IActionItem action = this.itemRepository.CreateAction(i.ItemID);
          if (action == null)
            return;
          this.BindActionMessage(action, i);
        }));
      if (((Sitecore.Web.UI.HtmlControls.Control) this.MessagesBorder).Controls.Count == 0)
      {
        Border border1 = new Border();
        ((Sitecore.Web.UI.HtmlControls.Control) border1).Class = "scWfmMessageHolder";
        Border border2 = border1;
        ((Sitecore.Web.UI.HtmlControls.Control) border2).Controls.Add((Sitecore.Web.UI.HtmlControls.Control) new Sitecore.Web.UI.HtmlControls.Literal()
        {
          Text = DependenciesManager.ResourceManager.Localize("ADD_ACTIONS_ON_TAB_AT_FIRST", DependenciesManager.ResourceManager.Localize("ACTIONS_TITLE"))
        });
        ((Sitecore.Web.UI.HtmlControls.Control) this.MessagesBorder).Controls.Add((Sitecore.Web.UI.HtmlControls.Control) border2);
      }
      if (!refreshClient)
        return;
      SheerResponse.SetOuterHtml(((Sitecore.Web.UI.HtmlControls.Control) this.MessagesBorder).ID, (Sitecore.Web.UI.HtmlControls.Control) this.MessagesBorder);
    }

    private void BindActionMessage(IActionItem action, IListItemDefinition item)
    {
      if (ActionUtil.GetActionState(action, ((object) action.ID).ToString(), this.Tracking, this.FormDefinition) == ActionState.Hidden)
        return;
      ControlCollection controls = ((Sitecore.Web.UI.HtmlControls.Control) this.MessagesBorder).Controls;
            Sitecore.Web.UI.HtmlControls.Literal literal1 = new Sitecore.Web.UI.HtmlControls.Literal();
      literal1.Text = DependenciesManager.ResourceManager.Localize("WHNE_ACTION_FAILED_SHOW", item.GetTitle() ?? action.DisplayName);
      ((Sitecore.Web.UI.HtmlControls.Control) literal1).Class = "scWfmActionTitle";
            Sitecore.Web.UI.HtmlControls.Literal literal2 = literal1;
      controls.Add((Sitecore.Web.UI.HtmlControls.Control) literal2);
      Memo memo = new Memo();
      ((Sitecore.Web.UI.HtmlControls.Control) memo).ID = "failed_message" + item.Unicid;
      ((Sitecore.Web.UI.HtmlControls.Control) memo).Class = "scWfmActionFailedText";
      ((Sitecore.Web.UI.HtmlControls.Control) memo).Value = item.GetFailedMessageForLanguage(this.CurrentLanguage, action.ActionType != ActionType.Check, ID.Null);
      ((Sitecore.Web.UI.HtmlControls.Control) this.MessagesBorder).Controls.Add((Sitecore.Web.UI.HtmlControls.Control) memo);
    }

    private void UpdateButtonsState(bool isSystem)
    {
      ((Sitecore.Web.UI.HtmlControls.Control) this.MoveDown).Disabled = true;
      ((Sitecore.Web.UI.HtmlControls.Control) this.MoveUp).Disabled = true;
      ((Sitecore.Web.UI.HtmlControls.Control) this.Edit).Disabled = true;
      ((Sitecore.Web.UI.HtmlControls.Control) this.Remove).Disabled = true;
      int selectedIndex = this.SelectedIndex;
      bool flag1 = false;
      int index1 = selectedIndex;
      while (--index1 >= 0)
      {
        if (!((WebControl) ((Sitecore.Web.UI.HtmlControls.Control) this.AddedCommands).Controls[index1].Controls[0]).Attributes["class"].Contains(" scwfmHidden"))
        {
          flag1 = true;
          break;
        }
      }
      if (flag1 && !isSystem)
        ((Sitecore.Web.UI.HtmlControls.Control) this.MoveUp).Disabled = false;
      ListDefinition definition = this.Definition;
      if (!isSystem && selectedIndex > -1 && definition.Groups.Any<IGroupDefinition>() && definition.Groups.First<IGroupDefinition>().ListItems.Any<IListItemDefinition>() && definition.Groups.First<IGroupDefinition>().ListItems.Count<IListItemDefinition>() > selectedIndex && !((WebControl) ((Sitecore.Web.UI.HtmlControls.Control) this.AddedCommands).Controls[selectedIndex].Controls[0]).Attributes["class"].Contains(" scwfmHidden") && !((WebControl) ((Sitecore.Web.UI.HtmlControls.Control) this.AddedCommands).Controls[selectedIndex].Controls[0]).Attributes["class"].Contains(" scwfmDisabled"))
        ((Sitecore.Web.UI.HtmlControls.Control) this.Remove).Disabled = false;
      if (definition.Groups.Any<IGroupDefinition>() && definition.Groups.First<IGroupDefinition>().ListItems.Any<IListItemDefinition>() && definition.Groups.First<IGroupDefinition>().ListItems.Count<IListItemDefinition>() > selectedIndex && !((WebControl) ((Sitecore.Web.UI.HtmlControls.Control) this.AddedCommands).Controls[selectedIndex].Controls[0]).Attributes["class"].Contains(" scwfmHidden") && !((WebControl) ((Sitecore.Web.UI.HtmlControls.Control) this.AddedCommands).Controls[selectedIndex].Controls[0]).Attributes["class"].Contains(" scwfmDisabled"))
      {
        ActionItem actionItem = new ActionItem(this.CurrentDatabase.GetItem(definition.Groups.First<IGroupDefinition>().GetListItem(this.SelectedIndex).ItemID));
        IAction action = DependenciesManager.FactoryObjectsProvider.CreateAction<IAction>((IActionItem) actionItem);
        if (!string.IsNullOrEmpty(actionItem.Editor) && DependenciesManager.RequirementsChecker.CheckRequirements(action.GetType()))
          ((Sitecore.Web.UI.HtmlControls.Control) this.Edit).Disabled = false;
      }
      if (definition.Groups.Any<IGroupDefinition>())
      {
        bool flag2 = false;
        int index2 = selectedIndex;
        while (++index2 < definition.Groups.First<IGroupDefinition>().ListItems.Count<IListItemDefinition>())
        {
          if (!((WebControl) ((Sitecore.Web.UI.HtmlControls.Control) this.AddedCommands).Controls[index2].Controls[0]).Attributes["class"].Contains(" scwfmHidden"))
          {
            flag2 = true;
            break;
          }
        }
        if (flag2)
          ((Sitecore.Web.UI.HtmlControls.Control) this.MoveDown).Disabled = false;
      }
      SheerResponse.Refresh((Sitecore.Web.UI.HtmlControls.Control) this.Edit);
      SheerResponse.Refresh((Sitecore.Web.UI.HtmlControls.Control) this.MoveUp);
      SheerResponse.Refresh((Sitecore.Web.UI.HtmlControls.Control) this.MoveDown);
    }

    private void Refresh(ListDefinition listdefinition)
    {
      this.Controls = new ArrayList();
      ((Sitecore.Web.UI.HtmlControls.Control) this.AddedCommands).Controls.Clear();
      ((Sitecore.Web.UI.HtmlControls.Control) this.Remove).Disabled = true;
      if (listdefinition.Groups.Any<IGroupDefinition>())
      {
        IGroupDefinition groupDefinition = listdefinition.Groups.FirstOrDefault<IGroupDefinition>();
        if (groupDefinition != null)
        {
          int number = 0;
          int selectedIndex = this.SelectedIndex;
          foreach (ListItemDefinition listItem1 in groupDefinition.ListItems)
          {
            ((Sitecore.Web.UI.HtmlControls.Control) this.Remove).Disabled = false;
            Item obj = this.CurrentDatabase.GetItem(listItem1.ItemID);
            if (obj != null)
            {
              ActionItem listItem2 = new ActionItem(obj);
              string className;
              switch (ActionUtil.GetActionState((IActionItem) listItem2, this.CurrentID, this.Tracking, this.FormDefinition))
              {
                case ActionState.Disabled:
                case ActionState.DisabledSingleCall:
                  className = "scwfmDisabled";
                  break;
                case ActionState.Hidden:
                  className = "scwfmHidden";
                  break;
                default:
                  className = string.Empty;
                  break;
              }
              this.RenderAction(listItem2, listItem1, number, selectedIndex, false, className);
            }
            else
              this.RenderAction((ActionItem) null, listItem1, number, selectedIndex, false, "scwfmHidden");
            ++number;
          }
          if (!string.IsNullOrEmpty(this.SystemRoot))
          {
            Item obj = StaticSettings.ContextDatabase.GetItem(this.SystemRoot);
            if (obj != null)
            {
              foreach (Item child in obj.Children)
              {
                ActionItem listItem = new ActionItem(child);
                if (ActionUtil.GetActionState((IActionItem) listItem, this.CurrentID, this.tracking, this.FormDefinition) != ActionState.Hidden)
                {
                  this.RenderAction(listItem, (ListItemDefinition) null, number, selectedIndex, true, (string) null);
                  ++number;
                }
              }
            }
          }
          this.UpdateButtonsState(false);
        }
      }
      SheerResponse.Refresh((Sitecore.Web.UI.HtmlControls.Control) this.Remove);
      SheerResponse.SetOuterHtml("AddedCommands", (Sitecore.Web.UI.HtmlControls.Control) this.AddedCommands);
    }

    private FormDefinition FormDefinition
    {
      get
      {
        if (this.formDefintion == null && !string.IsNullOrEmpty(this.Structure))
          this.formDefintion = FormDefinition.Parse(this.Structure);
        return this.formDefintion;
      }
    }

    private void RenderAction(
      ActionItem listItem,
      ListItemDefinition definition,
      int number,
      int selected,
      bool isSystem,
      string className)
    {
      XmlControl webControl = Resource.GetWebControl("SubmitCommands.CommandItem") as XmlControl;
      Assert.ArgumentNotNull((object) webControl, "webControl");
      AddedCommands.Controls.Add(webControl);
      string uniqueId = Sitecore.Web.UI.HtmlControls.Control.GetUniqueID("C");
      webControl["Click"] = (object) ("OnCommandClick(\"" + (object) number + "\", \"" + (object) (isSystem ? 1 : 0) + "\")");
      webControl["DblClick"] = (object) "forms:editcommand";
      webControl["Class"] = !isSystem ? (number == selected ? (object) "scFbSelectedCommand" : (object) "scFbCommand") : (number == selected ? (object) "scFbSystemSelectedCommand" : (object) "scFbSystemCommand");
      if (!string.IsNullOrEmpty(className))
      {
        XmlControl xmlControl1 = webControl;
        XmlControl xmlControl2 = xmlControl1;
        object obj = xmlControl1["Class"];
        string str1 = string.Join(string.Empty, new string[2]
        {
          " ",
          className
        });
        string str2 = obj.ToString() + str1;
        xmlControl2["Class"] = (object) str2;
      }
      webControl["Description"] = listItem != null ? (object) listItem.Tooltip : (object) string.Empty;
      this.Controls.Add((object) uniqueId);
      if (listItem != null)
      {
        webControl["ID"] = (object) uniqueId;
        webControl["Icon"] = (object) listItem.Icon;
        webControl["Header"] = definition != null ? (object) definition.GetTitle() : (object) listItem.DisplayName;
      }
      else
      {
        webControl["ID"] = (object) uniqueId;
        webControl["Icon"] = (object) "Applications/24x24/forbidden.png";
        webControl["Header"] = (object) "Unknown action";
      }
    }

    [HandleMessage("forms:addcommand", true)]
    protected void Add(ClientPipelineArgs args)
    {
      Assert.IsNotNull((object) args, "Parameter args is null");
      if (args.IsPostBack)
        return;
      Item obj = this.CurrentDatabase.GetItem(Commands.Value);
      if (!ActionItem.IsAction(obj))
        return;
      ActionItem actionItem = new ActionItem(obj);
      ListDefinition definition = this.Definition;
      if (!string.IsNullOrEmpty(this.CurrentID) && StaticSettings.ContextDatabase.GetItem(this.CurrentID) != null)
      {
        Tracking tracking = (Tracking) null;
        if (!string.IsNullOrEmpty(this.TrackingXml))
          tracking = new Tracking(this.TrackingXml, StaticSettings.ContextDatabase);
        switch (ActionUtil.GetActionState((IActionItem) actionItem, this.CurrentID, tracking, this.formDefintion))
        {
          case ActionState.Disabled:
          case ActionState.Hidden:
            SheerResponse.Alert(DependenciesManager.ResourceManager.Localize("ACTION_DISABLED"), Array.Empty<string>());
            return;
          case ActionState.SingleCall:
          case ActionState.DisabledSingleCall:
            if (this.Definition.Groups.First<IGroupDefinition>().GetListItems(actionItem.ID).Any<IListItemDefinition>())
            {
              SheerResponse.Alert(DependenciesManager.ResourceManager.Localize("ACTION_SINGLECALL"), Array.Empty<string>());
              return;
            }
            break;
        }
      }
      if (!definition.Groups.Any<IGroupDefinition>())
        definition.AddGroup((IGroupDefinition) new GroupDefinition()
        {
          DisplayName = "Commands",
          ID = StaticSettings.CommandsRoot,
          OnClick = string.Empty
        });
      IGroupDefinition groupDefinition = definition.Groups.FirstOrDefault<IGroupDefinition>();
      if (groupDefinition == null)
        return;
      IListItemDefinition listItemDefinition = (IListItemDefinition) new ListItemDefinition()
      {
        ItemID = ((object) obj.ID).ToString(),
        Parameters = ((BaseItem) obj).Fields[Sitecore.Form.Core.Configuration.FieldIDs.ActionParametersID].Value,
        Unicid = ((object) ShortID.NewId()).ToString()
      };
      groupDefinition.AddListItem(listItemDefinition);
      this.SelectedIndex = groupDefinition.ListItems.Count<IListItemDefinition>() - 1;
      this.Definition = definition;
      this.Refresh(definition);
      this.UpdateMessagesList(true);
    }

    [HandleMessage("forms:editcommand", true)]
    protected void EditCommand(ClientPipelineArgs args)
    {
      Assert.IsNotNull((object) args, "Parameter args is null");
      if (this.Controls.Count <= 0)
        return;
      if (args.IsPostBack)
      {
        this.TrackingXml = UrlHandle.Get(new UrlString(args.Parameters["url"]))["tracking"];
        if (this.SelectedIndex < 0 || !args.HasResult)
          return;
        ListDefinition definition = this.Definition;
        definition.Groups.First<IGroupDefinition>().GetListItem(this.SelectedIndex).Parameters = args.Result == "-" ? string.Empty : ParametersUtil.Expand(args.Result);
        this.Refresh(definition);
      }
      else
      {
        if (this.SelectedIndex < 0 || this.Definition.Groups.First<IGroupDefinition>().ListItems.Count<IListItemDefinition>() <= this.SelectedIndex)
          return;
        IListItemDefinition listItem = this.Definition.Groups.First<IGroupDefinition>().GetListItem(this.SelectedIndex);
        ActionItem actionItem = new ActionItem(this.CurrentDatabase.GetItem(listItem.ItemID));
        if (!string.IsNullOrEmpty(actionItem.Editor))
        {
          string name = ((object) ID.NewID).ToString();
          HttpContext.Current.Session.Add(name, (object) listItem.Parameters);
          UrlString url = actionItem.Editor.Contains("~/xaml/") ? new UrlString(actionItem.Editor) : new UrlString(UIUtil.GetUri(actionItem.Editor));
          UrlHandle urlHandle = new UrlHandle();
          urlHandle["tracking"] = this.TrackingXml;
          urlHandle.Add(url);
          urlHandle["actiondefinition"] = ((object) this.Definition).ToString();
          args.Parameters["handle"] = urlHandle.Handle;
          url.Append("params", name);
          args.Parameters.Add("params", name);
          url.Append("id", this.CurrentID);
          url.Append("actionid", ((object) actionItem.ID).ToString());
          url.Append("uniqid", ((object) actionItem.ID).ToString());
          url.Append("db", this.CurrentDatabase.Name);
          url.Append("la", this.CurrentLanguage.Name);
          args.Parameters["url"] = ((object) url).ToString();
          string queryString = actionItem.QueryString;
          ModalDialog.Show(url, queryString);
          args.WaitForPostBack();
        }
        else
          SheerResponse.Alert(DependenciesManager.ResourceManager.Localize("ACTION_HAS_NO_EDITOR"), Array.Empty<string>());
      }
    }

    [HandleMessage("forms:removecommand")]
    protected void RemoveCommand(Message message)
    {
      if (this.Controls.Count <= 0)
        return;
      ListDefinition definition = this.Definition;
      if (!definition.Groups.Any<IGroupDefinition>())
        return;
      IGroupDefinition groupDefinition = definition.Groups.First<IGroupDefinition>();
      if (groupDefinition == null)
        return;
      int selectedIndex = this.SelectedIndex;
      if (selectedIndex < 0)
        return;
      groupDefinition.RemoveListItem(selectedIndex);
      this.SelectedIndex = 0;
      ((Sitecore.Web.UI.HtmlControls.Control) this.AddedCommands).Controls.RemoveAt(selectedIndex);
      if (((Sitecore.Web.UI.HtmlControls.Control) this.AddedCommands).Controls.Count > 0 && ((WebControl) ((Sitecore.Web.UI.HtmlControls.Control) this.AddedCommands).Controls[this.SelectedIndex].Controls[0]).Attributes["class"].Contains(" scwfmHidden"))
      {
        int num = 0;
        foreach (Sitecore.Web.UI.HtmlControls.Control control in ((Sitecore.Web.UI.HtmlControls.Control) this.AddedCommands).Controls)
        {
          if (!((WebControl) control.Controls[0]).Attributes["class"].Contains(" scwfmHidden"))
          {
            this.SelectedIndex = num;
            break;
          }
          ++num;
        }
      }
      this.Definition = definition;
      this.Refresh(definition);
      this.UpdateMessagesList(true);
    }

    [HandleMessage("actions:sortdown")]
    protected void SortDown(Message message)
    {
      Assert.IsNotNull((object) message, "Parameter message is null");
      if (this.SelectedIndex < 0)
        return;
      ListDefinition definition = this.Definition;
      if (!definition.Groups.Any<IGroupDefinition>())
        return;
      IGroupDefinition groupDefinition = definition.Groups.FirstOrDefault<IGroupDefinition>();
      if (groupDefinition == null)
        return;
      int selectedIndex = this.SelectedIndex;
      if (selectedIndex < 0)
        return;
      do
      {
        IListItemDefinition listItem = groupDefinition.GetListItem(selectedIndex);
        groupDefinition.RemoveListItem(selectedIndex);
        ++selectedIndex;
        ++this.SelectedIndex;
        groupDefinition.InsertListItem(selectedIndex, listItem);
      }
      while (((WebControl) ((Sitecore.Web.UI.HtmlControls.Control) this.AddedCommands).Controls[selectedIndex].Controls[0]).Attributes["class"].Contains(" scwfmHidden") && groupDefinition.ListItems.Count<IListItemDefinition>() > selectedIndex);
      this.Definition = definition;
      this.Refresh(definition);
      this.UpdateMessagesList(true);
    }

    [HandleMessage("actions:sortup")]
    protected void SortUp(Message message)
    {
      Assert.IsNotNull((object) message, "Parameter message is null");
      if (this.SelectedIndex < 0)
        return;
      ListDefinition definition = this.Definition;
      if (!definition.Groups.Any<IGroupDefinition>())
        return;
      IGroupDefinition groupDefinition = definition.Groups.First<IGroupDefinition>();
      if (groupDefinition == null)
        return;
      int selectedIndex = this.SelectedIndex;
      if (selectedIndex < 0)
        return;
      do
      {
        IListItemDefinition listItem = groupDefinition.GetListItem(selectedIndex);
        groupDefinition.RemoveListItem(selectedIndex);
        --selectedIndex;
        --this.SelectedIndex;
        groupDefinition.InsertListItem(selectedIndex, listItem);
      }
      while (selectedIndex > 0 && ((WebControl) ((Sitecore.Web.UI.HtmlControls.Control) this.AddedCommands).Controls[selectedIndex].Controls[0]).Attributes["class"].Contains(" scwfmHidden"));
      this.Definition = definition;
      this.Refresh(definition);
      this.UpdateMessagesList(true);
    }

    public Database CurrentDatabase => this.database ?? (this.database = Factory.GetDatabase(Sitecore.Web.WebUtil.GetQueryString("db")));

    public ListDefinition ServerDefinition => HttpContext.Current.Session[Sitecore.Web.WebUtil.GetQueryString("definition")] as ListDefinition;

    public ArrayList Controls
    {
      get => Context.ClientPage.ServerProperties[nameof (Controls)] as ArrayList;
      set => Context.ClientPage.ServerProperties[nameof (Controls)] = (object) value;
    }

    public int SelectedIndex
    {
      get => MainUtil.GetInt(Context.ClientPage.ServerProperties[nameof (SelectedIndex)], 0);
      set => Context.ClientPage.ServerProperties[nameof (SelectedIndex)] = (object) value;
    }

    protected string Structure
    {
      get => (string) Context.ClientPage.ServerProperties["structure"];
      set => Context.ClientPage.ServerProperties.Add("structure", (object) value);
    }

    public Tracking Tracking => this.tracking ?? (this.tracking = new Tracking(this.TrackingXml, StaticSettings.ContextDatabase));

    public string TrackingXml
    {
      get => (string) Context.ClientPage.ServerProperties[nameof (TrackingXml)];
      set => Context.ClientPage.ServerProperties.Add(nameof (TrackingXml), (object) value);
    }

    public ListDefinition Definition
    {
      get => Context.ClientPage.ServerProperties[nameof (Definition)] as ListDefinition;
      set => Context.ClientPage.ServerProperties.Add(nameof (Definition), (object) value);
    }

    public string CurrentID => Sitecore.Web.WebUtil.GetQueryString("id");

    public FormItem FormItem => this.formItem ?? (this.formItem = (FormItem) this.CurrentDatabase.GetItem(this.CurrentID, this.CurrentLanguage));

    public Language CurrentLanguage => Language.Parse(Sitecore.Web.WebUtil.GetQueryString("la"));

    public string Root => Sitecore.Web.WebUtil.GetQueryString("root");

    public string SystemRoot => Sitecore.Web.WebUtil.GetQueryString("system");
  }
}
