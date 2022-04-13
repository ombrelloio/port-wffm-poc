// Decompiled with JetBrains decompiler
// Type: Sitecore.WFFM.Abstractions.Actions.IActionItem
// Assembly: Sitecore.WFFM.Abstractions, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 893E7AF0-E7C2-4A02-B47B-5687A98493FC
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.WFFM.Abstractions.dll

using Sitecore.Data;

namespace Sitecore.WFFM.Abstractions.Actions
{
  public interface IActionItem
  {
    ActionType ActionType { get; }

    IAction ActionInstance { get; }

    string Assembly { get; }

    string Class { get; }

    string FactoryObjectName { get; }

    string Description { get; }

    string Editor { get; }

    string GlobalParameters { get; }

    bool IsClientAction { get; }

    string LocalizedParameters { get; }

    string Parameters { get; }

    string QueryString { get; }

    string Tooltip { get; }

    Database Database { get; }

    string DisplayName { get; }

    string Icon { get; }

    ID ID { get; }

    Sitecore.Data.Items.Item InnerItem { get; }

    string Name { get; }

    string this[ID fieldId] { get; }

    void BeginEdit();

    void EndEdit();
  }
}
