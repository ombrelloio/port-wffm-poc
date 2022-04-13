// Decompiled with JetBrains decompiler
// Type: Sitecore.WFFM.Abstractions.RequiredAttribute
// Assembly: Sitecore.WFFM.Abstractions, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 893E7AF0-E7C2-4A02-B47B-5687A98493FC
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.WFFM.Abstractions.dll

using System;

namespace Sitecore.WFFM.Abstractions
{
  [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
  public class RequiredAttribute : Attribute
  {
    public RequiredAttribute(string propertyName, bool propertyValue = true)
    {
      this.PropertyName = propertyName;
      this.PropertyValue = propertyValue;
    }

    public string PropertyName { get; set; }

    public bool PropertyValue { get; set; }
  }
}
