// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Core.Data.FormItem
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Data;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Data.Managers;
using Sitecore.Diagnostics;
using Sitecore.Form.Core.Configuration;
using Sitecore.Form.Core.ContentEditor.Data;
using Sitecore.Form.Core.Data;
using Sitecore.Form.Core.Utility;
using Sitecore.Globalization;
using Sitecore.WFFM.Abstractions;
using Sitecore.WFFM.Abstractions.ContentEditor;
using Sitecore.WFFM.Abstractions.Data;
using Sitecore.WFFM.Abstractions.Data.Enums;
using Sitecore.WFFM.Abstractions.Dependencies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Web.UI;

namespace Sitecore.Forms.Core.Data
{
  public class FormItem : CustomItemBase, IFormItem
  {
    private Sitecore.Form.Core.Data.Tracking traking;

    public FormItem(Sitecore.Data.Items.Item innerItem)
      : base(innerItem)
    {
    }

    public IListDefinition ActionsDefinition
    {
      get
      {
        ListDefinition listDefinition1 = new ListDefinition();
        List<IGroupDefinition> groupDefinitionList = new List<IGroupDefinition>();
        ListDefinition listDefinition2 = ListDefinition.Parse(ActionUtil.OverrideParameters(this.SaveActions, this.LocalizedSaveActions));
        groupDefinitionList.AddRange(listDefinition2.Groups);
        groupDefinitionList.AddRange(ListDefinition.Parse(this.CheckActions).Groups);
        listDefinition1.Groups = (IEnumerable<IGroupDefinition>) groupDefinitionList;
        return (IListDefinition) listDefinition1;
      }
    }

    public string CheckActions
    {
      get => ((BaseItem) this.InnerItem).Fields[Sitecore.Form.Core.Configuration.FieldIDs.CheckActionsID].Value;
      set
      {
        this.InnerItem.Editing.BeginEdit();
        ((BaseItem) this.InnerItem).Fields[Sitecore.Form.Core.Configuration.FieldIDs.CheckActionsID].Value = value;
        this.InnerItem.Editing.EndEdit();
      }
    }

    public IFieldItem[] Fields
    {
      get
      {
        List<IFieldItem> fieldItemList = new List<IFieldItem>();
        fieldItemList.AddRange((IEnumerable<IFieldItem>) this.GetFields(this.InnerItem));
        foreach (Sitecore.Data.Items.Item section in this.Sections)
        {
          if (this.InnerItem.ID  !=  section.ID)
            fieldItemList.AddRange((IEnumerable<IFieldItem>) this.GetFields(section));
        }
        return fieldItemList.ToArray();
      }
    }

    public string Footer => ((BaseItem) this.InnerItem).Fields[Sitecore.Form.Core.Configuration.FieldIDs.FormFooterID].Value;

    public string FooterFieldName => ((BaseItem) this.InnerItem).Fields[Sitecore.Form.Core.Configuration.FieldIDs.FormFooterID].Name;

    public string FormName
    {
      get
      {
        string str = ((BaseItem) this.InnerItem).Fields[Sitecore.Form.Core.Configuration.FieldIDs.FormTitleID].Value;
        return string.IsNullOrEmpty(str) ? this.InnerItem.Name : str;
      }
    }

    public bool HasSections
    {
      get
      {
        foreach (Sitecore.Data.Items.Item child in this.InnerItem.Children)
        {
          if (child.TemplateID == IDs.SectionTemplateID)
            return true;
        }
        return false;
      }
    }

    public string Introduction => ((BaseItem) this.InnerItem).Fields[Sitecore.Form.Core.Configuration.FieldIDs.FormIntroductionID].Value;

    public string IntroductionFieldName => ((BaseItem) this.InnerItem).Fields[Sitecore.Form.Core.Configuration.FieldIDs.FormIntroductionID].Name;

    [Required("IsXdbTrackerEnabled", true)]
    public bool IsAnalyticsEnabled => DependenciesManager.RequirementsChecker.CheckRequirements(MethodBase.GetCurrentMethod().GetType()) && !this.Tracking.Ignore;

    public bool IsDropoutTrackingEnabled => this.IsAnalyticsEnabled && this.Tracking.IsDropoutTrackingEnabled;

    public bool IsAjaxMvcForm => MainUtil.GetBool(((BaseItem) this.InnerItem).Fields[Sitecore.Form.Core.Configuration.FieldIDs.IsAjaxMvcForm].Value, false);

