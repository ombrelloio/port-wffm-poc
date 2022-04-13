// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Mvc.Validators.Rules.ModelClientValidationCheckedRule
// Assembly: Sitecore.Forms.Mvc, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1AD2DE3-BABE-4597-B643-AA577F98B8D6
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Mvc.dll

using System.Web.Mvc;

namespace Sitecore.Forms.Mvc.Validators.Rules
{
  public class ModelClientValidationCheckedRule : ModelClientValidationRule
  {
    public ModelClientValidationCheckedRule(string errorMessage)
    {
      this.ErrorMessage = errorMessage;
      this.ValidationType = "ischecked";
    }
  }
}
