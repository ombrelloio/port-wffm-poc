// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Core.Dependencies.DefaultImplContextProvider
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.WFFM.Abstractions.Actions;
using Sitecore.WFFM.Abstractions.Shared;
using System;
using System.Collections.Generic;

namespace Sitecore.Forms.Core.Dependencies
{
  [Serializable]
  public class DefaultImplContextProvider : IContextProvider
  {
    public List<ExecuteResult.Failure> FormFailures
    {
      get => (List<ExecuteResult.Failure>) Context.Items["scwfm_failures"];
      set => Context.Items["scwfm_failures"] = (object) value;
    }
  }
}
