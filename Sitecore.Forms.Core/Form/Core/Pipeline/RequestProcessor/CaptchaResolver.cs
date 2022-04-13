// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.Pipeline.RequestProcessor.CaptchaResolver
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using MSCaptcha;
using Sitecore.Form.Core.Web;
using System.Web;
using System.Web.SessionState;

namespace Sitecore.Form.Core.Pipeline.RequestProcessor
{
  public class CaptchaResolver : IHttpHandler, IRequiresSessionState
  {
    public void ProcessRequest(HttpContext context)
    {
      IHttpHandler httpHandler = (IHttpHandler) null;
      if (context.Request.RawUrl.Contains("CaptchaImage.axd"))
        httpHandler = (IHttpHandler) new captchaImageHandler();
      if (context.Request.RawUrl.Contains("CaptchaAudio.axd"))
        httpHandler = (IHttpHandler) new CaptchaAudionHandler();
      if (httpHandler == null)
        return;
      if (context.Request.QueryString["guid"] == null)
        context.RewritePath(context.Request.Path, context.Request.PathInfo, "guid=");
      httpHandler.ProcessRequest(context);
    }

    public bool IsReusable { get; private set; }
  }
}
