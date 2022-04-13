// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Shell.UI.Controls.FormBuilder
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Data;
using Sitecore.Diagnostics;
using Sitecore.Form.Core.Data;
using Sitecore.Form.Core.Utility;
using Sitecore.Form.Core.Web;
using Sitecore.Form.Web.UI.Controls;
using Sitecore.Forms.Core.Data;
using Sitecore.Forms.Core.Rules;
using Sitecore.Web;
using Sitecore.Web.UI.HtmlControls;
using Sitecore.Web.UI.Sheer;
using Sitecore.Web.UI.WebControls;
using Sitecore.WFFM.Abstractions.Analytics;
using Sitecore.WFFM.Abstractions.Dependencies;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sitecore.Forms.Shell.UI.Controls
{
  public class FormBuilder : Sitecore.Web.UI.HtmlControls.Control
  {
    public static readonly string PropertyBuilderID = "PropertyBuilder";
    public static readonly string ScrollBoxID = nameof (FormPanel);
    public static readonly string TableBuilderID = nameof (FormBuilder);
    protected GridPanel DesktopPanel;
    protected Scrollbox FormPanel;
    protected Border FormTablePanel;
    protected Border Properties;
    protected Border PropertiesPanel;
    protected FormTable TableBuilder;
    private readonly IAnalyticsSettings analyticsSettings;

    public FormBuilder() => this.analyticsSettings = DependenciesManager.Resolve<IAnalyticsSettings>();

    protected override void OnLoad(EventArgs e)
    {
      Assert.ArgumentNotNull((object) e, nameof (e));
      base.OnLoad(e);
      if (!Sitecore.Context.ClientPage.IsEvent) 
      {
        Border border = new Border();
        border.Controls.Add((System.Web.UI.Control) new Sitecore.Web.UI.HtmlControls.Literal("<input ID=\"form\" Type=\"hidden\" value='" + DependenciesManager.ConvertionUtil.ConvertToJson((object) this.GetFormModel(true)) + "'/>"));
        border.Controls.Add((System.Web.UI.Control) new Sitecore.Web.UI.HtmlControls.Literal("<input ID=\"Structure\" Type=\"hidden\" />"));
        border.Controls.Add((System.Web.UI.Control) new Sitecore.Web.UI.HtmlControls.Literal("<input ID=\"AddNewSectionText\" Value=\"" + FormSection.EmptySectionName + "\" Type=\"hidden\" />"));
        border.Controls.Add((System.Web.UI.Control) new Sitecore.Web.UI.HtmlControls.Literal("<input ID=\"AddNewFieldText\" Value=\"" + FormField.EmptyFieldName + "\" Type=\"hidden\" />"));
        border.Controls.Add((System.Web.UI.Control) new Sitecore.Web.UI.HtmlControls.Literal("<input ID=\"FormID\" Value=\"" + (this.Form.FormID ?? string.Empty) + "\" Type=\"hidden\" />"));
        border.Controls.Add((System.Web.UI.Control) new Sitecore.Web.UI.HtmlControls.Literal("<input ID=\"Caption\" Value=\"" + (this.Form.DisplayName ?? string.Empty) + "\" Type=\"hidden\" />"));
        border.Controls.Add((System.Web.UI.Control) new Sitecore.Web.UI.HtmlControls.Literal("<input ID=\"Active\" Type=\"hidden\" />"));
        Controls.Add((System.Web.UI.Control) border);
        FormTable formTable = new FormTable();
        formTable.ID = FormBuilder.TableBuilderID;
        formTable.CssClass = "scFbTable";
        this.TableBuilder = formTable;
        Controls.Add((System.Web.UI.Control) this.TableBuilder);
        if (this.UriItem != null)
          this.TableBuilder.XmlDefinition = this.Form.ToXml();
        else
          this.TableBuilder.XmlDefinition = "<sitecore><section name=\"Data\" /></sitecore>";
      }
      else
      {
        this.FormPanel = ((System.Web.UI.Control) this).FindControl(FormBuilder.ScrollBoxID) as Scrollbox;
        this.TableBuilder = ((System.Web.UI.Control) this).FindControl(FormBuilder.TableBuilderID) as FormTable;
      }
    }

    public void UpgradeToSection(string id)
    {
      this.TableBuilder.UpdateBuilder();
      this.TableBuilder.Reorganize(true, id);
            System.Web.UI.Control emptySection = FormDesignerUtils.GetEmptySection(((object) ShortID.NewId()).ToString());
      ((System.Web.UI.Control) emptySection).Controls.Add((System.Web.UI.Control) FormDesignerUtils.GetField(((object) ShortID.NewId()).ToString(), new FieldDefinition()));
      this.TableBuilder.Controls.Add((System.Web.UI.Control) emptySection);
      SheerResponse.SetOuterHtml(this.TableBuilder.ID, (System.Web.UI.Control) this.TableBuilder);
    }

    public void AddToSetNewField()
    {
      string id = ((object) ShortID.NewId()).ToString();
      if (this.TableBuilder.Controls.Count == 1)
      {
        id = "f1";
        Sitecore.Context.ClientPage.ClientRequest.Form.Add("f1_field_name", string.Empty);
        Sitecore.Context.ClientPage.ClientRequest.Form.Add("Structure", "f1");
      }
      this.AddToSetNewField(this.TableBuilder.ID, id, this.TableBuilder.Controls.Count - 1);
    }

    public void AddToSetNewField(string parent, string id, int index) => this.AddToSetNewField(parent, id, index, new FieldDefinition(), true);

    public void AddToSetNewField(
      string parent,
      string id,
      int index,
      FieldDefinition fieldDefinition,
      bool updatePanel)
    {
      this.TableBuilder.UpdateBuilder();
      this.TableBuilder.Reorganize(false, string.Empty);
      if (id != "f1")
      {
                System.Web.UI.Control control = parent == this.TableBuilder.ID ? (System.Web.UI.Control) this.TableBuilder : (System.Web.UI.Control) this.TableBuilder.GetSection(parent);
        Assert.IsNotNull((object) control, "destination");
                System.Web.UI.Control field = FormDesignerUtils.GetField(id, fieldDefinition);
        if (parent == nameof (FormBuilder))
        {
          if (!this.FindLiteral())
          {
            control.Controls.Add((System.Web.UI.Control) new ControlLiteral("<div id=\"FormBuilder_empty\" class=\"emptyScopeForFields\">"));
            control.Controls.Add((System.Web.UI.Control) field);
            control.Controls.Add((System.Web.UI.Control) new ControlLiteral("</div>"));
            control.Controls.Add((System.Web.UI.Control) FormDesignerUtils.FieldButtons(nameof (FormBuilder)));
          }
          else
          {
            control.Controls.AddAt(this.LastLiteral(), (System.Web.UI.Control) field);
            if (!(control.Controls[control.Controls.Count - 1] is XmlControl))
              control.Controls.Add((System.Web.UI.Control) FormDesignerUtils.FieldButtons(nameof (FormBuilder)));
          }
        }
        else
          control.Controls.AddAt(index, (System.Web.UI.Control) field);
      }
      if (!updatePanel)
        return;
      SheerResponse.SetOuterHtml(this.TableBuilder.ID, (System.Web.UI.Control) this.TableBuilder);
    }

    private bool FindLiteral()
    {
      foreach (object control in this.TableBuilder.Controls)
      {
        if (control is ControlLiteral)
          return true;
      }
      return false;
    }

    private int LastLiteral()
    {
      for (int index = this.TableBuilder.Controls.Count - 1; index > -1; --index)
      {
        if (this.TableBuilder.Controls[index] is ControlLiteral)
          return index;
      }
      return this.TableBuilder.Controls.Count;
    }

    public void AddToSetNewSection(string id, int index)
    {
      this.TableBuilder.UpdateBuilder();
      this.TableBuilder.Reorganize(false, string.Empty);
      var emptySection = FormDesignerUtils.GetEmptySection(id);
      this.TableBuilder.Controls.Add((System.Web.UI.Control) emptySection);
      var field = FormDesignerUtils.GetField(((object) Sitecore.Data.ID.NewID.ToShortID()).ToString(), new FieldDefinition());
      ((WebControl) field).Style.Add("display", "block");
      ((System.Web.UI.Control) emptySection).Controls.Add((System.Web.UI.Control) field);
      SheerResponse.SetOuterHtml(this.TableBuilder.ID, (System.Web.UI.Control) this.TableBuilder);
    }

    public void ShowEmptyForm()
    {
      this.TableBuilder.UpdateBuilder();
      this.TableBuilder.Reorganize(false, string.Empty);
      if (this.TableBuilder.Controls[this.TableBuilder.Controls.Count - 1] is XmlControl)
        this.TableBuilder.Controls.RemoveAt(this.TableBuilder.Controls.Count - 1);
      this.TableBuilder.Controls.Add((System.Web.UI.Control) FormDesignerUtils.GlobalButtons(nameof (FormBuilder)));
      SheerResponse.SetOuterHtml(this.TableBuilder.ID, (System.Web.UI.Control) this.TableBuilder);
    }

    public FormDefinition FormStucture
    {
      get
      {
        this.TableBuilder.UpdateBuilder();
        FormDefinition formDefinition = FormDefinition.Parse(this.TableBuilder.XmlDefinition);
        FormModel formModel = this.GetFormModel(false);
        if (formModel != null)
        {
          foreach (SectionDefinition section in formDefinition.Sections)
          {
            section.Conditions = formModel.Get(section.ClientControlID, "Conditions");
            foreach (FieldDefinition field in section.Fields)
              field.Conditions = formModel.Get(field.ClientControlID, "Conditions");
          }
        }
        return formDefinition;
      }
    }

    public bool IsEmpty
    {
      get
      {
        FormDefinition form = this.Form;
        if (form.Sections.Count <= 0 || !(form.Sections[0] is SectionDefinition section))
          return true;
        return form.FormID == section.SectionID && section.Fields.Count == 0;
      }
    }

    public FormDefinition Form => FormDefinition.Parse(new FormItem(Sitecore.Data.Database.GetItem(ItemUri.Parse(this.UriItem))));

    public string UriItem
    {
      get => StringUtil.GetString(ViewState["CurrentItemUri"]);
      set
      {
        Assert.ArgumentNotNull((object) value, nameof (value));
        ViewState["CurrentItemUri"] = (object) value;
      }
    }

    public void ReloadForm()
    {
      this.TableBuilder.XmlDefinition = this.Form.ToXml();
      this.TableBuilder.Reorganize(false, string.Empty);
      this.TableBuilder.RenderControl(new HtmlTextWriter((TextWriter) new StringWriter()));
      SheerResponse.SetOuterHtml(this.TableBuilder.ID, (System.Web.UI.Control) this.TableBuilder);
      SheerResponse.Eval("$('Structure').value = '';");
      SheerResponse.SetAttribute("form", "value", DependenciesManager.ConvertionUtil.ConvertToJson((object) this.GetFormModel(true)));
      SheerResponse.Eval("Sitecore.FormBuilder.loadModel();");
    }

    public System.Web.UI.Control GetLastSection() => this.TableBuilder.Controls.LastOrDefault((Func<System.Web.UI.Control, bool>) (c => c is FormSection));

    internal FormModel GetFormModel() => this.GetFormModel(true);

    internal FormModel GetFormModel(bool saved)
    {
      if (!saved && Sitecore.Context.ClientPage.IsEvent)
      {
        FormModel t;
        Json.Instance.TryDeserializeObject<FormModel>(Sitecore.Web.WebUtil.GetFormValue("form"), out t);
        return t;
      }
      FormModel formModel = new FormModel(this.analyticsSettings.IsAnalyticsAvailable);
      List<Dictionary<string, Dictionary<string, string>>> dictionaryList = new List<Dictionary<string, Dictionary<string, string>>>();
      foreach (SectionDefinition section in this.Form.Sections)
      {
        Dictionary<string, Dictionary<string, string>> properties1 = this.GetProperties(section.ClientControlID, section.Conditions);
        if (properties1 != null)
          dictionaryList.Add(properties1);
        foreach (FieldDefinition field in section.Fields)
        {
          Dictionary<string, Dictionary<string, string>> properties2 = this.GetProperties(field.ClientControlID, field.Conditions);
          if (properties2 != null)
            dictionaryList.Add(properties2);
        }
      }
      formModel.Fields = dictionaryList.ToArray();
      return formModel;
    }

    private Dictionary<string, Dictionary<string, string>> GetProperties(
      string id,
      string conditions)
    {
      if (string.IsNullOrEmpty(conditions) || !(conditions != "<ruleset />"))
        return (Dictionary<string, Dictionary<string, string>>) null;
      return new Dictionary<string, Dictionary<string, string>>()
      {
        {
          nameof (id),
          new Dictionary<string, string>() { { "v", id } }
        },
        {
          "Conditions",
          new Dictionary<string, string>()
          {
            {
              "v",
              conditions
            },
            {
              "t",
              RuleRenderer.Render(conditions)
            }
          }
        }
      };
    }
  }
}
