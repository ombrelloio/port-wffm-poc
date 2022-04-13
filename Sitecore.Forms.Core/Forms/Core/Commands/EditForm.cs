// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Core.Commands.EditForm
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Newtonsoft.Json;
using Sitecore.Data;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Form.Core.Configuration;
using Sitecore.Form.Core.Utility;
using Sitecore.Forms.Core.Commands.FormDesigner;
using Sitecore.Forms.Core.Data;
using Sitecore.Globalization;
using Sitecore.Layouts;
using Sitecore.Shell.Applications.WebEdit.Commands;
using Sitecore.Shell.Framework.Commands;
using Sitecore.Web;
using Sitecore.Web.UI.Sheer;
using Sitecore.WFFM.Abstractions.Dependencies;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Xml;

namespace Sitecore.Forms.Core.Commands
{
  [Serializable]
  public class EditForm : Command
  {
    public override void Execute(CommandContext context)
    {
      Assert.ArgumentNotNull((object) context, nameof (context));
      NameValueCollection nameValueCollection = new NameValueCollection();
      string str = context.Parameters["id"];
      bool flag = false;
      if (context.Items.Length != 0)
        flag = FormItem.IsForm(context.Items[0]);
      string formValue1 = Sitecore.Web.WebUtil.GetFormValue("scLayout");
      nameValueCollection["sclayout"] = formValue1;
      if (!flag && !string.IsNullOrEmpty(formValue1))
      {
        XmlDocument xmlDocument = JsonConvert.DeserializeXmlNode(formValue1);
        if (xmlDocument.DocumentElement != null)
        {
          string outerXml = xmlDocument.DocumentElement.OuterXml;
          string formValue2 = Sitecore.Web.WebUtil.GetFormValue("scDeviceID");
          ShortID shortId;
          if (ShortID.TryParse(formValue2, out shortId))
            formValue2 = ((object) shortId.ToID()).ToString();
          RenderingDefinition renderingByUniqueId = LayoutDefinition.Parse(outerXml).GetDevice(formValue2).GetRenderingByUniqueId(context.Parameters["referenceId"]);
          if (renderingByUniqueId != null)
          {
            Sitecore.Web.WebUtil.SetSessionValue(StaticSettings.Mode, (object) StaticSettings.DesignMode);
            if (!string.IsNullOrEmpty(renderingByUniqueId.Parameters))
              str = HttpUtility.UrlDecode(StringUtil.ParseNameValueCollection(renderingByUniqueId.Parameters, '&', '=')["FormID"]);
          }
        }
      }
      Sitecore.Web.WebUtil.SetSessionValue("PageDesigner", (object) JsonConvert.DeserializeXmlNode(formValue1).DocumentElement.OuterXml);
      if (string.IsNullOrEmpty(str))
        return;
      nameValueCollection["referenceid"] = context.Parameters["referenceId"];
      nameValueCollection["formId"] = str;
      nameValueCollection["checksave"] = context.Parameters["checksave"] ?? "1";
      if (context.Items.Length != 0)
        nameValueCollection["contentlanguage"] = ((object) context.Items[0].Language).ToString();
      Context.ClientPage.Start((object) this, "CheckChanges", new ClientPipelineArgs(nameValueCollection));
    }

    protected void CheckChanges(ClientPipelineArgs args)
    {
      Assert.ArgumentNotNull((object) args, nameof (args));
      if (args.IsPostBack)
      {
        if (!(args.Result == "yes"))
          return;
        args.Parameters["save"] = "1";
        args.IsPostBack = false;
        Context.ClientPage.Start((object) this, "Run", args);
      }
      else
      {
        bool flag = false;
        if (args.Parameters["checksave"] != "0" && this.GetModifiedFields(FormItem.GetForm(args.Parameters["formId"])).Count<PageEditorField>() > 0)
        {
          flag = true;
          SheerResponse.Confirm(DependenciesManager.ResourceManager.Localize("ONE_OR_MORE_ITEMS_HAVE_BEEN_CHANGED"));
          args.WaitForPostBack();
        }
        if (flag)
          return;
        args.IsPostBack = false;
        Context.ClientPage.Start((object) this, "Run", args);
      }
    }

