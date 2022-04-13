// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Core.Data.FieldItem
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Collections;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Form.Core.Configuration;
using Sitecore.Form.Core.Data;
using Sitecore.Form.Core.Utility;
using Sitecore.Mvc.Extensions;
using Sitecore.WFFM.Abstractions.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Sitecore.Forms.Core.Data
{
  public class FieldItem : FieldTypeItem, IFieldItem, IFieldTypeItem
  {
    public FieldItem(Sitecore.Data.Items.Item innerItem)
      : base(innerItem)
    {
    }

    public override string AssemblyName
    {
      get
      {
        IFieldTypeItem type = this.Type;
        return type != null ? type.AssemblyName : string.Empty;
      }
    }

    public override string ClassName
    {
      get
      {
        IFieldTypeItem type = this.Type;
        return type != null ? type.ClassName : string.Empty;
      }
    }

    public string Conditions => ((BaseItem) ((CustomItemBase) this).InnerItem)[Sitecore.Form.Core.Configuration.FieldIDs.ConditionsFieldID];

    public override bool DenyTag => this.Type != null && this.Type.DenyTag;

    public string FieldDisplayName
    {
      get
      {
        string str = this.Title;
        if (string.IsNullOrEmpty(str))
          str = ((CustomItemBase) this).InnerItem.DisplayName;
        return str;
      }
    }

    public FieldCollection Fields => ((BaseItem) ((CustomItemBase) this).InnerItem).Fields;

    public override bool IsRequired => this.Type.IsRequired && MainUtil.GetBool(((BaseItem) ((CustomItemBase) this).InnerItem).Fields[Sitecore.Form.Core.Configuration.FieldIDs.FieldRequiredID].Value, false);

    public bool IsSaveToStorage => MainUtil.GetBool(((BaseItem) ((CustomItemBase) this).InnerItem).Fields[Sitecore.Form.Core.Configuration.FieldIDs.FieldSaveToStorage].Value, false);

    public bool IsTag => !this.DenyTag && MainUtil.GetBool(((BaseItem) ((CustomItemBase) this).InnerItem).Fields[Sitecore.Form.Core.Configuration.FieldIDs.FieldTagID].Value, false);

    public Dictionary<string, string> LocalizedParametersDictionary
    {
      get
      {
        Dictionary<string, string> source = this.Type != null ? this.GetParametersDictionary(this.Type.LocalizedParameters) : (Dictionary<string, string>) null;
        Dictionary<string, string> fieldParameters = this.GetParametersDictionary(this.LocalizedParameters) ?? new Dictionary<string, string>();
        return source == null ? fieldParameters : fieldParameters.Concat<KeyValuePair<string, string>>(source.Where<KeyValuePair<string, string>>((Func<KeyValuePair<string, string>, bool>) (ftp => !fieldParameters.ContainsKey(ftp.Key)))).ToDictionary<KeyValuePair<string, string>, string, string>((Func<KeyValuePair<string, string>, string>) (x => x.Key), (Func<KeyValuePair<string, string>, string>) (x => x.Value));
      }
    }

    public Dictionary<string, string> ParametersDictionary
    {
      get
      {
        Dictionary<string, string> source = this.Type != null ? this.GetParametersDictionary(this.Type.Parameters) : (Dictionary<string, string>) null;
        Dictionary<string, string> fieldParameters = this.GetParametersDictionary(this.Parameters) ?? new Dictionary<string, string>();
        return source == null ? fieldParameters : fieldParameters.Concat<KeyValuePair<string, string>>(source.Where<KeyValuePair<string, string>>((Func<KeyValuePair<string, string>, bool>) (ftp => !fieldParameters.ContainsKey(ftp.Key)))).ToDictionary<KeyValuePair<string, string>, string, string>((Func<KeyValuePair<string, string>, string>) (x => x.Key), (Func<KeyValuePair<string, string>, string>) (x => x.Value));
      }
    }

    public override string MVCClass
    {
      get
      {
        IFieldTypeItem type = this.Type;
        return type != null ? type.MVCClass : string.Empty;
      }
    }

    public Dictionary<string, string> MvcValidationMessages => this.GetMvcValidationMessages();

    public int SortOrder => ((CustomItemBase) this).InnerItem.Parent.TemplateID == IDs.SectionTemplateID ? ((CustomItemBase) this).InnerItem.Parent.Appearance.Sortorder * 10 + ((CustomItemBase) this).InnerItem.Appearance.Sortorder : ((CustomItemBase) this).InnerItem.Appearance.Sortorder;

    public new IEnumerable<ISubFieldItem> SubFields
    {
      get
      {
        IFieldTypeItem type = this.Type;
        return type != null ? type.SubFields : (IEnumerable<ISubFieldItem>) new List<SubFieldItem>();
      }
    }

    public IEnumerable<string> SubFieldsKeys => this.Type != null ? this.Type.SubFields.Select<ISubFieldItem, string>((Func<ISubFieldItem, string>) (s => s.Key)) : (IEnumerable<string>) new string[0];

    public string Title => ((BaseItem) ((CustomItemBase) this).InnerItem).Fields[Sitecore.Form.Core.Configuration.FieldIDs.FieldTitleID].Value;

    public IFieldTypeItem Type
    {
      get
      {
        Sitecore.Data.Items.Item innerItem = ((CustomItemBase) this).InnerItem.Database.GetItem(this.TypeID);
        return innerItem != null ? (IFieldTypeItem) new FieldTypeItem(innerItem) : (IFieldTypeItem) null;
      }
    }

    public ID TypeID
    {
      get
      {
        string str = ((BaseItem) ((CustomItemBase) this).InnerItem).Fields[Sitecore.Form.Core.Configuration.FieldIDs.FieldLinkTypeID].Value;
        return !string.IsNullOrEmpty(str) ? ID.Parse(str) : ID.Null;
      }
    }

    public override string UserControl
    {
      get
      {
        IFieldTypeItem type = this.Type;
        return type != null ? type.UserControl : string.Empty;
      }
    }

    public override string[] Validators => this.Type.Validators;

    public new string this[ID id] => ((BaseItem) ((CustomItemBase) this).InnerItem)[id];

    public List<IValidationItem> GetValidationItems() => ((IEnumerable<string>) this.Validators).Select<string, IValidationItem>((Func<string, IValidationItem>) (id => (IValidationItem) new ValidationItem(((CustomItemBase) this).InnerItem.Database.GetItem(id)))).ToList<IValidationItem>();

    public int GetSubFieldSortOrder(string key)
    {
      int num = 0;
      foreach (string subFieldsKey in this.SubFieldsKeys)
      {
        ++num;
        string strB = key;
        if (string.Compare(subFieldsKey, strB, StringComparison.OrdinalIgnoreCase) == 0)
          break;
      }
      return this.SortOrder + num;
    }

    public string GetSubFieldTitle(string key)
    {
      Assert.ArgumentNotNull((object) key, nameof (key));
      ISubFieldItem subField = this.SubFields.FirstOrDefault<ISubFieldItem>((Func<ISubFieldItem, bool>) (s => string.Compare(s.Key, key, StringComparison.OrdinalIgnoreCase) == 0));
      if (subField == null)
        return key;
      Pair<string, string> pair = ParametersUtil.XmlToPairArray(this.LocalizedParameters).FirstOrDefault<Pair<string, string>>((Func<Pair<string, string>, bool>) (p => string.Compare(p.Part1, subField.TitleProperty, StringComparison.OrdinalIgnoreCase) == 0));
      return pair == null ? subField.DefaultTitle : pair.Part2;
    }

    private Dictionary<string, string> GetParametersDictionary(string parameters)
    {
      Assert.ArgumentNotNull((object) parameters, nameof (parameters));
      return parameters.Trim() == string.Empty ? (Dictionary<string, string>) null : ParametersUtil.XmlToDictionary(parameters);
    }

    private Dictionary<string, string> GetMvcValidationMessages()
    {
      Dictionary<string, string> defaultErrorMessages = StaticSettings.GetDefaultMvcValidationErrors();
      Dictionary<string, string> customErrorMessagesFromDictionary = ((IEnumerable<string>)this.InnerItem[Sitecore.Form.Core.Configuration.FieldIDs.MvcValidationErrors].Split('|', false).ToArray<string>()).Select<string, Sitecore.Data.Items.Item>((Func<string, Sitecore.Data.Items.Item>)(id => this.InnerItem.Database.GetItem(id))).ToDictionary<Sitecore.Data.Items.Item, string, string>((Func<Sitecore.Data.Items.Item, string>)(x => x[Sitecore.Form.Core.Configuration.FieldIDs.MvcValidationErrorKey]), (Func<Sitecore.Data.Items.Item, string>)(y => y[Sitecore.Form.Core.Configuration.FieldIDs.MvcValidationText])).Where<KeyValuePair<string, string>>((Func<KeyValuePair<string, string>, bool>)(item1 => defaultErrorMessages.ContainsKey(item1.Key))).ToDictionary<string, string>();
            Dictionary<string, string> dictionary = EnumerableExtensions.ToDictionary<string, string>(customErrorMessagesFromDictionary.Union<KeyValuePair<string, string>>(defaultErrorMessages.Where<KeyValuePair<string, string>>((Func<KeyValuePair<string, string>, bool>) (item2 => !customErrorMessagesFromDictionary.ContainsKey(item2.Key)))));
      if (this.LocalizedParameters == null)
        return dictionary;
      Dictionary<string, string> customErrorMessagesFromParameters = EnumerableExtensions.ToDictionary<string, string>(ParametersUtil.XmlToDictionaryWithOriginalNames(this.LocalizedParameters).Where<KeyValuePair<string, string>>((Func<KeyValuePair<string, string>, bool>) (item => !string.IsNullOrEmpty(item.Value) && defaultErrorMessages.ContainsKey(item.Key))));
      return EnumerableExtensions.ToDictionary<string, string>(customErrorMessagesFromParameters.Union<KeyValuePair<string, string>>(dictionary.Where<KeyValuePair<string, string>>((Func<KeyValuePair<string, string>, bool>) (item2 => !customErrorMessagesFromParameters.ContainsKey(item2.Key)))));
    }

    [SpecialName]
    Sitecore.Data.Items.Item IFieldTypeItem.InnerItem => ((CustomItemBase) this).InnerItem;
  }
}
