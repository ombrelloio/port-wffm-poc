// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Shell.UI.Dialogs.UpdateContactDetailsPage
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Forms.Core.Data;
using Sitecore.Forms.Core.Data.Helpers;
using Sitecore.Web;
using Sitecore.Web.UI.HtmlControls;
using Sitecore.WFFM.Abstractions.Data;
using Sitecore.WFFM.Abstractions.Dependencies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Sitecore.Forms.Shell.UI.Dialogs
{
  public class UpdateContactDetailsPage : EditorBase
  {
    private const string MappingKey = "Mapping";
    protected PlaceHolder Content;
    protected Checkbox OverwriteContact;
    protected HtmlInputHidden MappedFields;
    protected Groupbox UserProfileGroupbox;
    private string FormFieldColumnHeader;
    private string ContactDetailsColumnHeader;

    public string RestrictedFieldTypes => WebUtil.GetQueryString(nameof (RestrictedFieldTypes), "{1F09D460-200C-4C94-9673-488667FF75D1}|{1AD5CA6E-8A92-49F0-889C-D082F2849FBD}|{7FB270BE-FEFC-49C3-8CB4-947878C099E5}");

    protected override void OnInit(EventArgs e)
    {
      this.MappedFields.Value = this.GetValueByKey("Mapping");
      base.OnInit(e);
    }

    protected override void OnLoad(EventArgs e)
    {
      base.OnLoad(e);
      if (Sitecore.Context.ClientPage.IsEvent)
        return;
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append(" if (typeof ($scw) === \"undefined\") {");
      stringBuilder.Append(" window.$scw = jQuery.noConflict(true); }");
      stringBuilder.Append(" $scw(document).ready(function () {");
      stringBuilder.Append(string.Format(" var treeData = $scw.parseJSON('{0}');", (object) ContactFacetsHelper.GetContactFacetsXmlTree()));
      stringBuilder.Append(string.Format(" var fieldsData = $scw.parseJSON('{0}');", (object) this.GetFieldsData(this.RestrictedFieldTypes)));
      stringBuilder.Append(string.Format(" var selectedDataValue = $scw(\"#{0}\").val();", (object) this.MappedFields.ClientID));
      stringBuilder.Append(" var selectedData = [];");
      stringBuilder.Append(" if(selectedDataValue) {");
      stringBuilder.Append(" selectedData = $scw.parseJSON(selectedDataValue); }");
      stringBuilder.Append(" $scw(\"#treeMap\").droptreemap({");
      stringBuilder.Append(" treeData: treeData.Top,");
      stringBuilder.Append(" selected: selectedData,");
      stringBuilder.Append(" listData: fieldsData,");
      stringBuilder.Append(string.Format(" fieldsHeader: \"{0}\",", (object) this.FormFieldColumnHeader));
      stringBuilder.Append(string.Format(" mappedKeysHeader: \"{0}\",", (object) this.ContactDetailsColumnHeader));
      stringBuilder.Append(string.Format(" addFieldTitle: \"{0}\",", (object) DependenciesManager.ResourceManager.Localize("ADD_FIELD")));
      stringBuilder.Append(" change: function (value) {");
      stringBuilder.Append(string.Format(" $scw(\"#{0}\").val(value);", (object) this.MappedFields.ClientID));
      stringBuilder.Append("} });   });");
      (this).Page.ClientScript.RegisterClientScriptBlock(((object) this).GetType(), "sc_wffm_update_contact" + (this).ClientID, stringBuilder.ToString(), true);
    }

    protected string GetFieldsData(string restrictedTypes = "") => "[" + string.Join(",", ((IEnumerable<IFieldItem>) new FormItem(this.CurrentDatabase.GetItem(this.CurrentID, this.CurrentLanguage)).Fields).Where<IFieldItem>((Func<IFieldItem, bool>) (property => string.IsNullOrEmpty(restrictedTypes) || !restrictedTypes.Contains(((object) property.TypeID).ToString()))).ToDictionary<IFieldItem, string, string>((Func<IFieldItem, string>) (property => ((object) property.ID).ToString()), (Func<IFieldItem, string>) (property => property.Title)).Select<KeyValuePair<string, string>, string>((Func<KeyValuePair<string, string>, string>) (d => "{\"id\":\"" + d.Key.Trim('{', '}') + "\",\"title\":\"" + d.Value + "\"}"))) + "]";

    protected override void Localize()
    {
      this.Header = DependenciesManager.ResourceManager.Localize("UPDATE_CONTACT_HEADER");
      this.Text = DependenciesManager.ResourceManager.Localize("UPDATE_CONTACT_DESCRIPTION");
      this.FormFieldColumnHeader = DependenciesManager.ResourceManager.Localize("FORM_FIELD");
      this.ContactDetailsColumnHeader = DependenciesManager.ResourceManager.Localize("CONTACT_DETAILS");
    }

    protected override void SaveValues()
    {
      base.SaveValues();
      this.SetValue("Mapping", this.MappedFields.Value);
    }
  }
}
