// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.Client.Validators.PasswordConfimationValidator
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Form.Core.Validators;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sitecore.Form.Core.Client.Validators
{
  public class PasswordConfimationValidator : FormCustomValidator
  {
    protected override bool OnServerValidate(string value)
    {
      Control control = this.FindControl(this.classAttributes["confirmationControlId"]);
      return control != null && ((TextBox) control).Text == value;
    }
  }
}
