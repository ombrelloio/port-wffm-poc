// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.Controls.Html.Fieldset
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using System.Web.UI;

namespace Sitecore.Form.Core.Controls.Html
{
  [ToolboxData("<fieldset runat=\"server\"></div>")]
  [PersistChildren(true)]
  public class Fieldset : HtmlBaseControl
  {
    public Fieldset()
      : base(HtmlTextWriterTag.Fieldset)
    {
    }
  }
}
