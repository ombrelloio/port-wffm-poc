// Decompiled with JetBrains decompiler
// Type: Sitecore.WFFM.Actions.Base.WffmAction
// Assembly: Sitecore.WFFM.Actions, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6607DF78-1371-42DC-8675-A986B5291018
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.WFFM.Actions.dll

using Sitecore.Data;
using Sitecore.Diagnostics;
using Sitecore.WFFM.Abstractions.Actions;
using System;

namespace Sitecore.WFFM.Actions.Base
{
  [Serializable]
  public abstract class WffmAction : IAction
  {
    protected WffmAction(ActionType actionType) => this.ActionType = actionType;

    public ID ActionID { get; set; }

    public string UniqueKey { get; set; }

    public ActionType ActionType { get; private set; }

    public virtual ActionState QueryState(ActionQueryContext queryContext)
    {
      Assert.ArgumentNotNull((object) queryContext, nameof (queryContext));
      return ActionState.Enabled;
    }
  }
}
