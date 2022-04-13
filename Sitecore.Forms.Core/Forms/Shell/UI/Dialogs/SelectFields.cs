// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Shell.UI.Dialogs.SelectFields
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Form.Core.Utility;
using Sitecore.Forms.Core.Data;
using Sitecore.Globalization;
using Sitecore.Text;
using Sitecore.Web;
using Sitecore.Web.UI.HtmlControls;
using Sitecore.Web.UI.Pages;
using Sitecore.Web.UI.Sheer;
using Sitecore.Web.UI.XmlControls;
using Sitecore.WFFM.Abstractions.Dependencies;
using System;
using System.Collections.Specialized;
using System.Text;
using System.Web.UI;

namespace Sitecore.Forms.Shell.UI.Dialogs
{
  public class SelectFields : DialogForm
  {
    protected Border Content;
    private string idPrefix = "checkbox_";
    protected XmlControl Dialog;

    protected override void OnLoad(EventArgs e)
    {
      base.OnLoad(e);
      if (Context.ClientPage.IsEvent)
        return;
      this.Dialog["Header"] = (object) DependenciesManager.ResourceManager.Localize("SELECT_COLUMNS");
      this.Dialog["Text"] = (object) DependenciesManager.ResourceManager.Localize("SELECT_COLUMNS_THAT_YOU_WNAT_TO_SEE");
      if (string.IsNullOrEmpty(this.CurrentItemID))
      {
        UrlHandle urlHandle = UrlHandle.Get();
        if (urlHandle == null)
          return;
        if (!string.IsNullOrEmpty(urlHandle["title"]))
          this.Dialog["Header"] = (object) urlHandle["title"];
        if (!string.IsNullOrEmpty(urlHandle["text"]))
          this.Dialog["Text"] = (object) urlHandle["text"];
        this.AddCollecitontoList(ParametersUtil.XmlToNameValueCollection(StringUtil.GetString(new string[2]
        {
          urlHandle["values"],
          string.Empty
        }), true));
      }
      else
        this.AddChildItemsToList();
    }

    protected void AddCollecitontoList(NameValueCollection collection)
    {
      bool flag = false;
      foreach (string key in collection.Keys)
      {
        if (key.StartsWith(Sitecore.WFFM.Abstractions.Constants.Core.Constants.OptGroupPrefix))
        {
          if (flag)
            (this.Content).Controls.Add(new Literal("</div>"));
          (this.Content).Controls.Add(this.GetCheckbox(key, collection[key], false, "scSfCheckboxBorder scfHeader", "javascript:return changeState(this, event);"));
          flag = true;
          (this.Content).Controls.Add(new Literal("<div class='scfOptGroup'>"));
        }
        else
          (this.Content).Controls.Add(this.GetCheckbox(key, collection[key], false, "scSfCheckboxBorder", flag ? "javascript:return updateGroup(this, event)" : ""));
      }
      if (!flag)
        return;
      (this.Content).Controls.Add(new Literal("</div>"));
    }

    protected void AddChildItemsToList()
    {
      FormItem formItem = new FormItem(this.CurrentItem);
      string str = DependenciesManager.FormRegistryUtil.GetString(this.RegistryPrefix, this.CurrentItemID, string.Empty);
      foreach (string field in this.Fields)
      {
        string[] strArray = field.Split('_');
        if (strArray.Length > 1)
        {
          bool check = str.IndexOf(strArray[0]) == -1;
          (this.Content).Controls.Add(this.GetCheckbox(strArray[0], strArray[1], check, "scSfCheckboxBorder"));
        }
      }
      foreach (FieldItem field in formItem.Fields)
      {
        bool check = str.IndexOf(((object) ((CustomItemBase) field).ID).ToString()) == -1;
        (this.Content).Controls.Add(this.GetCheckbox(((object) ((CustomItemBase) field).ID).ToString(), field.FieldDisplayName, check, "scSfCheckboxBorder"));
      }
    }

    protected override void OnOK(object sender, EventArgs args)
    {
      string selectedItems = this.GetSelectedItems();
      SheerResponse.SetDialogValue(selectedItems == string.Empty ? " " : selectedItems);
      base.OnOK(sender, args);
    }

