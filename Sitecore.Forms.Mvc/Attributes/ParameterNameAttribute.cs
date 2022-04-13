// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Mvc.Attributes.ParameterNameAttribute
// Assembly: Sitecore.Forms.Mvc, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1AD2DE3-BABE-4597-B643-AA577F98B8D6
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Mvc.dll

using System;

namespace Sitecore.Forms.Mvc.Attributes
{
  [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
  public class ParameterNameAttribute : Attribute
  {
    public ParameterNameAttribute(string name) => this.Name = name;

    public string Name { get; private set; }
  }
}
