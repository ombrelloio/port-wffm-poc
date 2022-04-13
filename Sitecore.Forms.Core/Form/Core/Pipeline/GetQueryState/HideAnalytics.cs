// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.Pipeline.GetQueryState.HideAnalytics
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Data;
using Sitecore.Form.Core.Configuration;
using Sitecore.Pipelines.GetQueryState;
using Sitecore.Shell.Framework.Commands;

namespace Sitecore.Form.Core.Pipeline.GetQueryState
{
  public class HideAnalytics
  {
    public static readonly ID AnalyticsAttributes = new ID("{6060CCB1-496B-4151-AA7D-961C4495DD6F}");

    public void Process(GetQueryStateArgs args)
    {
      if (args.CommandContext == null || args.CommandContext.Items.Length == 0 || !(args.CommandContext.Items[0].TemplateID==IDs.FormTemplateID) || args.CommandName == null || !args.CommandName.StartsWith("analytics:"))
        return;
      args.CommandState = (CommandState) 3;
    }
  }
}