    public bool IsSaveFormDataToStorage => MainUtil.GetBool(((BaseItem) this.InnerItem).Fields[Sitecore.Form.Core.Configuration.FieldIDs.SaveFormDataToStorage].Value, false);

    public Language Language => this.InnerItem.Language;

    public string LeftColumnStyle => ((BaseItem) this.InnerItem).Fields[Sitecore.Form.Core.Configuration.FieldIDs.Mvc.LeftColumnStyle].Value;

    public string RightColumnStyle => ((BaseItem) this.InnerItem).Fields[Sitecore.Form.Core.Configuration.FieldIDs.Mvc.RightColumnStyle].Value;

    public string FormAlignment
    {
      get
      {
        string str = ((BaseItem) this.InnerItem).Fields[Sitecore.Form.Core.Configuration.FieldIDs.Mvc.FormAlignment].Value;
        if (string.IsNullOrEmpty(str))
        {
          if (!string.IsNullOrEmpty(ThemesManager.GetThemeName(this.InnerItem, FormIDs.MvcFormAlignmentID)))
            return string.Empty;
          Sitecore.Data.Items.Item obj = this.Database.GetItem(str);
          return obj == null ? string.Empty : ((BaseItem) obj)["Value"];
        }
        Sitecore.Data.Items.Item obj1 = this.Database.GetItem(str);
        return obj1 == null ? string.Empty : ((BaseItem) obj1)["Value"];
      }
    }

    public HtmlTextWriterTag TitleTag
    {
      get
      {
        string str = ((BaseItem) this.InnerItem).Fields[Sitecore.Form.Core.Configuration.FieldIDs.FormTitleTagID].Value;
        HtmlTextWriterTag result;
        return string.IsNullOrEmpty(str) || !Enum.TryParse<HtmlTextWriterTag>(str, out result) ? HtmlTextWriterTag.H1 : result;
      }
    }

    public FormType FormType
    {
      get
      {
        string str = ((BaseItem) this.InnerItem).Fields[Sitecore.Form.Core.Configuration.FieldIDs.Mvc.FormType].Value;
        if (string.IsNullOrEmpty(str))
        {
          string themeName = ThemesManager.GetThemeName(this.InnerItem, FormIDs.MvcFormTypeID);
          return string.IsNullOrEmpty(themeName) ? FormType.Basic : (FormType) Enum.Parse(typeof (FormType), themeName);
        }
        Sitecore.Data.Items.Item obj = this.Database.GetItem(str);
        if (obj == null)
          return FormType.Basic;
        return (FormType) Enum.Parse(typeof (FormType), obj.Name);
      }
    }

    public string CustomCss => ((BaseItem) this.InnerItem).Fields[Sitecore.Form.Core.Configuration.FieldIDs.Mvc.FormCustomCssClass].Value;

    public string Parameters => ((BaseItem) this.InnerItem).Fields[Sitecore.Form.Core.Configuration.FieldIDs.Mvc.Parameters].Value;

    public string FormTypeClass
    {
      get
      {
        string str = ((BaseItem) this.InnerItem).Fields[Sitecore.Form.Core.Configuration.FieldIDs.Mvc.FormType].Value;
        if (string.IsNullOrEmpty(str))
        {
          if (!string.IsNullOrEmpty(ThemesManager.GetThemeName(this.InnerItem, FormIDs.MvcFormTypeID)))
            return string.Empty;
          Sitecore.Data.Items.Item obj = this.Database.GetItem(str);
          return obj == null ? string.Empty : ((BaseItem) obj)["Value"];
        }
        Sitecore.Data.Items.Item obj1 = this.Database.GetItem(str);
        return obj1 == null ? string.Empty : ((BaseItem) obj1)["Value"];
      }
    }

    public string ProfileItem
    {
      get
      {
        string str = ((BaseItem) this.InnerItem).Fields[Sitecore.Form.Core.Configuration.FieldIDs.ProfileItemId].Value;
        return string.IsNullOrEmpty(str) ? "{AE4C4969-5B7E-4B4E-9042-B2D8701CE214}" : str;
      }
    }

    public string SaveActions
    {
      get => ((BaseItem) this.InnerItem).Fields[Sitecore.Form.Core.Configuration.FieldIDs.SaveActionsID].Value;
      set
      {
        this.InnerItem.Editing.BeginEdit();
        ((BaseItem) this.InnerItem).Fields[Sitecore.Form.Core.Configuration.FieldIDs.SaveActionsID].Value = value;
        this.InnerItem.Editing.EndEdit();
      }
    }

