// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Core.Dependencies.DefaultImplTranslationProvider
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Form.Core.Configuration;
using Sitecore.WFFM.Abstractions.Shared;
using System;

namespace Sitecore.Forms.Core.Dependencies
{
  [Serializable]
  public class DefaultImplTranslationProvider : ITranslationProvider
  {
    public string SystemText(string key) => Translate.SystemText(key);

    public string Text(string key) => Translate.Text(key);

    public string Text(string key, params string[] parameters) => Translate.Text(key, parameters);
  }
}
