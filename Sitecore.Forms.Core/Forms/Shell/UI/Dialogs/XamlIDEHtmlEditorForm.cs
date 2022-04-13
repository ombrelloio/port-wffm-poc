// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Shell.UI.Dialogs.XamlIDEHtmlEditorForm
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Form.Web.UI.Controls;
using Sitecore.Forms.Core.Data;
using Sitecore.IO;
using Sitecore.Pipelines;
using Sitecore.Pipelines.LoadHtml;
using Sitecore.Security.Accounts;
using Sitecore.Shell.Applications.Layouts.IDE.Editors.HTML;
using Sitecore.Text;
using Sitecore.Web;
using Sitecore.Web.UI.HtmlControls;
using Sitecore.Web.UI.Sheer;
using Sitecore.WFFM.Abstractions.Dependencies;
using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.UI;

namespace Sitecore.Forms.Shell.UI.Dialogs
{
  public class XamlIDEHtmlEditorForm : EditorBase, IMessageHandler
  {
    private readonly XamlIDEHtmlEditorForm.IDEHtmlEditor baseForm = new XamlIDEHtmlEditorForm.IDEHtmlEditor();
    protected Toolbutton CodeButton;
    protected Memo CodeEditor;
    protected Border CodeEditorPane;
    protected Toolbutton ContentButton;
    protected Frame ContentEditor;
    protected TreePicker DataSource;
    protected Toolbutton DesignButton;
    protected Frame Editor;
    protected Toolbutton GridButton;
    protected Frame GridDesigner;
    protected Toolbutton HtmlButton;
    protected Memo HtmlEditor;
    protected Border HtmlEditorPane;
    protected Toolbutton LiveButton;

    protected override void OnInit(EventArgs e)
    {
      this.baseForm.CodeButton = this.CodeButton;
      this.baseForm.CodeEditor = this.CodeEditor;
      this.baseForm.CodeEditorPane = this.CodeEditorPane;
      this.baseForm.ContentButton = this.ContentButton;
      this.baseForm.ContentEditor = this.ContentEditor;
      this.baseForm.DataSource = this.DataSource;
      this.baseForm.DesignButton = this.DesignButton;
      this.baseForm.Editor = this.Editor;
      this.baseForm.GridButton = this.GridButton;
      this.baseForm.GridDesigner = this.GridDesigner;
      this.baseForm.HtmlButton = this.HtmlButton;
      this.baseForm.HtmlEditor = this.HtmlEditor;
      this.baseForm.HtmlEditorPane = this.HtmlEditorPane;
      this.baseForm.LiveButton = this.LiveButton;
      base.OnInit(e);
    }

    protected override void OnLoad(EventArgs e)
    {
      this.baseForm.OnLoad(e);
      (this.baseForm.HtmlEditorPane).Controls.Add(new Literal("<input ID=\"__Field\" Type=\"hidden\" value=\"" + this.GetFields() + "\" />"));
      base.OnLoad(e);
      if ((this).Page.IsPostBack)
        return;
      this.BuildUpClientDictionary();
    }

    protected virtual void BuildUpClientDictionary() => (this).Page.ClientScript.RegisterClientScriptBlock(((object) this).GetType(), "scWfmRadCommand", Sitecore.StringExtensions.StringExtensions.FormatWith("Sitecore.Wfm.EmailEditor.dictionary['Insert Field'] = \"{0}\";", new object[1]
    {
      (object) DependenciesManager.ResourceManager.Localize("INSERT_FIELD")
    }), true);

    protected override void Localize()
    {
      base.Localize();
      ((HeaderedItemsControl) this.DesignButton).Header = DependenciesManager.ResourceManager.Localize("DESIGN");
      ((HeaderedItemsControl) this.HtmlButton).Header = DependenciesManager.ResourceManager.Localize("HTML");
    }

    protected override void OnPreRender(EventArgs e)
    {
      this.baseForm.OnPreRender(e);
      base.OnPreRender(e);
    }

    public void HandleMessage(Message message) => ((BaseForm) this.baseForm).HandleMessage(message);

    public void ReadState() => this.baseForm.ReadState();

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
            assembly = Assembly.LoadFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin", field.AssemblyName));
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

    public bool EventsEnabled
    {
      get => ((BaseForm) this.baseForm).EventsEnabled;
      set => ((BaseForm) this.baseForm).EventsEnabled = value;
    }

    public StateBag ServerProperties => ((BaseForm) this.baseForm).ServerProperties;

    public string Body
    {
      get => this.baseForm.Body;
      set => this.baseForm.Body = value;
    }