    public string LocalizedSaveActions
    {
      get => ((BaseItem) this.InnerItem).Fields[Sitecore.Form.Core.Configuration.FieldIDs.LocalizedSaveActionsID].Value;
      set
      {
        this.InnerItem.Editing.BeginEdit();
        ((BaseItem) this.InnerItem).Fields[Sitecore.Form.Core.Configuration.FieldIDs.LocalizedSaveActionsID].Value = value;
        this.InnerItem.Editing.EndEdit();
      }
    }

    public Sitecore.Data.Items.Item[] SectionItems
    {
      get
      {
        List<Sitecore.Data.Items.Item> objList = new List<Sitecore.Data.Items.Item>();
        bool flag = true;
        foreach (Sitecore.Data.Items.Item child in this.InnerItem.Children)
        {
          if (child.TemplateID == IDs.SectionTemplateID)
            objList.Add(child);
          else if (flag && child.TemplateID == IDs.FieldTemplateID)
          {
            objList.Add(this.InnerItem);
            flag = false;
          }
        }
        return objList.ToArray();
      }
    }

    public Sitecore.Data.Items.Item[] Sections
    {
      get
      {
        List<Sitecore.Data.Items.Item> objList = new List<Sitecore.Data.Items.Item>();
        bool flag = true;
        foreach (Sitecore.Data.Items.Item child in this.InnerItem.Children)
        {
          if (child.TemplateID == IDs.SectionTemplateID)
            objList.Add(child);
          else if (flag && child.TemplateID == IDs.FieldTemplateID)
          {
            objList.Add(this.InnerItem);
            flag = false;
          }
        }
        return objList.ToArray();
      }
    }

    public string SubmitButtonType
    {
      get
      {
        string str = ((BaseItem) this.InnerItem).Fields[Sitecore.Form.Core.Configuration.FieldIDs.Mvc.SubmitButtonType].Value;
        if (string.IsNullOrEmpty(str))
          return string.Empty;
        Sitecore.Data.Items.Item obj = this.Database.GetItem(str);
        return obj == null ? string.Empty : ((BaseItem) obj)["Value"];
      }
    }

    public string SubmitButtonPosition
    {
      get
      {
        string str = ((BaseItem) this.InnerItem).Fields[Sitecore.Form.Core.Configuration.FieldIDs.Mvc.SubmitButtonPosition].Value;
        if (string.IsNullOrEmpty(str))
          return string.Empty;
        Sitecore.Data.Items.Item obj = this.Database.GetItem(str);
        return obj == null ? string.Empty : ((BaseItem) obj)["Value"];
      }
    }

    public string SubmitButtonSize
    {
      get
      {
        string str = ((BaseItem) this.InnerItem).Fields[Sitecore.Form.Core.Configuration.FieldIDs.Mvc.SubmitButtonSize].Value;
        if (string.IsNullOrEmpty(str))
          return string.Empty;
        Sitecore.Data.Items.Item obj = this.Database.GetItem(str);
        return obj == null ? string.Empty : ((BaseItem) obj)["Value"];
      }
    }

    public bool ShowFooter => MainUtil.GetBool(((BaseItem) this.InnerItem).Fields[Sitecore.Form.Core.Configuration.FieldIDs.ShowFormFooterID].Value, false);

    public bool ShowIntroduction => MainUtil.GetBool(((BaseItem) this.InnerItem).Fields[Sitecore.Form.Core.Configuration.FieldIDs.ShowFormIntroID].Value, false);

    public bool ShowTitle => MainUtil.GetBool(((BaseItem) this.InnerItem).Fields[Sitecore.Form.Core.Configuration.FieldIDs.ShowFormTitleID].Value, false);

    public string SubmitName
    {
      get => ((BaseItem) this.InnerItem).Fields[Sitecore.Form.Core.Configuration.FieldIDs.FormSubmitID].Value;
      set
      {
        this.InnerItem.Editing.BeginEdit();
        ((BaseItem) this.InnerItem).Fields[Sitecore.Form.Core.Configuration.FieldIDs.FormSubmitID].Value = value;
        this.InnerItem.Editing.EndEdit();
      }
    }

    public string SuccessMessage => ((BaseItem) this.InnerItem).Fields[Sitecore.Form.Core.Configuration.FieldIDs.SuccessMessageID].Value;

