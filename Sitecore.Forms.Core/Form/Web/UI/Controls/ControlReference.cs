// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Web.UI.Controls.ControlReference
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Data.Items;
using Sitecore.Form.Core.Attributes;
using Sitecore.Form.Core.Configuration;
using Sitecore.Form.Core.Controls;
using Sitecore.Form.Core.Data;
using Sitecore.Forms.Core.Data;
using System;
using System.Text.RegularExpressions;
using System.Web.UI;

namespace Sitecore.Form.Web.UI.Controls
{
  [Dummy]
  [PersistChildren(true)]
  public class ControlReference : Control
  {
    private string locParameters;
    private string parameters;
    private FieldReference reference;
    private FieldTypeItem typeItem;

    public ControlReference() => this.ReadQueryString = false;

    public ControlReference(FieldItem item)
      : this()
    {
      this.FieldItem = item;
    }

    protected override void OnInit(EventArgs e)
    {
      if (this.Reference == null)
        return;
      this.Controls.Add((Control) this.Reference);
      if (this.IsFastPreview)
        return;
      this.AddRequired((FieldTypeItem) this.FieldItem ?? this.TypeItem);
      this.AddControlValidator((FieldTypeItem) this.FieldItem ?? this.TypeItem);
    }

    private void AddControlValidator(FieldTypeItem field)
    {
      string[] validators = field.Validators;
      if (validators.Length == 0)
        return;
      foreach (string str in validators)
      {
        Item innerItem = ((CustomItemBase) field).Database.GetItem(str);
        if (innerItem != null)
        {
          ValidationItem validationItem = new ValidationItem(innerItem);
          ValidatorReference validatorReference = new ValidatorReference(validationItem, field);
          validatorReference.ControlToValidate = this.Reference.InnerControl;
          validatorReference.ValidationGroup = this.ValidationGroup;
          validatorReference.CssClass = "scfValidator";
          validatorReference.Parameters = this.Parameters;
          validatorReference.LocParameters = this.LocParameters;
          ValidatorReference validator = validatorReference;
          if (this.reference.InnerControl is IHasTitle)
            validator.ControlTitle = ((IHasTitle) this.reference.InnerControl).Title;
          this.AddValidator(validator, validationItem.IsInnerControl);
        }
      }
    }

    private void AddRequired(FieldTypeItem field)
    {
      if (!field.IsRequired)
        return;
      ValidationItem validationItem = new ValidationItem(((CustomItemBase) field).Database.GetItem(IDs.NotEmptyValidatorID));
      ValidatorReference validatorReference = new ValidatorReference(validationItem, field);
      validatorReference.ControlToValidate = this.Reference.InnerControl;
      validatorReference.ValidationGroup = this.ValidationGroup;
      validatorReference.CssClass = "scfValidatorRequired";
      validatorReference.Parameters = this.Parameters;
      validatorReference.LocParameters = this.LocParameters;
      ValidatorReference validator = validatorReference;
      if (this.reference.InnerControl is IHasTitle)
        validator.ControlTitle = ((IHasTitle) this.reference.InnerControl).Title;
      string text = validationItem.Text;
      if (string.IsNullOrEmpty(text) && !string.IsNullOrEmpty(validationItem.Params))
      {
        Match match = Regex.Match(validationItem.Params, "<Text>(?<value>[^<>]*)</Text>", RegexOptions.IgnoreCase);
        if (match.Success)
          text = match.Groups["value"].Value;
      }
      this.AddRequiredMarker(validator, text);
      this.AddValidator(validator, validationItem.IsInnerControl);
    }

    private void AddRequiredMarker(ValidatorReference validator, string text)
    {
      MarkerLabel markerLabel1 = new MarkerLabel();
      markerLabel1.CssClass = "scfRequired";
      markerLabel1.Text = text;
      markerLabel1.ToolTip = validator.ToolTip;
      MarkerLabel markerLabel2 = markerLabel1;
      if (this.Reference.InnerControl is ValidateControl)
        ((ValidateControl) this.Reference.InnerControl).AddValidator((Control) markerLabel2);
      if (this.Reference.InnerControl is ValidateUserControl)
        ((ValidateUserControl) this.Reference.InnerControl).AddValidator((Control) markerLabel2);
      if (!(this.Reference.InnerControl is BaseUserControl))
        return;
      ((BaseUserControl) this.Reference.InnerControl).IsRequired = true;
    }

    private void AddValidator(ValidatorReference validator, bool isInner)
    {
      if (this.Reference.InnerControl is ValidateControl)
      {
        if (isInner)
          ((ValidateControl) this.Reference.InnerControl).AddInnerValidator((Control) validator.InnerValidator);
        else
          ((ValidateControl) this.Reference.InnerControl).AddValidator((Control) validator.InnerValidator);
      }
      else if (this.Reference.InnerControl is ValidateUserControl)
      {
        if (isInner)
          ((ValidateUserControl) this.Reference.InnerControl).AddInnerValidator((Control) validator.InnerValidator);
        else
          ((ValidateUserControl) this.Reference.InnerControl).AddValidator((Control) validator.InnerValidator);
      }
      else if (this.Reference.InnerControl.Controls.Count > 0)
        this.Reference.InnerControl.Controls.Add((Control) validator.InnerValidator);
      else
        this.Controls.AddAt(this.Controls.IndexOf(this.Reference.InnerControl), (Control) validator.InnerValidator);
    }

    public string Description { get; set; }

    public string FieldID
    {
      get => ((object) ((CustomItemBase) this.FieldItem).ID).ToString();
      set => this.FieldItem = new FieldItem(Sitecore.Context.Database.GetItem(value));
    }

    public FieldItem FieldItem { get; private set; }

    public Control InnerControl => this.Reference.InnerControl;

    public bool IsFastPreview { get; set; }

    public string LocParameters
    {
      get => this.FieldItem == null ? this.locParameters : this.FieldItem.LocalizedParameters;
      set => this.locParameters = value;
    }

    public string Parameters
    {
      get => this.FieldItem == null ? this.parameters : this.FieldItem.Parameters;
      set => this.parameters = value;
    }

    public bool DisableWebEditing { get; set; }

    public bool ReadQueryString { get; set; }

    public FieldTypeItem TypeItem
    {
      get
      {
        if (this.typeItem == null && !string.IsNullOrEmpty(this.TypeReference))
          this.typeItem = new FieldTypeItem(StaticSettings.ContextDatabase.GetItem(this.TypeReference));
        return this.typeItem;
      }
    }

    public string TypeReference { get; set; }

    public string ValidationGroup { get; set; }

    public string RenderingParameters { get; set; }

    private FieldReference Reference
    {
      get
      {
        if (this.reference == null)
        {
          this.reference = new FieldReference((FieldTypeItem) this.FieldItem ?? this.TypeItem, this.Page)
          {
            ReadQueryString = this.ReadQueryString,
            DisableWebEditing = this.DisableWebEditing,
            RenderingParameters = this.RenderingParameters
          };
          if (!this.reference.IsHasTitle)
          {
            this.reference = (FieldReference) null;
            return (FieldReference) null;
          }
          if (this.reference.InnerControl is BaseUserControl)
            ((BaseUserControl) this.reference.InnerControl).ValidationGroup = this.ValidationGroup;
        }
        return this.reference;
      }
    }
  }
}
