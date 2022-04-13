// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.Visual.PropertiesFactory
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Collections;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Form.Core.Attributes;
using Sitecore.Form.Core.Configuration;
using Sitecore.Form.Core.Utility;
using Sitecore.Resources;
using Sitecore.WFFM.Abstractions.Dependencies;
using Sitecore.WFFM.Abstractions.Shared;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Sitecore.Form.Core.Visual
{
  public class PropertiesFactory
  {
    private static string fieldSetStart = "<fieldset class=\"sc-accordion-header\"><legend class=\"sc-accordion-header-left\"><span class=\"sc-accordion-header-center\">{0}<strong>{1}</strong><div class=\"sc-accordion-header-right\">&nbsp;</div></span></legend>";
    private static string fieldSetEnd = "</fieldset>";
    private static Hashtable infos = new Hashtable();
    private readonly Item item;
    private readonly IItemRepository itemRepository;
    private readonly IResourceManager resourceManager;

    public PropertiesFactory(
      Item item,
      IItemRepository itemRepository,
      IResourceManager resourceManager)
    {
      Assert.ArgumentNotNull((object) item, nameof (item));
      Assert.ArgumentNotNull((object) itemRepository, nameof (itemRepository));
      Assert.ArgumentNotNull((object) resourceManager, nameof (resourceManager));
      this.item = item;
      this.itemRepository = itemRepository;
      this.resourceManager = resourceManager;
    }

    public static string FieldSetStart
    {
      get => PropertiesFactory.fieldSetStart;
      set => PropertiesFactory.fieldSetStart = value;
    }

    public static string FieldSetEnd
    {
      get => PropertiesFactory.fieldSetEnd;
      set => PropertiesFactory.fieldSetEnd = value;
    }

    protected static Hashtable Infos
    {
      get => PropertiesFactory.infos;
      set => PropertiesFactory.infos = value;
    }

    public static string RenderPropertiesSection(Item item, ID assemblyField, ID classField) => new PropertiesFactory(item, DependenciesManager.Resolve<IItemRepository>(), DependenciesManager.Resolve<IResourceManager>()).RenderPropertiesEditor(assemblyField, classField);

    internal static IEnumerable<string> CompareTypes(
      IEnumerable<Pair<string, string>> properties,
      Item newType,
      Item oldType,
      ID assemblyField,
      ID classField)
    {
      if (!(properties is Pair<string, string>[] pairArray1))
        pairArray1 = properties.ToArray<Pair<string, string>>();
      Pair<string, string>[] pairArray2 = pairArray1;
      if (properties == null || !((IEnumerable<Pair<string, string>>) pairArray2).Any<Pair<string, string>>())
        return (IEnumerable<string>) new string[0];
      List<VisualPropertyInfo> newTypeInfos = new PropertiesFactory(newType, DependenciesManager.Resolve<IItemRepository>(), DependenciesManager.Resolve<IResourceManager>()).GetProperties(assemblyField, classField);
      List<VisualPropertyInfo> oldTypeInfos = new PropertiesFactory(oldType, DependenciesManager.Resolve<IItemRepository>(), DependenciesManager.Resolve<IResourceManager>()).GetProperties(assemblyField, classField);
      IEnumerable<string> source = (IEnumerable<string>) new string[0];
      if (oldTypeInfos.Count > 0)
        source = ((IEnumerable<Pair<string, string>>) pairArray2).Where<Pair<string, string>>((Func<Pair<string, string>, bool>) (p => oldTypeInfos.FirstOrDefault<VisualPropertyInfo>((Func<VisualPropertyInfo, bool>) (s => s.PropertyName.ToLower() == p.Part1.ToLower())) != null && oldTypeInfos.FirstOrDefault<VisualPropertyInfo>((Func<VisualPropertyInfo, bool>) (s => s.PropertyName.ToLower() == p.Part1.ToLower())).DefaultValue.ToLower() != p.Part2.ToLower())).Select<Pair<string, string>, string>((Func<Pair<string, string>, string>) (p => p.Part1.ToLower()));
      return source.Where<string>((Func<string, bool>) (f => newTypeInfos.Find((Predicate<VisualPropertyInfo>) (s => s.PropertyName.ToLower() == f)) == null)).Select<string, string>((Func<string, string>) (f => oldTypeInfos.Find((Predicate<VisualPropertyInfo>) (s => s.PropertyName.ToLower() == f)).DisplayName.TrimEnd(' ', ':')));
    }

    protected VisualPropertyInfo[] GetClassDefinedProperties(
      ICustomAttributeProvider type)
    {
      List<VisualPropertyInfo> visualPropertyInfoList = new List<VisualPropertyInfo>();
      if (type != null)
      {
        object[] customAttributes = type.GetCustomAttributes(typeof (VisualPropertiesAttribute), true);
        if (customAttributes.Length != 0 && customAttributes[0] is VisualPropertiesAttribute propertiesAttribute2)
        {
          string[] properties = propertiesAttribute2.Properties;
          visualPropertyInfoList.AddRange(((IEnumerable<string>) properties).Select<string, VisualPropertyInfo>(new Func<string, VisualPropertyInfo>(VisualPropertyInfo.Parse)));
        }
      }
      return visualPropertyInfoList.ToArray();
    }

    protected List<VisualPropertyInfo> GetProperties(
      ID assemblyField,
      ID classField)
    {
      Assert.ArgumentNotNull((object) assemblyField, nameof (assemblyField));
      Assert.ArgumentNotNull((object) classField, nameof (classField));
      string str = ((BaseItem) this.item)[assemblyField] + ((BaseItem) this.item)[classField] + ((BaseItem) this.item)[Sitecore.Form.Core.Configuration.FieldIDs.FieldUserControlID];
      if (PropertiesFactory.Infos.ContainsKey((object) str))
        return PropertiesFactory.Infos[(object) str] as List<VisualPropertyInfo>;
      List<VisualPropertyInfo> visualPropertyInfoList = new List<VisualPropertyInfo>();
      Type fieldType = FieldReflectionUtil.GetFieldType(((BaseItem) this.item)[assemblyField], ((BaseItem) this.item)[classField], ((BaseItem) this.item)[Sitecore.Form.Core.Configuration.FieldIDs.FieldUserControlID]);
      if (fieldType != (Type) null)
      {
        visualPropertyInfoList.AddRange((IEnumerable<VisualPropertyInfo>) this.GetClassDefinedProperties((ICustomAttributeProvider) fieldType));
        visualPropertyInfoList.AddRange((IEnumerable<VisualPropertyInfo>) this.GetPropertyDefinedProperties(fieldType));
      }
      string typeName = ((BaseItem) this.item)[Sitecore.Form.Core.Configuration.FieldIDs.MvcFieldId];
      if (!string.IsNullOrEmpty(typeName))
      {
        Type type = Type.GetType(typeName);
        if (type != (Type) null)
          visualPropertyInfoList.AddRange((IEnumerable<VisualPropertyInfo>) this.GetMvcCustomErrorMessageProperties(type));
      }
      visualPropertyInfoList.Sort((IComparer<VisualPropertyInfo>) new CategoryComparer());
      PropertiesFactory.Infos.Add((object) str, (object) visualPropertyInfoList);
      return visualPropertyInfoList;
    }

    protected VisualPropertyInfo[] GetPropertyDefinedProperties(Type type)
    {
      List<VisualPropertyInfo> visualPropertyInfoList = new List<VisualPropertyInfo>();
      if (type != (Type) null)
      {
        PropertyInfo[] properties = type.GetProperties();
        visualPropertyInfoList.AddRange(((IEnumerable<PropertyInfo>) properties).Select<PropertyInfo, VisualPropertyInfo>(new Func<PropertyInfo, VisualPropertyInfo>(VisualPropertyInfo.Parse)).Where<VisualPropertyInfo>((Func<VisualPropertyInfo, bool>) (property => property != null)));
      }
      return visualPropertyInfoList.ToArray();
    }

    protected VisualPropertyInfo[] GetMvcCustomErrorMessageProperties(Type type)
    {
      List<VisualPropertyInfo> visualPropertyInfoList = new List<VisualPropertyInfo>();
      List<System.Attribute> source = new List<System.Attribute>();
      foreach (PropertyInfo property in type.GetProperties())
        source.AddRange(((IEnumerable<System.Attribute>)System.Attribute.GetCustomAttributes((MemberInfo) property)).Where<System.Attribute>((Func<System.Attribute, bool>) (a => a.GetType().BaseType != typeof (System.ComponentModel.DataAnnotations.ValidationAttribute) && a.GetType().BaseType != typeof (DataTypeAttribute) && a is System.ComponentModel.DataAnnotations.ValidationAttribute)));
      Dictionary<string, string> mvcValidationMessages = this.itemRepository.CreateFieldItem(this.item).MvcValidationMessages;
      visualPropertyInfoList.AddRange(source.GroupBy<System.Attribute, string>((Func<System.Attribute, string>) (x => x.ToString())).Select<IGrouping<string, System.Attribute>, System.Attribute>((Func<IGrouping<string, System.Attribute>, System.Attribute>) (g => g.First<System.Attribute>())).Select<System.Attribute, VisualPropertyInfo>((Func<System.Attribute, VisualPropertyInfo>) (a => VisualPropertyInfo.Parse(a.GetType().Name, this.GetDisplayNameAttributeValue((MemberInfo) a.GetType()), mvcValidationMessages.FirstOrDefault<KeyValuePair<string, string>>((Func<KeyValuePair<string, string>, bool>) (m => m.Key == a.GetType().Name)).Value, "VALIDATION_ERROR_MESSAGES", true))));
      return visualPropertyInfoList.ToArray();
    }

    protected string RenderCategoryBegin(string name) => Sitecore.StringExtensions.StringExtensions.FormatWith(PropertiesFactory.FieldSetStart, new object[2]
    {
      (object) ((object) new ImageBuilder()
      {
        Width = 16,
        Height = 16,
        Border = "0",
        Align = "middle",
        Class = "sc-accordion-icon",
        Src = Themes.MapTheme("Applications/16x16/document_new.png", string.Empty, false)
      }).ToString(),
      (object) (Translate.Text(name) ?? string.Empty)
    });

    protected string RenderCategoryEnd() => PropertiesFactory.FieldSetEnd;

    protected string RenderPropertiesEditor(IEnumerable<VisualPropertyInfo> properties)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("<div class=\"scFieldProperties\" id=\"FieldProperties\" vAling=\"top\">");
      if (!(properties is VisualPropertyInfo[] visualPropertyInfoArray))
        visualPropertyInfoArray = properties.ToArray<VisualPropertyInfo>();
      if (!((IEnumerable<VisualPropertyInfo>) visualPropertyInfoArray).Any<VisualPropertyInfo>())
      {
        stringBuilder.Append("<div class=\"scFbSettingSectionEmpty\">");
        stringBuilder.AppendFormat("<label class='scFbHasNoPropLabel'>{0}</label>", (object) this.resourceManager.Localize("HAS_NO_PROPERTIES"));
        stringBuilder.Append("</div>");
      }
      string name = string.Empty;
      bool flag = false;
      foreach (VisualPropertyInfo info in visualPropertyInfoArray)
      {
        if (string.IsNullOrEmpty(name) || name != info.Category)
        {
          if (flag)
          {
            stringBuilder.Append("</div>");
            stringBuilder.Append(this.RenderCategoryEnd());
          }
          name = info.Category;
          flag = true;
          stringBuilder.Append(this.RenderCategoryBegin(name));
          stringBuilder.Append("<div class='sc-accordion-field-body'>");
        }
        stringBuilder.Append(this.RenderProperty(info));
      }
      stringBuilder.Append("</div>");
      return stringBuilder.ToString();
    }

    protected string RenderPropertiesEditor(ID assemblyField, ID classField) => this.RenderPropertiesEditor((IEnumerable<VisualPropertyInfo>) this.GetProperties(assemblyField, classField));

    protected string RenderProperty(VisualPropertyInfo info)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("<div class='scFbPeEntry'>");
      if (!string.IsNullOrEmpty(info.DisplayName))
      {
        string str1 = Translate.Text(info.DisplayName);
        string str2 = info.FieldType is EditField ? "scFbPeLabelFullWidth" : "scFbPeLabel";
        stringBuilder.AppendFormat("<label class='{0}' for='{1}'>{2}</label>", (object) str2, (object) info.ID, (object) str1);
      }
      stringBuilder.Append(info.RenderField());
      stringBuilder.Append("</div>");
      return stringBuilder.ToString();
    }

    private string GetDisplayNameAttributeValue(MemberInfo t)
    {
      DisplayNameAttribute customAttribute = t.GetCustomAttribute<DisplayNameAttribute>();
      return customAttribute != null ? customAttribute.DisplayName : t.Name;
    }
  }
}
