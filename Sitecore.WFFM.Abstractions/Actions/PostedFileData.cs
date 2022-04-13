// Decompiled with JetBrains decompiler
// Type: Sitecore.WFFM.Abstractions.Actions.PostedFileData
// Assembly: Sitecore.WFFM.Abstractions, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 893E7AF0-E7C2-4A02-B47B-5687A98493FC
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.WFFM.Abstractions.dll

using System;

namespace Sitecore.WFFM.Abstractions.Actions
{
  [Serializable]
  public class PostedFileData
  {
    public Guid BlobId { get; set; }

    public string Destination { get; set; }

    public string FileName { get; set; }

    public string DatabaseName { get; set; }
  }
}