    public LinkField SuccessPage => new LinkField(((BaseItem) this.InnerItem).Fields[Sitecore.Form.Core.Configuration.FieldIDs.SuccessPageID], ((BaseItem) this.InnerItem).Fields[Sitecore.Form.Core.Configuration.FieldIDs.SuccessPageID].Value);

    public ID SuccessPageID => new LinkField(((BaseItem) this.InnerItem).Fields[Sitecore.Form.Core.Configuration.FieldIDs.SuccessPageID]).TargetID;

    public bool SuccessRedirect => ((BaseItem) this.InnerItem).Fields[Sitecore.Form.Core.Configuration.FieldIDs.SuccessModeID].Value == "{F4D50806-6B89-4F2D-89FE-F77FC0A07D48}";

    public ITracking Tracking => (ITracking) this.traking ?? (ITracking) (this.traking = new Sitecore.Form.Core.Data.Tracking(((BaseItem) this.InnerItem)["__Tracking"], this.InnerItem.Database));

    public ItemUri Uri => this.InnerItem.Uri;

    public Sitecore.Data.Version Version => this.InnerItem.Version;

    public IFieldItem[] this[string section] => string.IsNullOrEmpty(section) || section == ((object) ID.Null).ToString() ? this.GetFields(this.InnerItem) : this.GetFields(this.GetSection(section));

    public static implicit operator FormItem(Sitecore.Data.Items.Item item) => item != null ? new FormItem(item) : (FormItem) null;

    public static FormItem GetForm(ID itemID)
    {
      Assert.ArgumentNotNull((object) itemID, nameof (itemID));
      Sitecore.Data.Items.Item innerItem = StaticSettings.ContextDatabase.GetItem(itemID);
      return innerItem != null ? new FormItem(innerItem) : (FormItem) null;
    }

    public static FormItem GetForm(string itemID) => !string.IsNullOrEmpty(itemID) ? new FormItem(StaticSettings.ContextDatabase.GetItem(itemID)) : (FormItem) null;

    public static bool IsForm(Sitecore.Data.Items.Item item)
    {
      if (item == null)
        return false;
      return item.TemplateID == IDs.FormTemplateID || ((IEnumerable<TemplateItem>) ((IEnumerable<TemplateItem>) item.Template.BaseTemplates).ToList<TemplateItem>()).FirstOrDefault<TemplateItem>((Func<TemplateItem, bool>) (t => ((CustomItemBase) t).ID == IDs.FormTemplateID)) != null;
    }

    public static void UpdateFormItem(
      Database database,
      Language language,
      FormDefinition definition)
    {
      Assert.ArgumentNotNull((object) definition, nameof (definition));
      Assert.ArgumentNotNull((object) database, nameof (database));
      Assert.ArgumentNotNull((object) language, nameof (language));
      new FormItemSynchronizer(database, language, definition).Synchronize();
    }

    public Sitecore.Data.Items.Item AddFormField(
      string fieldName,
      string type,
      bool isValidate)
    {
      Error.AssertString(fieldName, nameof (fieldName), false);
      Error.AssertString(fieldName, "fieldtype", false);
      TemplateItem templateItem = (TemplateItem)(this.InnerItem.Database.GetItem(type));
      return templateItem != null ? ItemManager.CreateItem(fieldName, this.InnerItem, ((CustomItemBase) templateItem).ID) : (Sitecore.Data.Items.Item) null;
    }

    public IFieldItem GetField(ID fieldID)
    {
      Sitecore.Data.Items.Item innerItem = this.InnerItem.Database.GetItem(fieldID);
      return innerItem.ParentID == this.InnerItem.ID || innerItem.Parent.ParentID == this.InnerItem.ID ? (IFieldItem) new FieldItem(innerItem) : (IFieldItem) null;
    }

    public IFieldItem[] GetFields(Sitecore.Data.Items.Item section)
    {
      Assert.ArgumentNotNull((object) section, nameof (section));
      List<IFieldItem> fieldItemList = new List<IFieldItem>();
      foreach (Sitecore.Data.Items.Item child in section.Children)
      {
        if (child.TemplateID == IDs.FieldTemplateID)
          fieldItemList.Add((IFieldItem) new FieldItem(child));
      }
      return fieldItemList.ToArray();
    }

    public Sitecore.Data.Items.Item GetSection(string id) => this.InnerItem.Database.GetItem(ID.Parse(id), this.InnerItem.Language);

    [SpecialName]
    Sitecore.Data.Items.Item IFormItem.InnerItem => this.InnerItem;
  }
}
