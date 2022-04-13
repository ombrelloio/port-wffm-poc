// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.Controls.MarkerLabel
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using System;
using System.Web.UI.WebControls;

namespace Sitecore.Form.Core.Controls
{
  public class MarkerLabel : Label, ICloneable
  {
    public object Clone() => this.MemberwiseClone();
  }
}
