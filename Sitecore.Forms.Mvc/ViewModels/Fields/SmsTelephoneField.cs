// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Mvc.ViewModels.Fields.SmsTelephoneField
// Assembly: Sitecore.Forms.Mvc, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1AD2DE3-BABE-4597-B643-AA577F98B8D6
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Mvc.dll

using Sitecore.Forms.Mvc.Validators;

namespace Sitecore.Forms.Mvc.ViewModels.Fields
{
  public class SmsTelephoneField : TelephoneField
  {
    [DynamicRegularExpression("^\\+?\\d{3,}", null, ErrorMessage = "The {0} field contains an invalid sms/mms telephone number.")]
    public override string Value { get; set; }
  }
}
