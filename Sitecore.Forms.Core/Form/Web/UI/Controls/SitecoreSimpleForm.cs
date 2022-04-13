// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Web.UI.Controls.SitecoreSimpleForm
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Form.Core.Ascx.Controls;
using Sitecore.Form.Core.Configuration;
using Sitecore.Form.Core.Data;
using Sitecore.Form.Core.Pipelines.FormSubmit;
using Sitecore.Form.Core.Utility;
using Sitecore.Forms.Core.Data;
using Sitecore.Forms.Core.Rules;
using Sitecore.Pipelines;
using Sitecore.Reflection;
using Sitecore.Web;
using Sitecore.WFFM.Abstractions.Actions;
using Sitecore.WFFM.Abstractions.ContentEditor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sitecore.Form.Web.UI.Controls
{
  [ToolboxData("<div runat=\"server\"></div>")]
  [PersistChildren(true)]
  public class SitecoreSimpleForm : SimpleForm
  {
    public static readonly string PrefixSubmitID = "_submit";
    protected SubmitSummary submitSummary;
    protected FormFooter footer;
    protected FormIntroduction intro;
    protected Sitecore.Form.Web.UI.Controls.FormSubmit submit;
    public ValidationSummary summary;
    protected FormTitle title;
    protected Panel fieldContainer;

    public SitecoreSimpleForm()
    {
    }

    public SitecoreSimpleForm(Item item)
    {
      Assert.IsNotNull((object) item, nameof (item));
      Assert.IsTrue(item.TemplateID == IDs.FormTemplateID, "This item is not a form");
      this.FormItem = new FormItem(item);
      this.Parameters = string.Empty;
    }

    public FormItem FormItem { get; protected internal set; }

    internal void Initialize()
    {
      this.CallRecursive((Control) this, "OnInit");
      this.CallRecursive((Control) this, "OnLoad");
      this.ClearDepend();
      this.CallRecursive((Control) this, "OnPreRender");
    }

    internal virtual void CallRecursive(Control container, string method)
    {
      try
      {
        ReflectionUtil.CallMethod((object) container, method, true, true, new object[1]
        {
          (object) EventArgs.Empty
        });
      }
      catch
      {
      }
      foreach (Control control in container.Controls)
        this.CallRecursive(control, method);
    }

    protected override void OnInit(EventArgs e)
    {
      if (this.Page == null)
      {
        this.Page = Sitecore.Form.Core.Utility.WebUtil.GetPage();
        ReflectionUtils.SetField(typeof (Page), (object) this.Page, "_enableEventValidation", (object) false);
      }
      this.Page.EnableViewState = true;
      base.OnInit(e);
      ThemesManager.RegisterCssScript(this.Page, this.FormItem.InnerItem, Sitecore.Context.Item);
      this.title = new FormTitle(this.FormItem.InnerItem);
      this.title.DisableWebEditing = this.DisableWebEditing;
      this.title.Parameters = this.Parameters;
      this.title.FastPreview = this.FastPreview;
      this.Controls.Add((Control) this.title);
      this.intro = new FormIntroduction(this.FormItem.InnerItem);
      this.intro.DisableWebEditing = this.DisableWebEditing;
      this.intro.Parameters = this.Parameters;
      this.intro.FastPreview = this.FastPreview;
      this.Controls.Add((Control) this.intro);
      this.submit = new Sitecore.Form.Web.UI.Controls.FormSubmit(this.FormItem.InnerItem);
      this.submit.ID = this.ID + SitecoreSimpleForm.PrefixSubmitID;
      this.submit.DisableWebEditing = this.DisableWebEditing;
      this.submit.Parameters = this.Parameters;
      this.submit.FastPreview = this.FastPreview;
      this.submit.ValidationGroup = this.submit.ID;
      this.submit.Click += OnClick;
      if (!this.FastPreview)
      {
        this.summary = new ValidationSummary();
        this.summary.ID = SimpleForm.prefixSummaryID;
        this.summary.ClientIDMode = ClientIDMode.Predictable;
        this.summary.ValidationGroup = this.submit.ID;
        this.summary.CssClass = "scfValidationSummary";
        this.Controls.Add((Control) this.summary);
      }
      this.submitSummary = new SubmitSummary();
      this.submitSummary.ID = SimpleForm.prefixErrorID;
      this.submitSummary.CssClass = "scfSubmitSummary";
      this.Controls.Add((Control) this.submitSummary);
      this.fieldContainer = new Panel();
      this.Controls.Add((Control) this.fieldContainer);
      this.Expand();
      this.footer = new FormFooter(this.FormItem.InnerItem);
      this.footer.DisableWebEditing = this.DisableWebEditing;
      this.footer.Parameters = this.Parameters;
      this.footer.FastPreview = this.FastPreview;
      this.Controls.Add((Control) this.footer);
      this.Controls.Add((Control) this.submit);
    }

    protected override void OnLoad(EventArgs e)
    {
      base.OnLoad(e);
      this.fieldContainer.DefaultButton = this.Submit.ID;
    }

    protected override void OnPreRender(EventArgs e)
    {
      if (this.FindControl("formreference") == null)
      {
        HiddenField hiddenField = new HiddenField();
        hiddenField.ID = "formreference";
        hiddenField.Value = this.ID;
        this.Controls.AddAt(0, (Control) hiddenField);
      }
      base.OnPreRender(e);
    }

    protected void Expand()
    {
      Item[] sections = this.FormItem.Sections;
      if (sections.Length == 1 && sections[0].TemplateID != IDs.SectionTemplateID)
      {
        FormSection formSection = new FormSection(sections[0], this.FormItem[((object) sections[0].ID.ToShortID()).ToString()], false, this.Submit.ID, this.FastPreview)
        {
          ReadQueryString = this.ReadQueryString,
          DisableWebEditing = this.DisableWebEditing,
          RenderingParameters = this.Parameters
        };
        ReflectionUtils.SetXmlProperties((object) formSection, ((BaseItem) sections[0])[Sitecore.Form.Core.Configuration.FieldIDs.FieldParametersID], true);
        ReflectionUtils.SetXmlProperties((object) formSection, ((BaseItem) sections[0])[Sitecore.Form.Core.Configuration.FieldIDs.FieldLocalizeParametersID], true);
        this.FieldContainer.Controls.Add((Control) formSection);
      }
      else
      {
        foreach (Item section in sections)
        {
          FormSection formSection = new FormSection(section, this.FormItem[((object) section.ID.ToShortID()).ToString()], true, this.Submit.ID, this.FastPreview)
          {
            ReadQueryString = this.ReadQueryString,
            DisableWebEditing = this.DisableWebEditing
          };
          ReflectionUtils.SetXmlProperties((object) formSection, ((BaseItem) section)[Sitecore.Form.Core.Configuration.FieldIDs.FieldParametersID], true);
          ReflectionUtils.SetXmlProperties((object) formSection, ((BaseItem) section)[Sitecore.Form.Core.Configuration.FieldIDs.FieldLocalizeParametersID], true);
          Rule.Run(((BaseItem) section)[Sitecore.Form.Core.Configuration.FieldIDs.ConditionsFieldID], (Control) formSection);
          this.FieldContainer.Controls.Add((Control) formSection);
        }
      }
    }

    private void ClearDepend()
    {
      if (this.IsClearDepend)
      {
        IListDefinition actionsDefinition = this.FormItem.ActionsDefinition;
        if (actionsDefinition.Groups.Any<IGroupDefinition>() && actionsDefinition.Groups.First<IGroupDefinition>().ListItems.Any<IListItemDefinition>())
        {
          foreach (IGroupDefinition group in actionsDefinition.Groups)
          {
            foreach (IListItemDefinition listItem in group.ListItems)
            {
              Item obj = this.FormItem.Database.GetItem(listItem.ItemID);
              if (obj != null)
              {
                ActionControl actionControl = new ActionControl();
                actionControl.Value = listItem.Parameters;
                actionControl.ActionID = ((object) obj.ID).ToString();
                actionControl.ID = "a_" + listItem.Unicid;
                this.Controls.Add((Control) actionControl);
              }
            }
          }
        }
        HiddenField hiddenField = new HiddenField();
        hiddenField.Value = HttpUtility.HtmlEncode(this.FormItem.SuccessRedirect ? Sitecore.Web.WebUtil.GetFullUrl(this.FormItem.SuccessPage.Url) : this.FormItem.SuccessMessage);
        hiddenField.ID = this.ID + SimpleForm.prefixSuccessMessageID;
        this.Controls.Add((Control) hiddenField);
      }
      else
        Sitecore.Form.Core.Utility.WebUtil.ExecuteForAllControls((Control) this, (Action<Control>) (control =>
        {
          if (control is WebControl)
            (control as WebControl).Enabled = false;
          if (!(control is BaseValidator))
            return;
          (control as BaseValidator).Visible = false;
        }));
    }

    protected override void CollectActions(Control source, List<IActionDefinition> list)
    {
      IListDefinition actionsDefinition = this.FormItem.ActionsDefinition;
      if (!actionsDefinition.Groups.Any<IGroupDefinition>())
        return;
      foreach (IGroupDefinition group in actionsDefinition.Groups)
        list.AddRange(group.ListItems.Select<IListItemDefinition, IActionDefinition>((Func<IListItemDefinition, IActionDefinition>) (li => (IActionDefinition) new ActionDefinition(li.ItemID, li.Parameters)
        {
          UniqueKey = li.Unicid
        })));
    }

    protected override void OnRefreshError(string[] messages)
    {
      Assert.ArgumentNotNull((object) messages, nameof (messages));
      Control control = this.FindControl(this.ID + SimpleForm.prefixErrorID);
      if (control == null || !(control is SubmitSummary))
        return;
      SubmitSummary submitSummary = (SubmitSummary) control;
      submitSummary.Messages = messages;
      if (submitSummary.Messages.Length == 0)
        return;
      this.SetFocus(control.ClientID, (string) null);
    }

    protected override void OnSuccessSubmit()
    {
      this.Controls.Clear();
      Literal literal = new Literal();
      SubmitSuccessArgs submitSuccessArgs = new SubmitSuccessArgs(this.FormItem);
      CorePipeline.Run("successAction", (PipelineArgs) submitSuccessArgs);
      literal.Text = submitSuccessArgs.Result;
      this.Controls.Add((Control) literal);
      this.SetFocus(this.ID, (string) null);
    }

    protected virtual Control FieldContainer => (Control) this.fieldContainer;

    protected virtual FormTitle Title => this.title;

    protected virtual FormIntroduction Intro => this.intro;

    protected virtual FormFooter Footer => this.footer;

    protected virtual Sitecore.Form.Web.UI.Controls.FormSubmit Submit => this.submit;

    protected virtual SubmitSummary SubmitSummary => this.submitSummary;

    public string Class
    {
      get => this.Attributes["class"];
      set => this.Attributes["class"] = value;
    }

    [Browsable(false)]
    public bool IsClearDepend { get; set; }

    [Browsable(false)]
    public bool DisableWebEditing { get; set; }

    [Browsable(false)]
    public string Parameters { get; set; }

    public override ID FormID => this.FormItem.ID;

    public bool ReadQueryString { get; set; }
  }
}
