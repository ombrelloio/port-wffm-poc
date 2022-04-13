// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.UI.Adapters.CheckboxAdapter
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Form.Core.Client.Submit;
using Sitecore.WFFM.Abstractions.Data;
using Sitecore.WFFM.Abstractions.Dependencies;

namespace Sitecore.Form.UI.Adapters
{
  public class CheckboxAdapter : Adapter
  {
    public override string AdaptResult(IFieldItem item, object value) => !(value.ToString() == "1") ? DependenciesManager.ResourceManager.Localize("UNSELECTED") : DependenciesManager.ResourceManager.Localize("SELECTED");
  }
}
