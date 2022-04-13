// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Shell.UI.Dialogs.ChangePasswordPage
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Diagnostics;
using Sitecore.Form.Web.UI.Controls;
using Sitecore.Web.UI.HtmlControls;
using Sitecore.WFFM.Abstractions.Dependencies;
using Sitecore.WFFM.Abstractions.Shared;
using System;
using System.Web.UI.WebControls;

namespace Sitecore.Forms.Shell.UI.Dialogs
{
  public class ChangePasswordPage : CheckUserPasswordPage
  {
    private readonly IResourceManager resourceManager;
    protected DropDownList OldPasswordField;
    protected Sitecore.Web.UI.HtmlControls.Literal OldPasswordLiteral;

    public ChangePasswordPage()
      : this(DependenciesManager.ResourceManager)
    {
    }

    public ChangePasswordPage(IResourceManager resourceManager)
    {
      Assert.IsNotNull((object) resourceManager, "Dependency resourceManager is null");
      this.resourceManager = resourceManager;
    }

    protected override void Localize()
    {
      base.Localize();
      this.PasswordLiteral.Text = this.resourceManager.Localize("NEW_PASSWORD");
      this.OldPasswordLiteral.Text = this.resourceManager.Localize("OLD_PASSWORD");
      this.Header = this.resourceManager.Localize("CHANGE_PASSWORD");
      this.Text = this.resourceManager.Localize("CHANGE_PASSWORD_OF_USER");
    }

    protected override void OnInit(EventArgs e)
    {
      this.FillFields(this.OldPasswordField, this.GetValueByKey("OldPasswordField"), this.PasswordAllowedTypes);
      base.OnInit(e);
    }

    protected override void SaveValues()
    {
      base.SaveValues();
      this.SetValue("OldPasswordField", this.OldPasswordField.GetEnabledSelectedValue());
    }
  }
}
