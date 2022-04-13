// Decompiled with JetBrains decompiler
// Type: Sitecore.WFFM.Actions.Base.WffmSystemAction
// Assembly: Sitecore.WFFM.Actions, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6607DF78-1371-42DC-8675-A986B5291018
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.WFFM.Actions.dll

using Sitecore.Data;
using Sitecore.WFFM.Abstractions.Actions;

namespace Sitecore.WFFM.Actions.Base
{
  public abstract class WffmSystemAction : WffmAction, ISystemAction, IAction
  {
    protected WffmSystemAction()
      : base(ActionType.System)
    {
    }

    public abstract void Execute(
      ID formid,
      AdaptedResultList adaptedFields,
      ActionCallContext actionCallContext = null,
      params object[] data);
  }
}
