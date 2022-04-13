// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Mvc.ViewModels.ValuedFieldViewModel`1
// Assembly: Sitecore.Forms.Mvc, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1AD2DE3-BABE-4597-B643-AA577F98B8D6
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Mvc.dll

using Sitecore.Forms.Mvc.Attributes;
using Sitecore.Forms.Mvc.Controllers.ModelBinders.FieldBinders;
using Sitecore.Forms.Mvc.Interfaces;
using Sitecore.Forms.Mvc.Validators;
using Sitecore.WFFM.Abstractions.Actions;

namespace Sitecore.Forms.Mvc.ViewModels
{
  public class ValuedFieldViewModel<TValue> : FieldViewModel, IFieldResult, IContainerMetadata
  {
    [PropertyBinder(typeof (DefaultFieldValueBinder))]
    [ParameterName("Text")]
    [DynamicRequired(ErrorMessage = "The {0} field is required.")]
    [MultiRegularExpression(null, "RegexPattern", ErrorMessage = "The value of the {0} field is not valid.")]
    public virtual TValue Value { get; set; }

    public virtual string ResultParameters { get; set; }

    public virtual ControlResult GetResult() => new ControlResult(this.FieldItemId, this.Name, (object) this.Value, this.ResultParameters);

    public virtual void SetValueFromQuery(string valueFromQuery)
    {
            if (typeof(TValue) == typeof(string))
            {
                Value = (TValue)(object)valueFromQuery;
            }
        }
  }
}
