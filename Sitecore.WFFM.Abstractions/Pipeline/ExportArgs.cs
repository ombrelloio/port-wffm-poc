// Decompiled with JetBrains decompiler
// Type: Sitecore.WFFM.Abstractions.Pipeline.ExportArgs
// Assembly: Sitecore.WFFM.Abstractions, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 893E7AF0-E7C2-4A02-B47B-5687A98493FC
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.WFFM.Abstractions.dll

using Sitecore.Web.UI.Sheer;
using System;

namespace Sitecore.WFFM.Abstractions.Pipeline
{
  [Serializable]
  public class ExportArgs : ClientPipelineArgs
  {
    public ExportArgs(string content, string contentType, string name)
    {
      this.Result = content;
      this.ContentType = contentType;
      this.FileName = name;
    }

    public string SessionKey { get; private set; }

    public string ContentType { get; private set; }

    public string FileName { get; private set; }
  }
}
