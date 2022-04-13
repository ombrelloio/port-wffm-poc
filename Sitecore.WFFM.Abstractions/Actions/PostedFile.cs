// Decompiled with JetBrains decompiler
// Type: Sitecore.WFFM.Abstractions.Actions.PostedFile
// Assembly: Sitecore.WFFM.Abstractions, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 893E7AF0-E7C2-4A02-B47B-5687A98493FC
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.WFFM.Abstractions.dll

using Sitecore.IO;
using Sitecore.Resources.Media;
using System;
using System.IO;

namespace Sitecore.WFFM.Abstractions.Actions
{
  [Serializable]
  public class PostedFile
  {
    public PostedFile()
    {
    }

    public PostedFile(byte[] data, string fileName, string destination)
      : this()
    {
      this.Data = data;
      this.FileName = fileName;
      this.Destination = destination;
    }

    public byte[] Data { get; set; }

    public string Destination { get; set; }

    public string FileName { get; set; }

    public Stream GetInputStream()
    {
      MemoryStream memoryStream = new MemoryStream();
      memoryStream.Write(this.Data, 0, this.Data.Length);
      return (Stream) memoryStream;
    }

    public void SaveAs(string filename)
    {
      using (FileStream fileStream = new FileStream(filename, FileMode.Create))
        fileStream.Write(this.Data, 0, this.Data.Length);
    }

    public override string ToString() => MediaPathManager.ProposeValidMediaPath(FileUtil.MakePath(this.Destination, Path.GetFileName(this.FileName), '/'));
  }
}
