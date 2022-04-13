// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Web.UI.Controls.ListControl
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Diagnostics;
using Sitecore.Form.Core.Attributes;
using Sitecore.Form.Core.Visual;
using Sitecore.Form.UI.Adapters;
using Sitecore.Form.UI.Converters;
using Sitecore.Forms.Core.Data;
using Sitecore.WFFM.Abstractions;
using Sitecore.WFFM.Abstractions.Actions;
using Sitecore.WFFM.Abstractions.Dependencies;
using Sitecore.WFFM.Abstractions.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sitecore.Form.Web.UI.Controls
{
  [ListAdapter("Items", typeof (ListItemsAdapter))]
  [Adapter(typeof (ListControlAdapter))]
  public abstract class ListControl : ValidateControl, IHasTitle
  {
    protected Label title = new Label();
    protected Panel generalPanel = new Panel();
    protected ListItemCollection items;
    protected ListItemCollection selectedItems;
    private readonly IRequirementsChecker requirementsChecker;
    private readonly ListFieldValueFormatter listFieldValueFormatter;

    protected ListControl(HtmlTextWriterTag tag)
      : this(DependenciesManager.RequirementsChecker, tag)
    {
    }

    protected ListControl(IRequirementsChecker requirementsChecker, HtmlTextWriterTag tag)
      : this(requirementsChecker, tag, new ListFieldValueFormatter(DependenciesManager.Resolve<ISettings>()))
    {
    }

    protected ListControl(
      IRequirementsChecker requirementsChecker,
      HtmlTextWriterTag tag,
      ListFieldValueFormatter listFieldValueFormatter)
      : base(tag)
    {
      Assert.IsNotNull((object) requirementsChecker, nameof (requirementsChecker));
      Assert.ArgumentNotNull((object) listFieldValueFormatter, nameof (listFieldValueFormatter));
      this.listFieldValueFormatter = listFieldValueFormatter;
      this.requirementsChecker = requirementsChecker;
      this.KeepHiddenValue = true;
      this.EnableViewState = false;
    }

    public string Title
    {
      get => this.title.Text;
      set => this.title.Text = value;
    }

    public override string DefaultValue
    {
      set => this.SelectedValue = (ListItemCollection) value;
    }

    [Browsable(false)]
    public override ControlResult Result
    {
      get
      {
        ControlResult formattedResult = this.listFieldValueFormatter.GetFormattedResult(this.ControlName, (IEnumerable<string>) this.InnerListControl.Items.Cast<ListItem>().Where<ListItem>((Func<ListItem, bool>) (i => i.Selected)).Select<ListItem, string>((Func<ListItem, string>) (i => i.Value)).ToList<string>(), (IEnumerable<string>) this.InnerListControl.Items.Cast<ListItem>().Where<ListItem>((Func<ListItem, bool>) (i => i.Selected)).Select<ListItem, string>((Func<ListItem, string>) (i => i.Text)).ToList<string>());
        formattedResult.AdaptForAnalyticsTag = false;
        return formattedResult;
      }
      set
      {
      }
    }

    [Description("Collection of items.")]
    [PersistenceMode(PersistenceMode.InnerProperty)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Browsable(false)]
    [VisualProperty("Items:", 100)]
    [VisualCategory("List")]
    [VisualFieldType(typeof (Sitecore.Form.Core.Visual.ListField))]
    [TypeConverter(typeof (ListItemCollectionConverter))]
    [Localize]
    public ListItemCollection Items
    {
      get => this.items;
      set => this.items = value;
    }

    [Browsable(false)]
    public char Separator => '|';

    [TypeConverter(typeof (ListItemCollectionConverter))]
    [Browsable(false)]
    [VisualProperty("Selected Value:", 200)]
    [VisualCategory("List")]
    [VisualFieldType(typeof (SelectedValueField))]
    [Localize]
    public ListItemCollection SelectedValue
    {
      get => this.selectedItems;
      set => this.selectedItems = value;
    }

    protected abstract System.Web.UI.WebControls.ListControl InnerListControl { get; }

    [Browsable(false)]
    protected bool KeepHiddenValue { get; set; }

    protected override Control InnerValidatorContainer => (Control) this.generalPanel;

    protected override Control ValidatorContainer => (Control) this;

    public override void RenderControl(HtmlTextWriter writer) => this.DoRender(writer);

    protected virtual void DoRender(HtmlTextWriter output) => base.RenderControl(output);

    protected override void OnInit(EventArgs e) => this.InitItems(this.items);

    protected virtual void InitItems(ListItemCollection collection)
    {
      if (collection == null)
        collection = new ListItemCollection();
      this.InnerListControl.Items.Clear();
      this.InnerListControl.Items.AddRange(collection.ToArray());
      if (this.selectedItems == null)
        return;
      foreach (ListItem selectedItem in this.selectedItems)
      {
        ListItem byValue = this.InnerListControl.Items.FindByValue(selectedItem.Value);
        if (byValue != null)
          byValue.Selected = true;
      }
    }

    [Required("IsXdbTrackerEnabled", true)]
    protected override void OnPreRender(EventArgs e)
    {
      if (this.KeepHiddenValue && this.requirementsChecker.CheckRequirements(MethodBase.GetCurrentMethod().GetCustomAttributes().ToArray<Attribute>()) && this.FindControl(this.ID + "_complexvalue") == null)
      {
        HiddenField hiddenField = new HiddenField();
        hiddenField.ID = this.ID + "_complexvalue";
        hiddenField.Value = HttpUtility.HtmlEncode((string) this.Result.Value);
        this.Controls.AddAt(0, (Control) hiddenField);
      }
      this.title.AssociatedControlID = this.InnerListControl.ID;
      base.OnPreRender(e);
    }
  }
}
