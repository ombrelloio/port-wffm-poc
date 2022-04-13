// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.Visual.DirectionField
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Diagnostics;
using Sitecore.WFFM.Abstractions.Dependencies;
using Sitecore.WFFM.Abstractions.Shared;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sitecore.Form.Core.Visual
{
  public class DirectionField : ValidationField
  {
    private readonly IResourceManager resourceManager;

    public DirectionField()
      : this(DependenciesManager.ResourceManager)
    {
    }

    public DirectionField(IResourceManager resourceManager)
      : base(HtmlTextWriterTag.Select.ToString())
    {
      Assert.IsNotNull((object) resourceManager, "Dependency resourceManager is null");
      this.resourceManager = resourceManager;
    }

    protected override void OnPreRender(object sender, EventArgs ev)
    {
      base.OnPreRender(sender, ev);
      this.Controls.Add((Control) new Literal()
      {
        Text = string.Format("<option {0} value='Vertical'>{1}</option>", this.DefaultValue == "Vertical" ? (object) "selected='selected'" : (object) string.Empty, (object) this.resourceManager.Localize("VERTICAL"))
      });
      this.Controls.Add((Control) new Literal()
      {
        Text = string.Format("<option {0} value='Horizontal'>{1}</option>", this.DefaultValue == "Horizontal" ? (object) "selected='selected'" : (object) string.Empty, (object) this.resourceManager.Localize("HORIZONTAL"))
      });
    }
  }
}
