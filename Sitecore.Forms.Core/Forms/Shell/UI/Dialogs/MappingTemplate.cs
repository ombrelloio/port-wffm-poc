// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Shell.UI.Dialogs.MappingTemplate
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Configuration;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Form.Core.Configuration;
using Sitecore.Form.Core.Utility;
using Sitecore.Forms.Core.Data;
using Sitecore.Forms.Shell.UI.Controls;
using Sitecore.Shell.Applications.Dialogs.ItemLister;
using Sitecore.Text;
using Sitecore.Web;
using Sitecore.Web.UI.HtmlControls;
using Sitecore.Web.UI.Pages;
using Sitecore.Web.UI.Sheer;
using Sitecore.Web.UI.XmlControls;
using Sitecore.WFFM.Abstractions.Data;
using Sitecore.WFFM.Abstractions.Dependencies;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI;

namespace Sitecore.Forms.Shell.UI.Dialogs
{
  public class MappingTemplate : WizardForm
  {
    protected Border Collision;
    protected Border CollisionFields;
    protected Literal DestinationName;
    protected Edit EbDestination;
    protected Edit EbTemplate;
    protected Border LostFields;
    protected Border MappingBorder;
    private NameValueCollection nvParams;
    protected Literal TemplateName;
    protected Border Warning;
    protected WizardDialogBaseXmlControl SelectTemplatePage;
    protected Literal TemplateLiteral;
    protected Literal DestinationLabel;
    protected Button SelectTemplateButton;
    protected Button SelectDestinationButton;
    protected WizardDialogBaseXmlControl MappingFormPage;
    protected Checkbox ShowStandardField;
    protected Groupbox MappingGroupbox;
    protected Groupbox SettingsGroupbox;
    protected WizardDialogBaseXmlControl ConfirmationPage;
    protected Literal TemplateConfirmLiteral;
    protected Literal ItemsWillBeStoredLiteral;
    protected Literal InformationLostLiteral;
    protected Literal ConflictLiteral;

    protected override void OnLoad(EventArgs e)
    {
      base.OnLoad(e);
      if (Context.ClientPage.IsEvent)
        return;
      this.Localize();
      this.LoadMapping();
      if (!string.IsNullOrEmpty(this.Template))
        (this.EbTemplate).Value = this.Template;
      if (!string.IsNullOrEmpty(this.Destination))
        (this.EbDestination).Value = this.Destination;
      ((Input) this.EbTemplate).ReadOnly = true;
      ((Input) this.EbDestination).ReadOnly = true;
      this.ShowStandardField.Checked = this.StandartFields == "1";
    }

    protected virtual void Localize()
    {
      (this.SelectTemplatePage)["Header"] = (object) DependenciesManager.ResourceManager.Localize("SELECT_TEMPLATE_CAPTION");
      (this.SelectTemplatePage)["Text"] = (object) DependenciesManager.ResourceManager.Localize("SELECT_TEMPLATE_AND_DESTINATION_FOR_ITEMS");
      this.TemplateLiteral.Text = DependenciesManager.ResourceManager.Localize("TEMPLATE");
      ((HeaderedItemsControl) this.SelectTemplateButton).Header = DependenciesManager.ResourceManager.Localize("BROWSE");
      ((HeaderedItemsControl) this.SelectDestinationButton).Header = DependenciesManager.ResourceManager.Localize("BROWSE");
      this.DestinationLabel.Text = DependenciesManager.ResourceManager.Localize("DESTINATION");
      (this.MappingFormPage)["Header"] = (object) DependenciesManager.ResourceManager.Localize("MAPPING_FORM_FIELDS");
      (this.MappingFormPage)["Text"] = (object) DependenciesManager.ResourceManager.Localize("MAPPING_FORM_FIELDS_DOT");
      this.ShowStandardField.Header = DependenciesManager.ResourceManager.Localize("SHOW_STANDARD_FIELDS");
      ((HeaderedItemsControl) this.MappingGroupbox).Header = DependenciesManager.ResourceManager.Localize("MAPPING");
      ((HeaderedItemsControl) this.SettingsGroupbox).Header = DependenciesManager.ResourceManager.Localize("SETTINGS");
      (this.ConfirmationPage)["Header"] = (object) DependenciesManager.ResourceManager.Localize("CONFIRMATION");
      (this.ConfirmationPage)["Text"] = (object) DependenciesManager.ResourceManager.Localize("CONFIRM_MAPPING_FORM_TO_ITEM");
      this.TemplateConfirmLiteral.Text = DependenciesManager.ResourceManager.Localize("TEMPLATE_FOR_ITEMS");
      this.ItemsWillBeStoredLiteral.Text = DependenciesManager.ResourceManager.Localize("ITEMS_WILL_BE_STORED");
      this.InformationLostLiteral.Text = DependenciesManager.ResourceManager.Localize("INFORMATION_FROM_FIELDS_WILL_BE_LOST");
      this.ConflictLiteral.Text = DependenciesManager.ResourceManager.Localize("DUE_TO_CONFLICT_DATA_WILL_BE_OVERWRITTEN");
    }