    protected void Run(ClientPipelineArgs args)
    {
      Assert.ArgumentNotNull((object) args, nameof (args));
      if (!args.IsPostBack)
      {
        FormItem form = FormItem.GetForm(args.Parameters["formId"]);
        if (form == null || args.HasResult)
          return;
        if (args.Parameters["save"] == "1")
          this.SaveFields(form);
        string parameter = args.Parameters["referenceId"];
        NameValueCollection urlParameters = new NameValueCollection();
        urlParameters["formid"] = ((object) form.ID).ToString();
        urlParameters["mode"] = StaticSettings.DesignMode;
        urlParameters["db"] = form.Database.Name;
        urlParameters["vs"] = ((object) form.Version).ToString();
        urlParameters["referenceId"] = ((object) form.Version).ToString();
        urlParameters["la"] = args.Parameters["contentlanguage"] ?? form.Language.Name;
        if (args.Parameters["referenceId"] != null)
          urlParameters["hdl"] = parameter;
        new FormDialog(form, DependenciesManager.ResourceManager).ShowModalDialog(urlParameters);
        SheerResponse.DisableOutput();
        args.WaitForPostBack();
      }
      else
      {
        if (string.IsNullOrEmpty(args.Parameters["scLayout"]))
          return;
        SheerResponse.SetAttribute("scLayoutDefinition", "value", args.Parameters["scLayout"]);
        string str = args.Parameters["referenceId"];
        if (!string.IsNullOrEmpty(str))
          str = "r_" + (object) ID.Parse(str).ToShortID();
        string parameter = args.Parameters["formId"];
        ID id;
        if (ID.TryParse(parameter, out id))
          parameter = ((object) id.ToShortID()).ToString();
        SheerResponse.Eval("window.parent.Sitecore.PageModes.ChromeManager.fieldValuesContainer.children().each(function(e){ if( window.parent.$sc('#form_" + parameter.ToUpper() + "').find('#' + this.id + '_edit').size() > 0 ) { window.parent.$sc(this).remove() }});");
        SheerResponse.Eval("window.parent.Sitecore.PageModes.ChromeManager.handleMessage('chrome:rendering:propertiescompleted', {controlId : '" + str + "'});");
      }
    }

    private IEnumerable<PageEditorField> GetFields(
      NameValueCollection form)
    {
      Assert.ArgumentNotNull((object) form, nameof (form));
      List<PageEditorField> pageEditorFieldList = new List<PageEditorField>();
      foreach (string key in form.Keys)
      {
        if (!string.IsNullOrEmpty(key) && (key.StartsWith("fld_", StringComparison.InvariantCulture) || key.StartsWith("flds_", StringComparison.InvariantCulture)))
        {
          string str1 = key;
          string str2 = form[key];
          int num = str1.IndexOf('$');
          if (num >= 0)
            str1 = StringUtil.Left(str1, num);
          string[] strArray = str1.Split('_');
          ID id1 = ShortID.DecodeID(strArray[1]);
          ID id2 = ShortID.DecodeID(strArray[2]);
          Language language = Language.Parse(strArray[3]);
          Sitecore.Data.Version version = Sitecore.Data.Version.Parse(strArray[4]);
          string str3 = strArray[5];
          Item obj = Client.ContentDatabase.GetItem(id1);
          if (obj != null)
          {
            Field field = ((BaseItem) obj).Fields[id2];
            if (key.StartsWith("flds_", StringComparison.InvariantCulture))
            {
              str2 = (string)Sitecore.Web.WebUtil.GetSessionValue((HttpContext.Current.Handler as Page).Request.Form[key]);
              if (string.IsNullOrEmpty(str2))
                str2 = field.Value;
            }
            string typeKey = field.TypeKey;
            if (!(typeKey == "html") && !(typeKey == "rich text"))
            {
              if (!(typeKey == "text"))
              {
                if (typeKey == "multi-line text" || typeKey == "memo")
                  str2 = StringUtil.RemoveTags(str2.Replace("<br>", "\r\n").Replace("<br/>", "\r\n").Replace("<br />", "\r\n").Replace("<BR>", "\r\n").Replace("<BR/>", "\r\n").Replace("<BR />", "\r\n"));
              }
              else
                str2 = StringUtil.RemoveTags(str2);
            }
            else
              str2 = str2.TrimEnd(' ');
            PageEditorField pageEditorField = new PageEditorField()
            {
              ControlId = str1,
              FieldID = id2,
              ItemID = id1,
              Language = language,
              Revision = str3,
              Value = str2,
              Version = version
            };
            pageEditorFieldList.Add(pageEditorField);
          }
        }
      }
      return (IEnumerable<PageEditorField>) pageEditorFieldList;
    }

    private IEnumerable<PageEditorField> GetModifiedFields(FormItem form)
    {
      List<PageEditorField> pageEditorFieldList = new List<PageEditorField>();
      if (form != null)
      {
        foreach (PageEditorField field in this.GetFields(((Page) Context.ClientPage).Request.Form))
        {
          Item obj = StaticSettings.ContextDatabase.GetItem(field.ItemID);
          if (form.GetField(field.ItemID) != null || obj.ID == form.ID)
          {
            string strB = ((BaseItem) obj)[field.FieldID];
            string strA = field.Value;
            if (string.Compare(strA, strB, StringComparison.OrdinalIgnoreCase) != 0 && string.CompareOrdinal(strA.TrimWhiteSpaces(), strB.TrimWhiteSpaces()) != 0)
              pageEditorFieldList.Add(field);
          }
        }
      }
      return (IEnumerable<PageEditorField>) pageEditorFieldList;
    }

    private void SaveFields(FormItem form)
    {
      foreach (PageEditorField modifiedField in this.GetModifiedFields(form))
      {
        Item obj = StaticSettings.ContextDatabase.GetItem(modifiedField.ItemID);
        obj.Editing.BeginEdit();
        ((BaseItem) obj)[modifiedField.FieldID] = modifiedField.Value;
        obj.Editing.EndEdit();
      }
    }
  }
}
