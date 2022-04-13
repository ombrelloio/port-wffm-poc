// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.Renderings.FormRender
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Configuration;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Form.Core.Configuration;
using Sitecore.Form.Core.Pipelines.RenderForm;
using Sitecore.Form.Core.Utility;
using Sitecore.Form.Web.UI.Controls;
using Sitecore.Forms.Core.Data;
using Sitecore.Layouts;
using Sitecore.Pipelines;
using Sitecore.Pipelines.ExecutePageEditorAction;
using Sitecore.Web;
using Sitecore.Web.UI;
using Sitecore.WFFM.Abstractions.Dependencies;
using System;
using System.IO;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sitecore.Form.Core.Renderings
{
  public class FormRender : Sitecore.Web.UI.WebControl, IPageEditorActionHandler
  {
    private SitecoreSimpleForm formControl;
    private string formID;

    public FormRender()
    {
    }

    public FormRender(string tag)
      : base(tag)
    {
    }

    public FormRender(SitecoreSimpleForm form)
    {
      this.FormID = ((object) form.FormID).ToString();
      this.DataSource = ((object) form.FormID).ToString();
      // ISSUE: explicit non-virtual call
      CssClass = form.CssClass;
      this.DisableWebEditing = form.DisableWebEditing;
      this.Parameters = form.Parameters;
      this.IsFastPreview = form.FastPreview;
      this.IsClearDepend = form.IsClearDepend;
      this.ReadQueryString = form.ReadQueryString ? "1" : "0";
      this.FormTemplate = form.AppRelativeVirtualPath;
    }

    public bool DisableWebEditing { get; set; }

    public string FormTemplate { get; set; }

    public bool IsClearDepend { get; set; }

    public bool IsFastPreview { get; set; }

    public string ReadQueryString { get; set; }

    protected string ActionName { get; private set; }

    protected RenderingReference RenderingReference { get; private set; }

    public string FormID
    {
      get
      {
        if (!string.IsNullOrEmpty(this.DataSource))
        {
          Item obj = StaticSettings.ContextDatabase.GetItem(this.DataSource);
          if (obj != null)
            return ((object) obj.ID).ToString();
        }
        return this.formID;
      }
      set => this.formID = value;
    }

    public UserControl FormControl => (UserControl) this.formControl;

    public Item Item => !string.IsNullOrEmpty(this.FormID) ? StaticSettings.ContextDatabase.GetItem(this.FormID) : (Item) null;

    protected override void OnInit(EventArgs e)
    {
      if (this.FormID != null && this.Item != null)
      {
        string str = "form_" + (object) this.Item.ID.ToShortID();
        if (!string.IsNullOrEmpty(this.FormTemplate))
        {
          try
          {
            this.formControl = (SitecoreSimpleForm)Sitecore.Form.Core.Utility.WebUtil.CreateUserControl(((Control) this).Page, this.FormTemplate);
          }
          catch (Exception ex)
          {
            DependenciesManager.Logger.Warn("Invalid form template", ex, (object) this);
          }
        }
        if (this.formControl == null)
          this.formControl = new SitecoreSimpleForm(this.Item);
        this.formControl.FormItem = new FormItem(this.Item);
        if (!string.IsNullOrEmpty(CssClass))
          this.formControl.CssClass = CssClass;
        this.formControl.ID = str;
        this.formControl.DisableWebEditing = this.DisableWebEditing;
        this.formControl.Parameters = this.Parameters;
        this.formControl.ReadQueryString = MainUtil.GetBool(this.ReadQueryString, false);
        this.formControl.FastPreview = this.IsFastPreview;
        this.formControl.IsClearDepend = this.IsClearDepend;
        ((Control) this).Controls.Add((Control) this.formControl);
        this.formControl.Page = ((Control) this).Page;
      }
      // ISSUE: explicit non-virtual call
      base.OnInit(e);
    }

    protected override void OnPreRender(EventArgs e)
    {
      // ISSUE: explicit non-virtual call
      base.OnPreRender(e);
      if (Sitecore.Context.Site.DisplayMode != Sites.DisplayMode.Edit)
        return;
      Item obj = this.Item;
      if (obj == null)
        return;
      RenderFormArgs renderFormArgs = new RenderFormArgs(obj)
      {
        Parameters = Sitecore.Web.WebUtil.ParseQueryString(this.Parameters),
        DisableWebEdit = this.DisableWebEditing
      };
      using (new LongRunningOperationWatcher(Sitecore.Configuration.Settings.Profiling.RenderFieldThreshold, "preRenderForm pipeline[id={0}]", new string[1]
      {
        ((object) obj.ID).ToString()
      }))
        CorePipeline.Run("preRenderForm", (PipelineArgs) renderFormArgs);
    }

    protected override void DoRender(HtmlTextWriter output)
    {
      Assert.ArgumentNotNull((object) output, nameof (output));
      if (this.formControl == null && ((Control) this).Controls.Count == 0)
        output.Write(this.RenderForm().ToString());
      if (this.formControl != null)
      {
        if (this.IsFastPreview)
        {
          output.Write("<div align=\"center\" style=\"width=100%;height=100%\">");
          output.Write("<div class=\"disabledFormOut\">");
          output.Write("<div class=\"disabledFormIn\">");
        }
        string str = this.RenderForm().ToString();
        output.Write(str);
        if (!this.IsFastPreview)
          return;
        output.Write("</div>");
        output.Write("</div>");
        output.Write("</div>");
      }
      else
      {
        if (((Control) this).Controls.Count <= 0)
          return;
        ((Control) this).Controls[0].RenderControl(output);
      }
    }

    public RenderFormResult RenderForm()
    {
      Item obj = this.Item;
      if (obj == null)
        return new RenderFormResult();
      RenderFormArgs renderFormArgs = new RenderFormArgs(obj)
      {
        Parameters = Sitecore.Web.WebUtil.ParseQueryString(this.Parameters),
        DisableWebEdit = this.DisableWebEditing
      };
      if (this.formControl != null)
      {
        HtmlTextWriter writer = new HtmlTextWriter((TextWriter) new StringWriter());
        if (this.RenderingReference != null && this.ActionName == "insert")
        {
          StringBuilder stringBuilder = new StringBuilder();
          stringBuilder.Append("var element = $sc('.sc-webform-openeditor');");
          stringBuilder.Append("if (element.length > 0) {");
          stringBuilder.Append("element.parents('.scPageDesignerControl:first').css('opacity', '1');");
          stringBuilder.Append("element.remove();");
          stringBuilder.AppendFormat("Sitecore.PageModes.PageEditor.postRequest('forms:edit(checksave=0,renderingId={0},referenceId={1},id={2})', null, true);", (object) this.RenderingReference.RenderingID, (object) this.RenderingReference.UniqueId, (object) this.FormID);
          stringBuilder.Append("}");
          this.formControl.Controls.Add((Control) new Literal()
          {
            Text = ("<img class='sc-webform-openeditor' src='/sitecore/images/blank.gif' style='display:none' width='1' height='1' onload=\"" + (object) stringBuilder + "\">")
          });
        }
        this.formControl.RenderControl(writer);
        renderFormArgs.Result.FirstPart += writer.InnerWriter.ToString();
      }
      return renderFormArgs.Result;
    }

    public void InitControls()
    {
      if (this.Item == null)
        return;
      OnInit(new EventArgs());
      if (this.formControl == null)
        return;
      this.formControl.Initialize();
    }

    public void ActionExecuted(RenderingReference renderingReference, string actionName)
    {
      this.RenderingReference = renderingReference;
      this.ActionName = actionName;
    }
  }
}
