// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Shell.UI.Dialogs.SmartDialogPage
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Configuration;
using Sitecore.Controls;
using Sitecore.Data;
using Sitecore.Form.Core.Utility;
using Sitecore.Globalization;
using Sitecore.Web;
using Sitecore.Web.UI.HtmlControls;
using Sitecore.Web.UI.XamlSharp.Xaml;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace Sitecore.Forms.Shell.UI.Dialogs
{
  public abstract class SmartDialogPage : DialogPage
  {
    protected NameValueCollection nvParams;

    protected override void OnLoad(EventArgs e)
    {
      base.OnLoad(e);
      if ((this).Page.IsPostBack)
        return;
      this.Localize();
    }

    protected virtual void Localize()
    {
    }

    public string GetValueByKey(string key) => this.GetValueByKey(key, (string) null);

    public string GetValueByKey(string key, string defaultValue)
    {
      if (this.nvParams == null)
        this.nvParams = ParametersUtil.XmlToNameValueCollection(this.Params, this.UseUrlCoding);
      return this.nvParams[key] ?? defaultValue;
    }

    public void SetLongValue(string key, string value)
    {
      string sessionKey = SessionUtil.GetSessionKey();
      (this).Page.Session[sessionKey] = (object) value;
      this.SetValue(key, sessionKey);
    }

    public void SetValue(string key, string value)
    {
      if (this.nvParams == null)
        this.nvParams = ParametersUtil.XmlToNameValueCollection(this.Params, this.UseUrlCoding);
      this.nvParams[key] = value;
    }

    protected override void OK_Click()
    {
      this.SaveValues();
      string str = string.Empty;
      if (this.nvParams != null)
      {
        str = ParametersUtil.NameValueCollectionToXml(this.nvParams, this.UseUrlCoding);
        if (str.Length == 0)
          str = "-";
      }
      XamlControl.AjaxScriptManager.SetDialogValue(str);
      base.OK_Click();
    }

    protected virtual void SaveValues()
    {
    }

    protected override void OnPreRender(EventArgs e)
    {
      ScriptFile scriptFile = ((IEnumerable<ScriptFile>) (Sitecore.Context.ClientPage).PageScriptManager.ScriptFiles).FirstOrDefault<ScriptFile>((Func<ScriptFile, bool>) (f => f.Key == "BaseDialogPage"));
      if (scriptFile != null)
        scriptFile.Block = "document.observe('keydown', function() { if (window.event.keyCode == 13) { var ctl = window.event.srcElement; if (ctl != null && ctl.onclick != null) {  ctl.click(); }  } if (window.event.keyCode == 27) {var cancel = $('Cancel'); if (cancel != null) { cancel.click(); } } });";
      // ISSUE: explicit non-virtual call
      base.OnPreRender(e);
    }

    public virtual Database CurrentDatabase => Factory.GetDatabase(Sitecore.Web.WebUtil.GetQueryString("db"));

    public virtual string CurrentID => Sitecore.Web.WebUtil.GetQueryString("id");

    public virtual Language CurrentLanguage => Language.Parse(Sitecore.Web.WebUtil.GetQueryString("la", "en"));

    public virtual string Params => HttpContext.Current.Session[Sitecore.Web.WebUtil.GetQueryString("params")] as string;

    public virtual bool UseUrlCoding => false;
  }
}
