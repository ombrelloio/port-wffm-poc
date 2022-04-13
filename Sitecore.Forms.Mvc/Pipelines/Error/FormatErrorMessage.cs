// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Mvc.Pipelines.Error.FormatErrorMessage
// Assembly: Sitecore.Forms.Mvc, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1AD2DE3-BABE-4597-B643-AA577F98B8D6
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Mvc.dll

using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Form.Core.Configuration;
using Sitecore.Forms.Mvc.Interfaces;
using Sitecore.Forms.Mvc.Models;
using Sitecore.WFFM.Abstractions.Actions;
using Sitecore.WFFM.Abstractions.Dependencies;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sitecore.Forms.Mvc.Pipelines.Error
{
  public class FormatErrorMessage : FormProcessorBase<IFormModel>
  {
    public override void Process(FormProcessorArgs<IFormModel> args)
    {
      Assert.ArgumentNotNull((object) args, nameof (args));
      IFormModel model = args.Model;
      if (model == null)
        return;
      for (int index = 0; index < model.Failures.Count; ++index)
      {
        ExecuteResult.Failure failure1 = model.Failures[index];
        Log.Warn(Sitecore.StringExtensions.StringExtensions.FormatWith("Web Forms for Marketers: an exception '{0}' has occured while trying to execute an action '{1}'.", new object[2]
        {
          (object) failure1.ErrorMessage,
          (object) failure1.FailedAction
        }), (object) this);
        ExecuteResult.Failure failure2 = model.Failures[index];
        if (!failure2.IsCustom && Sitecore.Form.Core.Configuration.Settings.HideInnerError)
        {
          failure2 = model.Failures[index];
          if (!failure2.IsMessageUnchangeable)
          {
            Database contextDatabase = StaticSettings.ContextDatabase;
            if (contextDatabase != null)
            {
              Item obj1 = contextDatabase.GetItem(((FormModel) model).Item.ID);
              if (obj1 != null)
              {
                string str = ((BaseItem) obj1)[FormIDs.SaveActionFailedMessage];
                if (!string.IsNullOrEmpty(str))
                {
                  failure1.ErrorMessage = str;
                  model.Failures[index] = failure1;
                  return;
                }
              }
              Item obj2 = contextDatabase.GetItem(FormIDs.SubmitErrorId);
              if (obj2 != null)
              {
                string str = ((BaseItem) obj2)["Value"];
                if (!string.IsNullOrEmpty(str))
                {
                  failure1.ErrorMessage = str;
                  model.Failures[index] = failure1;
                  return;
                }
              }
            }
            failure1.ErrorMessage = this.ClientMessage;
            model.Failures[index] = failure1;
          }
        }
      }
      IEnumerable<ExecuteResult.Failure> source = model.Failures.GroupBy<ExecuteResult.Failure, string>((Func<ExecuteResult.Failure, string>) (f => f.ErrorMessage)).Select<IGrouping<string, ExecuteResult.Failure>, ExecuteResult.Failure>((Func<IGrouping<string, ExecuteResult.Failure>, ExecuteResult.Failure>) (g => g.First<ExecuteResult.Failure>()));
      model.Failures = source.ToList<ExecuteResult.Failure>();
    }

    protected virtual string ClientMessage => DependenciesManager.ResourceManager.Localize("FAILED_SUBMIT");
  }
}
