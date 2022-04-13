// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.Client.Validators.CardTypeValidator
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Form.Core.Validators;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sitecore.Form.Core.Client.Validators
{
  public class CardTypeValidator : FormCustomValidator
  {
    public string CardType
    {
      get => this.classAttributes["cardTypeValue"] ?? string.Empty;
      set => this.classAttributes["cardTypeValue"] = value;
    }

    public string ValidationExpression
    {
      get => this.classAttributes["validationExpression"] ?? string.Empty;
      set => this.classAttributes["validationExpression"] = HttpContext.Current.Server.UrlEncode(value ?? string.Empty);
    }

    protected override bool OnServerValidate(string value)
    {
      if (!string.IsNullOrEmpty(this.classAttributes["cardTypeControlId"]))
      {
        Control control = this.FindControl(this.classAttributes["cardTypeControlId"]);
        if (control != null && control is ListControl && ((ListControl) control).SelectedValue == this.CardType)
          return Regex.Match(value, HttpContext.Current.Server.UrlDecode(this.ValidationExpression)).Success || Regex.Match(value, HttpContext.Current.Server.UrlDecode(HttpContext.Current.Server.UrlDecode(this.ValidationExpression))).Success;
      }
      return base.OnServerValidate(value);
    }
  }
}
