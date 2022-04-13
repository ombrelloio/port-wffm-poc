// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Shell.UI.Dialogs.LoginUserPage
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Diagnostics;
using Sitecore.Web.UI.HtmlControls;
using Sitecore.WFFM.Abstractions.Dependencies;
using Sitecore.WFFM.Abstractions.Shared;
using System;

namespace Sitecore.Forms.Shell.UI.Dialogs
{
  public class LoginUserPage : AuditMembershipActionPage
  {
    private readonly IResourceManager resourceManager;
    protected Groupbox IdentifyUser;
    protected Checkbox AssociateUserWithVisitor;

    public LoginUserPage()
      : this(DependenciesManager.ResourceManager)
    {
    }

    public LoginUserPage(IResourceManager resourceManager)
    {
      Assert.IsNotNull((object) resourceManager, "Dependency resourceManager is null");
      this.resourceManager = resourceManager;
    }

    protected override void OnInit(EventArgs e)
    {
      base.OnInit(e);
      this.AssociateUserWithVisitor.Checked = MainUtil.GetBool(this.GetValueByKey("AssociateUserWithVisitor"), true);
    }

    protected override void Localize()
    {
      base.Localize();
      this.Header = this.resourceManager.Localize("USER_LOGIN");
      this.Text = this.resourceManager.Localize("LOG_USER_IN_BASED_ON_INFORMATION_FROM_FORM");
      ((HeaderedItemsControl) this.IdentifyUser).Header = this.resourceManager.Localize("IDENTIFY_USER");
      this.AssociateUserWithVisitor.Header = this.resourceManager.Localize("ASSOCIATE_EXISTING_USER_WITH_THIS_WISITOR");
    }

    protected override void SaveValues()
    {
      base.SaveValues();
      this.SetValue("AssociateUserWithVisitor", this.AssociateUserWithVisitor.Checked.ToString());
    }
  }
}
