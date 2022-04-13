// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Web.UI.Controls.FieldRenderer
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Web.UI;
using Sitecore.Web.UI.WebControls;
using Sitecore.Xml.Xsl;

namespace Sitecore.Form.Web.UI.Controls
{
  public class FieldRenderer
  {
    public static string Render(Item item, string field, string parameters, bool webEditDisabled)
    {
      Assert.ArgumentNotNull((object) item, nameof (item));
      Assert.ArgumentNotNullOrEmpty(field, nameof (field));
      var fieldRenderer = new Sitecore.Web.UI.WebControls.FieldRenderer();
      fieldRenderer.Item = item;
      fieldRenderer.FieldName = field;
      ((WebControl) fieldRenderer).Parameters = parameters ?? string.Empty;
      fieldRenderer.DisableWebEditing = webEditDisabled;
      RenderFieldResult renderFieldResult = fieldRenderer.RenderField();
      return webEditDisabled || string.IsNullOrEmpty(renderFieldResult.LastPart) ? ((object) renderFieldResult).ToString() : "<span>" + (object) renderFieldResult + "</span>";
    }

    public static string Render(Item item, string field) => FieldRenderer.Render(item, field, (string) null, true);

    public static string Render(Item item, ID fieldId, string parameters, bool webEditDisabled)
    {
      Assert.ArgumentNotNull((object) fieldId, nameof (fieldId));
      return FieldRenderer.Render(item, ((object) fieldId).ToString(), parameters, webEditDisabled);
    }

    public static string Render(Item item, ID fieldId) => FieldRenderer.Render(item, fieldId, (string) null, true);
  }
}
