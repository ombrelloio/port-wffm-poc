// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Web.UI.Controls.BaseValidatorExtensions
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Diagnostics;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sitecore.Form.Web.UI.Controls
{
  public static class BaseValidatorExtensions
  {
    public static bool IsFailedAndRequireFocus(this BaseValidator validator)
    {
      Assert.ArgumentNotNull((object) validator, nameof (validator));
      return !validator.IsValid;
    }

    public static Control GetControlToValidate(this BaseValidator validator)
    {
      Assert.ArgumentNotNull((object) validator, nameof (validator));
      return !string.IsNullOrEmpty(validator.ControlToValidate) && validator.NamingContainer != null ? validator.NamingContainer.FindControl(validator.ControlToValidate) : (Control) null;
    }
  }
}
