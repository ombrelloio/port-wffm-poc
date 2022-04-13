// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.Pipelines.RenderForm.RenderFormText
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Web.UI;
using Sitecore.Web.UI.WebControls;

namespace Sitecore.Form.Core.Pipelines.RenderForm
{
  public abstract class RenderFormText
  {
    public abstract void Process(RenderFormArgs args);

    protected virtual string RenderField(
      Item item,
      Field field,
      bool disableWebEditing,
      string parameters)
    {
      Assert.IsNotNull((object) item, nameof (item));
      Assert.IsNotNull((object) field, nameof (field));
      FieldRenderer fieldRenderer = new FieldRenderer();
      fieldRenderer.Item = item;
      fieldRenderer.FieldName = field.Name;
      ((WebControl) fieldRenderer).Parameters = parameters;
      fieldRenderer.DisableWebEditing = disableWebEditing;
      return ((object) fieldRenderer.RenderField()).ToString();
    }
  }
}
