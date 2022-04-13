// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.UI.UserControls.CreditCard
// Assembly: Sitecore.Forms.Custom, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E620A323-75A8-42D2-B304-7093FE781621
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Custom.dll

using Sitecore.Form.Core.Attributes;
using Sitecore.Form.Core.Configuration;
using Sitecore.Form.Core.Controls;
using Sitecore.Form.Core.Utility;
using Sitecore.Form.Core.Visual;
using Sitecore.Form.UI.Adapters;
using Sitecore.Form.UI.Converters;
using Sitecore.Form.Validators;
using Sitecore.Form.Web.UI.Controls;
using Sitecore.WFFM.Abstractions.Actions;
using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sitecore.Form.UI.UserControls
{
  [ValidationProperty("Value")]
  [ListAdapter("CardTypes", typeof (CreditCardListAdapter))]
  [Adapter(typeof (CreditCardControlAdapter))]
  public class CreditCard : ValidateUserControl, IHasTitle
  {
    protected Panel cardTypeBorder;
    protected Sitecore.Form.Web.UI.Controls.Label cardTypeTitle;
    protected Panel creditCardPanel;
    protected DropDownList cardType;
    protected System.Web.UI.WebControls.Label cardTypeHelp;
    protected Panel creditCard;
    protected Sitecore.Form.Web.UI.Controls.Label cardNumberTitle;
    protected Panel cardNumberPanel;
    protected TextBox cardNumber;
    protected System.Web.UI.WebControls.Label cardNumberHelp;

    public CreditCard()
    {
      this.CssClass = "scfCreditCard";
      this.CardTypes = (Sitecore.Form.Web.UI.Controls.ListItemCollection) "source:/sitecore/system/Modules/Web Forms for Marketers/Settings/Meta data/Credit Card Type";
      this.EmptyChoice = "Yes";
    }

    protected override void OnInit(EventArgs e)
    {
      base.OnInit(e);
      this.cardType.Items.Clear();
      foreach (ListItem cardType in this.CardTypes)
      {
        ListItem listItem = new ListItem(cardType.Text, cardType.Value);
        if (this.SelectedValue != null && this.SelectedValue.FindByValue(cardType.Value) != null)
          listItem.Selected = true;
        this.cardType.Items.Add(listItem);
      }
      if (!(this.EmptyChoice == "Yes"))
        return;
      this.cardType.Items.Insert(0, new ListItem());
    }

    protected override void OnLoad(EventArgs e)
    {
      base.OnLoad(e);
      this.cardType.CssClass += string.Join(string.Empty, new string[4]
      {
        " fieldid.",
        this.FieldID,
        "/",
        SubFieldIds.CreditTypeId
      });
      this.cardNumber.CssClass += string.Join(string.Empty, new string[4]
      {
        " fieldid.",
        this.FieldID,
        "/",
        SubFieldIds.CreditNumberId
      });
      MarkerLabel firstOrDefault = (MarkerLabel) WebUtil.FindFirstOrDefault(this.ValidatorContainer, (Func<Control, bool>) (c => c is MarkerLabel));
      if (firstOrDefault == null)
        return;
      MarkerLabel markerLabel = (MarkerLabel) firstOrDefault.Clone();
      markerLabel.ID = firstOrDefault.ID + "typemarker";
      this.cardTypeBorder.Controls.Add((Control) markerLabel);
    }

    public override bool SetValidatorProperties(BaseValidator validator)
    {
      base.SetValidatorProperties(validator);
      RequiredWithMarkerValidator withMarkerValidator = (RequiredWithMarkerValidator) null;
      if (validator is RequiredWithMarkerValidator)
        withMarkerValidator = (RequiredWithMarkerValidator) ((RequiredWithMarkerValidator) validator).Clone();
      validator.ErrorMessage = Sitecore.StringExtensions.StringExtensions.FormatWith(validator.ErrorMessage, new object[1]
      {
        (object) this.CardNumberTitle
      });
      validator.Text = Sitecore.StringExtensions.StringExtensions.FormatWith(validator.Text, new object[1]
      {
        (object) this.CardNumberTitle
      });
      validator.ToolTip = Sitecore.StringExtensions.StringExtensions.FormatWith(validator.ToolTip, new object[1]
      {
        (object) this.CardNumberTitle
      });
      validator.CssClass += string.Join(string.Empty, new string[2]
      {
        " cardTypeControlId.",
        this.cardType.ID
      });
      if (withMarkerValidator != null)
      {
        withMarkerValidator.ErrorMessage = Sitecore.StringExtensions.StringExtensions.FormatWith(withMarkerValidator.ErrorMessage, new object[1]
        {
          (object) this.CardTypeTitle
        });
        withMarkerValidator.Text = Sitecore.StringExtensions.StringExtensions.FormatWith(withMarkerValidator.Text, new object[1]
        {
          (object) this.CardTypeTitle
        });
        withMarkerValidator.ToolTip = Sitecore.StringExtensions.StringExtensions.FormatWith(withMarkerValidator.ToolTip, new object[1]
        {
          (object) this.CardTypeTitle
        });
        withMarkerValidator.ID += "type";
        withMarkerValidator.ControlToValidate = this.cardType.ID;
        if (validator.Attributes["inner"] == "1")
          this.creditCardPanel.Controls.Add((Control) withMarkerValidator);
        else
          this.cardTypeBorder.Controls.Add((Control) withMarkerValidator);
      }
      return true;
    }

    protected override void OnPreRender(EventArgs e)
    {
      this.cardNumber.Text = string.Empty;
      base.OnPreRender(e);
    }

    public override string ID
    {
      get => this.cardNumber.ID;
      set
      {
        this.cardNumber.ID = value;
        this.cardNumberTitle.AssociatedControlID = value;
        base.ID = value + "border";
      }
    }

    [VisualProperty("Card Number Help:", 550)]
    [VisualCategory("Appearance")]
    [VisualFieldType(typeof (TextAreaField))]
    [Localize]
    public string CardNumberHelp
    {
      get => this.cardNumberHelp.Text;
      set
      {
        this.cardNumberHelp.Text = value;
        if (!string.IsNullOrEmpty(this.cardNumberHelp.Text))
          this.cardNumberHelp.Style.Remove("display");
        else
          this.cardNumberHelp.Style["display"] = "none";
      }
    }

    [VisualProperty("Card Type Help:", 250)]
    [VisualCategory("Appearance")]
    [VisualFieldType(typeof (TextAreaField))]
    [Localize]
    public string CardTypeHelp
    {
      get => this.cardTypeHelp.Text;
      set
      {
        this.cardTypeHelp.Text = value;
        if (!string.IsNullOrEmpty(this.cardTypeHelp.Text))
          this.cardTypeHelp.Style.Remove("display");
        else
          this.cardTypeHelp.Style["display"] = "none";
      }
    }

    [VisualProperty("Card Number Title:", 500)]
    [VisualCategory("Appearance")]
    [VisualFieldType(typeof (EditField))]
    [Localize]
    public string CardNumberTitle
    {
      get => this.cardNumberTitle.Text;
      set => this.cardNumberTitle.Text = value;
    }

    [VisualProperty("Card Type Title:", 200)]
    [VisualCategory("Appearance")]
    [VisualFieldType(typeof (EditField))]
    [Localize]
    public string CardTypeTitle
    {
      get => this.cardTypeTitle.Text;
      set => this.cardTypeTitle.Text = value;
    }

    [VisualProperty("Card Types:", 100)]
    [DefaultValue("source:/sitecore/system/Modules/Web Forms for Marketers/Settings/Meta data/Credit Card Type")]
    [VisualCategory("Credit Card Types")]
    [VisualFieldType(typeof (ListField))]
    [TypeConverter(typeof (ListItemCollectionConverter))]
    [PersistenceMode(PersistenceMode.InnerProperty)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Browsable(false)]
    public Sitecore.Form.Web.UI.Controls.ListItemCollection CardTypes { get; set; }

    [TypeConverter(typeof (ListItemCollectionConverter))]
    [Browsable(false)]
    [VisualProperty("Selected Value:", 150)]
    [VisualCategory("Credit Card Types")]
    [VisualFieldType(typeof (SelectedValueField))]
    public Sitecore.Form.Web.UI.Controls.ListItemCollection SelectedValue { get; set; }

    [VisualProperty("Empty Choice:", 160)]
    [DefaultValue("Yes")]
    [VisualCategory("Credit Card Types")]
    [VisualFieldType(typeof (EmptyChoiceField))]
    public string EmptyChoice { get; set; }

    public override ControlResult Result
    {
      get
      {
        if (string.IsNullOrEmpty(this.cardType.SelectedValue) && string.IsNullOrEmpty(this.cardNumber.Text))
          return new ControlResult(this.ControlName, (object) string.Empty, string.Join(string.Empty, new string[4]
          {
            "secure:",
            !string.IsNullOrEmpty(this.cardType.Text) ? this.cardType.Text : "[none]",
            ":",
            Sitecore.StringExtensions.StringExtensions.FormatWith("<schidden>{0}</schidden>", new object[1]
            {
              (object) this.cardNumber.Text
            })
          }))
          {
            Secure = true
          };
        return new ControlResult(this.ControlName, (object) string.Join(": ", new string[2]
        {
          this.cardType.SelectedValue,
          this.cardNumber.Text
        }), string.Join(string.Empty, new string[4]
        {
          "secure:",
          this.cardType.Text,
          ":",
          Sitecore.StringExtensions.StringExtensions.FormatWith("<schidden>{0}</schidden>", new object[1]
          {
            (object) this.cardNumber.Text
          })
        }))
        {
          Secure = true
        };
      }
      set
      {
      }
    }

    protected override Control ValidatorContainer => (Control) this.creditCard;

    protected override Control InnerValidatorContainer => (Control) this.cardNumberPanel;

    [DefaultValue("scfCreditCard")]
    [VisualProperty("CSS Class:", 600)]
    [VisualFieldType(typeof (CssClassField))]
    public new string CssClass
    {
      get => this.Attributes["class"];
      set => this.Attributes["class"] = value;
    }

    public string Title
    {
      get
      {
        if (string.IsNullOrEmpty(this.CardTypeTitle) || string.IsNullOrEmpty(this.CardNumberTitle))
          return string.Empty;
        return string.Join("-", new string[2]
        {
          this.CardTypeTitle,
          this.CardNumberTitle
        });
      }
      set
      {
      }
    }
  }
}
