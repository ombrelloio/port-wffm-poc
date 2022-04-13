// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Mvc.Controllers.JsonpResult
// Assembly: Sitecore.Forms.Mvc, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1AD2DE3-BABE-4597-B643-AA577F98B8D6
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Mvc.dll

using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Sitecore.Forms.Mvc.Controllers
{
  public class JsonpResult : JsonResult
  {
    public string Callback { get; set; }

    public override void ExecuteResult(ControllerContext context)
    {
      HttpResponseBase response = context.HttpContext.Response;
      response.ContentType = !string.IsNullOrEmpty(this.ContentType) ? this.ContentType : "application/javascript";
      if (this.ContentEncoding != null)
        response.ContentEncoding = this.ContentEncoding;
      if (string.IsNullOrEmpty(this.Callback))
      {
        this.Callback = context.HttpContext.Request.QueryString["callback"];
        if (!string.IsNullOrEmpty(this.Callback) && this.Callback[0] == ',')
          this.Callback = this.Callback.Remove(0, 1);
      }
      if (this.Data == null)
        return;
      string str = new JavaScriptSerializer().Serialize(this.Data);
      response.Write(this.Callback + "(" + str + ");");
    }
  }
}
