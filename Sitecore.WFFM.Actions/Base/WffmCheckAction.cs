// Decompiled with JetBrains decompiler
// Type: Sitecore.WFFM.Actions.Base.WffmCheckAction
// Assembly: Sitecore.WFFM.Actions, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6607DF78-1371-42DC-8675-A986B5291018
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.WFFM.Actions.dll

using Sitecore.Data;
using Sitecore.WFFM.Abstractions.Actions;
using System.Collections.Generic;

namespace Sitecore.WFFM.Actions.Base
{
  public abstract class WffmCheckAction : WffmAction, ICheckAction, IAction
  {
    protected WffmCheckAction()
      : base(ActionType.Check)
    {
    }

    public abstract void Execute(
      ID formid,
      IEnumerable<ControlResult> fields,
      ActionCallContext actionCallContext = null);
  }
}
