// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Mvc.Controllers.ModelBinders.FieldBinders.ListFieldValueBinder
// Assembly: Sitecore.Forms.Mvc, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1AD2DE3-BABE-4597-B643-AA577F98B8D6
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Mvc.dll

using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Sitecore.Forms.Mvc.Controllers.ModelBinders.FieldBinders
{
  public class ListFieldValueBinder : DefaultFieldValueBinder
  {
    public override object BindProperty(
      ControllerContext controllerContext,
      ModelBindingContext bindingContext,
      object value)
    {
      object obj = base.BindProperty(controllerContext, bindingContext, value);
      if (obj == null)
        return (object) null;
      if (string.IsNullOrEmpty(obj.ToString()))
        return (object) new List<string>();
      return (object) ((IEnumerable<string>) obj.ToString().Split(',')).ToList<string>();
    }
  }
}
