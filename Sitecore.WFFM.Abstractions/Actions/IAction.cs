// Decompiled with JetBrains decompiler
// Type: Sitecore.WFFM.Abstractions.Actions.IAction
// Assembly: Sitecore.WFFM.Abstractions, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 893E7AF0-E7C2-4A02-B47B-5687A98493FC
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.WFFM.Abstractions.dll

using Sitecore.Data;

namespace Sitecore.WFFM.Abstractions.Actions
{
  public interface IAction
  {
    ID ActionID { get; set; }

    string UniqueKey { get; set; }

    ActionType ActionType { get; }

    ActionState QueryState(ActionQueryContext queryContext);
  }
}
