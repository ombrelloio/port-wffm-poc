// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.SchemaGenerator.PropertyAttributeInstance
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using System;
using System.ComponentModel;
using System.Reflection;
using System.Web;
using System.Web.UI;

namespace Sitecore.Form.Core.SchemaGenerator
{
  internal class PropertyAttributeInstance
  {
    private bool browsable;
    private string _defaultValue;
    private bool _useRequired;
    private bool _nonFilterable;
    private string _category;
    private PropertyInfo _propertyInfo;
    private string _name;
    private Type _baseType;
    private string _value;
    private TypeConverter typeConverter;

    internal PropertyAttributeInstance(PropertyInfo pi, Type attributeType, TypeConverter tc)
    {
      BrowsableAttribute customAttribute = (BrowsableAttribute) DesignAttributesUtil.GetCustomAttribute((MemberInfo) pi, typeof (BrowsableAttribute));
      this.browsable = customAttribute == null || customAttribute.Browsable;
      this.typeConverter = tc;
      this.Init(pi, attributeType, tc);
    }

    protected void Init(PropertyInfo pi, Type attributeType, TypeConverter tc)
    {
      this._propertyInfo = pi;
      this._name = pi.Name;
      this._baseType = attributeType;
      this._useRequired = SpecialCases.IsContentPlaceHolderIDProperty(pi);
      FilterableAttribute customAttribute1 = (FilterableAttribute) DesignAttributesUtil.GetCustomAttribute((MemberInfo) pi, typeof (FilterableAttribute));
      this._nonFilterable = !(customAttribute1 != null ? customAttribute1.Filterable : FilterableAttribute.Default.Filterable);
      CategoryAttribute customAttribute2 = (CategoryAttribute) DesignAttributesUtil.GetCustomAttribute((MemberInfo) pi, typeof (CategoryAttribute));
      if (customAttribute2 != null)
        this._category = customAttribute2.Category;
      DefaultValueAttribute customAttribute3 = (DefaultValueAttribute) DesignAttributesUtil.GetCustomAttribute((MemberInfo) pi, typeof (DefaultValueAttribute));
      if (customAttribute3 == null || customAttribute3.Value == null)
        return;
      this._defaultValue = tc == null ? customAttribute3.Value.ToString() : PropertyAttributeInstance.ConvertToString(tc, customAttribute3.Value);
      if (this._defaultValue == null)
        return;
      this._defaultValue = HttpUtility.HtmlEncode(this._defaultValue);
    }

    public static string ConvertToString(TypeConverter tc, object value)
    {
      if (value == null)
        return string.Empty;
      if (tc != null && tc.CanConvertTo(typeof (string)))
      {
        if (value is string)
        {
          value = (object) (value as string);
        }
        else
        {
          try
          {
            value = (object) tc.ConvertToString(value);
          }
          catch
          {
          }
        }
      }
      return (value ?? (object) string.Empty).ToString();
    }

    public Type BaseType => this._baseType;

    public bool Browsable => this.browsable;

    public string DefaultValue => this._defaultValue;

    internal string Value
    {
      get => this._value ?? this._defaultValue;
      set => this._value = value;
    }

    public bool UseRequired => this._useRequired;

    public bool Filterable => this._nonFilterable;

    public string Category => this._category;

    public PropertyInfo PropertyInfo => this._propertyInfo;

    public string Name => this._name;

    public TypeConverter TypeConverter => this.typeConverter;
  }
}