    private void OnShowStandardField() => this.UpdateMapping();

    protected override void ActivePageChanged(string page, string oldPage)
    {
      base.ActivePageChanged(page, oldPage);
      if (!(page == "ConfirmationPage"))
        return;
      this.RenderLostFieldsWarning();
      this.RenderCollisionFieldsWarning();
      this.TemplateName.Text = (this.EbTemplate).Value;
      this.DestinationName.Text = (this.EbDestination).Value;
    }

    protected override void OnCancel(object sender, EventArgs formEventArgs)
    {
      if (this.Active == "ConfirmationPage")
      {
        this.Template = (this.EbTemplate).Value;
        this.Destination = (this.EbDestination).Value;
        this.Mapping = this.GetMappingResult();
        this.StandartFields = this.ShowStandardField.Checked ? "1" : "0";
        string str = ParametersUtil.NameValueCollectionToXml(this.nvParams);
        if (str.Length == 0)
          str = "-";
        SheerResponse.SetDialogValue(str);
      }
      base.OnCancel(sender, formEventArgs);
    }

    protected override bool ActivePageChanging(string page, ref string newpage)
    {
      bool flag = base.ActivePageChanging(page, ref newpage);
      if (page == "SelectTemplatePage" && newpage == "MappingFormPage")
      {
        if ((this.EbTemplate).Value == string.Empty)
        {
          SheerResponse.Alert(DependenciesManager.ResourceManager.Localize("MESSAGE_SELECT_TEMPLATE"), Array.Empty<string>());
          newpage = "SelectTemplatePage";
          return flag;
        }
        if ((this.EbDestination).Value == string.Empty)
        {
          SheerResponse.Alert(DependenciesManager.ResourceManager.Localize("MESSAGE_SELECT_DESTINATION"), Array.Empty<string>());
          newpage = "SelectTemplatePage";
          return flag;
        }
        this.UpdateMapping();
      }
      return flag;
    }

    [HandleMessage("dialog:selectdestination", true)]
    protected void SelectDestination(ClientPipelineArgs args)
    {
      if (args.IsPostBack)
      {
        if (!args.HasResult)
          return;
        (this.EbDestination).Value = StaticSettings.ContextDatabase.GetItem(args.Result).Paths.FullPath;
      }
      else
      {
        SheerResponse.ShowModalDialog(((object) new SelectItemOptions()
        {
          Root = StaticSettings.ContextDatabase.GetItem((ID) ItemIDs.RootID),
          Icon = "Applications/32x32/folder_cubes.png",
          SelectedItem = ((this.EbDestination).Value.Length <= 0 ? (string.IsNullOrEmpty(this.Destination) ? StaticSettings.ContextDatabase.GetItem((ID) ItemIDs.RootID) : StaticSettings.ContextDatabase.SelectSingleItem(this.Destination)) : StaticSettings.ContextDatabase.SelectSingleItem((this.EbDestination).Value)),
          Title = DependenciesManager.ResourceManager.Localize("SELECT_ITEM_TITLE"),
          Text = DependenciesManager.ResourceManager.Localize("SELECT_ITEM"),
          ButtonText = DependenciesManager.ResourceManager.Localize("SELECT")
        }.ToUrlString()).ToString(), true);
        args.WaitForPostBack();
      }
    }

