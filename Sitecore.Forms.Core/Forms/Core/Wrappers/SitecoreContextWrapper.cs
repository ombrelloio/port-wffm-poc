// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Core.Wrappers.SitecoreContextWrapper
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Data;
using Sitecore.Sites;
using Sitecore.WFFM.Abstractions.Wrappers;
using System;

namespace Sitecore.Forms.Core.Wrappers
{
  [Serializable]
  public class SitecoreContextWrapper : ISitecoreContextWrapper
  {
    public Database Database => Context.Database;

    public string DatabaseName => Context.Database.Name;

    public string ContentDatabaseName => Context.ContentDatabase.Name;

    public DisplayMode ContextSiteDisplayMode => Context.Site.DisplayMode;

    public bool PageModeIsPreview => Context.PageMode.IsPreview;

    public bool PageModeIsExperienceEditorEditing => Context.PageMode.IsExperienceEditorEditing;

    public object GetItem(string key) => Context.Items[key];

    public void SetItem(string key, object value) => Context.Items[key] = value;
  }
}
