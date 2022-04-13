// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Shell.UI.Dialogs.UserExistsPage
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Diagnostics;
using Sitecore.Forms.Core.Data;
using Sitecore.Web.UI.HtmlControls;
using Sitecore.WFFM.Abstractions.Dependencies;
using Sitecore.WFFM.Abstractions.Shared;
using System;
using System.Web.UI.HtmlControls;

namespace Sitecore.Forms.Shell.UI.Dialogs
{
  public class UserExistsPage : MemberhipActionPage
  {
    private readonly IResourceManager resourceManager;
    protected Literal ChoicesDescLiteral;
    protected HtmlInputRadioButton DoExist;
    protected Literal DoExistsLiteral;
    protected HtmlInputRadioButton DoNotExist;
    protected Literal DoNotExistsLiteral;

    public UserExistsPage()
      : this(DependenciesManager.ResourceManager)
    {
    }

    public UserExistsPage(IResourceManager resourceManager)
    {
      Assert.IsNotNull((object) resourceManager, "Dependency resourceManager is null");
      this.resourceManager = resourceManager;
    }

    protected override void Localize()
    {
      base.Localize();
      this.Header = this.resourceManager.Localize("USER_EXISTS");
      this.Text = this.resourceManager.Localize("CHECK_WHETHER_USER_IS_SITECORE_USER");
      this.ChoicesDescLiteral.Text = this.resourceManager.Localize("FORM_VARIFICATION_WILL_FAIL_IF");
      this.DoExistsLiteral.Text = this.resourceManager.Localize("USER_ALREADY_EXISTS", string.Empty);
      this.DoNotExistsLiteral.Text = this.resourceManager.Localize("USER_DOES_NOT_EXIST", string.Empty);
    }

    protected override void OnInit(EventArgs e)
    {
      base.OnInit(e);
      FailedCondition failedCondition;
      if (this.DoNotExist != null)
      {
        string valueByKey = this.GetValueByKey("FailWhen", "Denied");
        HtmlInputRadioButton doNotExist = this.DoNotExist;
        string str1 = valueByKey;
        failedCondition = FailedCondition.Denied;
        string str2 = failedCondition.ToString();
        int num;
        if (!(str1 == str2))
        {
          string str3 = valueByKey;
          failedCondition = FailedCondition.Denied;
          string str4 = failedCondition.ToString();
          num = str3 == str4 ? 1 : 0;
        }
        else
          num = 1;
        doNotExist.Checked = num != 0;
      }
      if (this.DoExist == null)
        return;
      string valueByKey1 = this.GetValueByKey("FailWhen", "Confirmed");
      HtmlInputRadioButton doExist = this.DoExist;
      string str5 = valueByKey1;
      failedCondition = FailedCondition.Confirmed;
      string str6 = failedCondition.ToString();
      int num1;
      if (!(str5 == str6))
      {
        string str7 = valueByKey1;
        failedCondition = FailedCondition.Denied;
        string str8 = failedCondition.ToString();
        num1 = str7 == str8 ? 1 : 0;
      }
      else
        num1 = 1;
      doExist.Checked = num1 != 0;
    }

    protected override void SaveValues()
    {
      base.SaveValues();
      if (this.DoNotExist == null)
        return;
      this.SetValue("FailWhen", this.DoNotExist.Checked ? FailedCondition.Denied.ToString() : FailedCondition.Confirmed.ToString());
    }
  }
}
