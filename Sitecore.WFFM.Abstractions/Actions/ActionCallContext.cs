// Decompiled with JetBrains decompiler
// Type: Sitecore.WFFM.Abstractions.Actions.ActionCallContext
// Assembly: Sitecore.WFFM.Abstractions, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 893E7AF0-E7C2-4A02-B47B-5687A98493FC
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.WFFM.Abstractions.dll

using Sitecore.WFFM.Abstractions.Data;
using System;
using System.Collections.Generic;

namespace Sitecore.WFFM.Abstractions.Actions
{
  [Serializable]
  public class ActionCallContext
  {
    private readonly Dictionary<string, object> parameters = new Dictionary<string, object>();

    public IDictionary<string, object> Parameters => (IDictionary<string, object>) this.parameters;

    public IFormItem FormItem { get; set; }
  }
}
