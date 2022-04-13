// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.Visual.VisualPropertyInfo
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Diagnostics;
using Sitecore.Form.Core.Attributes;
using Sitecore.Form.Core.Configuration;
using Sitecore.Web.UI.HtmlControls;
using Sitecore.WFFM.Abstractions.Dependencies;
using Sitecore.WFFM.Abstractions.Shared;
using System;
using System.ComponentModel;
using System.Reflection;

namespace Sitecore.Form.Core.Visual
{
  public class VisualPropertyInfo : Control
  {
    private readonly IResourceManager resourceManager;
    private string html;

    public VisualPropertyInfo()
      : this(DependenciesManager.ResourceManager)
    {
    }

    public VisualPropertyInfo(IResourceManager resourceManager)
    {
      Assert.IsNotNull((object) resourceManager, "Dependency resourceManager is null");
      this.resourceManager = resourceManager;
    }

    private VisualPropertyInfo(
      string propertyName,
      string category,
      string displayName,
      string defaultValue,
      int sortOrder,
      ValidationType validation,
      Type fieldType,
      object[] parameters,
      bool localize)
    {
      this.FieldType = (IVisualFieldType) fieldType.InvokeMember((string) null, BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.CreateInstance, (Binder) null, (object) null, parameters ?? new object[0]);
      if (fieldType == (Type) null)
        throw new NotSupportedException(string.Format(this.resourceManager.Localize("NOT_SUPPORT"), (object) fieldType.Name, (object) "IVisualFieldType"));
      this.FieldType.ID = StaticSettings.PrefixId + (localize ? StaticSettings.PrefixLocalizeId : string.Empty) + propertyName;
      this.FieldType.DefaultValue = defaultValue;
      this.FieldType.EmptyValue = defaultValue;
      this.FieldType.Validation = validation;
      this.FieldType.Localize = localize;
      this.DisplayName = displayName;
      this.Category = category;
      this.CategorySortOrder = sortOrder;
      this.PropertyName = propertyName;
    }

    public string Category { get; private set; }

    public int CategorySortOrder { get; private set; }

    public string DefaultValue
    {
      get => this.FieldType.DefaultValue;
      set => this.FieldType.DefaultValue = value;
    }

    public string DisplayName { get; private set; }

    public IVisualFieldType FieldType { get; private set; }

    public new string ID => this.FieldType.ID;

    public string PropertyName { get; private set; }

    public ValidationType Validation => this.FieldType.Validation;

    public static VisualPropertyInfo Parse(string propertyName) => new VisualPropertyInfo(propertyName, DependenciesManager.ResourceManager.Localize("APPEARANCE"), propertyName, string.Empty, -1, ValidationType.None, typeof (EditField), new object[0], false);

    public static VisualPropertyInfo Parse(PropertyInfo info)
    {
      if (!(info != (PropertyInfo) null) || !Attribute.IsDefined((MemberInfo) info, typeof (VisualPropertyAttribute), true))
        return (VisualPropertyInfo) null;
      string name = info.Name;
      string displayName = string.Empty;
      ValidationType validation = ValidationType.None;
      string category = DependenciesManager.ResourceManager.Localize("APPEARANCE");
      int sortOrder = -1;
      bool localize = false;
      Type fieldType = typeof (EditField);
      string empty = string.Empty;
      object[] parameters = (object[]) null;
      foreach (object customAttribute in info.GetCustomAttributes(true))
      {
        switch (customAttribute)
        {
          case VisualPropertyAttribute _:
            displayName = (customAttribute as VisualPropertyAttribute).DisplayName;
            sortOrder = (customAttribute as VisualPropertyAttribute).Sortorder;
            break;
          case VisualCategoryAttribute _:
            category = (customAttribute as VisualCategoryAttribute).Category;
            break;
          case ValidationAttribute _:
            validation = (customAttribute as ValidationAttribute).Validation;
            break;
          case VisualFieldTypeAttribute _:
            fieldType = (customAttribute as VisualFieldTypeAttribute).FieldType;
            parameters = (customAttribute as VisualFieldTypeAttribute).Parameters;
            break;
          case DefaultValueAttribute _:
            empty = (customAttribute as DefaultValueAttribute).Value.ToString();
            break;
          case LocalizeAttribute _:
            localize = true;
            break;
        }
      }
      return new VisualPropertyInfo(name, category, displayName, empty, sortOrder, validation, fieldType, parameters, localize);
    }

    public virtual string RenderField()
    {
      if (!string.IsNullOrEmpty(this.html) && this.FieldType.IsCacheable)
        return this.html;
      string str = this.FieldType.Render();
      if (this.FieldType.IsCacheable)
        this.html = str;
      return str;
    }

    internal static VisualPropertyInfo Parse(
      string propertyName,
      string displayName,
      string defaultValue,
      string category,
      bool storeInLocalizedParameters)
    {
      return new VisualPropertyInfo(propertyName, DependenciesManager.ResourceManager.Localize(category), DependenciesManager.ResourceManager.Localize(displayName), defaultValue, -1, ValidationType.None, typeof (EditField), new object[0], storeInLocalizedParameters);
    }
  }
}
