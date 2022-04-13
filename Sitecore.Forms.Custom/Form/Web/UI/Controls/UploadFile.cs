// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Web.UI.Controls.UploadFile
// Assembly: Sitecore.Forms.Custom, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E620A323-75A8-42D2-B304-7093FE781621
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Custom.dll

using Sitecore.Form.Core.Attributes;
using Sitecore.Form.Core.Visual;
using Sitecore.Form.UI.Adapters;
using Sitecore.WFFM.Abstractions.Actions;
using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sitecore.Form.Web.UI.Controls
{
  [Adapter(typeof (FileUploadAdapter))]
  public class UploadFile : ValidateControl, IHasTitle
  {
    private static readonly string baseCssClassName = "scfFileUploadBorder";
    protected Panel generalPanel = new Panel();
    protected System.Web.UI.WebControls.Label title = new System.Web.UI.WebControls.Label();
    protected FileUpload upload = new FileUpload();
    private string uploadDir = "/sitecore/media library";

    public UploadFile(HtmlTextWriterTag tag)
      : base(tag)
    {
      this.CssClass = UploadFile.baseCssClassName;
    }

    public UploadFile()
      : this(HtmlTextWriterTag.Div)
    {
    }

    public override void RenderControl(HtmlTextWriter writer) => this.DoRender(writer);

    protected virtual void DoRender(HtmlTextWriter writer) => base.RenderControl(writer);

    protected override void OnInit(EventArgs e)
    {
      this.upload.CssClass = "scfFileUpload";
      this.help.CssClass = "scfFileUploadUsefulInfo";
      this.title.CssClass = "scfFileUploadLabel";
      this.title.AssociatedControlID = this.upload.ID;
      this.generalPanel.CssClass = "scfFileUploadGeneralPanel";
      this.Controls.AddAt(0, (Control) this.generalPanel);
      this.Controls.AddAt(0, (Control) this.title);
      this.generalPanel.Controls.AddAt(0, (Control) this.help);
      this.generalPanel.Controls.AddAt(0, (Control) this.upload);
    }

    public override string ID
    {
      get => this.upload.ID;
      set
      {
        this.title.ID = value + "text";
        this.upload.ID = value;
        base.ID = value + "scope";
      }
    }

    [VisualProperty("Upload To:", 0)]
    [DefaultValue("/sitecore/media library")]
    [VisualCategory("Upload")]
    [VisualFieldType(typeof (SelectDirectoryField))]
    public string UploadTo
    {
      set => this.uploadDir = value;
      get => this.uploadDir;
    }

    public override ControlResult Result
    {
      get
      {
        if (!this.upload.HasFile)
          return new ControlResult(this.ControlName, (object) null, string.Empty);
        return new ControlResult(this.ControlName, (object) new PostedFile(this.upload.FileBytes, this.upload.FileName, this.UploadTo), "medialink")
        {
          AdaptForAnalyticsTag = false
        };
      }
      set
      {
      }
    }

    public string Title
    {
      set => this.title.Text = value;
      get => this.title.Text;
    }

    [DefaultValue("scfFileUploadBorder")]
    [VisualProperty("CSS Class:", 600)]
    [VisualFieldType(typeof (CssClassField))]
    public new string CssClass
    {
      get => base.CssClass;
      set => base.CssClass = value;
    }

    protected override Control ValidatorContainer => (Control) this;

    protected override Control InnerValidatorContainer => (Control) this.generalPanel;
  }
}
