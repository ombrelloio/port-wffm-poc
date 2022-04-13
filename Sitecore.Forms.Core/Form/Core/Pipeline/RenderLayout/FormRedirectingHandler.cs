// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.Pipeline.RenderLayout.FormRedirectingHandler
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Form.Core.Ascx.Controls;
using Sitecore.Form.Core.Configuration;
using Sitecore.Form.Core.Data;
using Sitecore.Form.Core.Renderings;
using Sitecore.Form.Core.Submit;
using Sitecore.Form.Core.Utility;
using Sitecore.Form.Web.UI.Controls;
using Sitecore.Forms.Core.Data;
using Sitecore.Forms.Core.Data.Serialization;
using Sitecore.Pipelines.RenderLayout;
using Sitecore.Web;
using Sitecore.Web.UI.WebControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sitecore.Form.Core.Pipeline.RenderLayout
{
  public class FormRedirectingHandler : RenderLayoutProcessor
  {
    public override void Process(RenderLayoutArgs args)
    {
      Assert.ArgumentNotNull((object) args, nameof (args));
      string empty = string.Empty;
      if (string.IsNullOrEmpty(empty))
        return;
      SimpleForm form = this.GetForm(empty);
      if (string.IsNullOrEmpty(Sitecore.Web.WebUtil.GetQueryString(SimpleForm.FormRedirectingPlaceholderKey)))
      {
        Context.Page.Page.PreRenderComplete += new EventHandler(new FormRedirectingHandler.FormReplacer((Control) null, form).OnPreRenderComplete);
      }
      else
      {
        Placeholder placeholder = ((IEnumerable<Placeholder>) Context.Page.Placeholders).FirstOrDefault<Placeholder>((Func<Placeholder, bool>) (p => p.Key == Sitecore.Web.WebUtil.GetQueryString(SimpleForm.FormRedirectingPlaceholderKey, "content")));
        if (placeholder == null)
          return;
        if (form != null)
        {
          if (!Context.Page.Page.IsPostBack)
          {
            if (string.IsNullOrEmpty(Sitecore.Web.WebUtil.GetQueryString(SimpleForm.FormRedirectingPreviousPageKey)))
              this.HandleReturning(empty, form);
            else
              this.HandleRedirecting(form, placeholder);
          }
          else
            this.HandleRedirectingPostBack(form, placeholder);
        }
        else
          this.HandleRobotsRedirecting(empty, form, placeholder);
      }
    }

    private SimpleForm GetForm(string formId)
    {
      FormState state = StorageUtil.GetValue<FormState>(formId);
      if (state == null)
        return (SimpleForm) null;
      Item innerItem = StaticSettings.ContextDatabase.GetItem(state.FormItemId);
      SitecoreSimpleForm sitecoreSimpleForm = state.IsSubmitted ? new SitecoreSimpleForm() : new SitecoreSimpleForm(innerItem);
      sitecoreSimpleForm.Page = Context.Page.Page;
      if (!state.IsSubmitted)
      {
        sitecoreSimpleForm.SetChildState(state.ControlResults);
        sitecoreSimpleForm.ID = state.ID;
        sitecoreSimpleForm.IsClearDepend = true;
        sitecoreSimpleForm.Load += new EventHandler(new FormRedirectingHandler.FormReplacer((Control) null, (SimpleForm) sitecoreSimpleForm, state).OnLoadFormState);
        sitecoreSimpleForm.SucceedSubmit += new EventHandler<EventArgs>(new FormRedirectingHandler.FormReplacer((Control) null, (SimpleForm) sitecoreSimpleForm, state).OnSucceedSubmit);
      }
      else
        sitecoreSimpleForm.Controls.Add((Control) new Literal()
        {
          Text = new FormItem(innerItem).SuccessMessage
        });
      return (SimpleForm) sitecoreSimpleForm;
    }

    private void HandleRedirectingPostBack(SimpleForm simpleForm, Placeholder placeholder) => this.RestoreFormOnPostBack(placeholder, simpleForm, false);

    private void HandleRedirecting(SimpleForm simpleForm, Placeholder placeholder) => Context.Page.Page.PreRenderComplete += new EventHandler(new FormRedirectingHandler.FormReplacer(this.RestoreFormOnPostBack(placeholder, simpleForm, false), simpleForm).OnPreRenderComplete);

    private void HandleReturning(string formId, SimpleForm simpleForm)
    {
      StorageUtil.ClearValue(formId);
      Context.Page.Page.PreRenderComplete += new EventHandler(new FormRedirectingHandler.FormReplacer((Control) null, simpleForm).OnPreRenderComplete);
    }

    private void HandleRobotsRedirecting(
      string formId,
      SimpleForm simpleForm,
      Placeholder placeholder)
    {
      StorageUtil.ClearValue(formId);
      this.RestoreFormOnPostBack(placeholder, simpleForm, true);
    }

    private Control RestoreFormOnPostBack(
      Placeholder placeholder,
      SimpleForm form,
      bool isRobot)
    {
      FormRender renderer;
      if (form is SitecoreSimpleForm)
      {
        renderer = new FormRender((SitecoreSimpleForm) form);
      }
      else
      {
        renderer = new FormRender()
        {
          FormID = Sitecore.Web.WebUtil.GetQueryString(SimpleForm.FormRedirectingFormIdKey)
        };
        if (renderer.Item == null)
        {
          Control child = ((Control) placeholder).Page.LoadControl(form.AppRelativeVirtualPath);
          child.ID = form != null ? form.ID : string.Empty;
          renderer.Controls.Add(child);
        }
      }
      if (renderer != null)
      {
        ((Control) placeholder).Controls.Add((Control) renderer);
        if (!string.IsNullOrEmpty(Sitecore.Web.WebUtil.GetQueryString(SimpleForm.FormRedirectingPreviousPageKey)))
        {
          ((Control) placeholder).Page.InitComplete += new EventHandler(new FormRedirectingHandler.PageEventCompleted(new Action<FormRender, string, DateTime>(this.RestorePreviouseState), renderer, form != null ? form.ID : string.Empty)
          {
            BasePageTime = form != null ? form.RenderedTime : DateTime.MinValue
          }.OnEventCompleted);
          if (isRobot)
            ((Control) placeholder).Page.InitComplete += new EventHandler(new FormRedirectingHandler.PageEventCompleted(new Action<FormRender, string, DateTime>(this.RestorePreviouseStateForRobot), renderer, form != null ? form.ID : string.Empty).OnEventCompleted);
        }
      }
      return (Control) renderer;
    }

    private void RestorePreviouseStateForRobot(
      FormRender renderer,
      string formControlID,
      DateTime baseTime)
    {
      SimpleForm activeForm = (SimpleForm) ((Control) renderer).Controls[0];
      activeForm.ID = formControlID;
      activeForm.PreRender += (EventHandler) ((o, e) => activeForm.Page.Validators.ForEach((Action<IValidator>) (v =>
      {
        if (!(v is IAttackProtection) || ((IAttackProtection) v).Type != ProtectionType.Robot)
          return;
        v.IsValid = false;
      })));
    }

    private void RestorePreviouseState(
      FormRender renderer,
      string formControlID,
      DateTime baseTime)
    {
      SimpleForm control = (SimpleForm) ((Control) renderer).Controls[0];
      control.ID = formControlID;
      control.IsTresholdRedirect = true;
      control.RenderedTime = baseTime;
            Sitecore.Form.Core.Utility.WebUtil.ExecuteForAllControls((Control) renderer, (Action<Control>) (c =>
      {
        if (!(c is UserControl))
          return;
        UserControl userControl = (UserControl) c;
        if (string.IsNullOrEmpty(userControl.Attributes["AttackProtection"]))
          return;
        userControl.Attributes["AttackProtection"] = "1";
      }));
      string queryString = Sitecore.Web.WebUtil.GetQueryString(SimpleForm.FormRedirectingPreviousPageItemKey);
      if (!string.IsNullOrEmpty(queryString) && ID.IsID(queryString))
        control.PageItem = StaticSettings.ContextDatabase.GetItem(queryString);
      control.SucceedValidation += new EventHandler<EventArgs>(new FormRedirector(Sitecore.Web.WebUtil.GetQueryString(SimpleForm.FormRedirectingPreviousPageKey), (string) null, (string) null).OnSubmit);
    }

    private class FormReplacer
    {
      public FormReplacer(Control stateForm, SimpleForm previousForm, FormState state = null)
      {
        Assert.ArgumentNotNull((object) previousForm, "previouseForm");
        this.StateForm = stateForm;
        this.PreviouseForm = previousForm;
        this.ControlsState = state;
      }

      public SimpleForm PreviouseForm { get; private set; }

      public Control StateForm { get; private set; }

      public FormState ControlsState { get; private set; }

      public void OnPreRenderComplete(object sender, EventArgs e)
      {
        Page page = (Page) sender;
        Control control1;
        int index;
        if (this.StateForm == null)
        {
          this.StateForm = Sitecore.Web.WebUtil.FindControl((Control) page, this.PreviouseForm.ID);
          control1 = this.StateForm.Parent is FormRender ? this.StateForm.Parent.Parent : this.StateForm.Parent;
          Control control2 = this.StateForm.Parent is FormRender ? this.StateForm.Parent : this.StateForm;
          index = control1.Controls.IndexOf(control2);
          control1.Controls.Remove(control2);
        }
        else
        {
          control1 = this.StateForm.Parent;
          index = this.StateForm.Parent.Controls.IndexOf(this.StateForm);
          this.StateForm.Parent.Controls.Remove(this.StateForm);
        }
        control1.Controls.AddAt(index, (Control) this.PreviouseForm);
      }

      public void OnLoadFormState(object sender, EventArgs e)
      {
        if (!(sender is SimpleForm simpleForm))
          return;
        simpleForm.SetChildState(this.ControlsState.ControlResults);
      }

      public void OnSucceedSubmit(object sender, EventArgs e)
      {
        string queryString = Sitecore.Web.WebUtil.GetQueryString(SimpleForm.FormRedirectingHandlerKey);
        if (string.IsNullOrEmpty(queryString))
          return;
        FormState formState = StorageUtil.GetValue<FormState>(queryString);
        formState.IsSubmitted = true;
        StorageUtil.SetValue(queryString, (object) formState);
      }
    }

    private class PageEventCompleted
    {
      public PageEventCompleted(
        Action<FormRender, string, DateTime> action,
        FormRender renderer,
        string formControlID)
      {
        Assert.ArgumentNotNull((object) action, nameof (action));
        this.Action = action;
        this.Renderer = renderer;
        this.FormControlID = formControlID;
      }

      public Action<FormRender, string, DateTime> Action { get; set; }

      public FormRender Renderer { get; set; }

      public string FormControlID { get; set; }

      public DateTime BasePageTime { get; set; }

      public void OnEventCompleted(object sender, EventArgs e) => this.Action(this.Renderer, this.FormControlID, this.BasePageTime);
    }
  }
}
