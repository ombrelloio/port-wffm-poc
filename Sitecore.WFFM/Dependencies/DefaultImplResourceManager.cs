// Decompiled with JetBrains decompiler
// Type: Sitecore.WFFM.Core.Dependencies.DefaultImplResourceManager
// Assembly: Sitecore.WFFM, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 979A341A-AC67-414D-B20E-04AAE41E21EA
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.WFFM.dll

using Sitecore.Diagnostics;
using Sitecore.WFFM.Abstractions.Shared;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;

namespace Sitecore.WFFM.Core.Dependencies
{
  public class DefaultImplResourceManager : IResourceManager
  {
    private readonly ResourceManager rm;
    private readonly ITranslationProvider translationProvider;

    public DefaultImplResourceManager(string resourceName, ITranslationProvider translationProvider)
    {
      Assert.IsNotNull((object) translationProvider, "Dependency translationProvider is null");
      this.rm = new ResourceManager(resourceName, Assembly.GetExecutingAssembly());
      Assert.IsNotNull((object) this.rm, "Resource manager for " + resourceName + " is not initialized");
      this.translationProvider = translationProvider;
    }

    public string GetString(string resIdentifier) => this.rm.GetString(resIdentifier);

    public string Localize(string resIdentifier) => this.translationProvider.Text(this.GetString(resIdentifier) ?? string.Empty);

    public string Localize(string resIdentifier, params string[] parameters) => this.translationProvider.Text(this.GetString(resIdentifier), parameters);

    public UnmanagedMemoryStream GetObject(string resIdentifier)
    {
      Assembly executingAssembly = Assembly.GetExecutingAssembly();
      string name = ((IEnumerable<string>) executingAssembly.GetManifestResourceNames()).SingleOrDefault<string>((Func<string, bool>) (resource => resource.EndsWith(resIdentifier, StringComparison.InvariantCultureIgnoreCase)));
      return name != null ? (UnmanagedMemoryStream) executingAssembly.GetManifestResourceStream(name) : (UnmanagedMemoryStream) null;
    }
  }
}
