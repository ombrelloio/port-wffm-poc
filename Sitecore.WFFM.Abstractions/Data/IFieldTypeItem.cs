// Decompiled with JetBrains decompiler
// Type: Sitecore.WFFM.Abstractions.Data.IFieldTypeItem
// Assembly: Sitecore.WFFM.Abstractions, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 893E7AF0-E7C2-4A02-B47B-5687A98493FC
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.WFFM.Abstractions.dll

using Sitecore.Data;
using Sitecore.Data.Items;
using System.Collections.Generic;

namespace Sitecore.WFFM.Abstractions.Data
{
  public interface IFieldTypeItem
  {
    string AssemblyName { get; }

    string ClassName { get; }

    bool DenyTag { get; }

    bool IsRequired { get; }

    string LocalizedParameters { get; }

    string MVCClass { get; }

    string Parameters { get; }

    IEnumerable<ISubFieldItem> SubFields { get; }

    string UserControl { get; }

    string[] Validators { get; }

    Database Database { get; }

    string DisplayName { get; }

    string Icon { get; }

    ID ID { get; }

    Item InnerItem { get; }

    string Name { get; }

    void BeginEdit();

    void EndEdit();
  }
}
