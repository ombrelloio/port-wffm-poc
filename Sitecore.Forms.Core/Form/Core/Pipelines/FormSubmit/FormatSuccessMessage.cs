// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.Pipelines.FormSubmit.FormatSuccessMessage
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Diagnostics;

namespace Sitecore.Form.Core.Pipelines.FormSubmit
{
  public class FormatSuccessMessage
  {
    public void Process(SubmitSuccessArgs args)
    {
      Assert.IsNotNull((object) args, nameof (args));
      if (args.Form == null)
        return;
      args.Result = args.Form.SuccessMessage;
    }
  }
}
