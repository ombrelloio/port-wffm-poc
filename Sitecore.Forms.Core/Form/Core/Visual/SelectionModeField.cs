// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.Visual.SelectionModeField
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Diagnostics;
using Sitecore.Form.Core.Configuration;
using Sitecore.WFFM.Abstractions.Dependencies;
using Sitecore.WFFM.Abstractions.Shared;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sitecore.Form.Core.Visual
{
  public class SelectionModeField : ValidationField
  {
    private readonly IResourceManager resourceManager;

    public SelectionModeField()
      : this(DependenciesManager.ResourceManager)
    {
    }

    public SelectionModeField(IResourceManager resourceManager)
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
        Text = string.Format("<option {0} value='Single'>{1}</option>", this.DefaultValue == "Single" ? (object) "selected='selected'" : (object) string.Empty, (object) this.resourceManager.Localize("SINGLE"))
      });
      this.Controls.Add((Control) new Literal()
      {
        Text = string.Format("<option {0} value='Multiple'>{1}</option>", this.DefaultValue == "Multiple" ? (object) "selected='selected'" : (object) string.Empty, (object) this.resourceManager.Localize("MULTIPLE"))
      });
      this.Attributes["onblur"] = string.Format("Sitecore.PropertiesBuilder.onSaveValueInfluenceList('{0}', '{1}', '0')", (object) StaticSettings.PrefixId, (object) this.ID);
      this.Attributes["onchange"] = this.Attributes["onblur"];
      this.Attributes["onkeyup"] = this.Attributes["onblur"];
      this.Attributes["onpaste"] = this.Attributes["onblur"];
      this.Attributes["oncut"] = this.Attributes["onblur"];
    }
  }
}
