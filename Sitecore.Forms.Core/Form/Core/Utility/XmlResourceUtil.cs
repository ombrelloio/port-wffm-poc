// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.Utility.XmlResourceUtil
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Diagnostics;
using Sitecore.Resources;
using Sitecore.Web.UI.XmlControls;
using System.Collections.Specialized;
using System.IO;
using System.Web.UI;

namespace Sitecore.Form.Core.Utility
{
  public static class XmlResourceUtil
  {
    public static Control FindByUniqueID(Sitecore.Web.UI.XmlControls.XmlControl control, string id)
    {
      foreach (Control control1 in ((Control) control).Controls)
      {
        if (control1.UniqueID == id)
          return control1;
        Control byUniqueId = XmlResourceUtil.FindByUniqueID(control1, id);
        if (byUniqueId != null)
          return byUniqueId;
      }
      return (Control) null;
    }

    private static Control FindByUniqueID(Control parent, string id)
    {
      if (parent == null)
        return (Control) null;
      foreach (Control control in parent.Controls)
      {
        if (control.UniqueID == id)
          return control;
        Control byUniqueId = XmlResourceUtil.FindByUniqueID(control, id);
        if (byUniqueId != null)
          return byUniqueId;
      }
      return (Control) null;
    }

    public static string GetResourceContent(string name, NameValueCollection parameters)
    {
      XmlControl resourceControl = XmlResourceUtil.GetResourceControl(name, parameters);
      HtmlTextWriter htmlTextWriter = new HtmlTextWriter((TextWriter) new StringWriter());
      HtmlTextWriter writer = htmlTextWriter;
      ((Control) resourceControl).RenderControl(writer);
      return htmlTextWriter.InnerWriter.ToString();
    }

    public static string GetResourceContent(XmlControl control)
    {
      HtmlTextWriter writer = new HtmlTextWriter((TextWriter) new StringWriter());
      ((Control) control).RenderControl(writer);
      return writer.InnerWriter.ToString();
    }

    public static XmlControl GetResourceControl(
      string name,
      NameValueCollection parameters)
    {
      XmlControl webControl = Resource.GetWebControl(name) as XmlControl;
      Assert.IsNotNull((object) webControl, "xml control");
      if (parameters != null)
      {
        foreach (string allKey in parameters.AllKeys)
          webControl[allKey] = (object) parameters[allKey];
      }
      return webControl;
    }
  }
}
