// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Shell.UI.Dialogs.CreateUserPage
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using ComponentArt.Web.UI;
using Sitecore.Data.Managers;
using Sitecore.Data.Templates;
using Sitecore.Form.Core.Configuration;
using Sitecore.Form.Web.UI.Controls;
using Sitecore.Forms.Core.Data;
using Sitecore.Forms.Shell.UI.Controls;
using Sitecore.Resources;
using Sitecore.Web;
using Sitecore.Web.UI;
using Sitecore.Web.UI.HtmlControls;
using Sitecore.Web.UI.WebControls;
using Sitecore.WFFM.Abstractions.Dependencies;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace Sitecore.Forms.Shell.UI.Dialogs
{
  public class CreateUserPage : AuditMembershipActionPage
  {
    protected PlaceHolder Content;
    protected CallBack MapCallBack;
    protected DropDownList PasswordField;
    protected Checkbox AssociateUserWithVisitor;
    protected Checkbox OverwriteProfile;
    protected HtmlInputHidden MappedFields;
    protected MultiPage MultiPage;
    protected System.Web.UI.WebControls.Image AddProperty;
    protected TabStripTab BasicUsedInformationTab;
    protected TabStripTab AdditionalUserPropertiesTab;
    protected Sitecore.Web.UI.HtmlControls.Literal MakeSureNameIsUniqueLiteral;
    protected Sitecore.Web.UI.HtmlControls.Literal ActionWillStoreLiteral;
    protected Sitecore.Web.UI.HtmlControls.Literal UserFormatLiteral;
    protected Sitecore.Web.UI.HtmlControls.Literal PasswordLiteral;
    protected Groupbox UserProfileGroupbox;
    protected Sitecore.Web.UI.HtmlControls.Literal AddPropertyLiteral;

    protected override void OnInit(EventArgs e)
    {
      this.FillPassword(this.PasswordField, this.GetValueByKey("PasswordField", "randomPassword"));
      this.AssociateUserWithVisitor.Checked = MainUtil.GetBool(this.GetValueByKey("AssociateUserWithVisitor"), true);
      this.OverwriteProfile.Checked = MainUtil.GetBool(this.GetValueByKey("OverwriteProfile"), true);
      // ISSUE: method pointer
      this.MapCallBack.Callback += new CallBack.CallbackEventHandler(OnMapCallBack);
      base.OnInit(e);
      this.AddProperty.ImageUrl = Images.GetThemedImageSource("Software/16x16/element_add.png");
    }

    protected override void OnLoad(EventArgs e)
    {
      base.OnLoad(e);
      if (Page.IsPostBack || (this.MapCallBack).IsCallback)
        return;
      string valueByKey = this.GetValueByKey("Mapping");
      this.MappedFields.Value = valueByKey;
      if (!string.IsNullOrEmpty(valueByKey) && valueByKey != "<data></data>")
        this.Content.Controls.Add(this.ReBuildMaping("[remove]" + valueByKey));
      else
        this.Content.Controls.Add(this.ReBuildMaping(valueByKey));
      this.MapCallBack.Content = new CallBackContent();
      (this.MapCallBack.Content).Controls.Add(this.Content);
      Page.ClientScript.RegisterClientScriptBlock(((object) this).GetType(), "startup", "document.observe('dom:loaded', function(){serializeMapping();})", true);
    }

    protected override void Localize()
    {
      base.Localize();
      ((NavigationNode) this.BasicUsedInformationTab).Text = DependenciesManager.ResourceManager.Localize("BASIC_USER_INFORMATION");
      (this.BasicUsedInformationTab).ToolTip = ((NavigationNode) this.BasicUsedInformationTab).Text;
      ((NavigationNode) this.AdditionalUserPropertiesTab).Text = DependenciesManager.ResourceManager.Localize("ADDITIONAL_USER_PROPERTIES");
      (this.AdditionalUserPropertiesTab).ToolTip = ((NavigationNode) this.AdditionalUserPropertiesTab).Text;
      this.Header = DependenciesManager.ResourceManager.Localize("CREATE_USER");
      this.Text = DependenciesManager.ResourceManager.Localize("SELECT_FORM_FIELD_VALUES_TO_CREATE_USER");
      this.MakeSureNameIsUniqueLiteral.Text = DependenciesManager.ResourceManager.Localize("TO_MAKE_SURE_USER_NAME_IS_UNIQUE");
      this.ActionWillStoreLiteral.Text = DependenciesManager.ResourceManager.Localize("ACTION_WILL_STORE_SELECTED_FIELDS");
      this.UserFormatLiteral.Text = DependenciesManager.ResourceManager.Localize("USER_NAME_WILL_BE_CREATED_WITH_FORMAT");
      this.PasswordLiteral.Text = DependenciesManager.ResourceManager.Localize("USER_PASSWORD");
      this.AssociateUserWithVisitor.Header = DependenciesManager.ResourceManager.Localize("ASSOCIATE_NEW_USER_WITH_THIS_VISITOR");
      ((HeaderedItemsControl) this.UserProfileGroupbox).Header = DependenciesManager.ResourceManager.Localize("CREATE_OR_UPDATE_USER_PROFILE");
      this.OverwriteProfile.Header = DependenciesManager.ResourceManager.Localize("OVERWRITE_USER_FIELDS_IF_THEY_CONTAINS_VALUE");
      this.AddPropertyLiteral.Text = DependenciesManager.ResourceManager.Localize("ADD_FIELD");
    }

    protected override void SaveValues()
    {
      base.SaveValues();
      this.SetValue("PasswordField", this.PasswordField.GetEnabledSelectedValue());
      this.SetValue("AssociateUserWithVisitor", this.AssociateUserWithVisitor.Checked.ToString());
      this.SetValue("OverwriteProfile", this.OverwriteProfile.Checked.ToString());
      this.SetValue("Mapping", this.MappedFields.Value);
      string str = new FormItem(this.CurrentDatabase.GetItem(this.CurrentID)).ProfileItem;
      if (string.IsNullOrEmpty(str))
        str = "{AE4C4969-5B7E-4B4E-9042-B2D8701CE214}";
      this.SetValue("ProfileItemId", str);
    }

    private GridPanel ReBuildMaping(string param)
    {
      GridPanel gridPanel1 = new GridPanel();
      gridPanel1.Columns = 3;
      (gridPanel1).Margin = "0";
      GridPanel gridPanel2 = gridPanel1;
      (gridPanel2).Attributes["class"] = "MappingGrid";
      bool flag = !string.IsNullOrEmpty(param) && param.StartsWith("[remove]");
      if (flag)
        param = param.Remove(0, "[remove]".Length);
      if (!string.IsNullOrEmpty(param) && param != "<data></data>" || !flag)
      {
        (gridPanel2).Controls.Add(new Sitecore.Web.UI.HtmlControls.Literal()
        {
          Text = Sitecore.StringExtensions.StringExtensions.FormatWith("<span>{0}</span>", new object[1]
          {
            (object) DependenciesManager.ResourceManager.Localize("FORM_FIELD")
          })
        });
        (gridPanel2).Controls.Add(new Sitecore.Web.UI.HtmlControls.Literal()
        {
          Text = Sitecore.StringExtensions.StringExtensions.FormatWith("<span>{0}</span>", new object[1]
          {
            (object) DependenciesManager.ResourceManager.Localize("USER_PROFILE_COLON")
          })
        });
        (gridPanel2).Controls.Add(new Sitecore.Web.UI.HtmlControls.Literal());
      }
      int num1 = 0;
      if (!string.IsNullOrEmpty(param))
      {
        XDocument xdocument = XDocument.Parse(param);
        if (xdocument.Root != null)
        {
          foreach (XElement element in xdocument.Root.Elements())
          {
            string str = element.Attribute((XName) "fieldid").Value;
            string defaultValue = element.Element((XName) "profile").Attribute((XName) "fieldid").Value;
            if (!string.IsNullOrEmpty(str) && !string.IsNullOrEmpty(defaultValue))
            {
              DropDownList dropDownList1 = new DropDownList();
              dropDownList1.ID = "fieldChoice" + (object) num1;
              DropDownList list = dropDownList1;
              list.Attributes["onchange"] = "javascript:return serializeMapping()";
              list.Items.LoadItemsFromForm(this.CurrentForm);
              list.Select(str);
              (gridPanel2).Controls.Add(list);
              DropDownList dropDownList2 = new DropDownList();
              dropDownList2.ID = "profileChoice" + (object) num1;
              DropDownList profileList = dropDownList2;
              profileList.Attributes["onchange"] = "javascript:return serializeMapping()";
              this.FillProfiles(profileList, defaultValue);
              (gridPanel2).Controls.Add(profileList);
              System.Web.UI.WebControls.Image image1 = new System.Web.UI.WebControls.Image();
              image1.ImageUrl = Images.GetThemedImageSource("Applications/16x16/delete2.png");
              image1.ToolTip = DependenciesManager.ResourceManager.Localize("REMOVE");
              System.Web.UI.WebControls.Image image2 = image1;
              LinkButton linkButton = new LinkButton()
              {
                PostBackUrl = "#"
              };
              linkButton.Attributes["onclick"] = Sitecore.StringExtensions.StringExtensions.FormatWith("javascript:return removeMapping({0})", new object[1]
              {
                (object) num1
              });
              linkButton.Controls.Add(image2);
              (gridPanel2).Controls.Add(linkButton);
              ++num1;
            }
          }
        }
      }
      if (!flag)
      {
        DropDownList dropDownList3 = new DropDownList();
        dropDownList3.ID = "fieldChoice" + (object) num1;
        DropDownList dropDownList4 = dropDownList3;
        dropDownList4.Attributes["onchange"] = "javascript:return serializeMapping()";
        dropDownList4.Items.LoadItemsFromForm(this.CurrentForm);
        (gridPanel2).Controls.Add(dropDownList4);
        DropDownList dropDownList5 = new DropDownList();
        dropDownList5.ID = "profileChoice" + (object) num1;
        DropDownList profileList = dropDownList5;
        profileList.Attributes["onchange"] = "javascript:return serializeMapping()";
        this.FillProfiles(profileList, (string) null);
        (gridPanel2).Controls.Add(profileList);
        System.Web.UI.WebControls.Image image3 = new System.Web.UI.WebControls.Image();
        image3.ImageUrl = Images.GetThemedImageSource("Applications/16x16/delete2.png");
        image3.ToolTip = DependenciesManager.ResourceManager.Localize("REMOVE");
        System.Web.UI.WebControls.Image image4 = image3;
        LinkButton linkButton = new LinkButton()
        {
          PostBackUrl = "#"
        };
        linkButton.Attributes["onclick"] = Sitecore.StringExtensions.StringExtensions.FormatWith("javascript:return removeMapping({0})", new object[1]
        {
          (object) num1
        });
        linkButton.Controls.Add(image4);
        (gridPanel2).Controls.Add(linkButton);
        int num2 = num1 + 1;
      }
      return gridPanel2;
    }

    private void FillPassword(DropDownList passwordList, string defaultValue)
    {
      passwordList.Items.Add(new System.Web.UI.WebControls.ListItem(DependenciesManager.ResourceManager.Localize("COMMANDS"))
      {
        Attributes = {
          ["optgroup"] = "optgroup"
        }
      });
      System.Web.UI.WebControls.ListItem listItem1 = new System.Web.UI.WebControls.ListItem(DependenciesManager.ResourceManager.Localize("BLANK_PASSWORD"), "blankPassword");
      if (defaultValue == "blankPassword")
        listItem1.Selected = true;
      passwordList.Items.Add(listItem1);
      System.Web.UI.WebControls.ListItem listItem2 = new System.Web.UI.WebControls.ListItem(DependenciesManager.ResourceManager.Localize("RANDOM_PASSWORD"), "randomPassword");
      if (defaultValue == "randomPassword")
        listItem2.Selected = true;
      passwordList.Items.Add(listItem2);
      passwordList.Items.Add(new System.Web.UI.WebControls.ListItem(DependenciesManager.ResourceManager.Localize("FORM_FIELDS"))
      {
        Attributes = {
          ["optgroup"] = "optgroup"
        }
      });
      passwordList.Items.LoadItemsFromForm(this.CurrentForm).DisableAll();
      passwordList.Items.EnableFieldTypes(this.PasswordAllowedTypes);
      passwordList.Select(defaultValue);
    }

    private void FillProfiles(DropDownList profileList, string defaultValue)
    {
      profileList.Items.Clear();
      Dictionary<string, string> dictionary = new Dictionary<string, string>()
      {
        {
          "Full Name",
          DependenciesManager.ResourceManager.Localize("FULL_NAME")
        },
        {
          "Name",
          DependenciesManager.ResourceManager.Localize("NAME")
        },
        {
          "Email",
          DependenciesManager.ResourceManager.Localize("EMAIL")
        },
        {
          "Comment",
          DependenciesManager.ResourceManager.Localize("COMMENT")
        }
      };
      foreach (Sitecore.Data.Templates.TemplateField field in TemplateManager.GetTemplate(StaticSettings.CoreDatabase.GetItem(new FormItem(this.CurrentDatabase.GetItem(this.CurrentID)).ProfileItem).TemplateID, StaticSettings.CoreDatabase).GetFields(false))
      {
        if (!dictionary.ContainsKey(field.Name))
          dictionary.Add(field.Name, field.Name);
      }
      foreach (KeyValuePair<string, string> keyValuePair in dictionary)
      {
        System.Web.UI.WebControls.ListItem listItem = new System.Web.UI.WebControls.ListItem(keyValuePair.Value, keyValuePair.Key);
        if (string.IsNullOrEmpty(defaultValue) && profileList.Items.Count == 0)
          listItem.Selected = true;
        else if (listItem.Value == defaultValue)
          listItem.Selected = true;
        profileList.Items.Add(listItem);
      }
    }

    private void OnMapCallBack(object sender, CallBackEventArgs e)
    {
      this.Content.Controls.Clear();
      this.Content.Controls.Add(this.ReBuildMaping((string) e.Parameter));
      this.Content.RenderControl((HtmlTextWriter) e.Output);
      ((TextWriter) e.Output).Write("<script type=\"text/javascript\" language=\"javascript\">serializeMapping();</script>");
    }

    public string PasswordAllowedTypes => WebUtil.GetQueryString(nameof (PasswordAllowedTypes), "{1F09D460-200C-4C94-9673-488667FF75D1}|{1AD5CA6E-8A92-49F0-889C-D082F2849FBD}");
  }
}
