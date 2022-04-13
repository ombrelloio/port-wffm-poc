// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Mvc.Data.Wrappers.WrappedRendering
// Assembly: Sitecore.Forms.Mvc, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1AD2DE3-BABE-4597-B643-AA577F98B8D6
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Mvc.dll

using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Form.Core.Configuration;
using Sitecore.Mvc.Presentation;
using System;

namespace Sitecore.Forms.Mvc.Data.Wrappers
{
  public class WrappedRendering : IRendering
  {
    private readonly Rendering sitecoreRendering;

    public WrappedRendering(Rendering rendering) => this.sitecoreRendering = rendering;

    public IRenderingParameters Parameters => (IRenderingParameters) new WrappedRenderingParameters(this.sitecoreRendering.Parameters);

    public string DataSource => this.sitecoreRendering.DataSource;

    public bool IsFormRendering => this.sitecoreRendering != null && (((CustomItemBase) this.sitecoreRendering.RenderingItem).InnerItem.ID == IDs.FormMvcInterpreterID);

    public Guid UniqueId => this.sitecoreRendering.UniqueId;
  }
}
