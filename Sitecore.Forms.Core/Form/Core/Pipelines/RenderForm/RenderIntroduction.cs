// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.Pipelines.RenderForm.RenderIntroduction
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Form.Core.Configuration;
using System.Text;

namespace Sitecore.Form.Core.Pipelines.RenderForm
{
  public class RenderIntroduction : RenderFormText
  {
    public override void Process(RenderFormArgs args)
    {
      Assert.IsNotNull((object) args, nameof (args));
      Assert.IsNotNull((object) args.Item, "args.item");
      args.Result.FirstPart += this.RenderIntroductionSection(args.Item, args.DisableWebEdit, args.FastPreview, "");
    }

    protected virtual string RenderIntroductionSection(
      Item item,
      bool disableWebEditing,
      bool fasePreview,
      string parameters)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("<br/>");
      stringBuilder.Append(this.RenderField(item, ((BaseItem) item).Fields[Sitecore.Form.Core.Configuration.FieldIDs.FormIntroductionID], disableWebEditing, parameters));
      return stringBuilder.ToString();
    }
  }
}
