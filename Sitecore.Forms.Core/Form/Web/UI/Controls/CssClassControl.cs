// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Web.UI.Controls.CssClassControl
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Form.Core.Attributes;
using Sitecore.Form.Core.Visual;
using System.Web.UI;

namespace Sitecore.Form.Web.UI.Controls
{
  public abstract class CssClassControl : BaseControl
  {
    protected CssClassControl(HtmlTextWriterTag tag)
      : base(tag)
    {
    }

    protected CssClassControl()
    {
    }

    [VisualProperty("CSS Class:", 600)]
    [VisualFieldType(typeof (CssClassField))]
    public new string CssClass
    {
      set => base.CssClass = value;
      get => base.CssClass;
    }
  }
}
