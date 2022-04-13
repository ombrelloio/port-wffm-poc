// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Core.Handlers.FormDataHandler
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Abstractions;
using Sitecore.Collections;
using Sitecore.Data;
using Sitecore.Data.Managers;
using Sitecore.Diagnostics;
using Sitecore.Form.Core;
using Sitecore.Form.Core.Configuration;
using Sitecore.Form.Core.Data;
using Sitecore.Form.Core.Pipelines.FormSubmit;
using Sitecore.Form.Core.Utility;
using Sitecore.Links;
using Sitecore.Pipelines;
using Sitecore.WFFM.Abstractions.Actions;
using Sitecore.WFFM.Abstractions.Data;
using Sitecore.WFFM.Abstractions.Dependencies;
using Sitecore.WFFM.Abstractions.Shared;
using Sitecore.WFFM.Abstractions.Utils;
using Sitecore.WFFM.Abstractions.Wrappers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Sitecore.Forms.Core.Handlers
{
  [DependencyPath("wffm/formDataHandler")]
  public class FormDataHandler
  {
    private readonly IActionExecutor _actionExecutor;
    private readonly IAnalyticsTracker _analyticsTracker;
    private readonly BaseCorePipelineManager _baseCorePipelineManager;
    private readonly IFormContext _formContext;
    private readonly IItemRepository _itemRepository;
    private readonly ILogger _logger;
    private readonly Sitecore.WFFM.Abstractions.Shared.ISettings _settings;
    private readonly ISitecoreContextWrapper _sitecoreContextWrapper;
    private readonly IWebUtil _webUtil;

    public FormDataHandler(
      IActionExecutor actionExecutor,
      ILogger logger,
      ISitecoreContextWrapper sitecoreContextWrapper,
      Sitecore.WFFM.Abstractions.Shared.ISettings settings,
      IAnalyticsTracker analyticsTracker,
      IItemRepository itemRepository,
      BaseCorePipelineManager baseCorePipelineManager,
      IWebUtil webUtil,
      IFormContext formContext)
    {
      Assert.ArgumentNotNull((object) actionExecutor, "actionExecutorParam");
      Assert.ArgumentNotNull((object) logger, nameof (logger));
      Assert.ArgumentNotNull((object) sitecoreContextWrapper, nameof (sitecoreContextWrapper));
      Assert.ArgumentNotNull((object) settings, nameof (settings));
      Assert.ArgumentNotNull((object) analyticsTracker, nameof (analyticsTracker));
      Assert.ArgumentNotNull((object) itemRepository, nameof (itemRepository));
      Assert.ArgumentNotNull((object) baseCorePipelineManager, nameof (baseCorePipelineManager));
      Assert.ArgumentNotNull((object) webUtil, nameof (webUtil));
      Assert.ArgumentNotNull((object) formContext, nameof (formContext));
      this._actionExecutor = actionExecutor;
      this._logger = logger;
      this._sitecoreContextWrapper = sitecoreContextWrapper;
      this._settings = settings;
      this._analyticsTracker = analyticsTracker;
      this._itemRepository = itemRepository;
      this._baseCorePipelineManager = baseCorePipelineManager;
      this._webUtil = webUtil;
      this._formContext = formContext;
    }

    protected string EventDatabaseName => !string.IsNullOrEmpty(this._settings.SharedDatabaseName) ? this._settings.SharedDatabaseName : Context.Database.Name;

    public void ProcessForm(ID formId, ControlResult[] fields, IActionDefinition[] actions) => this.ProcessFormImplementation(formId, fields, actions, this._actionExecutor);

    private void ProcessFormImplementation(
      ID formId,
      ControlResult[] fields,
      IActionDefinition[] actions,
      IActionExecutor actionExecutorParam)
    {
      Assert.ArgumentNotNull((object) formId, "formID");
      Assert.ArgumentNotNull((object) fields, nameof (fields));
      Assert.ArgumentNotNull((object) actions, nameof (actions));
      this._formContext.Failures = new List<ExecuteResult.Failure>();
      if (ID.IsNullOrEmpty(formId))
        return;
      actionExecutorParam.ExecuteChecking(formId, fields, actions);
      try
      {
        this.ExecuteSaveActions(formId, fields, actions, actionExecutorParam);
        actionExecutorParam.ExecuteSystemAction(formId, fields);
      }
      catch (Exception ex)
      {
        this._logger.Warn(ex.Message, ex, (object) this);
        this._formContext.Failures.Add(new ExecuteResult.Failure()
        {
          ErrorMessage = ex.Message,
          FailedAction = ((object) ID.Null).ToString(),
          IsCustom = false
        });
      }
      if (this._formContext.Failures.Any<ExecuteResult.Failure>())
      {
        SubmittedFormFailuresArgs formFailuresArgs1 = new SubmittedFormFailuresArgs(formId, (IEnumerable<ExecuteResult.Failure>) this._formContext.Failures);
        formFailuresArgs1.Database = this._settings.ContextDatabaseName;
        SubmittedFormFailuresArgs formFailuresArgs2 = formFailuresArgs1;
        try
        {
          this._baseCorePipelineManager.Run("errorSubmit", (PipelineArgs) formFailuresArgs2);
        }
        catch (Exception ex)
        {
          this._logger.Warn(ex.Message, ex, (object) this);
        }
        throw new FormSubmitException((IEnumerable<ExecuteResult.Failure>) formFailuresArgs2.Failures);
      }
    }

    private ControlResult GetSerializedControlResult(ControlResult result)
    {
      string serializedType;
      return new ControlResult()
      {
        FieldID = result.FieldID,
        FieldName = result.FieldName,
        Value = this.GetSerializedValue(result.Value, out serializedType),
        FieldType = serializedType,
        Parameters = result.Parameters,
        Secure = result.Secure,
        AdaptForAnalyticsTag = result.AdaptForAnalyticsTag
      };
    }

    private IEnumerable<ControlResult> GetSerializableControlResults(
      IEnumerable<ControlResult> fields)
    {
      if (!(fields is ControlResult[] controlResultArray))
        controlResultArray = fields.ToArray<ControlResult>();
      List<ControlResult> controlResultList = new List<ControlResult>();
      foreach (ControlResult result in controlResultArray)
        controlResultList.Add(this.GetSerializedControlResult(result));
      return (IEnumerable<ControlResult>) controlResultList;
    }

    private long GetUploadedSizeOfAllFiles(IEnumerable<ControlResult> fields)
    {
            long num = 0L;
            foreach (ControlResult field in fields)
            {
                PostedFile postedFile = field.Value as PostedFile;
                if (postedFile?.Data != null)
                {
                    num += postedFile.Data.Length;
                }
            }
            return num;
        }

    private object GetSerializedValue(object value, out string serializedType)
    {
      if (value is PostedFile postedFile)
      {
        Guid guid = Guid.NewGuid();
        Database database = string.IsNullOrEmpty(Settings.SharedDatabaseName) ? Context.Database : Database.GetDatabase(Settings.SharedDatabaseName);
        using (MemoryStream memoryStream = new MemoryStream(postedFile.Data))
          ItemManager.SetBlobStream((Stream) memoryStream, guid, database);
        value = (object) new PostedFileData()
        {
          BlobId = guid,
          Destination = postedFile.Destination,
          FileName = postedFile.FileName,
          DatabaseName = database.Name
        };
      }
      serializedType = value == null ? typeof (object).ToString() : value.GetType().AssemblyQualifiedName;
      StringBuilder sb = new StringBuilder();
      using (TextWriter textWriter = (TextWriter) new StringWriter(sb))
      {
        Type type = value?.GetType();
        if ((object) type == null)
          type = typeof (object);
        new XmlSerializer(type).Serialize(textWriter, value);
        value = (object) sb.ToString();
      }
      return value;
    }

    private void ExecuteSaveActions(
      ID formId,
      ControlResult[] fields,
      IActionDefinition[] actions,
      IActionExecutor actionExecutorParam)
    {
            if ((this._sitecoreContextWrapper.ContextSiteDisplayMode == Sites.DisplayMode.Normal ? 1 : (this._sitecoreContextWrapper.ContextSiteDisplayMode == Sites.DisplayMode.Preview ? 1 : 0)) == 0 || this._webUtil.GetQueryString("sc_debug", (string)null) != null)
                return;
            if (this._settings.IsRemoteActions)
      {
        this.TransformSendEmailAction(actions);
        WffmActionEvent wffmActionEvent = new WffmActionEvent()
        {
          FormID = formId,
          SessionIDGuid = this._analyticsTracker.SessionId.Guid,
          Actions = ((IEnumerable<IActionDefinition>) actions).Where<IActionDefinition>((Func<IActionDefinition, bool>) (s =>
          {
            IActionItem action = this._itemRepository.CreateAction(s.ActionID);
            return action != null && !action.IsClientAction;
          })).ToArray<IActionDefinition>(),
          Fields = this.GetSerializableControlResults((IEnumerable<ControlResult>) fields).ToArray<ControlResult>(),
          UserName = this._settings.RemoteActionsUserName,
          Password = this._settings.RemoteActionsUserPassword,
          CMInstanceName = this._settings.CMInstanceName
        };
        (string.IsNullOrEmpty(Settings.SharedDatabaseName) ? Context.Database : Database.GetDatabase(Settings.SharedDatabaseName)).RemoteEvents.EventQueue.QueueEvent<WffmActionEvent>(wffmActionEvent, true, false);
        ExecuteResult executeResult = actionExecutorParam.ExecuteSaving(formId, fields, ((IEnumerable<IActionDefinition>) actions).Where<IActionDefinition>((Func<IActionDefinition, bool>) (s =>
        {
          IActionItem action = this._itemRepository.CreateAction(s.ActionID);
          return action != null && action.IsClientAction;
        })).ToArray<IActionDefinition>(), true, this._analyticsTracker.SessionId);
        if (!((IEnumerable<ExecuteResult.Failure>) executeResult.Failures).Any<ExecuteResult.Failure>())
          return;
        this._formContext.Failures.AddRange((IEnumerable<ExecuteResult.Failure>) executeResult.Failures);
      }
      else
      {
        ExecuteResult executeResult = actionExecutorParam.ExecuteSaving(formId, fields, actions, false, this._analyticsTracker.SessionId);
        if (!((IEnumerable<ExecuteResult.Failure>) executeResult.Failures).Any<ExecuteResult.Failure>())
          return;
        this._formContext.Failures.AddRange((IEnumerable<ExecuteResult.Failure>) executeResult.Failures);
      }
    }

    private void TransformSendEmailAction(IActionDefinition[] actions)
    {
      foreach (IActionDefinition action in actions)
      {
        if (action.ActionID == ((object) ActionsIDs.SendMail).ToString())
          this.ResolveEmailParameters(action);
      }
    }

    private void ResolveEmailParameters(IActionDefinition actionDefinition)
    {
      IEnumerable<Pair<string, string>> pairArray1 = ParametersUtil.XmlToPairArray(actionDefinition.Paramaters);
      if (!(pairArray1 is Pair<string, string>[] pairArray2))
        pairArray2 = pairArray1.ToArray<Pair<string, string>>();
      Pair<string, string>[] pairArray3 = pairArray2;
      actionDefinition.Paramaters = this.ResolveEmailBodyInternalLink((IEnumerable<Pair<string, string>>) pairArray3);
    }

    private string ResolveEmailBodyInternalLink(
      IEnumerable<Pair<string, string>> originalPairParameters)
    {
      string newValue = string.Join(string.Empty, new string[3]
      {
        "href=\"",
        this._webUtil.GetServerUrl(),
        "/"
      });
      List<Pair<string, string>> pairList = new List<Pair<string, string>>();
      foreach (Pair<string, string> originalPairParameter in originalPairParameters)
      {
        string str = originalPairParameter.Part2;
        if (string.Equals(originalPairParameter.Part1, "Mail", StringComparison.OrdinalIgnoreCase))
          str = LinkManager.ExpandDynamicLinks(originalPairParameter.Part2).Replace("href=\"/", newValue);
        pairList.Add(new Pair<string, string>(originalPairParameter.Part1, str));
      }
      return ParametersUtil.PairArrayToXml((IEnumerable<Pair<string, string>>) pairList);
    }
  }
}
