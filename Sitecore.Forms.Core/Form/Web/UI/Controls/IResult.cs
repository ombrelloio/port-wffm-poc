// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Web.UI.Controls.IResult
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.WFFM.Abstractions.Actions;

namespace Sitecore.Form.Web.UI.Controls
{
  public interface IResult
  {
    ControlResult Result { get; set; }

    string ControlName { get; set; }

    string FieldID { get; set; }

    string DefaultValue { set; }
  }
}