    [HandleMessage("dialog:selecttemplate", true)]
    protected void SelectTemplete(ClientPipelineArgs args)
    {
      if (args.IsPostBack)
      {
        if (!args.HasResult)
          return;
        (this.EbTemplate).Value = StaticSettings.ContextDatabase.GetTemplate(args.Result).FullName;
      }
      else
      {
        UrlString urlString = new UrlString(UIUtil.GetUri("control:Forms.SelectTemplate"));
        urlString.Append("id", (this.EbTemplate).Value.Length > 0 ? (this.EbTemplate).Value : this.Template);
        Context.ClientPage.ClientResponse.ShowModalDialog(((object) urlString).ToString(), true);
        args.WaitForPostBack();
      }
    }

    public string GetValueByKey(string key)
    {
      if (this.nvParams == null)
        this.nvParams = ParametersUtil.XmlToNameValueCollection(this.Params);
      return MainUtil.DecodeName(this.nvParams[key] ?? string.Empty);
    }

    public void SetValue(string key, string value)
    {
      if (this.nvParams == null)
        this.nvParams = ParametersUtil.XmlToNameValueCollection(this.Params);
      this.nvParams[key] = value;
    }

    private string GetMappingResult()
    {
      TemplateItem template = StaticSettings.ContextDatabase.GetTemplate((this.EbTemplate).Value);
      NameValueCollection nameValueCollection = new NameValueCollection();
      foreach (var control in (this.MappingBorder).Controls)
      {
        if (control is TemplateMenu templateMenu1 && !string.IsNullOrEmpty(templateMenu1.TemplateFieldID) && templateMenu1.TemplateFieldID != ((object) ID.Null).ToString() && (this.ShowStandardField.Checked && template.GetField(templateMenu1.TemplateFieldID) != null || !this.ShowStandardField.Checked && this.IsOwnField(template, ID.Parse(templateMenu1.TemplateFieldID))))
          nameValueCollection.Add(templateMenu1.FieldID, templateMenu1.TemplateFieldID);
      }
      return StringUtil.NameValuesToString(nameValueCollection, "|");
    }

    private bool IsOwnField(TemplateItem template, ID templateFieldID)
    {
      foreach (CustomItemBase ownField in template.OwnFields)
      {
        if (ownField.ID == templateFieldID)
          return true;
      }
      return false;
    }

    private Dictionary<string, List<string>> GetCollisionFields()
    {
      TemplateItem template = StaticSettings.ContextDatabase.GetTemplate((this.EbTemplate).Value);
      Dictionary<string, List<string>> dictionary1 = new Dictionary<string, List<string>>();
      foreach (var control in (this.MappingBorder).Controls)
      {
        if (control is TemplateMenu templateMenu1 && !string.IsNullOrEmpty(templateMenu1.TemplateFieldID) && templateMenu1.TemplateFieldID != ((object) ID.Null).ToString() && (this.ShowStandardField.Checked && template.GetField(templateMenu1.TemplateFieldID) != null || !this.ShowStandardField.Checked && this.IsOwnField(template, ID.Parse(templateMenu1.TemplateFieldID))))
        {
          if (dictionary1.ContainsKey(templateMenu1.TemplateFieldID))
            dictionary1[templateMenu1.TemplateFieldID].Add(templateMenu1.FieldName);
          else
            dictionary1.Add(templateMenu1.TemplateFieldID, new List<string>((IEnumerable<string>) new string[1]
            {
              templateMenu1.FieldName
            }));
        }
      }
      Dictionary<string, List<string>> dictionary2 = new Dictionary<string, List<string>>();
      foreach (KeyValuePair<string, List<string>> keyValuePair in dictionary1)
      {
        if (keyValuePair.Value.Count > 1)
          dictionary2.Add(keyValuePair.Key, keyValuePair.Value);
      }
      return dictionary2;
    }

    private List<string> GetLostFields()
    {
      TemplateItem template = StaticSettings.ContextDatabase.GetTemplate((this.EbTemplate).Value);
      List<string> stringList = new List<string>();
      foreach (var control in (this.MappingBorder).Controls)
      {
        if (control is TemplateMenu templateMenu1 && (string.IsNullOrEmpty(templateMenu1.TemplateFieldID) || templateMenu1.TemplateFieldID == ((object) ID.Null).ToString() || this.ShowStandardField.Checked && template.GetField(templateMenu1.TemplateFieldID) == null || !this.ShowStandardField.Checked && !this.IsOwnField(template, ID.Parse(templateMenu1.TemplateFieldID))))
        {
          FieldItem fieldItem = new FieldItem(StaticSettings.ContextDatabase.GetItem(templateMenu1.FieldID));
          stringList.Add(fieldItem.FieldDisplayName);
        }
      }
      return stringList;
    }

