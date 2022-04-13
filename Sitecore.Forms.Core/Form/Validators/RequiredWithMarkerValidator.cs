// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Validators.RequiredWithMarkerValidator
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using System;
using System.Web.UI.WebControls;

namespace Sitecore.Form.Validators
{
  public class RequiredWithMarkerValidator : RequiredFieldValidator, ICloneable
  {
    public string Marker
    {
      get => this.Attributes["marker"];
      set => this.Attributes["marker"] = value;
    }

    public virtual object Clone()
    {
      RequiredWithMarkerValidator withMarkerValidator = new RequiredWithMarkerValidator();
      this.CopyBaseAttributes((WebControl) withMarkerValidator);
      withMarkerValidator.ErrorMessage = this.ErrorMessage;
      withMarkerValidator.Text = this.Text;
      withMarkerValidator.Marker = this.Marker;
      withMarkerValidator.ToolTip = this.ToolTip;
      withMarkerValidator.ValidationGroup = this.ValidationGroup;
      withMarkerValidator.Display = this.Display;
      withMarkerValidator.ID = this.ID;
      withMarkerValidator.ControlToValidate = this.ControlToValidate;
      withMarkerValidator.CssClass = this.CssClass;
      withMarkerValidator.InitialValue = this.InitialValue;
      withMarkerValidator.EnableClientScript = this.EnableClientScript;
      withMarkerValidator.Enabled = this.Enabled;
      withMarkerValidator.ForeColor = this.ForeColor;
      withMarkerValidator.SetFocusOnError = this.SetFocusOnError;
      return (object) withMarkerValidator;
    }
  }
}
