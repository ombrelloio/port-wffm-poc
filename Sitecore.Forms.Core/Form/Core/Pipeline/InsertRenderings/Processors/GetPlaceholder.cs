// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.Pipeline.InsertRenderings.Processors.GetPlaceholder
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Diagnostics;
using Sitecore.Form.Core.Configuration;
using Sitecore.Pipelines.GetPlaceholderRenderings;

namespace Sitecore.Form.Core.Pipeline.InsertRenderings.Processors
{
  public class GetPlaceholder
  {
    public void Process(GetPlaceholderRenderingsArgs args)
    {
      Assert.IsNotNull((object) args, nameof (args));
      if (Context.Page.Page == null || !Context.Page.Page.IsPostBack || args.PlaceholderKey == null)
        return;
      Context.ClientData.SetValue(StaticSettings.PrefixId + StaticSettings.PlaceholderKeyId, args.PlaceholderKey);
    }
  }
}
