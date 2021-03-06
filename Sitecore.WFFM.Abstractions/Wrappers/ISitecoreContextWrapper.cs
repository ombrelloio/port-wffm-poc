// Decompiled with JetBrains decompiler
// Type: Sitecore.WFFM.Abstractions.Wrappers.ISitecoreContextWrapper
// Assembly: Sitecore.WFFM.Abstractions, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 893E7AF0-E7C2-4A02-B47B-5687A98493FC
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.WFFM.Abstractions.dll

using Sitecore.Data;
using Sitecore.Sites;
using Sitecore.WFFM.Abstractions.Dependencies;

namespace Sitecore.WFFM.Abstractions.Wrappers
{
  [DependencyPath("wffm/sitecoreContextWrapper")]
  public interface ISitecoreContextWrapper
  {
    Database Database { get; }

    string DatabaseName { get; }

    string ContentDatabaseName { get; }

    DisplayMode ContextSiteDisplayMode { get; }

    bool PageModeIsPreview { get; }

    bool PageModeIsExperienceEditorEditing { get; }

    void SetItem(string key, object value);

    object GetItem(string key);
  }
}
