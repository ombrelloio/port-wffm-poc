// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.Attributes.VisualPropertiesAttribute
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using System;

namespace Sitecore.Form.Core.Attributes
{
  [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
  public class VisualPropertiesAttribute : Attribute
  {
    private string[] properties;

    public VisualPropertiesAttribute(string[] properties) => this.properties = properties;

    public string[] Properties => this.properties;
  }
}
