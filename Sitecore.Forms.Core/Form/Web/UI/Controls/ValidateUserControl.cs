// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Web.UI.Controls.ValidateUserControl
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Form.Core.Controls;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sitecore.Form.Web.UI.Controls
{
  public abstract class ValidateUserControl : BaseUserControl
  {
    protected ValidatorCollection GetValidators()
    {
      List<BaseValidator> source = new List<BaseValidator>();
      source.AddRange((IEnumerable<BaseValidator>) ValidateUserControl.GetValidators(this.InnerValidatorContainer));
      source.AddRange((IEnumerable<BaseValidator>) ValidateUserControl.GetValidators(this.ValidatorContainer));
      ValidatorCollection validatorCollection = new ValidatorCollection();
      validatorCollection.AddRange(source.Distinct<BaseValidator>().Cast<IValidator>().ToArray<IValidator>());
      return validatorCollection;
    }

    private static BaseValidator[] GetValidators(Control parent)
    {
      List<BaseValidator> baseValidatorList = new List<BaseValidator>();
      if (parent != null)
      {
        foreach (object control in parent.Controls)
        {
          if (control is BaseValidator)
            baseValidatorList.Add((BaseValidator) control);
        }
      }
      return baseValidatorList.ToArray();
    }

    private List<MarkerLabel> GetRequredMarker(Control parent)
    {
      List<MarkerLabel> markerLabelList = new List<MarkerLabel>();
      if (parent != null)
      {
        foreach (object control in parent.Controls)
        {
          if (control is MarkerLabel)
            markerLabelList.Add((MarkerLabel) control);
        }
      }
      return markerLabelList;
    }

    protected ValidatorCollection SetValidators(ValidatorCollection collection)
    {
      Control validatorContainer1 = this.InnerValidatorContainer;
      Control validatorContainer2 = this.ValidatorContainer;
      foreach (BaseValidator baseValidator in collection)
      {
        if (baseValidator.CssClass.Contains(" inner.1"))
          validatorContainer1?.Controls.Add((Control) baseValidator);
        else
          validatorContainer2?.Controls.Add((Control) baseValidator);
      }
      return collection;
    }

    protected void SetRequredMarker(List<MarkerLabel> collection)
    {
      Control validatorContainer = this.ValidatorContainer;
      foreach (MarkerLabel markerLabel in collection)
        validatorContainer?.Controls.Add((Control) markerLabel);
    }

    protected internal void AddInnerValidator(Control validator)
    {
      Control validatorContainer = this.InnerValidatorContainer;
      if (validatorContainer == null)
        return;
      if (validator is BaseValidator)
        ((BaseValidator) validator).CssClass += " inner.1";
      validatorContainer.Controls.Add(validator);
    }

    protected internal void AddValidator(Control validator) => this.ValidatorContainer?.Controls.Add(validator);

    [Description("Collection of validators.")]
    [PersistenceMode(PersistenceMode.InnerProperty)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Browsable(false)]
    public ValidatorCollection Validators
    {
      get => this.GetValidators();
      set => this.SetValidators(value);
    }

    [Description("Required Marker")]
    [PersistenceMode(PersistenceMode.InnerProperty)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Browsable(false)]
    public List<MarkerLabel> Requred
    {
      get => this.GetRequredMarker(this.ValidatorContainer);
      set => this.SetRequredMarker(value);
    }

    protected abstract Control ValidatorContainer { get; }

    protected abstract Control InnerValidatorContainer { get; }
  }
}
