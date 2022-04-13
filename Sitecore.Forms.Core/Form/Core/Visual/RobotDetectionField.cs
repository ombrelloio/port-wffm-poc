// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.Visual.RobotDetectionField
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Diagnostics;
using Sitecore.Form.Core.Configuration;
using Sitecore.Form.Core.Data;
using Sitecore.Resources;
using Sitecore.WFFM.Abstractions.Dependencies;
using Sitecore.WFFM.Abstractions.Shared;
using System;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sitecore.Form.Core.Visual
{
  public class RobotDetectionField : ValidationField
  {
    private readonly IResourceManager resourceManager;

    public RobotDetectionField()
      : this(DependenciesManager.ResourceManager)
    {
    }

    public RobotDetectionField(IResourceManager resourceManager)
      : base(HtmlTextWriterTag.Div.ToString())
    {
      Assert.IsNotNull((object) resourceManager, "Dependency resourceManager is null");
      this.resourceManager = resourceManager;
    }

    protected override void OnPreRender(object sender, EventArgs ev)
    {
      base.OnPreRender(sender, ev);
      this.Attributes["class"] += " scFbWide";
      string xml = this.Attributes["value"] ?? string.Empty;
      ProtectionSchema schema = ProtectionSchema.Parse(xml);
      Literal literal = new Literal();
      string prefixId = StaticSettings.PrefixId;
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.AppendFormat("<input id='{0}' style='width:89%' type='hidden' value='{1}' ", (object) this.ID, (object) xml);
      stringBuilder.AppendFormat(" onblur=\"Sitecore.PropertiesBuilder.onSaveValue('{0}', '{1}', '0')\"", (object) prefixId, (object) this.ID);
      stringBuilder.AppendFormat(" onchange=\"Sitecore.PropertiesBuilder.onSaveShowCAPTCHAValue('{0}', '{1}');\"", (object) prefixId, (object) this.ID);
      stringBuilder.AppendFormat(" onkeyup=\"Sitecore.PropertiesBuilder.onSaveValue('{0}', '{1}', '0')\"", (object) prefixId, (object) this.ID);
      stringBuilder.AppendFormat(" onpaste=\"Sitecore.PropertiesBuilder.onSaveValue('{0}', '{1}', '0')\"", (object) prefixId, (object) this.ID);
      stringBuilder.AppendFormat(" oncut=\"Sitecore.PropertiesBuilder.onSaveValue('{0}', '{1}', '0')\"", (object) prefixId, (object) this.ID);
      stringBuilder.Append(" />");
      this.Controls.Add((Control) new Literal()
      {
        Text = "</div>"
      });
      this.Controls.Add((Control) new Literal()
      {
        Text = "<div>"
      });
      stringBuilder.Append("<div class='scContentControlLayoutDeviceRenderings' style='padding-left:0px;margin:0px;font-weight:normal;'>");
      stringBuilder.Append("<div class='robot-modes'>");
      stringBuilder.AppendFormat("<input id='showAlways' name='showCAPTCHA' type='radio' value='always' {0} onclick=\"Sitecore.PropertiesBuilder.onSaveShowCAPTCHAValue('{1}', '{2}', '1');\"></input>", !schema.Enabled ? (object) "checked='checked'" : (object) string.Empty, (object) prefixId, (object) this.ID);
      stringBuilder.AppendFormat("<label for='showAlways' style='height:20px;margin:0px;padding:0px;padding-top:5px'>{0}</label>", (object) this.resourceManager.Localize("ALWAYS_SHOW_CAPTCHA"));
      stringBuilder.Append("</div>");
      stringBuilder.Append("<div style='border:0px;margin: 0px 1px 0px 0px;padding:0px'>");
      stringBuilder.Append("<div id='EditAttackProtection' style='float:right; margin-top:7px;text-align:right;'>");
      stringBuilder.AppendFormat("<a class='scwfmEditLink' id='EditLink' {0} href='#' style='padding:0px;'>", schema.Enabled ? (object) ("onclick=\"Sitecore.FormBuilder.rise('forms:openrobotdetection','" + this.ID + "');\"") : (object) string.Empty);
      stringBuilder.AppendFormat("<span id='EditLiteral' class='scwfmEditLabel' style='text-align:right'>{0}</span>", (object) this.resourceManager.Localize("EDIT"));
      stringBuilder.Append("</a>");
      stringBuilder.Append("</div>");
      stringBuilder.AppendFormat("<input id='showAuto' name='showCAPTCHA' value='auto' type='radio' onclick=\"Sitecore.PropertiesBuilder.onSaveShowCAPTCHAValue('{0}', '{1}', '0');\"></input>", (object) prefixId, (object) this.ID);
      stringBuilder.AppendFormat("<label for='showAuto' style='height:20px;margin:0px;padding:0px;padding-top:5px'>{0}</label>", (object) this.resourceManager.Localize("SHOW_CAPTCHA_IF"));
      stringBuilder.Append("</div>");
      stringBuilder.Append("<div style='background-color:#dddddd;margin:2px 0px 2px 20px;padding:0 0 0 10;height:1px' height='1px'>");
      stringBuilder.Append("<span />");
      stringBuilder.Append("</div>");
      stringBuilder.Append("<div id='AttackProtection' id='ShowSettings'>");
      stringBuilder.Append("<a id='IsRobot' style='padding: 3px 0 3px 0; margin: 3px 0px;display:block' class='scFbListItem' onclick=''>");
      stringBuilder.Append(this.GetBulletImg(schema));
      stringBuilder.Append(this.resourceManager.Localize("THE_VISITOR_IS_ROBOT"));
      stringBuilder.Append("</a>");
      stringBuilder.Append("<a id='sessionLimit' style='padding: 3px 0 3px 0; margin: 3px 0;display:block;display:none' class='scFbListItem' onclick=''>");
      stringBuilder.Append(this.GetBulletImg(schema));
      stringBuilder.Append(this.resourceManager.Localize("VISITOR_SUBMITS_FORM_MORE_THAN", Sitecore.StringExtensions.StringExtensions.FormatWith("<span id='sessionTimes'>{0}</span>", new object[1]
      {
        (object) schema.Session.SubmitsNumber
      }), Sitecore.StringExtensions.StringExtensions.FormatWith("<span id='sessionMinutes'>{0}</span>", new object[1]
      {
        (object) schema.Session.MinutesInterval
      })));
      stringBuilder.Append("</a>");
      stringBuilder.Append("<a id='serverLimit' style='padding: 3px 0px 3px 0px; margin: 3px 0px;display:none' class='scFbListItem' onclick=''>");
      stringBuilder.Append(this.GetBulletImg(schema));
      stringBuilder.Append(this.resourceManager.Localize("FORM_SUBMITTED_MORE_THAN", Sitecore.StringExtensions.StringExtensions.FormatWith("<span id='serverTimes'>{0}</span>", new object[1]
      {
        (object) schema.Server.SubmitsNumber
      }), Sitecore.StringExtensions.StringExtensions.FormatWith("<span id='serverMinutes'>{0}</span>", new object[1]
      {
        (object) schema.Server.MinutesInterval
      })));
      stringBuilder.Append("</a>");
      stringBuilder.Append("</div>");
      literal.Text = stringBuilder.ToString();
      this.Controls.Add((Control) literal);
      this.ID += "input";
    }

    protected string GetBulletImg(ProtectionSchema schema)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.AppendFormat("<img height='16' width='16' border='0' align='middle' style='margin:0 4px 0 0' ");
      stringBuilder.AppendFormat("src='{0}' ", (object) Themes.MapTheme("/~/icon/Network/16x16/shield_new.png", string.Empty, !schema.Enabled));
      stringBuilder.AppendFormat("link='{0}' ", (object) Themes.MapTheme("/~/icon/Network/16x16/shield_new.png", string.Empty, schema.Enabled));
      stringBuilder.AppendFormat("linkd='{0}' ", (object) Themes.MapTheme("/~/icon/Network/16x16/shield_new.png", string.Empty, !schema.Enabled));
      stringBuilder.Append(" />");
      return stringBuilder.ToString();
    }
  }
}
