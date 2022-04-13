// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.Data.ValidationItem
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Data.Items;
using Sitecore.Form.Core.Configuration;
using Sitecore.WFFM.Abstractions.Data;
using Sitecore.WFFM.Abstractions.Dependencies;
using System;
using System.Runtime.CompilerServices;
using System.Web.UI.WebControls;

namespace Sitecore.Form.Core.Data
{
  public class ValidationItem : CustomItem, IValidationItem
  {
    public ValidationItem(Item innerItem)
      : base(innerItem)
    {
    }

    public string Assembly => ((BaseItem) ((CustomItemBase) this).InnerItem).Fields[Sitecore.Form.Core.Configuration.FieldIDs.FieldTypeAssemblyID].Value;

    public string Class => ((BaseItem) ((CustomItemBase) this).InnerItem).Fields[Sitecore.Form.Core.Configuration.FieldIDs.FieldTypeClassID].Value;

    public ValidatorDisplay Display
    {
      get
      {
        try
        {
          string str = ((BaseItem) ((CustomItemBase) this).InnerItem)[Sitecore.Form.Core.Configuration.FieldIDs.Validator.Display];
          if (!string.IsNullOrEmpty(str))
            return (ValidatorDisplay) Enum.Parse(typeof (ValidatorDisplay), str);
        }
        catch (Exception ex)
        {
          DependenciesManager.Logger.Warn(string.Join(string.Empty, new string[4]
          {
            "Invalid value. Item: ",
            ((object) ((CustomItemBase) this).InnerItem.ID).ToString(),
            " Field ID: ",
            ((object) Sitecore.Form.Core.Configuration.FieldIDs.Validator.Display).ToString()
          }), ex, (object) this);
        }
        return ValidatorDisplay.Dynamic;
      }
    }

    public bool EnableClientScript => MainUtil.GetBool(((BaseItem) ((CustomItemBase) this).InnerItem).Fields[Sitecore.Form.Core.Configuration.FieldIDs.Validator.EnableClientScript].Value, false);

    public string ErrorMessage => ((BaseItem) ((CustomItemBase) this).InnerItem).Fields[Sitecore.Form.Core.Configuration.FieldIDs.Validator.ErrorMessage].Value;

    public string GlobalParameters => string.Join(string.Empty, new string[2]
    {
      this.Params,
      this.LocalizedParameters
    });

    public bool IsInnerControl => MainUtil.GetBool(((BaseItem) ((CustomItemBase) this).InnerItem).Fields[Sitecore.Form.Core.Configuration.FieldIDs.Validator.IsInnerControl].Value, false);

    public string LocalizedParameters => ((BaseItem) ((CustomItemBase) this).InnerItem).Fields[Sitecore.Form.Core.Configuration.FieldIDs.Validator.LocalizedParameters].Value;

    public string Params => ((BaseItem) ((CustomItemBase) this).InnerItem).Fields[Sitecore.Form.Core.Configuration.FieldIDs.Validator.Parameters].Value;

    public string Text => ((BaseItem) ((CustomItemBase) this).InnerItem).Fields[Sitecore.Form.Core.Configuration.FieldIDs.Validator.Text].Value;

    public string TrackEvent => ((BaseItem) ((CustomItemBase) this).InnerItem).Fields[Sitecore.Form.Core.Configuration.FieldIDs.Validator.AnalyticsEvent].Value;

    public string ValidationExpression => ((BaseItem) ((CustomItemBase) this).InnerItem).Fields[Sitecore.Form.Core.Configuration.FieldIDs.Validator.ValidationExpression] != null ? ((BaseItem) ((CustomItemBase) this).InnerItem).Fields[Sitecore.Form.Core.Configuration.FieldIDs.Validator.ValidationExpression].Value : (string) null;

    [SpecialName]
    Item IValidationItem.InnerItem => ((CustomItemBase) this).InnerItem;
  }
}
