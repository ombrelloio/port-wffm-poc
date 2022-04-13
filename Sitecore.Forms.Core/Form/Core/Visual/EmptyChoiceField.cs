// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.Visual.EmptyChoiceField
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Form.Core.Configuration;
using Sitecore.WFFM.Abstractions.Dependencies;
using Sitecore.WFFM.Abstractions.Shared;
using System;

namespace Sitecore.Form.Core.Visual
{
  public class EmptyChoiceField : BooleanField
  {
    public EmptyChoiceField()
      : this(DependenciesManager.ResourceManager)
    {
    }

    public EmptyChoiceField(IResourceManager resourceManager)
      : base(resourceManager)
    {
    }

    protected override void OnPreRender(object sender, EventArgs ev)
    {
      base.OnPreRender(sender, ev);
      this.Attributes["onblur"] = string.Format("Sitecore.PropertiesBuilder.onSaveValueInfluenceList('{0}', '{1}', '0')", (object) StaticSettings.PrefixId, (object) this.ID);
      this.Attributes["onchange"] = this.Attributes["onblur"];
      this.Attributes["onkeyup"] = this.Attributes["onblur"];
      this.Attributes["onpaste"] = this.Attributes["onblur"];
      this.Attributes["oncut"] = this.Attributes["onblur"];
    }
  }
}
