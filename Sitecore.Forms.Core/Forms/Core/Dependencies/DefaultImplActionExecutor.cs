// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Core.Dependencies.DefaultImplActionExecutor
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Events;
using Sitecore.Form.Core.Configuration;
using Sitecore.Form.Core.ContentEditor.Data;
using Sitecore.Form.Core.Data;
using Sitecore.Form.Core.Pipelines.FormSubmit;
using Sitecore.Form.Core.Utility;
using Sitecore.Pipelines;
using Sitecore.WFFM.Abstractions.Actions;
using Sitecore.WFFM.Abstractions.Analytics;
using Sitecore.WFFM.Abstractions.ContentEditor;
using Sitecore.WFFM.Abstractions.Data;
using Sitecore.WFFM.Abstractions.Shared;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sitecore.Forms.Core.Dependencies
{
    [Serializable]
    public class DefaultImplActionExecutor : IActionExecutor
    {
        private readonly IItemRepository itemRepository;
        private readonly IRequirementsChecker requirementsChecker;
        private readonly ILogger logger;
        private readonly IResourceManager resourceManager;
        private readonly IAnalyticsTracker analyticsTracker;
        private readonly IWffmDataProvider dataProvider;
        private readonly IFieldProvider fieldProvider;
        private readonly IFormContext formContext;

        public DefaultImplActionExecutor(
          IItemRepository itemRepository,
          IRequirementsChecker requirementsChecker,
          ILogger logger,
          IResourceManager resourceManager,
          IAnalyticsTracker analyticsTracker,
          IWffmDataProvider dataProvider,
          IFieldProvider fieldProvider,
          IFormContext formContext)
        {
            Assert.ArgumentNotNull((object)itemRepository, nameof(itemRepository));
            Assert.ArgumentNotNull((object)requirementsChecker, nameof(requirementsChecker));
            Assert.ArgumentNotNull((object)logger, nameof(logger));
            Assert.ArgumentNotNull((object)resourceManager, nameof(resourceManager));
            Assert.ArgumentNotNull((object)analyticsTracker, nameof(analyticsTracker));
            Assert.ArgumentNotNull((object)dataProvider, nameof(dataProvider));
            Assert.ArgumentNotNull((object)fieldProvider, nameof(fieldProvider));
            Assert.ArgumentNotNull((object)formContext, nameof(formContext));
            this.itemRepository = itemRepository;
            this.requirementsChecker = requirementsChecker;
            this.logger = logger;
            this.resourceManager = resourceManager;
            this.analyticsTracker = analyticsTracker;
            this.dataProvider = dataProvider;
            this.fieldProvider = fieldProvider;
            this.formContext = formContext;
        }

        public IActionItem GetAcitonByUniqId(IFormItem form, string uniqid, bool saveAction)
        {
            Assert.ArgumentNotNull((object)form, nameof(form));
            ListDefinition listDefinition = ListDefinition.Parse(saveAction ? form.SaveActions : form.CheckActions);
            if (listDefinition.Groups.Any<IGroupDefinition>())
            {
                IListItemDefinition listItemDefinition = listDefinition.Groups.First<IGroupDefinition>().ListItems.FirstOrDefault<IListItemDefinition>((Func<IListItemDefinition, bool>)(i => i.Unicid == uniqid));
                if (listItemDefinition != null && !string.IsNullOrEmpty(listItemDefinition.ItemID))
                {
                    Item obj = form.Database.GetItem(listItemDefinition.ItemID);
                    if (obj != null)
                        return (IActionItem)new ActionItem(obj);
                }
            }
            return (IActionItem)null;
        }

        public IEnumerable<IActionItem> GetActions(Item form) => (IEnumerable<IActionItem>)ListDefinition.Parse(((BaseItem)form)[Sitecore.Form.Core.Configuration.FieldIDs.SaveActionsID]).Groups.First<IGroupDefinition>().ListItems.Select<IListItemDefinition, Item>((Func<IListItemDefinition, Item>)(item => form.Database.GetItem(item.ItemID))).Where<Item>((Func<Item, bool>)(command => command != null)).Select<Item, ActionItem>((Func<Item, ActionItem>)(command => new ActionItem(command))).ToList<ActionItem>();

        public IEnumerable<IActionItem> GetSaveActions(Item form) => this.GetActions(form).Where<IActionItem>((Func<IActionItem, bool>)(s => s.ActionType == ActionType.Save));

        public IEnumerable<IActionItem> GetCheckActions(Item form) => this.GetActions(form).Where<IActionItem>((Func<IActionItem, bool>)(s => s.ActionType == ActionType.Check));

        public void ExecuteChecking(
          ID formID,
          ControlResult[] fields,
          IActionDefinition[] actionDefinitions)
        {
            IActionDefinition actionDefinition1 = (IActionDefinition)null;
            IFormItem formItem = this.itemRepository.CreateFormItem(formID);
            try
            {
                this.RaiseEvent("forms:check", new object[2]
                {
          (object) formID,
          (object) fields
                });
                ActionCallContext actionCallContext = new ActionCallContext()
                {
                    FormItem = formItem
                };
                foreach (IActionDefinition actionDefinition2 in actionDefinitions)
                {
                    actionDefinition1 = actionDefinition2;
                    IActionItem action = this.itemRepository.CreateAction(actionDefinition2.ActionID);
                    if (action != null)
                    {
                        if (action.ActionInstance is ICheckAction actionInstance5 && this.requirementsChecker.CheckRequirements(actionInstance5.GetType()))
                        {
                            ReflectionUtils.SetXmlProperties((object)actionInstance5, actionDefinition2.Paramaters, true);
                            ReflectionUtils.SetXmlProperties((object)actionInstance5, action.GlobalParameters, true);
                            actionInstance5.UniqueKey = actionDefinition2.UniqueKey;
                            actionInstance5.ActionID = action.ID;
                            actionInstance5.Execute(formID, (IEnumerable<ControlResult>)fields, actionCallContext);
                        }
                    }
                    else
                        this.logger.Warn(string.Format("Web Forms for Marketers : The '{0}' action does not exist", (object)actionDefinition2.ActionID), new object());
                }
            }
            catch (Exception ex)
            {
                if (actionDefinition1 != null)
                {
                    string str = actionDefinition1.GetFailureMessage(false, ID.Null);
                    bool isCustomErrorMessage = true;
                    if (string.IsNullOrEmpty(str))
                    {
                        str = ex.Message;
                        isCustomErrorMessage = false;
                    }
                    CheckFailedArgs checkFailedArgs1 = new CheckFailedArgs(formID, actionDefinition1.ActionID, fields, ex);
                    checkFailedArgs1.ErrorMessage = str;
                    CheckFailedArgs checkFailedArgs2 = checkFailedArgs1;
                    CorePipeline.Run("errorCheck", (PipelineArgs)checkFailedArgs2);
                    if (formItem.IsDropoutTrackingEnabled)
                        this.analyticsTracker.TriggerEvent(PageEventIds.FormCheckActionError, "Form Check Action Error", formID, checkFailedArgs2.ErrorMessage, actionDefinition1.GetTitle(), string.Empty);
                    FormVerificationException verificationException = new FormVerificationException(checkFailedArgs2.ErrorMessage, isCustomErrorMessage);
                    verificationException.Source = ex.Source;
                    throw verificationException;
                }
                throw;
            }
        }

        public virtual void RaiseEvent(string eventName, params object[] args)
        {
            try
            {
                Event.RaiseEvent(eventName, args);
            }
            catch
            {
            }
        }

        public ExecuteResult ExecuteSaving(
          ID formID,
          ControlResult[] fields,
          IActionDefinition[] actionDefinitions,
          bool simpleAdapt,
          ID sessionID)
        {
            Assert.ArgumentNotNull((object)fields, nameof(fields));
            Assert.ArgumentNotNull((object)actionDefinitions, nameof(actionDefinitions));
            AdaptedResultList adaptedResultList = (AdaptedResultList)this.AdaptResult((IEnumerable<ControlResult>)fields, simpleAdapt);
            this.RaiseEvent("forms:save", new object[2]
            {
        (object) formID,
        (object) adaptedResultList
            });
            List<ExecuteResult.Failure> failureList = new List<ExecuteResult.Failure>();
            try
            {
                this.SaveFormToDatabase(formID, adaptedResultList);
            }
            catch (Exception ex)
            {
                this.logger.Warn(ex.Message, ex, (object)ex);
            }
            ActionCallContext actionCallContext = new ActionCallContext();
            foreach (IActionDefinition actionDefinition in actionDefinitions)
            {
                try
                {
                    IActionItem action = this.itemRepository.CreateAction(actionDefinition.ActionID);
                    if (action != null)
                    {
                        if (action.ActionInstance is ISaveAction actionInstance)
                        {
                            if (this.requirementsChecker.CheckRequirements(actionInstance.GetType()))
                            {
                                ReflectionUtils.SetXmlProperties((object)actionInstance, actionDefinition.Paramaters, true);
                                ReflectionUtils.SetXmlProperties((object)actionInstance, action.GlobalParameters, true);
                                actionInstance.UniqueKey = actionDefinition.UniqueKey;
                                actionInstance.ActionID = action.ID;
                                actionInstance.Execute(formID, adaptedResultList, actionCallContext, (object)sessionID);
                            }
                            else
                                this.logger.Warn(string.Format("Save action {0} is tried to be executed but system configuration doesn't meet with it's requirements. Recommendation is to delete this save action from fields.", (object)actionDefinition.ActionID), new object());
                        }
                    }
                    else
                        this.logger.Warn(string.Format("The '{0}' action item does not exist", (object)actionDefinition.ActionID), (object)this);
                }
                catch (Exception ex)
                {
                    this.logger.Warn(ex.Message, ex, (object)ex);
                    string failureMessage = actionDefinition.GetFailureMessage();
                    ExecuteResult.Failure failure = new ExecuteResult.Failure()
                    {
                        IsCustom = !string.IsNullOrEmpty(failureMessage)
                    };
                    SaveFailedArgs saveFailedArgs1 = new SaveFailedArgs(formID, adaptedResultList, actionDefinition.ActionID, ex);
                    saveFailedArgs1.ErrorMessage = failure.IsCustom ? failureMessage : ex.Message;
                    SaveFailedArgs saveFailedArgs2 = saveFailedArgs1;
                    CorePipeline.Run("errorSave", (PipelineArgs)saveFailedArgs2);
                    failure.ApiErrorMessage = ex.Message;
                    failure.ErrorMessage = saveFailedArgs2.ErrorMessage;
                    failure.FailedAction = actionDefinition.ActionID;
                    failure.StackTrace = ex.StackTrace;
                    if (Settings.AbortSaveActionPipelineIfSaveActionFails)
                    {
                        failure.ErrorMessage += Settings.AbortSaveActionPipelineErrorMessage;
                        Log.Error("Execution of Save action pipeline has been aborted. See the WFM.SaveActionPipeline.AbortIfActionFails setting for more details.", (object)this);
                        failureList.Add(failure);
                        break;
                    }
                    failureList.Add(failure);
                }
            }
            return new ExecuteResult()
            {
                Failures = failureList.ToArray()
            };
        }

        public void ExecuteSystemAction(ID formID, ControlResult[] list)
        {
            Item obj = this.itemRepository.GetItem(FormIDs.SystemActionsRootID);
            if (obj == null || !obj.HasChildren)
                return;
            AdaptedResultList adaptedFields = (AdaptedResultList)this.AdaptResult((IEnumerable<ControlResult>)list, true);
            string str = string.Format(".//*[@@templateid = '{0}']", (object)FormIDs.ActionTemplateID);
            foreach (Item selectItem in obj.Axes.SelectItems(str))
            {
                IActionItem action = this.itemRepository.CreateAction(selectItem.ID);
                try
                {
                    if (action != null)
                    {
                        if (action.ActionInstance is ISystemAction actionInstance5)
                        {
                            if (this.requirementsChecker.CheckRequirements(actionInstance5.GetType()))
                            {
                                ReflectionUtils.SetXmlProperties((object)actionInstance5, action.GlobalParameters, true);
                                actionInstance5.Execute(formID, adaptedFields, (ActionCallContext)null, (object)this.analyticsTracker.SessionId);
                            }
                        }
                    }
                    else
                        this.logger.Warn(string.Format("The '{0}' action item does not exist", (object)selectItem.ID), (object)this);
                }
                catch (Exception ex)
                {
                    this.formContext.Failures.Add(new ExecuteResult.Failure()
                    {
                        FailedAction = action == null ? "Undefined action item " + (object)selectItem.ID : action.Name
                    });
                }
            }
        }

        public void SaveFormToDatabase(ID formid, AdaptedResultList fields)
        {
            this.logger.IsNull((object)this.analyticsTracker.Current, "Tracker.Current");
            IFormItem formItem = this.itemRepository.CreateFormItem(formid);
            if (formItem == null)
                this.logger.Warn(string.Format("Form item {0} isn't found in db", (object)formid), (object)this);
            else if (!formItem.IsSaveFormDataToStorage)
            {
                this.logger.Audit(string.Format("Form {0} is not saving in db becouse it's save option is set to false", (object)formid), (object)this);
            }
            else
            {
                this.logger.Audit(string.Format("Form {0} is saving to db", (object)formid), (object)this);
                FieldData[] array = fields.Where<AdaptedControlResult>((Func<AdaptedControlResult, bool>)(f => ((IEnumerable<IFieldItem>)formItem.Fields).FirstOrDefault<IFieldItem>((Func<IFieldItem, bool>)(itemField => ((object)itemField.ID).ToString() == f.FieldID && itemField.IsSaveToStorage)) != null)).Select<AdaptedControlResult, FieldData>((Func<AdaptedControlResult, FieldData>)(f => new FieldData()
                {
                    FieldId = new Guid(f.FieldID),
                    FieldName = ((IEnumerable<IFieldItem>)formItem.Fields).First<IFieldItem>((Func<IFieldItem, bool>)(flds => ((object)flds.ID).ToString() == f.FieldID)).Name,
                    Data = f.Secure ? string.Empty : f.Parameters,
                    Value = f.Secure ? string.Empty : f.Value
                })).ToArray<FieldData>();
                this.dataProvider.InsertFormData(new FormData()
                {
                    ContactId = this.logger.IsNull((object)this.analyticsTracker.CurrentContact, "Tracker.Current.Contact") ? Guid.Empty : this.analyticsTracker.CurrentContact.ContactId,
                    FormID = formid.Guid,
                    InteractionId = this.logger.IsNull((object)this.analyticsTracker.CurrentInteraction, " Tracker.Current.Interaction") ? Guid.Empty : this.analyticsTracker.CurrentInteraction.InteractionId,
                    Fields = (IEnumerable<FieldData>)array,
                    Timestamp = DateTime.UtcNow
                });
            }
        }

        public virtual List<AdaptedControlResult> AdaptResult(
          IEnumerable<ControlResult> list,
          bool simpleAdapt)
        {
            return list.Select<ControlResult, AdaptedControlResult>((Func<ControlResult, AdaptedControlResult>)(result => new AdaptedControlResult(result, this.fieldProvider, simpleAdapt))).ToList<AdaptedControlResult>();
        }
    }
}
