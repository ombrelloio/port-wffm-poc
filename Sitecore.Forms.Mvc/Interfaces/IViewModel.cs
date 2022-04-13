// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Mvc.Interfaces.IViewModel
// Assembly: Sitecore.Forms.Mvc, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1AD2DE3-BABE-4597-B643-AA577F98B8D6
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Mvc.dll

using System.Collections.Generic;

namespace Sitecore.Forms.Mvc.Interfaces
{
  public interface IViewModel
  {
    string Information { get; set; }

    Dictionary<string, string> Parameters { get; set; }

    bool ShowInformation { get; set; }

    bool ShowTitle { get; set; }

    string Title { get; set; }

    string Name { get; set; }

    bool Visible { get; set; }
  }
}
