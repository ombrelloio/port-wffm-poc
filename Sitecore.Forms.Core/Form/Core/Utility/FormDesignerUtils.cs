// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.Utility.FormDesignerUtils
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Data;
using Sitecore.Diagnostics;
using Sitecore.Form.Core.Data;
using Sitecore.Forms.Shell.UI.Controls;
using Sitecore.Web.UI.HtmlControls;
using Sitecore.Web.UI.XmlControls;
using Sitecore.WFFM.Abstractions.Dependencies;
using System.Collections.Specialized;
using System.Web.UI;

namespace Sitecore.Form.Core.Utility
{
  public class FormDesignerUtils
  {
    public static Sitecore.Web.UI.HtmlControls.Control GetField(string id, FieldDefinition field)
    {
      FormField formField = new FormField(field);
      formField.ID = id;
      formField.Class = "scFbTableField";
      return (Sitecore.Web.UI.HtmlControls.Control) formField;
    }

    public static Sitecore.Web.UI.HtmlControls.Control GetSection(SectionDefinition sectionDef)
    {
      FormSection formSection = new FormSection(sectionDef);
      formSection.ID = sectionDef.SectionID;
      formSection.Class = "scFbTableSection";
      return (Sitecore.Web.UI.HtmlControls.Control) formSection;
    }

    public static Sitecore.Web.UI.HtmlControls.Control GetEmptySection(string id)
    {
      FormSection formSection = new FormSection(id);
      formSection.ID = id;
      formSection.Class = "scFbTableSection";
      return (Sitecore.Web.UI.HtmlControls.Control) formSection;
    }

    public static Sitecore.Forms.Shell.UI.Controls.XmlControl SectionButtons(string id)
    {
            Sitecore.Forms.Shell.UI.Controls.XmlControl buttonSection = FormDesignerUtils.GetButtonSection(id, "scDivLine");
            Sitecore.Web.UI.HtmlControls.Control byUniqueId1 = (Sitecore.Web.UI.HtmlControls.Control) XmlResourceUtil.FindByUniqueID((Sitecore.Web.UI.XmlControls.XmlControl) buttonSection, "buttonsleftContainer" + id);
      Assert.IsNotNull((object) byUniqueId1, "left container");
      byUniqueId1.Controls.Add(FormDesignerUtils.GetButton(((object) ShortID.NewId()).ToString(), DependenciesManager.ResourceManager.Localize("ADD_FIELD"), "Applications/16x16/add.png", "javascript:return Sitecore.FormBuilder.addField(this,event,'" + id + "', true)", "16"));
      var byUniqueId2 = (Sitecore.Web.UI.HtmlControls.Control) XmlResourceUtil.FindByUniqueID((Sitecore.Web.UI.XmlControls.XmlControl) buttonSection, "buttonsrightContainer" + id);
      Assert.IsNotNull((object) byUniqueId2, "right container");
      byUniqueId2.Controls.Add(FormDesignerUtils.GetButton(((object) ShortID.NewId()).ToString(), DependenciesManager.ResourceManager.Localize("ADD_SECTION"), "Business/16x16/note_add.png", "javascript:return Sitecore.FormBuilder.addSection(this,event,'" + id + "', true)", "16"));
      return buttonSection;
    }

    public static Sitecore.Forms.Shell.UI.Controls.XmlControl FieldButtons(string id)
    {
            Sitecore.Forms.Shell.UI.Controls.XmlControl buttonSection = FormDesignerUtils.GetButtonSection(id, "scDivLine");
      var byUniqueId1 = (Sitecore.Web.UI.HtmlControls.Control) XmlResourceUtil.FindByUniqueID((Sitecore.Web.UI.XmlControls.XmlControl) buttonSection, "buttonsleftContainer" + id);
      Assert.IsNotNull((object) byUniqueId1, "left container");
      byUniqueId1.Controls.Add(FormDesignerUtils.GetButton(((object) ShortID.NewId()).ToString(), DependenciesManager.ResourceManager.Localize("ADD_FIELD"), "Applications/16x16/add.png", "javascript:return Sitecore.FormBuilder.addField(this,event,'" + id + "', true)", "16"));
      var byUniqueId2 = (Sitecore.Web.UI.HtmlControls.Control) XmlResourceUtil.FindByUniqueID((Sitecore.Web.UI.XmlControls.XmlControl) buttonSection, "buttonsrightContainer" + id);
      Assert.IsNotNull((object) byUniqueId2, "right container");
      byUniqueId2.Controls.Add(FormDesignerUtils.GetButton(((object) ShortID.NewId()).ToString(), DependenciesManager.ResourceManager.Localize("ADD_SECTION"), "Business/16x16/note_add.png", "javascript:return Sitecore.FormBuilder.upgradeToSection(this,event, true)", "16"));
      return buttonSection;
    }

    public static Sitecore.Forms.Shell.UI.Controls.XmlControl GlobalButtons(string id)
    {
      var resourceControl = (Sitecore.Forms.Shell.UI.Controls.XmlControl) XmlResourceUtil.GetResourceControl("Forms.WelcomeButton", new NameValueCollection());
      resourceControl["Text"] = (object) DependenciesManager.ResourceManager.Localize("ADD_A_FIELD");
      return resourceControl;
    }

    private static Sitecore.Forms.Shell.UI.Controls.XmlControl GetButtonSection(string id, string separatorStyle) => (Sitecore.Forms.Shell.UI.Controls.XmlControl) XmlResourceUtil.GetResourceControl("FieldButtons", new NameValueCollection()
    {
      {
        "ButtonsID",
        "buttons" + id
      },
      {
        "MessageContentID",
        "buttonsmessage" + id
      },
      {
        "LeftConteiner",
        "buttonsleftContainer" + id
      },
      {
        "RightConteiner",
        "buttonsrightContainer" + id
      },
      {
        "SeparatorStyle",
        separatorStyle
      }
    });

    public static Sitecore.Web.UI.HtmlControls.Control GetButton(
      string id,
      string text,
      string icon,
      string click,
      string size)
    {
      InlineButton inlineButton = new InlineButton(text, icon, click, size);
      inlineButton.ID = "button_" + id;
      return inlineButton;
    }
  }
}
