// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Shell.UI.Dialogs.LoginUserWithPasswordPage
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Diagnostics;
using Sitecore.Form.Web.UI.Controls;
using Sitecore.Web;
using Sitecore.Web.UI.HtmlControls;
using Sitecore.WFFM.Abstractions.Dependencies;
using Sitecore.WFFM.Abstractions.Shared;
using System;
using System.Web.UI.WebControls;

namespace Sitecore.Forms.Shell.UI.Dialogs
{
  public class LoginUserWithPasswordPage : LoginUserPage
  {
    private readonly IResourceManager resourceManager;
    protected DropDownList PasswordField;
    protected Sitecore.Web.UI.HtmlControls.Literal PasswordLiteral;

    public LoginUserWithPasswordPage()
      : this(DependenciesManager.ResourceManager)
    {
    }

    public LoginUserWithPasswordPage(IResourceManager resourceManager)
    {
      Assert.IsNotNull((object) resourceManager, "Dependency resourceManager is null");
      this.resourceManager = resourceManager;
    }

    protected override void OnInit(EventArgs e)
    {
      base.OnInit(e);
      this.FillFields(this.PasswordField, this.GetValueByKey("PasswordField"), this.PasswordAllowedTypes);
    }

    protected override void Localize()
    {
      base.Localize();
      this.PasswordLiteral.Text = this.resourceManager.Localize("USER_PASSWORD");
      this.Header = this.resourceManager.Localize("USER_LOGIN_WITH_PASSWORD");
      this.Text = this.resourceManager.Localize("LOG_USER_IN_BASED_ON_INFORMATION_FROM_FORM_WITH_PASSWORD");
    }

    protected override void SaveValues()
    {
      base.SaveValues();
      this.SetValue("PasswordField", this.PasswordField.GetEnabledSelectedValue());
    }

    public string PasswordAllowedTypes => WebUtil.GetQueryString(nameof (PasswordAllowedTypes), "{1F09D460-200C-4C94-9673-488667FF75D1}|{1AD5CA6E-8A92-49F0-889C-D082F2849FBD}");
  }
}
