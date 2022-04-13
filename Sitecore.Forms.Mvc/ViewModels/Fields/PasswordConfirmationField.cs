// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Mvc.ViewModels.Fields.PasswordConfirmationField
// Assembly: Sitecore.Forms.Mvc, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1AD2DE3-BABE-4597-B643-AA577F98B8D6
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Mvc.dll

using Sitecore.Forms.Mvc.Validators;
using System.ComponentModel.DataAnnotations;

namespace Sitecore.Forms.Mvc.ViewModels.Fields
{
  public class PasswordConfirmationField : PasswordField
  {
    public PasswordConfirmationField()
    {
      this.PasswordTitle = "Password";
      this.ConfirmationTitle = nameof (Confirmation);
    }

    [DataType(DataType.Password)]
    [DynamicCompare("Value", "PasswordTitle", "ConfirmationTitle", ErrorMessage = "The {0} and {1} fields must be the same.")]
    public string Confirmation { get; set; }

    public string ConfirmationHelp { get; set; }

    public string PasswordHelp { get; set; }

    public string PasswordTitle { get; set; }

    public string ConfirmationTitle { get; set; }
  }
}
