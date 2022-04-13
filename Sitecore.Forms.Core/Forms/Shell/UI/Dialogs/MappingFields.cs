// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Shell.UI.Dialogs.MappingFields
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Configuration;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Form.Core.Configuration;
using Sitecore.Form.Core.Utility;
using Sitecore.Forms.Core.Data;
using Sitecore.Web;
using Sitecore.Web.UI.HtmlControls;
using Sitecore.Web.UI.Pages;
using Sitecore.Web.UI.Sheer;
using Sitecore.Web.UI.XmlControls;
using Sitecore.WFFM.Abstractions.Dependencies;
using System;
using System.Collections.Specialized;
using System.Text;
using System.Web;
using System.Web.UI;

namespace Sitecore.Forms.Shell.UI.Dialogs
{
  public class MappingFields : DialogForm
  {
    private static readonly string prefixKey = "mappingfields";
    private NameValueCollection nvParams;
    protected Border Content;
    protected XmlControl Dialog;

    protected override void OnLoad(EventArgs e)
    {
      base.OnLoad(e);
      if (Context.ClientPage.IsEvent)
        return;
      this.Localize();
      NameValueCollection fields = this.Fields;
      foreach (string allKey in fields.AllKeys)
      {
        ID fieldId;
        ID.TryParse(this.GetValueByKey(allKey), out fieldId);
        Border border = new Border();
        (border).Class = "scfFieldScope";
        Literal literal = new Literal(Translate.Text(fields[allKey]) + ":");
        (literal).Class = "scfFieldLabel";
        (border).Controls.Add(literal);
        (border).Controls.Add(this.GetDropDownContent(allKey, fieldId));
        (this.Content).Controls.Add(border);
      }
    }

    protected virtual void Localize()
    {
      this.Dialog["Header"] = (object) DependenciesManager.ResourceManager.Localize("SELECT_FIELDS_TO_USE");
      this.Dialog["Text"] = (object) DependenciesManager.ResourceManager.Localize("SET_FIELDS_WHERE_ACTION_WILL_TAKE_PARAMETERS");
    }

    protected override void OnOK(object sender, EventArgs args)
    {
      foreach (string allKey in this.Fields.AllKeys)
      {
        string str = Context.ClientPage.ClientRequest.Form[MappingFields.prefixKey + allKey];
        if (!string.IsNullOrEmpty(str))
          this.SetValue(allKey, str);
      }
      string str1 = ParametersUtil.NameValueCollectionToXml(this.nvParams ?? new NameValueCollection());
      if (str1.Length == 0)
        str1 = "-";
      SheerResponse.SetDialogValue(str1);
      this.OnCancel(sender, args);
    }

    private void SetValue(string key, string value)
    {
      if (this.nvParams == null)
        this.nvParams = StringUtil.GetNameValues(this.Params, '=', '&');
      this.nvParams[key] = value;
    }

    private string GetValueByKey(string key)
    {
      if (this.nvParams == null)
        this.nvParams = ParametersUtil.XmlToNameValueCollection(this.Params);
      return this.nvParams[key] ?? string.Empty;
    }

    private Literal GetDropDownContent(string name, ID fieldId)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.AppendFormat("<select id='{0}{1}' class='scfFieldSelect'>", (object) MappingFields.prefixKey, (object) name);
      stringBuilder.AppendFormat("{0}</select>", (object) this.RenderFields(name, fieldId));
      return new Literal(stringBuilder.ToString());
    }

    private string RenderFields(string selected, ID fieldId)
    {
      Assert.ArgumentNotNull((object) selected, nameof (selected));
      FormItem formItem = new FormItem(this.CurrentDatabase.GetItem(this.CurrentID));
      Assert.IsNotNull((object) formItem, typeof (Item), "Form  \"" + this.CurrentID + "\" not found", new object[0]);
      StringBuilder html = new StringBuilder();
      html.AppendFormat("<option selected=\"selected\"/>");
      if (formItem.HasSections)
      {
        foreach (Item child in formItem.InnerItem.Children)
        {
          if ((child.TemplateID==IDs.SectionTemplateID) && child.HasChildren)
          {
            html.Append("<optgroup label=\"" + child.DisplayName + "\">");
            this.RenderOptions(child, selected, fieldId, html);
            html.Append("</optgroup>");
          }
        }
      }
      else
        this.RenderOptions(formItem.InnerItem, selected, fieldId, html);
      return html.ToString();
    }

    private void RenderOptions(Item itemGroup, string selected, ID fieldId, StringBuilder html)
    {
      foreach (Item child in itemGroup.Children)
      {
        if ((child.TemplateID==IDs.FieldTemplateID))
        {
          FieldItem fieldItem = new FieldItem(child);
          string str = ((object) child.ID).ToString();
          html.AppendFormat("<option id=\"{0}\" value=\"{1}\" ", (object) selected, (object) str);
          if (ID.IsNullOrEmpty(fieldId))
          {
            if (string.Compare(child.Name, selected, StringComparison.OrdinalIgnoreCase) == 0)
              html.Append(" selected=\"selected\"");
          }
          else if ((child.ID==fieldId))
            html.Append(" selected=\"selected\"");
          html.Append(">" + fieldItem.FieldDisplayName + "</option>");
        }
      }
    }

    public string Params => HttpContext.Current.Session[Sitecore.Web.WebUtil.GetQueryString("params")] as string;

    public NameValueCollection Fields => StringUtil.ParseNameValueCollection(this.FieldsQs, ',', '|');

    public string FieldsQs => Sitecore.Web.WebUtil.GetQueryString("fields", string.Empty);

    public Database CurrentDatabase => Factory.GetDatabase(Sitecore.Web.WebUtil.GetQueryString("db"));

    public string CurrentID => Sitecore.Web.WebUtil.GetQueryString("id");
  }
}
