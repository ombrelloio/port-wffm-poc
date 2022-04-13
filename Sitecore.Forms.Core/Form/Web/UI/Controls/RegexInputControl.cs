// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Web.UI.Controls.RegexInputControl
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Form.Core.Attributes;
using Sitecore.Form.Core.Visual;
using System.ComponentModel;
using System.Web.UI;

namespace Sitecore.Form.Web.UI.Controls
{
  public abstract class RegexInputControl : InputControl
  {
    protected RegexInputControl()
    {
    }

    protected RegexInputControl(HtmlTextWriterTag tag)
      : base(tag)
    {
    }

    [VisualFieldType(typeof (SelectPredefinedValidatorField))]
    [VisualProperty("Validation:", 790)]
    [DefaultValue("")]
    [VisualCategory("Validation")]
    public string PredefinedValidator
    {
      get => this.classAtributes["predefinedValidator"];
      set => this.classAtributes["predefinedValidator"] = value;
    }

    [VisualFieldType(typeof (TextAreaField))]
    [Localize]
    [VisualProperty("Error Message:", 810)]
    [SitecoreDefaultValue("{070FCA14-1E9A-45D7-8611-EA650F20FE77}", "{2D63E327-79CC-479D-950E-DA4E1F7A7C82}")]
    [VisualCategory("Validation")]
    public string PredefinedValidatorTextMessage { get; set; }

    [VisualFieldType(typeof (RegexField))]
    [VisualProperty("Regular Expression:", 800)]
    [DefaultValue("")]
    [VisualCategory("Validation")]
    public string RegexPattern
    {
      get => this.classAtributes["regex"];
      set => this.classAtributes["regex"] = value;
    }
  }
}
