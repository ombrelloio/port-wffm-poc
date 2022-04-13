// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.Pipeline.ParseAscx.ParseAscxArgs
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Pipelines;
using System;
using System.Runtime.Serialization;

namespace Sitecore.Form.Core.Pipeline.ParseAscx
{
  [Serializable]
  public class ParseAscxArgs : PipelineArgs
  {
    public ParseAscxArgs(string ascxContent) => this.AscxContent = ascxContent;

    protected ParseAscxArgs(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }

    public string AscxContent { get; set; }
  }
}
