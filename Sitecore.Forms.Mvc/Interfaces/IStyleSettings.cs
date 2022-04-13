// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Mvc.Interfaces.IStyleSettings
// Assembly: Sitecore.Forms.Mvc, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1AD2DE3-BABE-4597-B643-AA577F98B8D6
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Mvc.dll

using Sitecore.WFFM.Abstractions.Data.Enums;

namespace Sitecore.Forms.Mvc.Interfaces
{
  public interface IStyleSettings
  {
    string CssClass { get; set; }

    FormType FormType { get; set; }

    string LeftColumnStyle { get; set; }

    string RightColumnStyle { get; set; }
  }
}
