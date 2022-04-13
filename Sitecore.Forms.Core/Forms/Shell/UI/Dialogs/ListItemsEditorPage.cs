// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Shell.UI.Dialogs.ListItemsEditorPage
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using ComponentArt.Web.UI;
using Sitecore.Configuration;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Form.Core.Configuration;
using Sitecore.Form.Core.Utility;
using Sitecore.Forms.Core.Data;
using Sitecore.Globalization;
using Sitecore.Resources;
using Sitecore.Shell.Applications.Dialogs.ItemLister;
using Sitecore.Web;
using Sitecore.Web.UI;
using Sitecore.Web.UI.HtmlControls;
using Sitecore.Web.UI.Sheer;
using Sitecore.Web.UI.WebControls;
using Sitecore.Web.UI.XamlSharp.Xaml;
using Sitecore.WFFM.Abstractions.Dependencies;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Sitecore.Forms.Shell.UI.Dialogs
{
  public class ListItemsEditorPage : SmartDialogPage
  {
    private static readonly string gridCallBackScript = "ListItems.callback('0&queriessessionkey={0}&querytype={1}&{2}={3}&{4}={5}&{6}={7}&{8}={9}&{10}={11}')";
    protected GridPanel ManualSettingsGrid;
    protected GridPanel FromRootSettingsGrid;
    protected GridPanel XPathQuerySettingsGrid;
    protected GridPanel SitecoreQuerySettingsGrid;
    protected GridPanel FastQuerySettingsGrid;
    protected GridPanel CustomSettingsGrid;
    protected DropDownList SetItemsMode;
    protected Border SettingsHolder;
    protected Border FieldsNavigatorHolder;
    protected Border PreviewHolder;
    protected Border ManualSettings;
    protected ContextMenu FieldsContextMenu;
    protected Edit FromRootEdit;
    protected Edit XPathQueryEdit;
    protected Edit SitecoreQueryEdit;
    protected Edit FastQueryEdit;
    protected HtmlInputHidden ItemValueField;
    protected HtmlInputHidden ItemTextField;
    protected HtmlInputHidden QueryKeyHolder;
    protected HtmlInputHidden EmptyValueListItemHolder;
    protected HtmlInputHidden EmptyTextListItemHolder;
    protected HtmlInputHidden ShowOnlyValue;
    protected Border ManualSettingsHolder;
    protected Grid ListItems;
    protected Border ListItemsHolder;
    protected Sitecore.Web.UI.HtmlControls.Literal ValueFieldLiteral;
    protected Sitecore.Web.UI.HtmlControls.Literal TextFieldLiteral;
    protected Sitecore.Web.UI.HtmlControls.Literal SetItemsByLiteral;
    protected Sitecore.Web.UI.HtmlControls.Literal ValueLiteral;
    protected Sitecore.Web.UI.HtmlControls.Literal TextCaptionLiteral;
    protected GridPanel TextCaptionGrid;
    protected Sitecore.Web.UI.HtmlControls.Literal SelectRootItemLiteral;
    protected Sitecore.Web.UI.HtmlControls.Literal BrowseRootLiteral;
    protected Sitecore.Web.UI.HtmlControls.Literal XPathQueryLiteral;
    protected Sitecore.Web.UI.HtmlControls.Literal PreviewXpathLiteral;
    protected Sitecore.Web.UI.HtmlControls.Literal SitecoreQueryLiteral;
    protected Sitecore.Web.UI.HtmlControls.Literal PreviewSitecoreQueryLiteral;
    protected Sitecore.Web.UI.HtmlControls.Literal FastQueryLiteral;
    protected Sitecore.Web.UI.HtmlControls.Literal PreviewFastQueryLiteral;
    protected Sitecore.Web.UI.HtmlControls.Literal GridValueLiteral;
    protected Sitecore.Web.UI.HtmlControls.Literal GridTextLiteral;
    protected Sitecore.Web.UI.HtmlControls.Literal TextLeftBlank;
    protected Sitecore.Web.UI.HtmlControls.Literal ValueFieldCaptionLiteral;
    protected Sitecore.Web.UI.HtmlControls.Literal TextFieldCaptionLiteral;
    protected Sitecore.Web.UI.HtmlControls.Literal TextLanguageMarker;
    protected HtmlAnchor EnterDifferentTextLink;
    protected HtmlAnchor UseOnlyValueTextLink;
    protected HtmlAnchor EnterDifferentTextLinkForGrid;
    protected HtmlAnchor UseOnlyValueTextLinkForGrid;
    protected GridPanel QueryGridColumnHolder;
    protected Border QueryGridColumnValueHolder;
    protected Border QueryGridColumnTextHolder;
    protected ThemedImage LockValueImage;
    protected ThemedImage UnLockValueImage;
    protected HtmlAnchor ValueLockLink;
    protected Sitecore.Web.UI.HtmlControls.Literal DoNotUseDifferentTextManual;
    protected Sitecore.Web.UI.HtmlControls.Literal DoNotUseDifferentTextGrid;
    protected Sitecore.Web.UI.HtmlControls.Literal EnterDifferentTextToDisplayManual;
    protected Sitecore.Web.UI.HtmlControls.Literal EnterDifferentTextToDisplayGrid;
    private GridPanel[] grids;
    private List<QuerySettings> queries;

    protected override void OnInit(EventArgs e)
    {
      this.ItemValueField.Value = "__Item Name";
      this.ItemTextField.Value = "__Item Name";
      this.FillSetItemsMode();
    }

    protected override void OnLoad(EventArgs e)
    {
      base.OnLoad(e);
      this.RestoreValus();
      this.grids = new GridPanel[6]
      {
        this.ManualSettingsGrid,
        this.FromRootSettingsGrid,
        this.XPathQuerySettingsGrid,
        this.SitecoreQuerySettingsGrid,
        this.FastQuerySettingsGrid,
        this.CustomSettingsGrid
      };
      this.UpdateActiveMode(false);
      if (!(this).Page.IsPostBack && !XamlControl.AjaxScriptManager.IsEvent)
      {
        if (!(this.ListItems).IsCallback)
        {
          this.EmptyTextListItemHolder.Value = DependenciesManager.ResourceManager.Localize("TYPE_TEXT_OF_LIST_ITEM");
          this.EmptyValueListItemHolder.Value = DependenciesManager.ResourceManager.Localize("TYPE_VALUE_OF_LIST_ITEM");
          this.HandleTextColumnMode();
          QuerySettings query = this.Queries.FirstOrDefault<QuerySettings>();
          if (query != null)
          {
            this.SetEditValues(query);
            this.SetItemsMode.SelectedValue = query.QueryType;
            this.ItemValueField.Value = query.ValueFieldName;
            this.ItemTextField.Value = query.TextFieldName;
            this.UpdateGridHeader();
          }
          this.TextLanguageMarker.Text = this.CurrentLanguage.CultureInfo.DisplayName;
          this.UpdateActiveMode(true);
          this.LoadManualItems();
          this.OnFieldsSetChanged();
        }
        this.LoadDataToPreviewGrid();
        (this.LockValueImage).Src = Images.GetThemedImageSource("Network/16x16/lock.png", (ImageDimension) 1);
        (this.UnLockValueImage).Src = Images.GetThemedImageSource("Network/16x16/lock_open.png", (ImageDimension) 1);
        this.BuildUpClientDictionary();
      }
      this.ListItems.SearchText = DependenciesManager.ResourceManager.Localize("SEARCH");
      this.ListItems.GroupingNotificationText = DependenciesManager.ResourceManager.Localize("DRAG_COLUMN_TO_AREA_TO_GROUP_BY_IT");
    }

    protected virtual void BuildUpClientDictionary() => (this).Page.ClientScript.RegisterClientScriptBlock(((object) this).GetType(), "scWfmListItemDictionary", Sitecore.StringExtensions.StringExtensions.FormatWith("Sitecore.Wfm.ListEditor.dictionary['unlockValues'] = \"{0}\";", new object[1]
    {
      (object) DependenciesManager.ResourceManager.Localize("DO_YOU_REALLY_WANT_TO_CHANGE_THE_VALUE")
    }), true);

    private void HandleTextColumnMode()
    {
      QuerySettings querySettings = this.Queries.FirstOrDefault<QuerySettings>();
      if (querySettings == null || querySettings.QueryType != "default" || querySettings.ShowOnlyValue)
      {
        this.ShowOnlyValue.Value = "1";
        (this.ManualSettingsGrid).SetExtensibleProperty(this.TextCaptionGrid, "style", "display:none");
        (this.ManualSettingsGrid).SetExtensibleProperty(this.ValueLockLink, "width", "100%");
        this.EnterDifferentTextLink.Style["display"] = string.Empty;
        this.UseOnlyValueTextLink.Style["display"] = "none";
      }
      else
      {
        this.EnterDifferentTextLink.Style["display"] = "none";
        this.UseOnlyValueTextLink.Style["display"] = string.Empty;
      }
      this.EnterDifferentTextLink.Attributes["href"] = "#";
      this.UseOnlyValueTextLink.Attributes["href"] = "#";
      this.ValueLockLink.Attributes["href"] = "#";
      if (querySettings == null || querySettings.QueryType == "default")
        (this.ManualSettings).Style["display"] = string.Empty;
      else
        (this.ManualSettings).Style["display"] = "none";
    }

    protected override void Localize()
    {
      base.Localize();
      this.Header = DependenciesManager.ResourceManager.Localize("LIST_ITEMS");
      this.Text = DependenciesManager.ResourceManager.Localize("SELECT_VALUES_AND_TEXT_TO_DISPLAY");
      this.SetItemsByLiteral.Text = DependenciesManager.ResourceManager.Localize("SET_ITEMS_BY");
      this.ValueLiteral.Text = DependenciesManager.ResourceManager.Localize("VALUE");
      this.TextCaptionLiteral.Text = DependenciesManager.ResourceManager.Localize("TEXT");
      this.SelectRootItemLiteral.Text = DependenciesManager.ResourceManager.Localize("SELECTED_ROOT_ITEM");
      this.BrowseRootLiteral.Text = DependenciesManager.ResourceManager.Localize("BROWSE");
      this.XPathQueryLiteral.Text = DependenciesManager.ResourceManager.Localize("XPATH_QUERY");
      this.PreviewXpathLiteral.Text = DependenciesManager.ResourceManager.Localize("PREVIEW");
      this.SitecoreQueryLiteral.Text = DependenciesManager.ResourceManager.Localize("SITECORE_QUERY");
      this.PreviewSitecoreQueryLiteral.Text = DependenciesManager.ResourceManager.Localize("PREVIEW");
      this.FastQueryLiteral.Text = DependenciesManager.ResourceManager.Localize("FAST_QUERY");
      this.PreviewFastQueryLiteral.Text = DependenciesManager.ResourceManager.Localize("PREVIEW");
      this.GridValueLiteral.Text = DependenciesManager.ResourceManager.Localize("VALUE_CAPTION");
      this.ValueFieldCaptionLiteral.Text = DependenciesManager.ResourceManager.Localize("FIELD") + " ";
      this.ValueFieldLiteral.Text = "__Item Name";
      this.GridTextLiteral.Text = DependenciesManager.ResourceManager.Localize("TEXT");
      this.TextFieldCaptionLiteral.Text = DependenciesManager.ResourceManager.Localize("FIELD") + " ";
      this.TextFieldLiteral.Text = "__Item Name";
      this.DoNotUseDifferentTextManual.Text = DependenciesManager.ResourceManager.Localize("DO_NOT_USE_DIFFERENT_TEXT");
      this.EnterDifferentTextToDisplayManual.Text = DependenciesManager.ResourceManager.Localize("ENTER_DIFFERENT_TEXT_TO_DISPLAY");
    }

    private void SetEditValues(QuerySettings query)
    {
      string queryType = query.QueryType;
      if (!(queryType == "root"))
      {
        if (!(queryType == "xpath"))
        {
          if (!(queryType == "sitecore"))
          {
            if (!(queryType == "fast"))
              return;
            (this.FastQueryEdit).Value = query.QueryText;
          }
          else
            (this.SitecoreQueryEdit).Value = query.QueryText;
        }
        else
          (this.XPathQueryEdit).Value = query.QueryText;
      }
      else
      {
        string str = query.QueryText;
        if (!string.IsNullOrEmpty(str))
        {
          Item obj = StaticSettings.ContextDatabase.GetItem(str);
          if (obj != null)
            str = obj.Paths.FullPath;
        }
        (this.FromRootEdit).Value = str;
      }
    }

    private void FillSetItemsMode()
    {
            System.Web.UI.WebControls.ListItem[] listItemArray = new System.Web.UI.WebControls.ListItem[this.SetItemsMode.Items.Count];
      this.SetItemsMode.Items.CopyTo((Array) listItemArray, 0);
      this.SetItemsMode.Items.Clear();
      this.SetItemsMode.Items.AddRange(new System.Web.UI.WebControls.ListItem[5]
      {
        new System.Web.UI.WebControls.ListItem(DependenciesManager.ResourceManager.Localize("MANUALLY_ENTERING_NAMES"), "default"),
        new System.Web.UI.WebControls.ListItem(DependenciesManager.ResourceManager.Localize("SELECTING_SITECORE_ITEMS"), "root"),
        new System.Web.UI.WebControls.ListItem(DependenciesManager.ResourceManager.Localize("USING_XPATH_QUERY"), "xpath"),
        new System.Web.UI.WebControls.ListItem(DependenciesManager.ResourceManager.Localize("USING_SITECORE_QUERY"), "sitecore"),
        new System.Web.UI.WebControls.ListItem(DependenciesManager.ResourceManager.Localize("USING_FAST_QUERY"), "fast")
      });
      while (this.SetItemsMode.Items.Count < listItemArray.Length)
        this.SetItemsMode.Items.Add(listItemArray[this.SetItemsMode.Items.Count]);
    }

    private void LoadManualItems()
    {
      IEnumerable<QuerySettings> querySettingses = this.Queries.Where<QuerySettings>((Func<QuerySettings, bool>) (q => q.QueryType == "default"));
      NameValueCollection nameValueCollection;
      using (new LanguageSwitcher(this.CurrentLanguage))
        nameValueCollection = QueryManager.Select(querySettingses);
      while ((this.ManualSettingsGrid).Controls.Count > 4)
        (this.ManualSettingsGrid).Controls.RemoveAt(4);
      if (nameValueCollection.Keys.Count > 0)
      {
        for (int index = 0; index < nameValueCollection.Keys.Count; ++index)
        {
          string key = nameValueCollection.GetKey(index);
          if (!string.IsNullOrEmpty(key))
          {
            string text = nameValueCollection.Get(index);
            QuerySettings querySettings = querySettingses.FirstOrDefault<QuerySettings>((Func<QuerySettings, bool>) (q => q.QueryText == key));
            if (querySettings != null && querySettings.LocalizedTexts.ContainsKey(this.LanguageName))
              querySettings.LocalizedTexts.TryGetValue(this.LanguageName, out text);
            if (querySettings == null)
              text = (string) null;
            this.AddListItem(index, text, key);
          }
        }
      }
      else
      {
        this.ValueLockLink.Attributes.Remove("href");
        this.ValueLockLink.Attributes.Remove("onclick");
        this.ValueLockLink.Attributes["class"] = "disabled-link";
        (this.LockValueImage).Style["display"] = "none";
        (this.UnLockValueImage).Style["display"] = "none";
      }
      if ((this.ManualSettingsGrid).Controls.Count > 4)
        return;
      this.AddListItem(1, (string) null, (string) null);
    }

    private void AddListItem(int i, string text, string value)
    {
      string str1 = text;
      if (str1 == null)
        str1 = DependenciesManager.ResourceManager.Localize("TYPE_TEXT_OF_LIST_ITEM", value != null ? "\"" + value + "\"" : string.Empty);
      string str2 = str1;
      string str3 = value ?? DependenciesManager.ResourceManager.Localize("TYPE_VALUE_OF_LIST_ITEM");
      Edit edit1 = new Edit();
      (edit1).ID = string.Join(string.Empty, new string[2]
      {
        "wfmListItem_v",
        i.ToString()
      });
      (edit1).Width = Unit.Percentage(100.0);
      (edit1).Value = str3;
      (edit1).Class = value == null ? "scWfmListItemEdit scWfmEmpty" : "scWfmListItemEdit";
      Edit edit2 = edit1;
      (edit2).Attributes["onfocus"] = "javascript:return Sitecore.Wfm.ListEditor.onListItemFocus(this, event)";
      (edit2).Attributes["onblur"] = "javascript:return Sitecore.Wfm.ListEditor.onListItemBlur(this, event)";
      (edit2).Attributes["itemtype"] = nameof (value);
      if (value != null)
      {
        (edit2).Class = "disabled";
        (edit2).Disabled = true;
      }
      (this.ManualSettingsGrid).Controls.Add(edit2);
      Edit edit3 = new Edit();
      (edit3).ID = string.Join(string.Empty, new string[2]
      {
        "wfmListItem_t",
        i.ToString()
      });
      (edit3).Width = Unit.Percentage(100.0);
      (edit3).Value = str2;
      (edit3).Margin = "0 0 0 5";
      (edit3).Class = text == null ? "scWfmListItemEdit scWfmEmpty" : "scWfmListItemEdit";
      Edit edit4 = edit3;
      if (this.ShowOnlyValues)
        (this.ManualSettingsGrid).SetExtensibleProperty(edit4, "style", "display:none");
      (edit4).Attributes["onfocus"] = "javascript:return Sitecore.Wfm.ListEditor.onListItemFocus(this, event)";
      (edit4).Attributes["onblur"] = "javascript:return Sitecore.Wfm.ListEditor.onListItemBlur(this, event)";
      (edit4).Attributes["itemtype"] = nameof (text);
      (this.ManualSettingsGrid).Controls.Add(edit4);
      ControlCollection controls = (this.ManualSettingsGrid).Controls;
      Toolbutton toolbutton1 = new Toolbutton();
      toolbutton1.Icon = "Applications/16x16/add.png";
      (toolbutton1).ToolTip = DependenciesManager.ResourceManager.Localize("ADD");
      (toolbutton1).Margin = "0 0 0 2";
      (toolbutton1).Click = "javascript:Sitecore.Wfm.ListEditor.onAddNewItem(this, evt)";
      controls.Add(toolbutton1);
      Border border = new Border();
      (this.ManualSettingsGrid).Controls.Add(border);
      Toolbutton toolbutton2 = new Toolbutton();
      toolbutton2.Icon = "Core3/16x16/stop.png";
      (toolbutton2).ToolTip = DependenciesManager.ResourceManager.Localize("DELETE");
      (toolbutton2).Click = "javascript:Sitecore.Wfm.ListEditor.onRemoveItem(this, evt)";
      Toolbutton toolbutton3 = toolbutton2;
      (toolbutton3).Style["display"] = value != null ? "none" : string.Empty;
      (border).Controls.Add(toolbutton3);
      ThemedImage themedImage1 = new ThemedImage();
      (themedImage1).Src = Images.GetThemedImageSource("Core3/16x16/stop_d.png", (ImageDimension) 1);
      (themedImage1).ToolTip = DependenciesManager.ResourceManager.Localize("DELETE");
      (themedImage1).Width = (Unit) 16;
      (themedImage1).Height = (Unit) 16;
      (themedImage1).Class = "remove-img";
      ThemedImage themedImage2 = themedImage1;
      (themedImage2).Style["display"] = value != null ? string.Empty : "none";
      (border).Controls.Add(themedImage2);
    }

    private void RestoreValus()
    {
      if ((this).Page.Request.Params[this.ItemValueField.ClientID] != null)
      {
        this.ItemValueField.Value = (this).Page.Request.Params[this.ItemValueField.ClientID];
        this.CurrentQuery.ValueFieldName = this.ItemValueField.Value;
      }
      if ((this).Page.Request.Params[this.ItemTextField.ClientID] != null)
      {
        this.ItemTextField.Value = (this).Page.Request.Params[this.ItemTextField.ClientID];
        this.CurrentQuery.TextFieldName = this.ItemTextField.Value;
      }
      if ((this).Page.Request.Params[(this.XPathQueryEdit).ID] != null)
        (this.XPathQueryEdit).Value = (this).Page.Request.Form[(this.XPathQueryEdit).ID];
      if ((this).Page.Request.Params[(this.SitecoreQueryEdit).ID] != null)
        (this.SitecoreQueryEdit).Value = (this).Page.Request.Params[(this.SitecoreQueryEdit).ID];
      if ((this).Page.Request.Params[(this.FastQueryEdit).ID] == null)
        return;
      (this.FastQueryEdit).Value = (this).Page.Request.Params[(this.FastQueryEdit).ID];
    }

    protected void LoadDataToPreviewGrid()
    {
      this.UpdateQueryText();
      NameValueCollection collection;
      using (new LanguageSwitcher(this.CurrentLanguage))
        collection = QueryManager.Select(this.Queries.Where<QuerySettings>((Func<QuerySettings, bool>) (q => q.QueryType == this.SelectedMode)));
      this.ListItems.DataSource = (object) collection.Where((Func<string, string, bool>) ((k, v) => !string.IsNullOrEmpty(k))).Select((v, t) => new
      {
        Value = v,
        Text = t
      });
      (this.ListItems).DataBind();
    }

    public void OnItemsModeChanged()
    {
      this.UpdateActiveMode(true);
      if (this.CurrentQuery == null)
        this.Queries.Add(new QuerySettings(this.SelectedMode, string.Empty));
      this.ItemValueField.Value = this.CurrentQuery.ValueFieldName;
      this.ItemTextField.Value = this.CurrentQuery.TextFieldName;
      SheerResponse.SetAttribute(this.ItemValueField.ClientID, "value", this.ItemValueField.Value);
      SheerResponse.SetAttribute(this.ItemTextField.ClientID, "value", this.ItemTextField.Value);
      if (this.SelectedMode != "default")
      {
        SheerResponse.Eval("$('ManualSettings').style.display='none'");
        this.OnFieldsSetChanged();
        SheerResponse.SetOuterHtml((this.FieldsContextMenu).ID, this.FieldsContextMenu);
        this.UpdateGridHeader();
        this.RegistreGridCallBack();
      }
      else
        SheerResponse.Eval("$('ManualSettings').style.display='block'");
    }

    protected override void SaveValues()
    {
      List<QuerySettings> querySettingsList = new List<QuerySettings>();
      if (this.SelectedMode == "default")
      {
        string[] array = ((IEnumerable<string>) (this).Page.Request.Form.AllKeys).Where<string>((Func<string, bool>) (k => !string.IsNullOrEmpty(k) && k.StartsWith("wfmListItem_"))).ToArray<string>();
        IEnumerable<QuerySettings> source = this.Queries.Where<QuerySettings>((Func<QuerySettings, bool>) (q => q.QueryType == this.SelectedMode));
        string str1 = DependenciesManager.ResourceManager.Localize("TYPE_VALUE_OF_LIST_ITEM");
        for (int index = 0; index < ((IEnumerable<string>) array).Count<string>(); index += 2)
        {
          string key = (this).Page.Request.Form[array[index]];
          string str2 = (this).Page.Request.Form[array[index + 1]];
          if (!string.IsNullOrEmpty(key) && key != str1)
          {
            QuerySettings querySettings = source.FirstOrDefault<QuerySettings>((Func<QuerySettings, bool>) (q =>
            {
              if (q.QueryText == key)
                return true;
              return q.QueryText.StartsWith(string.Join(string.Empty, new string[2]
              {
                key,
                "|"
              }));
            })) ?? new QuerySettings(this.SelectedMode, key);
            querySettings.ShowOnlyValue = this.ShowOnlyValues;
            if (str2 != DependenciesManager.ResourceManager.Localize("TYPE_TEXT_OF_LIST_ITEM", "\"" + key + "\""))
            {
              if (querySettings.LocalizedTexts.ContainsKey(this.LanguageName))
                querySettings.LocalizedTexts[this.LanguageName] = str2;
              else
                querySettings.LocalizedTexts.Add(this.LanguageName, str2);
            }
            else
              querySettings.LocalizedTexts.Remove(this.LanguageName);
            querySettingsList.Add(querySettings);
          }
        }
      }
      else
      {
        this.UpdateQueryText();
        querySettingsList.Add(this.CurrentQuery);
      }
      this.SetLongValue("queries", QuerySettings.ToString((IEnumerable<QuerySettings>) querySettingsList));
    }

    public void OnBrowseRoot() => this.BrowseRoot();

    public void OnPreviewClick()
    {
      this.UpdateQueryText();
      this.OnFieldsSetChanged();
      SheerResponse.SetOuterHtml((this.FieldsContextMenu).ID, this.FieldsContextMenu);
      this.RegistreGridCallBack();
    }

    public void BrowseRoot()
    {
      ClientPipelineArgs currentArgs = ContinuationManager.Current.CurrentArgs as ClientPipelineArgs;
      Assert.ArgumentNotNull((object) currentArgs, "args");
      if (!currentArgs.IsPostBack)
      {
        SelectItemOptions selectItemOptions = new SelectItemOptions();
        string str = string.IsNullOrEmpty(this.CurrentQuery.QueryText) ? this.CurrentQuery.ContextRoot : this.CurrentQuery.QueryText;
        selectItemOptions.Root = StaticSettings.ContextDatabase.GetItem(((object) ItemIDs.RootID).ToString());
        selectItemOptions.SelectedItem = StaticSettings.ContextDatabase.SelectSingleItem(str);
        selectItemOptions.Icon = "Imaging/16x16/layer_blend.png";
        selectItemOptions.Title = DependenciesManager.ResourceManager.Localize("SOURCE_TITLE");
        selectItemOptions.Text = DependenciesManager.ResourceManager.Localize("SOURCE_DESC");
        selectItemOptions.ButtonText = DependenciesManager.ResourceManager.Localize("SELECT");
        SheerResponse.ShowModalDialog(((object) selectItemOptions.ToUrlString()).ToString(), true);
        currentArgs.WaitForPostBack();
      }
      else
      {
        if (!currentArgs.HasResult)
          return;
        Item obj = StaticSettings.ContextDatabase.GetItem(currentArgs.Result);
        if (obj == null || !(this.CurrentQuery.QueryText != ((object) obj.ID).ToString()))
          return;
        (this.FromRootEdit).Value = obj.Paths.FullPath;
        XamlControl.AjaxScriptManager.SetOuterHtml((this.FromRootEdit).ID, this.FromRootEdit);
        this.CurrentQuery.QueryText = ((object) obj.ID).ToString();
        this.OnFieldsSetChanged();
        SheerResponse.SetOuterHtml((this.FieldsContextMenu).ID, this.FieldsContextMenu);
        this.RegistreGridCallBack();
      }
    }

    public void OnFieldsSetChanged()
    {
      Item obj1 = Factory.GetDatabase("master").GetItem(((object) ItemIDs.RootID).ToString());
      if (this.CurrentQuery == null)
        this.Queries.Add(new QuerySettings(this.SelectedMode, string.Empty));
      Item obj2 = (Item) null;
      if (!string.IsNullOrEmpty(this.CurrentQuery.QueryText))
        obj2 = QueryManager.SelectItems(this.CurrentQuery).FirstOrDefault<Item>();
      this.RenderFieldsPopup(Sitecore.Form.Core.Utility.ItemUtil.GetTemplateFields(obj2 ?? obj1));
    }

    public void RenderFieldsPopup(IEnumerable<TemplateFieldItem> fields)
    {
      if (fields.Count<TemplateFieldItem>() <= 0)
        return;
      HtmlTextWriter htmlTextWriter = new HtmlTextWriter((TextWriter) new StringWriter());
      string str1 = (string) null;
      foreach (TemplateFieldItem field in fields)
      {
        if (((CustomItemBase) field.Section).Name != str1)
        {
          str1 = ((CustomItemBase) field.Section).Name;
          htmlTextWriter.Write("<tr><td><a id=\"Nav_" + (object) ((CustomItemBase) field.Section).ID.ToShortID() + "\" class=\"scEditorHeaderNavigatorSection\">" + str1 + "</a></td></tr>");
          if (str1 == "Appearance")
          {
            htmlTextWriter.Write("<tr><td><a id=\"Nav___ItemName\" href=\"#\" class=\"scEditorHeaderNavigatorField\" onclick=\"javascript:return Sitecore.Wfm.ListEditor.onChangeField(this, event, '__Item Name')\">__Item Name</a></td></tr>");
            htmlTextWriter.Write("<tr><td><a id=\"Nav___ItemName\" href=\"#\" class=\"scEditorHeaderNavigatorField\" onclick=\"javascript:return Sitecore.Wfm.ListEditor.onChangeField(this, event, '__ID')\">__ID</a></td></tr>");
          }
        }
        string str2 = ((object) ((CustomItemBase) field).ID.ToShortID()).ToString();
        string str3 = Sitecore.Web.WebUtil.SafeEncode(StringUtil.GetString(new string[2]
        {
          field.Title,
          ((CustomItemBase) field).DisplayName
        }));
        htmlTextWriter.Write("<tr><td><a id=\"Nav_" + str2 + "\" href=\"#\" class=\"scEditorHeaderNavigatorField\" onclick=\"javascript:return Sitecore.Wfm.ListEditor.onChangeField(this, event, '" + ((CustomItemBase) field).Name + "')\">" + str3 + "</a></td></tr>");
      }
      (this.FieldsContextMenu).Controls.Clear();
      (this.FieldsContextMenu).Controls.Add(new LiteralControl(htmlTextWriter.InnerWriter.ToString()));
    }

    private void UpdateGridHeader()
    {
      this.ValueFieldLiteral.Text = this.ItemValueField.Value;
      this.TextFieldLiteral.Text = this.ItemTextField.Value;
      AjaxScriptManager.Current.SetOuterHtml((this.ValueFieldLiteral).ID, this.ValueFieldLiteral);
      AjaxScriptManager.Current.SetOuterHtml((this.TextFieldLiteral).ID, this.TextFieldLiteral);
    }

    private void UpdateQueryText()
    {
      string selectedMode = this.SelectedMode;
      if (!(selectedMode == "xpath"))
      {
        if (!(selectedMode == "sitecore"))
        {
          if (!(selectedMode == "fast"))
            return;
          this.CurrentQuery.QueryText = (this.FastQueryEdit).Value;
        }
        else
          this.CurrentQuery.QueryText = (this.SitecoreQueryEdit).Value;
      }
      else
        this.CurrentQuery.QueryText = (this.XPathQueryEdit).Value;
    }

    private void UpdateActiveMode(bool updateClient)
    {
      Array.ForEach<GridPanel>(this.grids, (System.Action<GridPanel>) (g => (g).Style.Add("display", "none")));
      if (this.grids.Length > this.SetItemsMode.SelectedIndex)
        (this.grids[this.SetItemsMode.SelectedIndex > -1 ? this.SetItemsMode.SelectedIndex : 0]).Style.Add("display", "block");
      else
        (((IEnumerable<GridPanel>) this.grids).Last<GridPanel>()).Style.Add("display", "block");
      if (!updateClient)
        return;
      foreach (GridPanel grid in this.grids)
        AjaxScriptManager.Current.SetStyle((grid).ID, "display", (grid).Style["display"]);
      (this.PreviewHolder).Style.Add("display", (this.ManualSettingsGrid).Style["display"] == "block" ? "none" : "block");
      AjaxScriptManager.Current.SetStyle((this.PreviewHolder).ID, "display", (this.PreviewHolder).Style["display"]);
    }

    private string GetGridCallBack() => Sitecore.StringExtensions.StringExtensions.FormatWith(ListItemsEditorPage.gridCallBackScript, new object[12]
    {
      (object) this.QueriesKey,
      (object) this.SelectedMode,
      (object) this.ItemValueField.ClientID,
      (object) this.ItemValueField.Value,
      (object) this.ItemTextField.ClientID,
      (object) this.ItemTextField.Value,
      (object) (this.XPathQueryEdit).ID,
      (object) (this.XPathQueryEdit).Value.Replace("'", "\\'"),
      (object) (this.SitecoreQueryEdit).ID,
      (object) (this.SitecoreQueryEdit).Value.Replace("'", "\\'"),
      (object) (this.FastQueryEdit).ID,
      (object) (this.FastQueryEdit).Value.Replace("'", "\\'")
    });

    private void RegistreGridCallBack() => SheerResponse.Eval(this.GetGridCallBack());

    public QuerySettings CurrentQuery => this.Queries.FirstOrDefault<QuerySettings>((Func<QuerySettings, bool>) (q => q.QueryType == this.SelectedMode));

    public string LanguageName => this.CurrentLanguage.GetLowerCaseName();

    public List<QuerySettings> Queries
    {
      get
      {
        if (this.queries == null)
        {
          this.queries = (this).Page.Session[this.QueriesKey] as List<QuerySettings>;
          if (this.queries == null)
          {
            this.queries = QuerySettings.ParseRange(this.GetValueByKey("queries")).ToList<QuerySettings>();
            (this).Page.Session[this.QueriesKey] = (object) this.queries;
          }
        }
        return this.queries;
      }
    }

    protected string QueriesKey
    {
      get
      {
        if (!string.IsNullOrEmpty((string) (this).ViewState["queriesKey"]))
          return (string) (this).ViewState["queriesKey"];
        string str = (this).Page.Request.Params["queriessessionkey"];
        if (string.IsNullOrEmpty(str))
        {
          str = ((object) Sitecore.Data.ID.NewID.ToShortID()).ToString();
          this.QueryKeyHolder.Value = str;
          XamlControl.AjaxScriptManager.SetAttribute(this.QueryKeyHolder.ID, "value", str);
        }
        (this).ViewState["queriesKey"] = (object) str;
        return str;
      }
    }

    protected string SelectedMode
    {
      get
      {
        string selectedValue = (this).Page.Request.Params["querytype"];
        if (string.IsNullOrEmpty(selectedValue))
          selectedValue = this.SetItemsMode.SelectedValue;
        return selectedValue;
      }
    }

    public override bool UseUrlCoding => true;

    public bool ShowOnlyValues => this.ShowOnlyValue.Value == "1";
  }
}
