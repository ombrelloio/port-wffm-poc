// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Mvc.Extensions.TypeExtensions
// Assembly: Sitecore.Forms.Mvc, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1AD2DE3-BABE-4597-B643-AA577F98B8D6
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Mvc.dll

using Sitecore.Diagnostics;
using System.Reflection;

namespace Sitecore.Forms.Mvc.Extensions
{
  public static class TypeExtensions
  {
    public static T GetPropertyValue<T>(this object entity, string propertyName)
    {
      Assert.ArgumentNotNull(entity, nameof (entity));
      T obj = default (T);
      if (entity == null || string.IsNullOrEmpty(propertyName))
        return obj;
      PropertyInfo property = entity.GetType().GetProperty(propertyName);
      return property != (PropertyInfo) null && typeof (T).IsAssignableFrom(property.PropertyType) ? (T) property.GetValue(entity) : obj;
    }
  }
}