    public long HtmlCrc
    {
      get => this.baseForm.HtmlCrc;
      set => this.baseForm.HtmlCrc = value;
    }

    public string Footer
    {
      get => this.baseForm.Footer;
      set => this.baseForm.Footer = value;
    }

    public bool Changed
    {
      get => this.baseForm.Changed;
      set => this.baseForm.Changed = value;
    }

    public bool SupportWebControls
    {
      get => this.baseForm.SupportWebControls;
      set => this.baseForm.SupportWebControls = value;
    }

    private class IDEHtmlEditor : IDEHtmlEditorForm
    {
      public new void OnLoad(EventArgs e) => base.OnLoad(e);

     public new void OnPreRender(EventArgs e) => base.OnPreRender(e);

      protected override void InitializeEditor()
      {
        string sessionString = WebUtil.GetSessionString("hdl");
        WebUtil.RemoveSessionValue("hdl");
        string uniqueId = Sitecore.Web.UI.HtmlControls.Control.GetUniqueID("H");
        UrlString urlString = new UrlString("/sitecore/shell/Controls/Rich Text Editor/Default.aspx");
        urlString.Append("hdl", uniqueId);
        urlString.Append("da", Sitecore.Context.Database.Name);
        urlString.Append("us", ((Account) Sitecore.Context.User).Name);
        urlString.Append("la", WebUtil.GetQueryString("la"));
        urlString.Append("so", WebUtil.GetQueryString("so", "/sitecore/system/Settings/Html Editor Profiles/Rich Text Mail"));
        urlString.Append("id", WebUtil.GetQueryString("id"));
        urlString.Append("mo", "Editor");
        urlString.Append("sc_hidetrace", "1");
        urlString.Append("sc_hideprof", "1");
        ((Frame) this.Editor).SourceUri = ((object) urlString).ToString();
        LoadHtmlArgs loadHtmlArgs = new LoadHtmlArgs(sessionString, "control:IDEHtmlEditorControl");
        PipelineFactory.GetPipeline("uiLoadHtml").Start((PipelineArgs) loadHtmlArgs);
        this.Header = loadHtmlArgs.Header;
        this.Footer = loadHtmlArgs.Footer;
        HttpContext.Current.Session[uniqueId] = (object) loadHtmlArgs.Body;
        (this.HtmlEditor).Value = this.Header + loadHtmlArgs.RawBody + this.Footer;
        this.HtmlCrc = Crc.CRC(loadHtmlArgs.RawBody);
        if (Sitecore.Context.Database.GetItem(WebUtil.GetQueryString("so", "/sitecore/system/Settings/Html Editor Profiles/Rich Text Mail") + "/Buttons/HTML View") != null)
          return;
        (this.HtmlButton).Visible = false;
      }

      public void ReadState() => this.Update();

      public Frame Editor
      {
        get => (Frame) this.Editor;
        set => this.Editor = value;
      }

      public Memo CodeEditor
      {
        get => (Memo) this.CodeEditor;
        set => this.CodeEditor = value;
      }

      public Frame ContentEditor
      {
        get => (Frame) this.ContentEditor;
        set => this.ContentEditor = value;
      }

      public Frame GridDesigner
      {
        get => (Frame) this.GridDesigner;
        set => this.GridDesigner = value;
      }

      public Border HtmlEditorPane
      {
        get => (Border) this.HtmlEditorPane;
        set => this.HtmlEditorPane = value;
      }

      public Border CodeEditorPane
      {
        get => (Border) this.CodeEditorPane;
        set => this.CodeEditorPane = value;
      }

      public Toolbutton DesignButton
      {
        get => (Toolbutton) this.DesignButton;
        set => this.DesignButton = value;
      }

      public Toolbutton HtmlButton
      {
        get => (Toolbutton) this.HtmlButton;
        set => this.HtmlButton = value;
      }

      public Memo HtmlEditor
      {
        get => (Memo) this.HtmlEditor;
        set => this.HtmlEditor = value;
      }

      public Toolbutton CodeButton
      {
        get => (Toolbutton) this.CodeButton;
        set => this.CodeButton = value;
      }

      public Toolbutton ContentButton
      {
        get => (Toolbutton) this.ContentButton;
        set => this.ContentButton = value;
      }

      public Toolbutton GridButton
      {
        get => (Toolbutton) this.GridButton;
        set => this.GridButton = value;
      }

      public Toolbutton LiveButton
      {
        get => (Toolbutton) this.LiveButton;
        set => this.LiveButton = value;
      }

      public TreePicker DataSource
      {
        get => (TreePicker) this.DataSource;
        set => this.DataSource = value;
      }
    }
  }
}
