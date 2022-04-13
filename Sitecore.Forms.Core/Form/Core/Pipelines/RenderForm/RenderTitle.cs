// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.Pipelines.RenderForm.RenderTitle
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Form.Core.Configuration;
using System.Text;

namespace Sitecore.Form.Core.Pipelines.RenderForm
{
  public class RenderTitle : RenderFormText
  {
    public override void Process(RenderFormArgs args)
    {
      Assert.IsNotNull((object) args, nameof (args));
      Assert.IsNotNull((object) args.Item, "args.item");
      args.Result.FirstPart += this.RenderTitleSection(args.Item, args.DisableWebEdit, args.FastPreview, "");
    }

    protected virtual string RenderTitleSection(
      Item item,
      bool disableWebEditing,
      bool fastPreview,
      string parameters)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("<br/>");
      if (!fastPreview)
        stringBuilder.Append("<h2>");
      stringBuilder.Append(this.RenderField(item, ((BaseItem) item).Fields[Sitecore.Form.Core.Configuration.FieldIDs.FormTitleID], disableWebEditing, parameters));
      if (!fastPreview)
        stringBuilder.Append("</h2>");
      return stringBuilder.ToString();
    }
  }
}
