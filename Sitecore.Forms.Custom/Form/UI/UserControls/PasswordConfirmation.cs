// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.UI.UserControls.PasswordConfirmation
// Assembly: Sitecore.Forms.Custom, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E620A323-75A8-42D2-B304-7093FE781621
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Custom.dll

using Sitecore.Form.Core.Attributes;
using Sitecore.Form.Core.Configuration;
using Sitecore.Form.Core.Controls;
using Sitecore.Form.Core.Utility;
using Sitecore.Form.Core.Visual;
using Sitecore.Form.Web.UI.Controls;
using Sitecore.WFFM.Abstractions.Actions;
using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sitecore.Form.UI.UserControls
{
  public class PasswordConfirmation : ValidateUserControl, IHasTitle
  {
    protected Panel passwordBorder;
    protected Sitecore.Form.Web.UI.Controls.Label passwordTitle;
    protected Panel passwordPanel;
    protected TextBox password;
    protected System.Web.UI.WebControls.Label passwordHelp;
    protected Panel confirmationBorder;
    protected Sitecore.Form.Web.UI.Controls.Label confirmationTitle;
    protected Panel confirmationPanel;
    protected TextBox confirmation;
    protected System.Web.UI.WebControls.Label confimationHelp;

    public PasswordConfirmation() => this.CssClass = "scfPasswordConfirmation";

    public override bool SetValidatorProperties(BaseValidator validator)
    {
      base.SetValidatorProperties(validator);
      object obj = (object) null;
      if (validator is ICloneable)
        obj = ((ICloneable) validator).Clone();
      validator.ErrorMessage = Sitecore.StringExtensions.StringExtensions.FormatWith(validator.ErrorMessage, new object[4]
      {
        (object) this.PasswordTitle,
        (object) this.MinLength,
        (object) this.MaxLength,
        (object) this.ConfirmationTitle
      });
      validator.Text = Sitecore.StringExtensions.StringExtensions.FormatWith(validator.Text, new object[4]
      {
        (object) this.PasswordTitle,
        (object) this.MinLength,
        (object) this.MaxLength,
        (object) this.ConfirmationTitle
      });
      validator.ToolTip = Sitecore.StringExtensions.StringExtensions.FormatWith(validator.ToolTip, new object[4]
      {
        (object) this.PasswordTitle,
        (object) this.MinLength,
        (object) this.MaxLength,
        (object) this.ConfirmationTitle
      });
      validator.CssClass += string.Join(string.Empty, new string[2]
      {
        " confirmationControlId.",
        this.confirmation.ID
      });
      if (obj != null && obj is BaseValidator)
      {
        BaseValidator baseValidator = (BaseValidator) obj;
        baseValidator.ErrorMessage = Sitecore.StringExtensions.StringExtensions.FormatWith(baseValidator.ErrorMessage, new object[4]
        {
          (object) this.ConfirmationTitle,
          (object) this.MinLength,
          (object) this.MaxLength,
          (object) this.PasswordTitle
        });
        baseValidator.Text = Sitecore.StringExtensions.StringExtensions.FormatWith(baseValidator.Text, new object[4]
        {
          (object) this.ConfirmationTitle,
          (object) this.MinLength,
          (object) this.MaxLength,
          (object) this.PasswordTitle
        });
        baseValidator.ToolTip = Sitecore.StringExtensions.StringExtensions.FormatWith(baseValidator.ToolTip, new object[4]
        {
          (object) this.ConfirmationTitle,
          (object) this.MinLength,
          (object) this.MaxLength,
          (object) this.PasswordTitle
        });
        baseValidator.ID += "confirmation";
        baseValidator.ControlToValidate = this.confirmation.ID;
        if (validator.Attributes["inner"] == "1")
          this.confirmationPanel.Controls.Add((Control) baseValidator);
        else
          this.confirmationBorder.Controls.Add((Control) baseValidator);
      }
      return true;
    }

    protected override void OnLoad(EventArgs e)
    {
      base.OnLoad(e);
      string str = string.Join(string.Empty, new string[4]
      {
        " fieldid.",
        this.FieldID,
        "/",
        SubFieldIds.ConfirmationId
      });
      this.password.CssClass += string.Join(string.Empty, new string[4]
      {
        " fieldid.",
        this.FieldID,
        "/",
        SubFieldIds.PasswordId
      });
      this.confirmation.CssClass += str;
      MarkerLabel firstOrDefault = (MarkerLabel) WebUtil.FindFirstOrDefault(this.ValidatorContainer, (Func<Control, bool>) (c => c is MarkerLabel));
      if (firstOrDefault == null)
        return;
      MarkerLabel markerLabel = (MarkerLabel) firstOrDefault.Clone();
      markerLabel.ID = firstOrDefault.ID + "confirmationmarker";
      this.confirmationBorder.Controls.Add((Control) markerLabel);
    }

    public override string ID
    {
      get => this.password.ID;
      set
      {
        this.password.ID = value;
        this.passwordTitle.AssociatedControlID = value;
        base.ID = value + "border";
      }
    }

    [VisualProperty("Password Help:", 200)]
    [VisualCategory("Appearance")]
    [VisualFieldType(typeof (TextAreaField))]
    [Localize]
    public string PasswordHelp
    {
      get => this.passwordHelp.Text;
      set
      {
        this.passwordHelp.Text = value;
        if (!string.IsNullOrEmpty(this.passwordHelp.Text))
          this.passwordHelp.Style.Remove("display");
        else
          this.passwordHelp.Style["display"] = "none";
      }
    }

    [VisualProperty("Confirm Password Help:", 500)]
    [VisualCategory("Appearance")]
    [VisualFieldType(typeof (TextAreaField))]
    [Localize]
    public string ConfirmationHelp
    {
      get => this.confimationHelp.Text;
      set
      {
        this.confimationHelp.Text = value;
        if (!string.IsNullOrEmpty(this.confimationHelp.Text))
          this.confimationHelp.Style.Remove("display");
        else
          this.confimationHelp.Style["display"] = "none";
      }
    }

    [DefaultValue("scfPasswordConfirmation")]
    [VisualProperty("CSS Class:", 600)]
    [VisualFieldType(typeof (CssClassField))]
    public new string CssClass
    {
      get => this.Attributes["class"];
      set => this.Attributes["class"] = value;
    }

    [DefaultValue("Password")]
    [VisualProperty("Password Title:", 190)]
    [VisualFieldType(typeof (EditField))]
    [Localize]
    public string PasswordTitle
    {
      get => this.passwordTitle.Text;
      set => this.passwordTitle.Text = value;
    }

    [DefaultValue("Confirm Password")]
    [VisualProperty("Confirm Password Title:", 300)]
    [VisualFieldType(typeof (EditField))]
    [Localize]
    public string ConfirmationTitle
    {
      get => this.confirmationTitle.Text;
      set => this.confirmationTitle.Text = value;
    }

    [VisualFieldType(typeof (SelectPredefinedValidatorField))]
    [VisualProperty("Validation:", 790)]
    [DefaultValue("")]
    [VisualCategory("Validation")]
    public string PredefinedValidator { set; get; }

    [VisualFieldType(typeof (RegexField))]
    [VisualProperty("Regular Expression:", 800)]
    [DefaultValue("")]
    [VisualCategory("Validation")]
    public string RegexPattern { set; get; }

    [VisualProperty("Maximum Length:", 2000)]
    [DefaultValue(256)]
    [VisualCategory("Validation")]
    public int MaxLength
    {
      get => this.password.MaxLength;
      set
      {
        this.password.MaxLength = value;
        this.confirmation.MaxLength = value;
      }
    }

    [VisualProperty("Minimum Length:", 1000)]
    [DefaultValue(0)]
    [VisualCategory("Validation")]
    public int MinLength { get; set; }

    public override ControlResult Result
    {
      get => new ControlResult(this.ControlName, (object) this.password.Text, Sitecore.StringExtensions.StringExtensions.FormatWith("secure:<schidden>{0}</schidden>", new object[1]
      {
        (object) this.password.Text
      }))
      {
        Secure = true
      };
      set => this.password.Text = value.Value.ToString();
    }

    protected override Control ValidatorContainer => (Control) this.passwordBorder;

    protected override Control InnerValidatorContainer => (Control) this.passwordPanel;

    public string Title
    {
      get
      {
        if (string.IsNullOrEmpty(this.PasswordTitle) || string.IsNullOrEmpty(this.ConfirmationTitle))
          return string.Empty;
        return string.Join("-", new string[2]
        {
          this.PasswordTitle,
          this.ConfirmationTitle
        });
      }
      set
      {
      }
    }
  }
}
