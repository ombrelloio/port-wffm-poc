// Decompiled with JetBrains decompiler
// Type: Sitecore.WFFM.Actions.Base.WffmSaveAction
// Assembly: Sitecore.WFFM.Actions, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6607DF78-1371-42DC-8675-A986B5291018
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.WFFM.Actions.dll

using Sitecore.Data;
using Sitecore.WFFM.Abstractions.Actions;
using System;

namespace Sitecore.WFFM.Actions.Base
{
  [Serializable]
  public abstract class WffmSaveAction : WffmAction, ISaveAction, IAction
  {
    protected WffmSaveAction()
      : base(ActionType.Save)
    {
    }

    public abstract void Execute(
      ID formId,
      AdaptedResultList adaptedFields,
      ActionCallContext actionCallContext = null,
      params object[] data);
  }
}
