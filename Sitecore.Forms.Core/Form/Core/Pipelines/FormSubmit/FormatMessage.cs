// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.Pipelines.FormSubmit.FormatMessage
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Configuration;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Form.Core.Configuration;
using Sitecore.WFFM.Abstractions.Actions;
using Sitecore.WFFM.Abstractions.Dependencies;
using System.Collections.Generic;
using System.Linq;

namespace Sitecore.Form.Core.Pipelines.FormSubmit
{
  public class FormatMessage
  {
    public void Process(SubmittedFormFailuresArgs failedArgs)
    {
      Assert.IsNotNull((object) failedArgs, "args");
      Assert.IsNotNull((object) failedArgs.FormID, "FormID");
      DependenciesManager.Logger.Warn(Sitecore.StringExtensions.StringExtensions.FormatWith("Web Forms for Marketers: an exception: {0} has occured while trying to execute an action.", new object[1]
      {
        (object) failedArgs.ErrorMessage
      }), (object) this);
      for (int index = 0; index < ((IEnumerable<ExecuteResult.Failure>) failedArgs.Failures).Count<ExecuteResult.Failure>(); ++index)
      {
        if (Sitecore.Form.Core.Configuration.Settings.HideInnerError && !failedArgs.Failures[index].IsCustom)
        {
          Database database = Factory.GetDatabase(failedArgs.Database, false);
          if (database != null)
          {
            Item obj1 = database.GetItem(failedArgs.FormID);
            if (obj1 != null)
            {
              string str = ((BaseItem) obj1)[FormIDs.SaveActionFailedMessage];
              if (!string.IsNullOrEmpty(str))
              {
                failedArgs.Failures[index].ErrorMessage = str;
                break;
              }
            }
            Item obj2 = database.GetItem(FormIDs.SubmitErrorId);
            if (obj2 != null)
            {
              string str = ((BaseItem) obj2)["Value"];
              if (!string.IsNullOrEmpty(str))
              {
                failedArgs.Failures[index].ErrorMessage = str;
                break;
              }
            }
          }
          failedArgs.Failures[index].ErrorMessage = this.ClienMessage;
        }
      }
    }

    protected virtual string ClienMessage => DependenciesManager.ResourceManager.Localize("FAILED_SUBMIT");
  }
}
