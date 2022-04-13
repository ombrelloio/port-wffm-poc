// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Web.UI.Controls.SitecoreSimpleFormAscx
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Diagnostics;
using Sitecore.Form.Core.Ascx.Controls;
using Sitecore.Form.Core.Attributes;
using Sitecore.Form.Core.Configuration;
using Sitecore.Form.Core.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Helpers;
using System.Web.UI;

namespace Sitecore.Form.Web.UI.Controls
{
  [ToolboxData("<div runat=\"server\"></div>")]
  [Dummy]
  [PersistChildren(true)]
  public class SitecoreSimpleFormAscx : SitecoreSimpleForm
  {
    protected override FormTitle Title => this.title;

    protected override FormIntroduction Intro => this.intro;

    protected override FormFooter Footer => this.footer;

    protected override FormSubmit Submit => this.submit;

    protected override SubmitSummary SubmitSummary => this.submitSummary;

    protected override Control FieldContainer => (Control) this.fieldContainer;

    protected override void OnInit(EventArgs e)
    {
      Assert.IsNotNull((object) this.FormItem, "FormItem");
      if (this.Page == null)
      {
        this.Page = WebUtil.GetPage();
        ReflectionUtils.SetField(typeof (Page), (object) this.Page, "_enableEventValidation", (object) false);
      }
      this.Page.EnableViewState = true;
      ThemesManager.RegisterCssScript(this.Page, this.FormItem.InnerItem, Sitecore.Context.Item);
      this.title.Item = this.FormItem.InnerItem;
      this.title.SetTagKey(this.FormItem.TitleTag);
      this.title.DisableWebEditing = this.DisableWebEditing;
      this.title.Parameters = this.Parameters;
      this.title.FastPreview = this.FastPreview;
      this.intro.Item = this.FormItem.InnerItem;
      this.intro.DisableWebEditing = this.DisableWebEditing;
      this.intro.Parameters = this.Parameters;
      this.intro.FastPreview = this.FastPreview;
      this.submit.Item = this.FormItem.InnerItem;
      this.submit.ID = this.ID + SitecoreSimpleForm.PrefixSubmitID;
      this.submit.DisableWebEditing = this.DisableWebEditing;
      this.submit.Parameters = this.Parameters;
      this.submit.FastPreview = this.FastPreview;
      this.submit.ValidationGroup = this.submit.ID;
      this.submit.Click += OnClick;
      if (this.FastPreview)
        this.summary.Visible = false;
      this.summary.ID = SimpleForm.prefixSummaryID;
      this.summary.ValidationGroup = this.submit.ID;
      this.submitSummary.ID = this.ID + SimpleForm.prefixErrorID;
      this.Expand();
      this.footer.Item = this.FormItem.InnerItem;
      this.footer.DisableWebEditing = this.DisableWebEditing;
      this.footer.Parameters = this.Parameters;
      this.footer.FastPreview = this.FastPreview;
      this.EventCounter.ID = this.ID + SimpleForm.prefixEventCountID;
      this.Controls.Add((Control) this.EventCounter);
      this.AntiCsrf.ID = this.ID + SimpleForm.PrefixAntiCsrfId;
      this.Controls.Add((Control) this.AntiCsrf);
      string cookieValue1 = CookieUtil.GetCookieValue(this.AntiCsrf.ID);
      string cookieValue2;
      string str;
      AntiForgery.GetTokens(cookieValue1, out cookieValue2, out str);
      if (string.IsNullOrEmpty(cookieValue1))
        CookieUtil.SetCookie(this.AntiCsrf.ID, cookieValue2);
      if (this.IsPostBack && ((IEnumerable<string>) this.Request.Form.AllKeys).Any<string>((Func<string, bool>) (k => k != null && k.Contains(this.submit.ID))))
        return;
      this.AntiCsrf.Value = str;
    }
  }
}
