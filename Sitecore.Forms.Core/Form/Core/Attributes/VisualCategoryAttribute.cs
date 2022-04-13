// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.Attributes.VisualCategoryAttribute
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Form.Core.Configuration;
using System;

namespace Sitecore.Form.Core.Attributes
{
  [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
  public class VisualCategoryAttribute : Attribute
  {
    private readonly string category;

    public VisualCategoryAttribute(string category) => this.category = category;

    public string Category => Translate.Text(this.category);
  }
}
