// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Shell.UI.Dialogs.IdeHtmlEditorBase
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Configuration;
using Sitecore.Data;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Form.Core.Configuration;
using Sitecore.Form.Core.Utility;
using Sitecore.Form.Web.UI.Controls;
using Sitecore.Forms.Core.Data;
using Sitecore.Globalization;
using Sitecore.IO;
using Sitecore.Pipelines;
using Sitecore.Security.Accounts;
using Sitecore.Shell.Applications.Layouts.IDE.Editors.HTML;
using Sitecore.Shell.Controls.RichTextEditor.Pipelines.LoadRichTextContent;
using Sitecore.Text;
using Sitecore.Web;
using Sitecore.Web.UI.HtmlControls;
using Sitecore.WFFM.Abstractions.Dependencies;
using System;
using System.Collections.Specialized;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;

namespace Sitecore.Forms.Shell.UI.Dialogs
{
  public abstract class IdeHtmlEditorBase : IDEHtmlEditorForm
  {
    protected Border HtmlEditorPane;
    protected NameValueCollection NvParams;

    public void SetLongValue(string key, string value)
    {
      string sessionKey = SessionUtil.GetSessionKey();
      HttpContext.Current.Session[sessionKey] = (object) value;
      this.SetValue(key, sessionKey);
    }

    public string GetValueByKey(string key)
    {
      if (this.NvParams == null)
        this.NvParams = this.ConvertParameters(ParametersUtil.XmlToNameValueCollection(this.Params));
      return this.NvParams[key] ?? string.Empty;
    }

    public void SetValue(string key, string value)
    {
      if (this.NvParams == null)
        this.NvParams = StringUtil.GetNameValues(this.Params, '=', '&');
      this.NvParams[key] = value;
    }

    protected NameValueCollection ConvertParameters(
      NameValueCollection parameters)
    {
      char[] chArray = new char[1]{ '|' };
      foreach (string name in "to|cc|bcc|from|subject".Split(chArray))
      {
        string str = this.ConvertIdToValue(parameters.Get(name));
        if (!string.IsNullOrEmpty(str))
          parameters.Set(name, str);
      }
      return parameters;
    }

    protected string ConvertIdToValue(string valueWithId)
    {
      if (!string.IsNullOrEmpty(valueWithId))
        valueWithId = Regex.Replace(valueWithId, "\\{([^}]*)\\}", new MatchEvaluator(this.ReplaceEvaluator), RegexOptions.IgnoreCase | RegexOptions.Singleline);
      return valueWithId;
    }

    private string ReplaceEvaluator(Match match)
    {
      Assert.IsNotNull((object) match, nameof (match));
      string str = match.Value;
      if (!ID.IsID(str))
        return str;
      Item obj = this.CurrentDatabase.GetItem(str, this.CurrentLanguage);
      if (obj == null)
        return str;
      Field field = ((BaseItem) obj).Fields[Sitecore.Form.Core.Configuration.FieldIDs.FieldTitleID];
      if (field == null)
        return str;
      return !string.IsNullOrEmpty(field.Value) ? ((BaseItem) obj).Fields[Sitecore.Form.Core.Configuration.FieldIDs.FieldTitleID].Value : obj.Name;
    }

    protected override void InitializeEditor()
    {
      string sessionString = Sitecore.Web.WebUtil.GetSessionString("hdl");
      Sitecore.Web.WebUtil.RemoveSessionValue("hdl");
      string uniqueId = Sitecore.Web.UI.HtmlControls.Control.GetUniqueID("H");
      UrlString urlString = new UrlString("/sitecore/shell/Controls/Rich Text Editor/Default.aspx");
      urlString.Append("hdl", uniqueId);
      urlString.Append("da", Context.Database.Name);
      urlString.Append("us", ((Account) Context.User).Name);
      urlString.Append("la", Sitecore.Web.WebUtil.GetQueryString("la"));
      urlString.Append("so", Sitecore.Web.WebUtil.GetQueryString("so", "/sitecore/system/Settings/Html Editor Profiles/Rich Text Mail"));
      urlString.Append("id", Sitecore.Web.WebUtil.GetQueryString("id"));
      urlString.Append("mo", "Editor");
      urlString.Append("sc_hidetrace", "1");
      urlString.Append("sc_hideprof", "1");
      ((Frame) this.Editor).SourceUri = ((object) urlString).ToString();
      LoadRichTextContentArgs richTextContentArgs = new LoadRichTextContentArgs(sessionString);
      PipelineFactory.GetPipeline("loadRichTextContent").Start((PipelineArgs) richTextContentArgs);
      this.Header = string.Empty;
      this.Footer = string.Empty;
      HttpContext.Current.Session[uniqueId] = (object) richTextContentArgs.Content;
      (this.HtmlEditor).Value = richTextContentArgs.Content;
      this.HtmlCrc = Crc.CRC(richTextContentArgs.Content);
      if (Context.Database.GetItem(Sitecore.Web.WebUtil.GetQueryString("so", "/sitecore/system/Settings/Html Editor Profiles/Rich Text Mail") + "/Buttons/HTML View") == null)
        (this.HtmlButton).Visible = false;
      (this.HtmlEditorPane).Controls.Add(new Literal("<input ID=\"__Field\" Type=\"hidden\" value=\"" + this.GetFields() + "\" />"));
    }

    protected string GetFields()
    {
      FormItem formItem = new FormItem(this.CurrentDatabase.GetItem(this.CurrentID, this.CurrentLanguage));
      StringBuilder stringBuilder = new StringBuilder();
      foreach (FieldItem field in formItem.Fields)
      {
        if (!string.IsNullOrEmpty(field.Title))
        {
          Assembly assembly = (Assembly) null;
          try
          {
            assembly = Assembly.LoadFile(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin", field.AssemblyName));
          }
          catch (Exception ex)
          {
            Log.Error(ex.Message, ex);
          }
          if (assembly != (Assembly) null && assembly.GetType(field.ClassName).IsSubclassOf(typeof (ListControl)))
          {
            string str1 = DependenciesManager.ResourceManager.Localize("TEXT");
            string str2 = DependenciesManager.ResourceManager.Localize("VALUE");
            stringBuilder.AppendFormat("{0}={1}={2}&", (object) ((CustomItemBase) field).ID, (object) string.Format("{0} {1}", (object) str1, (object) field.Title), (object) "Text");
            stringBuilder.AppendFormat("{0}={1}={2}&", (object) ((CustomItemBase) field).ID, (object) string.Format("{0} {1}", (object) str2, (object) field.Title), (object) "Value");
          }
          else
            stringBuilder.AppendFormat("{0}={1}&", (object) ((CustomItemBase) field).ID, (object) field.Title);
        }
      }
      return stringBuilder.Length <= 0 ? string.Empty : stringBuilder.ToString(0, stringBuilder.Length - 1);
    }

    public Database CurrentDatabase => Factory.GetDatabase(Sitecore.Web.WebUtil.GetQueryString("db"));

    public string CurrentID => Sitecore.Web.WebUtil.GetQueryString("id");

    public string Params => HttpContext.Current.Session[Sitecore.Web.WebUtil.GetQueryString("params")] as string;

    public FormItem FormItem => new FormItem(this.CurrentDatabase.GetItem(this.CurrentID, this.CurrentLanguage));

    public Language CurrentLanguage => Language.Parse(Sitecore.Web.WebUtil.GetQueryString("la"));

    protected abstract void SaveValues();
  }
}
