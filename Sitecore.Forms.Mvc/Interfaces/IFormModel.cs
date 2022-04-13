// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Mvc.Interfaces.IFormModel
// Assembly: Sitecore.Forms.Mvc, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1AD2DE3-BABE-4597-B643-AA577F98B8D6
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Mvc.dll

using Sitecore.WFFM.Abstractions.Actions;
using Sitecore.WFFM.Abstractions.Data;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace Sitecore.Forms.Mvc.Interfaces
{
  public interface IFormModel : IModelEntity
  {
    IFormItem Item { get; }

    DateTime RenderedTime { get; set; }

    int EventCounter { get; set; }

    List<ControlResult> Results { get; set; }

    bool ReadQueryString { get; set; }

    NameValueCollection QueryParameters { get; set; }
  }
}
