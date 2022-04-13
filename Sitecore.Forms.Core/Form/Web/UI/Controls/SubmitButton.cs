// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Web.UI.Controls.SubmitButton
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Form.Core.Utility;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sitecore.Form.Web.UI.Controls
{
  public class SubmitButton : Button
  {
    public new event EventHandler Click
    {
      add => base.Click += value;
      remove => base.Click -= value;
    }

    protected override void AddAttributesToRender(HtmlTextWriter writer)
    {
      if (this.Page != null)
        this.Page.VerifyRenderingInServerForm((Control) this);
      writer.AddAttribute(HtmlTextWriterAttribute.Type, this.UseSubmitBehavior ? "submit" : "button");
      PostBackOptions postBackOptions = this.GetPostBackOptions();
      string uniqueId = this.UniqueID;
      if (uniqueId != null && (postBackOptions == null || postBackOptions.TargetControl == this))
        writer.AddAttribute(HtmlTextWriterAttribute.Name, uniqueId);
      writer.AddAttribute(HtmlTextWriterAttribute.Value, this.Text);
      bool isEnabled = this.IsEnabled;
      string firstScript = string.Empty;
      if (isEnabled)
      {
        firstScript = ScriptUtils.EnsureEndWithSemiColon(this.OnClientClick);
        if (this.HasAttributes)
        {
          string attribute = this.Attributes["onclick"];
          if (attribute != null)
          {
            firstScript += ScriptUtils.EnsureEndWithSemiColon(attribute);
            this.Attributes.Remove("onclick");
          }
        }
        if (this.Page != null)
        {
          string backEventReference = this.Page.ClientScript.GetPostBackEventReference(postBackOptions, false);
          if (backEventReference != null)
            firstScript = ScriptUtils.MergeScript(ScriptUtils.EnsureEndWithSemiColon(ScriptUtils.MergeScript(firstScript, backEventReference)), this.FocusIsNotValid(postBackOptions.ValidationGroup));
        }
      }
      if (this.Page != null)
        this.Page.ClientScript.RegisterForEventValidation(postBackOptions);
      if (firstScript.Length > 0)
        writer.AddAttribute(HtmlTextWriterAttribute.Onclick, firstScript);
      if (this.Enabled && !isEnabled)
        writer.AddAttribute(HtmlTextWriterAttribute.Disabled, "disabled");
      if (this.ID != null)
        writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID);
      if (this.CssClass != null)
        writer.AddAttribute(HtmlTextWriterAttribute.Class, this.CssClass);
      string toolTip = this.ToolTip;
      if (toolTip.Length <= 0)
        return;
      writer.AddAttribute(HtmlTextWriterAttribute.Title, toolTip);
    }

    private string FocusIsNotValid(string validationGroup)
    {
      if (string.IsNullOrEmpty(validationGroup))
        return string.Empty;
      return Sitecore.StringExtensions.StringExtensions.FormatWith("$scw.webform.validators.setFocusToFirstNotValid('{0}')", new object[1]
      {
        (object) validationGroup
      });
    }
  }
}
