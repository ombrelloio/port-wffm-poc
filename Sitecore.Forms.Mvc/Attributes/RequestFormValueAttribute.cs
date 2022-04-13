// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Mvc.Attributes.RequestFormValueAttribute
// Assembly: Sitecore.Forms.Mvc, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1AD2DE3-BABE-4597-B643-AA577F98B8D6
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Mvc.dll

using Sitecore.Diagnostics;
using System;

namespace Sitecore.Forms.Mvc.Attributes
{
  public class RequestFormValueAttribute : Attribute
  {
    public RequestFormValueAttribute(string name)
    {
      Assert.ArgumentNotNull((object) name, nameof (name));
      this.Name = name;
    }

    public string Name { get; set; }
  }
}
