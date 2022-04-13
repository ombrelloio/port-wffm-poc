// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.Pipelines.RenderForm.RenderFormArgs
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Collections;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Form.Core.Configuration;
using Sitecore.Pipelines;
using System.Collections.Generic;

namespace Sitecore.Form.Core.Pipelines.RenderForm
{
  public class RenderFormArgs : PipelineArgs
  {
    private bool disableWebEdit;
    private readonly Item item;
    private RenderFormResult _result;
    private SafeDictionary<string> _parameters;
    private IEnumerable<WebEditButton> buttons;
    private bool fastPreview;
    private bool designMode;

    public RenderFormArgs(Item item)
    {
      this.item = item;
      this._parameters = new SafeDictionary<string>();
      this._result = new RenderFormResult();
      this.buttons = StaticSettings.FormsWebEditButtons;
    }

    public bool DisableWebEdit
    {
      get => this.disableWebEdit;
      set => this.disableWebEdit = value;
    }

    public Item Item => this.item;

    public RenderFormResult Result
    {
      get => this._result;
      set => this._result = value;
    }

    public SafeDictionary<string> Parameters
    {
      get => this._parameters;
      set => this._parameters = value;
    }

    public IEnumerable<WebEditButton> WebEditButtons => this.buttons;

    public bool FastPreview
    {
      get => this.fastPreview;
      set => this.fastPreview = value;
    }

    public bool DesignMode
    {
      get => this.designMode;
      set => this.designMode = value;
    }
  }
}
