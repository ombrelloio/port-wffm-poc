// Decompiled with JetBrains decompiler
// Type: Sitecore.WFFM.Abstractions.Actions.IActionExecutor
// Assembly: Sitecore.WFFM.Abstractions, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 893E7AF0-E7C2-4A02-B47B-5687A98493FC
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.WFFM.Abstractions.dll

using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.WFFM.Abstractions.Data;
using Sitecore.WFFM.Abstractions.Dependencies;
using System.Collections.Generic;

namespace Sitecore.WFFM.Abstractions.Actions
{
  [DependencyPath("wffm/wffmActionExecutor")]
  public interface IActionExecutor
  {
    IActionItem GetAcitonByUniqId(IFormItem form, string uniqid, bool saveAction);

    IEnumerable<IActionItem> GetSaveActions(Item form);

    IEnumerable<IActionItem> GetCheckActions(Item form);

    void ExecuteChecking(ID formID, ControlResult[] fields, IActionDefinition[] actionDefinitions);

    ExecuteResult ExecuteSaving(
      ID formID,
      ControlResult[] fields,
      IActionDefinition[] actionDefinitions,
      bool simpleAdapt,
      ID sessionID);

    void ExecuteSystemAction(ID formID, ControlResult[] list);

    void SaveFormToDatabase(ID formid, AdaptedResultList fields);

    void RaiseEvent(string eventName, params object[] args);

    IEnumerable<IActionItem> GetActions(Item form);
  }
}
