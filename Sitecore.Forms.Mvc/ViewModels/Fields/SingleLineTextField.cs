// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Mvc.ViewModels.Fields.SingleLineTextField
// Assembly: Sitecore.Forms.Mvc, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1AD2DE3-BABE-4597-B643-AA577F98B8D6
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Mvc.dll

using Sitecore.Forms.Mvc.Validators;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Sitecore.Forms.Mvc.ViewModels.Fields
{
  public class SingleLineTextField : ValuedFieldViewModel<string>
  {
    [DefaultValue(256)]
    public int MaxLength { get; set; }

    [DefaultValue(0)]
    public int MinLength { get; set; }

    [DataType(DataType.Text)]
    [DynamicStringLength("MinLength", "MaxLength", ErrorMessage = "The {0} field must be a string with a minimum length of {1} and a maximum length of {2}.")]
    public override string Value { get; set; }

    public override void Initialize()
    {
      if (this.MaxLength != 0)
        return;
      this.MaxLength = 256;
    }
  }
}
