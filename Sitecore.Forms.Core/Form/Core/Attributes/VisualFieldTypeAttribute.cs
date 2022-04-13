// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.Attributes.VisualFieldTypeAttribute
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using System;

namespace Sitecore.Form.Core.Attributes
{
  [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
  public class VisualFieldTypeAttribute : Attribute
  {
    private readonly Type fieldType;

    public VisualFieldTypeAttribute(Type fieldType) => this.fieldType = fieldType;

    public VisualFieldTypeAttribute(Type fieldType, object[] parameters)
    {
      this.Parameters = parameters;
      this.fieldType = fieldType;
    }

    public Type FieldType => this.fieldType;

    public object[] Parameters { get; set; }
  }
}
