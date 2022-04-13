// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Mvc.Controllers.ModelBinders.FieldBinders.CaptchaFieldBinder
// Assembly: Sitecore.Forms.Mvc, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1AD2DE3-BABE-4597-B643-AA577F98B8D6
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Mvc.dll

using MSCaptcha;
using Sitecore.Diagnostics;
using Sitecore.Forms.Mvc.ViewModels.Fields;
using System.Web;
using System.Web.Mvc;

namespace Sitecore.Forms.Mvc.Controllers.ModelBinders.FieldBinders
{
  public class CaptchaFieldBinder : DefaultFieldValueBinder
  {
    public override object BindProperty(
      ControllerContext controllerContext,
      ModelBindingContext bindingContext,
      object value)
    {
      object obj = base.BindProperty(controllerContext, bindingContext, value);
      if (obj == null)
        return (object) null;
      if (!(this.FieldModel is CaptchaField fieldModel))
        return obj;
      bool useSession = fieldModel.UseSession;
      string id = obj.ToString();
      CaptchaImage cachedCaptcha = CaptchaFieldBinder.GetCachedCaptcha(controllerContext.HttpContext, id, useSession);
      if (cachedCaptcha != null)
        fieldModel.Captcha = cachedCaptcha;
      return (object) id;
    }

    private static CaptchaImage GetCachedCaptcha(
      HttpContextBase context,
      string id,
      bool useSession)
    {
      Assert.ArgumentNotNull((object) context, nameof (context));
      if (string.IsNullOrWhiteSpace(id))
        return (CaptchaImage) null;
      CaptchaImage captchaImage = (CaptchaImage) null;
      if (!useSession)
      {
        captchaImage = context.Cache.Get(id) as CaptchaImage;
        context.Cache.Remove(id);
      }
      else if (context.Session != null)
      {
        captchaImage = context.Session[id] as CaptchaImage;
        context.Session.Remove(id);
      }
      return captchaImage;
    }
  }
}