    private void RenderCollisionFieldsWarning()
    {
      Dictionary<string, List<string>> collisionFields = this.GetCollisionFields();
      if (collisionFields.Count == 0)
      {
        (this.Collision).Visible = false;
      }
      else
      {
        (this.Collision).Visible = true;
        HtmlTextWriter htmlTextWriter = new HtmlTextWriter((TextWriter) new StringWriter());
        htmlTextWriter.Write("<ul>");
        foreach (KeyValuePair<string, List<string>> keyValuePair in collisionFields)
        {
          StringBuilder stringBuilder = new StringBuilder();
          foreach (string str in keyValuePair.Value)
            stringBuilder.AppendFormat("{0}</br>", (object) str);
          htmlTextWriter.Write(string.Format("<li>{0}</li>", (object) stringBuilder));
        }
        htmlTextWriter.Write("</ul>");
        this.CollisionFields.InnerHtml = htmlTextWriter.InnerWriter.ToString();
      }
    }

    private void RenderLostFieldsWarning()
    {
      List<string> lostFields = this.GetLostFields();
      if (lostFields.Count == 0)
      {
        (this.Warning).Visible = false;
      }
      else
      {
        (this.Warning).Visible = true;
        HtmlTextWriter htmlTextWriter = new HtmlTextWriter((TextWriter) new StringWriter());
        htmlTextWriter.Write("<ul>");
        foreach (string str in lostFields)
          htmlTextWriter.Write(string.Format("<li>{0}</li>", (object) str));
        htmlTextWriter.Write("</ul>");
        this.LostFields.InnerHtml = htmlTextWriter.InnerWriter.ToString();
      }
    }

    private void UpdateMapping()
    {
      foreach (var control in (this.MappingBorder).Controls)
      {
        if (control is TemplateMenu)
        {
          TemplateMenu templateMenu = control as TemplateMenu;
          templateMenu.TemplateID = (this.EbTemplate).Value;
          templateMenu.ShowStandardField = this.ShowStandardField.Checked ? "1" : "0";
          templateMenu.Redraw();
        }
      }
    }

    private void LoadMapping()
    {
      IFieldItem[] fields = new FormItem(this.CurrentDatabase.GetItem(this.CurrentID)).Fields;
      NameValueCollection nameValueCollection = StringUtil.ParseNameValueCollection(this.Mapping, '|', '=');
      foreach (FieldItem fieldItem in fields)
      {
        string name = ((object) ((CustomItemBase) fieldItem).ID).ToString();
        TemplateMenu templateMenu = new TemplateMenu((this.EbTemplate).Value);
        (templateMenu).ID = "t_" + name;
        templateMenu.FieldName = fieldItem.FieldDisplayName;
        templateMenu.FieldID = name;
        templateMenu.TemplateFieldName = DependenciesManager.ResourceManager.Localize("NOD_DEFINED");
        templateMenu.ShowStandardField = this.ShowStandardField.Checked ? "1" : "0";
        if (!string.IsNullOrEmpty(nameValueCollection[name]))
          templateMenu.TemplateFieldID = nameValueCollection[name];
        (this.MappingBorder).Controls.Add(templateMenu);
      }
    }

    public Database CurrentDatabase => Factory.GetDatabase(Sitecore.Web.WebUtil.GetQueryString("db"));

    public string CurrentID => Sitecore.Web.WebUtil.GetQueryString("id");

    public string Params => HttpContext.Current.Session[Sitecore.Web.WebUtil.GetQueryString("params")] as string;

    public string Mapping
    {
      get => this.GetValueByKey("mapping");
      set => this.SetValue("mapping", value);
    }

    public string Destination
    {
      get => this.GetValueByKey("destination");
      set => this.SetValue("destination", value);
    }

    public string Template
    {
      get => this.GetValueByKey("template");
      set => this.SetValue("template", value);
    }

    public string StandartFields
    {
      get => this.GetValueByKey("showstandartfields");
      set => this.SetValue("showstandartfields", value);
    }
  }
}
