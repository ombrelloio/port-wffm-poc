// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.Pipelines.FormUploadFile.ResolveFolder
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Form.Core.Configuration;

namespace Sitecore.Form.Core.Pipelines.FormUploadFile
{
  public class ResolveFolder
  {
    public void Process(FormUploadFileArgs args)
    {
      Assert.ArgumentNotNull((object) args, nameof (args));
      Item obj = StaticSettings.ContextDatabase.GetItem(args.Folder);
      Assert.IsNotNull((object) obj, typeof (Item), "ID: {0}", new object[1]
      {
        (object) args.Folder
      });
      args.Folder = obj.Paths.LongID;
    }
  }
}
