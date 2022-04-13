// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Shell.UI.Dialogs.MemberhipActionPage
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Form.Web.UI.Controls;
using Sitecore.Forms.Core.Data;
using Sitecore.Forms.Shell.UI.Controls;
using Sitecore.Web;
using Sitecore.Web.UI.HtmlControls;
using Sitecore.Web.UI.XamlSharp.Xaml;
using Sitecore.WFFM.Abstractions.Data;
using Sitecore.WFFM.Abstractions.Dependencies;
using System;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sitecore.Forms.Shell.UI.Dialogs
{
  public class MemberhipActionPage : DomainEditor
  {
    protected DropDownList NameField;
    protected DropDownList DomainField;
    protected Border Summary;
    protected Sitecore.Web.UI.HtmlControls.Literal NameRequired;
    protected Sitecore.Web.UI.HtmlControls.Literal DomainRequired;
    protected Sitecore.Web.UI.HtmlControls.Literal DomainLiteral;
    protected Sitecore.Web.UI.HtmlControls.Literal UserNameLiteral;

    protected override void Localize()
    {
      this.DomainLiteral.Text = DependenciesManager.ResourceManager.Localize("DOMAIN");
      this.UserNameLiteral.Text = DependenciesManager.ResourceManager.Localize("USER_NAME");
      ((WebControl) this.NameRequired).ToolTip = DependenciesManager.ResourceManager.Localize("USER_NAME_IS_REQUIRED");
      ((WebControl) this.DomainRequired).ToolTip = DependenciesManager.ResourceManager.Localize("DOMAIN_IS_REQUIRED");
    }

    protected override void OnInit(EventArgs e)
    {
      this.NameField.Items.LoadItemsFromForm(this.CurrentForm).DisableAll();
      this.NameField.Items.EnableFieldTypes(this.UserNameAllowedTypes);
      this.NameField.Select(this.GetValueByKey("UserNameField"));
      this.FillDomain(this.DomainField, this.GetValueByKey("DomainField"));
      base.OnInit(e);
    }

    protected void FillFields(DropDownList fieldList, string defaultValue, string allowedTypes)
    {
      fieldList.Items.Clear();
      foreach (IFieldItem field in new FormItem(this.CurrentDatabase.GetItem(this.CurrentID)).Fields)
      {
        if (string.IsNullOrEmpty(allowedTypes) || allowedTypes.Contains(((object) field.TypeID).ToString()))
        {
          System.Web.UI.WebControls.ListItem listItem = new System.Web.UI.WebControls.ListItem(field.Name, ((object) field.ID).ToString());
          if (listItem.Value == defaultValue)
            listItem.Selected = true;
          fieldList.Items.Add(listItem);
        }
      }
    }

    protected override void OK_Click()
    {
      if (this.RequireUserAndDomain && (string.IsNullOrEmpty(this.NameField.GetEnabledSelectedValue()) || string.IsNullOrEmpty(this.DomainField.GetEnabledSelectedValue())))
      {
        (this.Summary).Visible = true;
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("<ul>");
        if (string.IsNullOrEmpty(this.NameField.GetEnabledSelectedValue()))
        {
          ((WebControl) this.NameRequired).Style.Add("color", "red");
          XamlControl.AjaxScriptManager.SetOuterHtml((this.NameRequired).ID, this.NameRequired);
          stringBuilder.AppendFormat("<li>{0}</li>", (object) ((WebControl) this.NameRequired).ToolTip);
        }
        if (string.IsNullOrEmpty(this.DomainField.GetEnabledSelectedValue()))
        {
          ((WebControl) this.DomainRequired).Style.Add("color", "red");
          XamlControl.AjaxScriptManager.SetOuterHtml((this.DomainRequired).ID, this.DomainRequired);
          stringBuilder.AppendFormat("<li>{0}</li>", (object) ((WebControl) this.DomainRequired).ToolTip);
        }
        stringBuilder.Append("</ul>");
        (this.Summary).Controls.Add(new Sitecore.Web.UI.HtmlControls.Literal()
        {
          Text = stringBuilder.ToString()
        });
        XamlControl.AjaxScriptManager.SetOuterHtml((this.Summary).ID, this.Summary);
      }
      else
        base.OK_Click();
    }

    protected override void SaveValues()
    {
      base.SaveValues();
      this.SetValue("UserNameField", this.NameField.GetEnabledSelectedValue());
      this.SetValue("DomainField", this.DomainField.GetEnabledSelectedValue());
    }

    protected virtual bool RequireUserAndDomain => true;

    public string UserNameAllowedTypes => WebUtil.GetQueryString(nameof (UserNameAllowedTypes), "{84ABDA34-F9B1-4D3A-A69B-E28F39697069}|{6353E864-D3AE-4FF9-88DD-60B0F779A44A}|{F3A4D32A-0DD1-4613-8B0F-AC4C40E5D583}|{5A45591E-2FDC-444F-AB2C-AD814788928F}");
  }
}
