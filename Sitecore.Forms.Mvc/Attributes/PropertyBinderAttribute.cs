// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Mvc.Attributes.PropertyBinderAttribute
// Assembly: Sitecore.Forms.Mvc, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1AD2DE3-BABE-4597-B643-AA577F98B8D6
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Mvc.dll

using System;

namespace Sitecore.Forms.Mvc.Attributes
{
  [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
  public class PropertyBinderAttribute : Attribute
  {
    public PropertyBinderAttribute(Type binderType) => this.BinderType = binderType;

    public Type BinderType { get; private set; }
  }
}
