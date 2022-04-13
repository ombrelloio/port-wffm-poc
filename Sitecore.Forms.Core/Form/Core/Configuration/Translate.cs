// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.Configuration.Translate
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Diagnostics;
using Sitecore.Globalization;
using System;

namespace Sitecore.Form.Core.Configuration
{
  public class Translate
  {
    public static string Text(string key, params string[] parameters)
    {
      Assert.ArgumentNotNull((object) key, nameof (key));
      Assert.ArgumentNotNull((object) parameters, nameof (parameters));
      string format = Translate.Text(key);
      return parameters.Length != 0 ? string.Format(format, (object[]) parameters) : format;
    }

    public static string Text(string key) => Sitecore.Globalization.Translate.TextByLanguage(key, Context.Language, key);

    public static string TextByItemLanguage(string key, string itemLanguageDisplayName) => string.Compare(itemLanguageDisplayName, Context.Language.GetDisplayName(), StringComparison.InvariantCultureIgnoreCase) != 0 ? Sitecore.Globalization.Translate.TextByLanguage(key, Context.Language, key) : key;

    internal static string SystemText(string key) => Translate.Text(key);
  }
}
