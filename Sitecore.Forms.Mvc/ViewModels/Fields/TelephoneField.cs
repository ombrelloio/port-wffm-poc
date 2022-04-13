// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Mvc.ViewModels.Fields.TelephoneField
// Assembly: Sitecore.Forms.Mvc, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1AD2DE3-BABE-4597-B643-AA577F98B8D6
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Mvc.dll

using Sitecore.Forms.Mvc.Validators;
using Sitecore.WFFM.Abstractions.Actions;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Sitecore.Forms.Mvc.ViewModels.Fields
{
  public class TelephoneField : SingleLineTextField
  {
    [DataType(DataType.PhoneNumber)]
    [DynamicRegularExpression("^\\+?\\s{0,}\\d{0,}\\s{0,}(\\(\\s{0,}\\d{1,}\\s{0,}\\)\\s{0,}|\\d{0,}\\s{0,}-?\\s{0,})\\d{2,}\\s{0,}-?\\s{0,}\\d{2,}\\s{0,}(-?\\s{0,}\\d{2,}|\\s{0,})\\s{0,}$", null, ErrorMessage = "The {0} field contains an invalid telephone number.")]
    public override string Value { get; set; }

    public override ControlResult GetResult()
    {
      if (this.Value == null)
        return (ControlResult) null;
      string str1 = new string(this.Value.Where<char>(new Func<char, bool>(char.IsDigit)).ToArray<char>());
      string fieldItemId = this.FieldItemId;
      string title = this.Title;
      string str2 = str1;
      string parameters;
      if (!string.IsNullOrEmpty(this.Value))
        parameters = string.Join(string.Empty, new string[3]
        {
          "<scfriendly>",
          this.Value,
          "</scfriendly>"
        });
      else
        parameters = (string) null;
      return new ControlResult(fieldItemId, title, (object) str2, parameters);
    }
  }
}
