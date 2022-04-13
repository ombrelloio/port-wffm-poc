// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.Pipelines.FormSubmit.ClearBrokenMedia
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Form.Core.Configuration;
using Sitecore.SecurityModel;
using Sitecore.WFFM.Abstractions.Actions;

namespace Sitecore.Form.Core.Pipelines.FormSubmit
{
  public class ClearBrokenMedia
  {
    public void Process(SaveFailedArgs failedArgs)
    {
      Assert.IsNotNull((object) failedArgs, "args");
      Assert.IsNotNull((object) failedArgs.FormID, "FormID");
      if (failedArgs.Fields == null || !(failedArgs.ActionFailed==ActionsIDs.SaveInDatabase))
        return;
      foreach (AdaptedControlResult field in failedArgs.Fields)
      {
        if (field.FieldID != null && !string.IsNullOrEmpty(field.Parameters) && field.Parameters.StartsWith("medialink"))
        {
          Item obj = Database.GetItem(ItemUri.Parse(field.Value));
          if (obj != null)
          {
            ID id = obj.ID;
            using (new SecurityDisabler())
              obj.Delete();
            Log.Info(string.Format("Media item has been deleted from database:{0}", (object) id), (object) this);
          }
        }
      }
    }
  }
}
