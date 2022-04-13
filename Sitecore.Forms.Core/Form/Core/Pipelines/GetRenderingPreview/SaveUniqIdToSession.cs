// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.Pipelines.GetRenderingPreview.SaveUniqIdToSession
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Diagnostics;
using Sitecore.Pipelines.GetRenderingPreview;
using Sitecore.Web;

namespace Sitecore.Form.Core.Pipelines.GetRenderingPreview
{
  public class SaveUniqIdToSession : GetRenderingPreviewProcessor
  {
    public override void Process(GetRenderingPreviewArgs args)
    {
      Assert.ArgumentNotNull((object) args, nameof (args));
      if (args.RenderingReference == null || !(Context.ClientPage.ClientRequest.Form["command"] == "insert"))
        return;
      WebUtil.SetSessionValue(Sitecore.WFFM.Abstractions.Constants.Core.Constants.RenderingUniqIdInSession, (object) args.RenderingReference.UniqueId);
      WebUtil.SetSessionValue(Sitecore.WFFM.Abstractions.Constants.Core.Constants.RenderingIdInSession, (object) args.RenderingReference.RenderingID);
    }
  }
}
