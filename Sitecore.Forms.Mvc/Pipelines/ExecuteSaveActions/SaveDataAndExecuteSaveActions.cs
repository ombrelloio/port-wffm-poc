// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Mvc.Pipelines.ExecuteSaveActions.SaveDataAndExecuteSaveActions
// Assembly: Sitecore.Forms.Mvc, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1AD2DE3-BABE-4597-B643-AA577F98B8D6
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Mvc.dll

using Sitecore.Diagnostics;
using Sitecore.Form.Core.Data;
using Sitecore.Forms.Core.Handlers;
using Sitecore.Forms.Mvc.Interfaces;
using Sitecore.WFFM.Abstractions.Actions;
using Sitecore.WFFM.Abstractions.ContentEditor;
using Sitecore.WFFM.Abstractions.Shared;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sitecore.Forms.Mvc.Pipelines.ExecuteSaveActions
{
  public class SaveDataAndExecuteSaveActions : FormProcessorBase<IFormModel>
  {
    private readonly FormDataHandler formDataHandler;
    private readonly IAnalyticsTracker analyticsTracker;

    public SaveDataAndExecuteSaveActions(
      FormDataHandler formDataHandler,
      IAnalyticsTracker analyticsTracker)
    {
      Assert.ArgumentNotNull((object) formDataHandler, nameof (formDataHandler));
      Assert.ArgumentNotNull((object) analyticsTracker, nameof (analyticsTracker));
      this.formDataHandler = formDataHandler;
      this.analyticsTracker = analyticsTracker;
    }

    public override void Process(FormProcessorArgs<IFormModel> args)
    {
      Assert.ArgumentNotNull((object) args, nameof (args));
      if (args.Model == null)
        return;
      IFormModel model = args.Model;
      try
      {
        this.formDataHandler.ProcessForm(((Sitecore.Forms.Mvc.Models.FormModel) model).Item.ID, model.Results.ToArray(), this.GetActions(model.Item.ActionsDefinition));
      }
      catch (Sitecore.Form.Core.Data.FormSubmitException ex)
      {
        model.Failures.AddRange(ex.Failures);
      }
      catch (FormVerificationException ex1)
      {
        try
        {
          model.Failures.Add(new ExecuteResult.Failure()
          {
            ErrorMessage = ex1.Message,
            StackTrace = ex1.StackTrace,
            IsCustom = ex1.IsCustomErrorMessage,
            IsMessageUnchangeable = ex1.Source.Equals(Sitecore.WFFM.Abstractions.Constants.Core.Constants.CheckActions)
          });
        }
        catch (Exception ex2)
        {
          Log.Error(ex2.Message, ex2, (object) this);
        }
      }
      model.EventCounter = this.analyticsTracker.EventCounter + 1;
    }

    private IActionDefinition[] GetActions(IListDefinition definition)
    {
      Assert.ArgumentNotNull((object) definition, nameof (definition));
      List<IActionDefinition> actionDefinitionList = new List<IActionDefinition>();
      if (definition.Groups.Any<IGroupDefinition>())
      {
        foreach (IGroupDefinition group in definition.Groups)
        {
          if (group.ListItems != null)
            actionDefinitionList.AddRange(group.ListItems.Select<IListItemDefinition, IActionDefinition>((Func<IListItemDefinition, IActionDefinition>) (li => (IActionDefinition) new ActionDefinition(li.ItemID, li.Parameters)
            {
              UniqueKey = li.Unicid
            })));
        }
      }
      return actionDefinitionList.ToArray();
    }
  }
}
