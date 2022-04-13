// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.Attributes.VisualPropertyAttribute
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using System;

namespace Sitecore.Form.Core.Attributes
{
  [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
  public class VisualPropertyAttribute : Attribute
  {
    private string displayName;
    private readonly int sortorder = -1;

    public VisualPropertyAttribute()
    {
    }

    public VisualPropertyAttribute(string displayName) => this.displayName = displayName;

    public VisualPropertyAttribute(string displayName, int sortorder)
    {
      this.displayName = displayName;
      this.sortorder = sortorder;
    }

    public string DisplayName
    {
      get => this.displayName;
      set => this.displayName = value;
    }

    public int Sortorder => this.sortorder;
  }
}