    private Literal GetCheckbox(
      string id,
      string name,
      bool check,
      string className,
      string onClick)
    {
      Literal literal = new Literal();
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.AppendFormat("<a href='#' class='{0}' onclick=\"Sitecore.Wfm.Utils.select(this, '{1}{2}')\">", (object) className, (object) this.idPrefix, (object) id);
      stringBuilder.AppendFormat("<input id='{0}{1}' class='{2}' type='checkbox' {3} value='{4}' ", (object) this.idPrefix, (object) id, (object) "scSfCheckbox", check ? (object) "CHECKED" : (object) string.Empty, check ? (object) "on" : (object) string.Empty);
      if (!string.IsNullOrEmpty(onClick))
        stringBuilder.AppendFormat(" onclick=\"{0}\"", (object) onClick);
      stringBuilder.Append(" />");
      stringBuilder.AppendFormat("<label for='{0}{1}'class='{2}' onclick=\"Sitecore.Wfm.Utils.select(this.parentNode, '{0}{1}')\">{3}</label>", (object) this.idPrefix, (object) id, (object) "scSfCheckboxLabel", (object) name);
      stringBuilder.Append("</a>");
      stringBuilder.Append("<br/>");
      literal.Text = stringBuilder.ToString();
      return literal;
    }

    private Literal GetCheckbox(string id, string name, bool check, string className)
    {
      Literal literal = new Literal();
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.AppendFormat("<a href='#' class='{0}' onclick=\"Sitecore.Wfm.Utils.select(this, '{1}{2}')\">", (object) className, (object) this.idPrefix, (object) id);
      stringBuilder.AppendFormat("<input id='{0}{1}' class='{2}' type='checkbox' {3} value='{4}' />", (object) this.idPrefix, (object) id, (object) "scSfCheckbox", check ? (object) "CHECKED" : (object) string.Empty, check ? (object) "on" : (object) string.Empty);
      stringBuilder.AppendFormat("<label for='{0}{1}'class='{2}' onclick=\"Sitecore.Wfm.Utils.select(this.parentNode, '{0}{1}')\">{3}</label>", (object) this.idPrefix, (object) id, (object) "scSfCheckboxLabel", (object) name);
      stringBuilder.Append("</a>");
      stringBuilder.Append("<br/>");
      literal.Text = stringBuilder.ToString();
      return literal;
    }

    protected string GetSelectedItems()
    {
      NameValueCollection form = Context.ClientPage.ClientRequest.Form;
      return (!string.IsNullOrEmpty(this.CurrentItemID) ? (object) this.GetFormRestriction(form) : (object) this.GetSimpleRestriction(form)).ToString();
    }

    private ListString GetSimpleRestriction(NameValueCollection collection)
    {
      ListString listString = new ListString();
      foreach (string key in collection.Keys)
      {
        if (!string.IsNullOrEmpty(key) && key.StartsWith(this.idPrefix) && !key.StartsWith(this.idPrefix + Sitecore.WFFM.Abstractions.Constants.Core.Constants.OptGroupPrefix))
          listString.Add(key.Remove(0, this.idPrefix.Length));
      }
      return listString;
    }

    private ListString GetFormRestriction(NameValueCollection collection)
    {
      FormItem formItem = new FormItem(this.CurrentItem);
      ListString listString = new ListString();
      foreach (FieldItem field in formItem.Fields)
      {
        if (collection[this.idPrefix + (object) ((CustomItemBase) field).ID] == null)
          listString.Add(((object) ((CustomItemBase) field).ID).ToString());
      }
      foreach (string field in this.Fields)
      {
        string[] strArray = field.Split('_');
        if (collection[this.idPrefix + strArray[0]] == null)
          listString.Add(strArray[0]);
      }
      return listString;
    }

    private ListString Fields => new ListString(Sitecore.Web.WebUtil.GetQueryString("fields"));

    private string RegistryPrefix => Sitecore.Web.WebUtil.GetQueryString("prefix");

    private Language CurrentLanguage => Language.Parse(Sitecore.Web.WebUtil.GetQueryString("la"));

    private string CurrentDatabase => Sitecore.Web.WebUtil.GetQueryString("db");

    private Sitecore.Data.Version CurrentVersion => Sitecore.Data.Version.Parse(Sitecore.Web.WebUtil.GetQueryString("vs"));

    private string CurrentItemID => Sitecore.Web.WebUtil.GetQueryString("id");

    private Item CurrentItem => Database.GetItem(new ItemUri(this.CurrentItemID, this.CurrentLanguage, this.CurrentVersion, this.CurrentDatabase));
  }
}
