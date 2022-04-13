// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Shell.UI.Dialogs.SendMailEditor
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Diagnostics;
using Sitecore.Form.Core.Utility;
using Sitecore.Web;
using Sitecore.Web.UI.HtmlControls;
using Sitecore.Web.UI.Sheer;
using Sitecore.Web.UI.XmlControls;
using Sitecore.WFFM.Abstractions.Data;
using Sitecore.WFFM.Abstractions.Dependencies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sitecore.Forms.Shell.UI.Dialogs
{
  public class SendMailEditor : IdeHtmlEditorBase
  {
    public static readonly string CCKey = nameof (CC);
    public static readonly string BCCKey = nameof (BCC);
    public static readonly string MailKey = "Mail";
    public static readonly string SubjectKey = nameof (Subject);
    public static readonly string ToKey = nameof (To);
    public static readonly string FromKey = nameof (From);
    public static readonly string LocalFromKey = "LocalFrom";
    public static readonly string LocalizedKey = nameof (Localized);
    protected Sitecore.Web.UI.HtmlControls.Button Cancel;
    protected Edit CC;
    protected Edit BCC;
    protected Sitecore.Web.UI.HtmlControls.Button OK;
    protected Edit Subject;
    protected Edit To;
    protected Edit From;
    protected Checkbox Localized;
    protected GenericControl ToLink;
    protected GenericControl FromLink;
    protected GenericControl CCLink;
    protected GenericControl SubjectLink;
    protected Sitecore.Web.UI.HtmlControls.Image ToMenuImg;
    protected Sitecore.Web.UI.HtmlControls.Image FromMenuImg;
    protected Sitecore.Web.UI.HtmlControls.Image CCMenuImg;
    protected Sitecore.Web.UI.HtmlControls.Image SubjectMenuImg;
    protected Sitecore.Web.UI.HtmlControls.Literal ToLabel;
    protected Sitecore.Web.UI.HtmlControls.Literal FromLabel;
    protected Sitecore.Web.UI.HtmlControls.Literal CCLabel;
    protected Sitecore.Web.UI.HtmlControls.Literal BCCLabel;
    protected Sitecore.Web.UI.HtmlControls.Literal SubjectLabel;
    protected Sitecore.Web.UI.HtmlControls.Literal LocalizedLabel;
    protected ContextMenu ToContextMenu;
    protected ContextMenu FromContextMenu;
    protected ContextMenu CCContextMenu;
    protected ContextMenu SubjectContextMenu;
    protected XmlControl Dialog;
    protected bool IsLocalizedCheckedOnLoad;

    protected override void OnLoad(EventArgs e)
    {
      Sitecore.Web.WebUtil.SetSessionValue("hdl", (object) this.MailValue);
      base.OnLoad(e);
      if (this.OK != null)
        this.OK.OnClick += new EventHandler(this.OnOK);
      if (this.Cancel != null)
        this.Cancel.OnClick += new EventHandler(this.OnCancel);
      if (!Context.ClientPage.IsEvent)
      {
        this.FillContextMenu(this.ToContextMenu, this.AllowedToTypes, this.To, this.ToLink, this.ToMenuImg, this.ToLabel);
        this.FillContextMenu(this.FromContextMenu, this.AllowedFromTypes, this.From, this.FromLink, this.FromMenuImg, this.FromLabel);
        this.FillContextMenu(this.CCContextMenu, this.AllowedCCTypes, this.CC, this.CCLink, this.CCMenuImg, this.CCLabel);
        this.FillContextMenu(this.SubjectContextMenu, this.AllowedSubjectTypes, this.Subject, this.SubjectLink, this.SubjectMenuImg, this.SubjectLabel);
        ((Sitecore.Web.UI.HtmlControls.Control) this.To).Value = this.ToValue;
        ((Sitecore.Web.UI.HtmlControls.Control) this.From).Value = this.FromValue;
        ((Sitecore.Web.UI.HtmlControls.Control) this.CC).Value = this.CCValue;
        ((Sitecore.Web.UI.HtmlControls.Control) this.BCC).Value = this.BCCValue;
        ((Sitecore.Web.UI.HtmlControls.Control) this.Subject).Value = this.SubjectValue;
        ((Sitecore.Web.UI.HtmlControls.Control) this.Localized).Value = this.LocalizedValue;
        this.Localize();
        this.BuildUpClientDictionary();
      }
      this.IsLocalizedCheckedOnLoad = this.LocalizedValue == "1";
    }

    protected virtual void BuildUpClientDictionary() => ((Sitecore.Web.UI.HtmlControls.Page) Context.ClientPage).ClientScript.RegisterClientScriptBlock(((object) this).GetType(), "scWfmRadCommand", Sitecore.StringExtensions.StringExtensions.FormatWith("Sitecore.Wfm.EmailEditor.dictionary['Insert Field'] = \"{0}\";", new object[1]
    {
      (object) DependenciesManager.ResourceManager.Localize("INSERT_FIELD")
    }), true);

    protected virtual void Localize()
    {
      this.Dialog["Header"] = (object) DependenciesManager.ResourceManager.Localize("SEND_MAIL_EDITOR");
      this.Dialog["Text"] = (object) DependenciesManager.ResourceManager.Localize("CONFIGURE_THE_TEMPLATE_OF_YOUR_MAIL");
      this.ToLabel.Text = DependenciesManager.ResourceManager.Localize("TO");
      this.FromLabel.Text = DependenciesManager.ResourceManager.Localize("FROM");
      this.ToMenuImg.Alt = DependenciesManager.ResourceManager.Localize("INSERT_FIELDS");
      this.FromMenuImg.Alt = DependenciesManager.ResourceManager.Localize("INSERT_FIELDS");
      this.CCLabel.Text = DependenciesManager.ResourceManager.Localize("CC");
      this.CCMenuImg.Alt = DependenciesManager.ResourceManager.Localize("INSERT_FIELDS");
      this.BCCLabel.Text = DependenciesManager.ResourceManager.Localize("BCC");
      this.SubjectLabel.Text = DependenciesManager.ResourceManager.Localize("SUBJECT");
      this.SubjectMenuImg.Alt = DependenciesManager.ResourceManager.Localize("INSERT_FIELDS");
      this.LocalizedLabel.Text = DependenciesManager.ResourceManager.Localize("LOCALIZED");
      ((HeaderedItemsControl) this.DesignButton).Header = DependenciesManager.ResourceManager.Localize("DESIGN");
      ((HeaderedItemsControl) this.HtmlButton).Header = DependenciesManager.ResourceManager.Localize("HTML");
    }

    protected virtual void OnOK(object sender, EventArgs args)
    {
      this.SaveValues();
      if (this.IsLocalizedCheckedOnLoad && this.LocalizedValue != "1")
        this.NvParams.Add("deletelocalized", "1");
      string str = ParametersUtil.NameValueCollectionToXml(this.NvParams);
      if (str.Length == 0)
        str = "-";
      SheerResponse.SetDialogValue(str);
      this.OnCancel(sender, args);
    }

    protected override void SaveValues()
    {
      IFieldItem[] fields = this.FormItem.Fields;
      this.ToValue = this.ConvertToId(((Sitecore.Web.UI.HtmlControls.Control) this.To).Value, fields);
      this.FromValue = this.ConvertToId(((Sitecore.Web.UI.HtmlControls.Control) this.From).Value, fields);
      this.CCValue = this.ConvertToId(((Sitecore.Web.UI.HtmlControls.Control) this.CC).Value, fields);
      this.BCCValue = this.ConvertToId(((Sitecore.Web.UI.HtmlControls.Control) this.BCC).Value, fields);
      this.LocalFromValue = this.ConvertToId(((Sitecore.Web.UI.HtmlControls.Control) this.From).Value, fields);
      this.SubjectValue = this.ConvertToId(((Sitecore.Web.UI.HtmlControls.Control) this.Subject).Value, fields);
      this.LocalizedValue = this.ConvertToId(((Sitecore.Web.UI.HtmlControls.Control) this.Localized).Value, fields);
      this.Update();
      this.SetLongValue(SendMailEditor.MailKey, this.Body);
    }

    protected string ConvertToId(string value, IFieldItem[] fields)
    {
      if (!string.IsNullOrEmpty(value))
        value = Regex.Replace(value, "\\[[^\\]]*\\]", (MatchEvaluator) (match => this.ReplaceEvaluator(match, (IEnumerable<IFieldItem>) fields)), RegexOptions.IgnoreCase | RegexOptions.Singleline);
      return value;
    }

    private string ReplaceEvaluator(Match match, IEnumerable<IFieldItem> fields)
    {
      string str = match.Value.Substring(1, match.Value.Length - 2);
      foreach (IFieldItem field in fields)
      {
        if (str == field.Title || str == field.Name)
          return "[" + (object) field.ID + "]";
      }
      return match.Value;
    }

    protected virtual void OnCancel(object sender, EventArgs args)
    {
      Assert.ArgumentNotNull(sender, nameof (sender));
      Assert.ArgumentNotNull((object) args, nameof (args));
      SheerResponse.CloseWindow();
    }

    private void FillContextMenu(
      ContextMenu menu,
      string allowedTypes,
      Edit insertValueTo,
      GenericControl link,
      Sitecore.Web.UI.HtmlControls.Image img,
      Sitecore.Web.UI.HtmlControls.Literal label)
    {
      foreach (IFieldItem field in this.FormItem.Fields)
      {
        if ((string.IsNullOrEmpty(allowedTypes) || allowedTypes.Contains(((object) field.TypeID).ToString())) && !string.IsNullOrEmpty(field.Title))
          ((Sitecore.Web.UI.HtmlControls.Menu) menu).Add(field.Title, string.Empty, Sitecore.StringExtensions.StringExtensions.FormatWith("AddValue(\"{0}\", \"{1}\")", new object[2]
          {
            (object) field.ID,
            (object) ((Sitecore.Web.UI.HtmlControls.Control) insertValueTo).ID
          }));
      }
      if (((Sitecore.Web.UI.HtmlControls.Control) menu).Controls.Count != 0)
        return;
      ((WebControl) link).Attributes.Remove("href");
      ((WebControl) link).Attributes.Remove("onclick");
      ((WebControl) label).Style.Add("margin", "0 22 0 0");
      ((WebControl) img).Style.Add("display", "none");
    }

    protected void AddValue(string value, string id)
    {
      string str = string.Join(string.Empty, new string[3]
      {
        "[",
        ((IEnumerable<IFieldItem>) this.FormItem.Fields).First<IFieldItem>((Func<IFieldItem, bool>) (f => ((object) f.ID).ToString() == value)).Title,
        "]"
      });
      if (!(id == "To"))
      {
        if (!(id == "From"))
        {
          if (!(id == "CC"))
          {
            if (id == "Subject")
            {
              Edit subject = this.Subject;
              ((Sitecore.Web.UI.HtmlControls.Control) subject).Value = ((Sitecore.Web.UI.HtmlControls.Control) subject).Value + str;
              SheerResponse.SetOuterHtml(id, (Sitecore.Web.UI.HtmlControls.Control) this.Subject);
            }
          }
          else
          {
            this.SmartAdd(this.CC, str);
            SheerResponse.SetOuterHtml(id, (Sitecore.Web.UI.HtmlControls.Control) this.CC);
          }
        }
        else
        {
          this.SmartAdd(this.From, str);
          SheerResponse.SetOuterHtml(id, (Sitecore.Web.UI.HtmlControls.Control) this.From);
        }
      }
      else
      {
        this.SmartAdd(this.To, str);
        SheerResponse.SetOuterHtml(id, (Sitecore.Web.UI.HtmlControls.Control) this.To);
      }
      SheerResponse.Eval("scForm.browser.closePopups();if (Sitecore.Wfm.PopupMenu.activePopup != null && Sitecore.Wfm.PopupMenu.activePopup.parentNode != null) {$(Sitecore.Wfm.PopupMenu.activePopup).remove();}");
    }

    private void SmartAdd(Edit edit, string value)
    {
      if (string.IsNullOrEmpty(((Sitecore.Web.UI.HtmlControls.Control) edit).Value))
        ((Sitecore.Web.UI.HtmlControls.Control) edit).Value = value;
      else if (((Sitecore.Web.UI.HtmlControls.Control) edit).Value.EndsWith(" "))
      {
        Edit edit1 = edit;
        ((Sitecore.Web.UI.HtmlControls.Control) edit1).Value = ((Sitecore.Web.UI.HtmlControls.Control) edit1).Value + value;
      }
      else if (((Sitecore.Web.UI.HtmlControls.Control) edit).Value.EndsWith(";"))
      {
        Edit edit2 = edit;
        ((Sitecore.Web.UI.HtmlControls.Control) edit2).Value = ((Sitecore.Web.UI.HtmlControls.Control) edit2).Value + " " + value;
      }
      else
      {
        Edit edit3 = edit;
        ((Sitecore.Web.UI.HtmlControls.Control) edit3).Value = ((Sitecore.Web.UI.HtmlControls.Control) edit3).Value + "; " + value;
      }
    }

    public string AllowedToTypes => Sitecore.Web.WebUtil.GetQueryString(nameof (AllowedToTypes), "{84ABDA34-F9B1-4D3A-A69B-E28F39697069}");

    public string AllowedFromTypes => Sitecore.Web.WebUtil.GetQueryString(nameof (AllowedFromTypes), "{84ABDA34-F9B1-4D3A-A69B-E28F39697069}");

    public string AllowedCCTypes => Sitecore.Web.WebUtil.GetQueryString(nameof (AllowedCCTypes), "{84ABDA34-F9B1-4D3A-A69B-E28F39697069}");

    public string AllowedSubjectTypes => Sitecore.Web.WebUtil.GetQueryString(nameof (AllowedSubjectTypes), string.Empty);

    public string MailValue
    {
      get => this.GetValueByKey(SendMailEditor.MailKey);
      set => this.SetValue(SendMailEditor.MailKey, value);
    }

    public string SubjectValue
    {
      get => this.GetValueByKey(SendMailEditor.SubjectKey);
      set => this.SetValue(SendMailEditor.SubjectKey, value);
    }

    public string LocalizedValue
    {
      get => this.GetValueByKey(SendMailEditor.LocalizedKey);
      set => this.SetValue(SendMailEditor.LocalizedKey, value);
    }

    public string BCCValue
    {
      get => this.GetValueByKey(SendMailEditor.BCCKey);
      set => this.SetValue(SendMailEditor.BCCKey, value);
    }

    public string CCValue
    {
      get => this.GetValueByKey(SendMailEditor.CCKey);
      set => this.SetValue(SendMailEditor.CCKey, value);
    }

    public string ToValue
    {
      get => this.GetValueByKey(SendMailEditor.ToKey);
      set => this.SetValue(SendMailEditor.ToKey, value);
    }

    public string FromValue
    {
      get => this.GetValueByKey(SendMailEditor.FromKey);
      set => this.SetValue(SendMailEditor.FromKey, value);
    }

    public string LocalFromValue
    {
      get => this.GetValueByKey(SendMailEditor.LocalFromKey);
      set => this.SetValue(SendMailEditor.LocalFromKey, value);
    }
  }
}
