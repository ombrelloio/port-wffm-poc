// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.Media.Wave
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Forms.Core.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Sitecore.Form.Core.Media
{
  internal class Wave
  {
    public static void Concat(StreamList inStreams, BinaryWriter to)
    {
      if (inStreams == null)
        return;
      BinaryReader binaryReader = new BinaryReader(inStreams[0]);
      to.Write(binaryReader.ReadBytes(42));
      to.Write(Wave.GetBodyLength(inStreams));
      foreach (Stream inStream in (List<Stream>) inStreams)
      {
        if (inStream != null)
        {
          byte[] buffer = new byte[inStream.Length - 46L];
          inStream.Position = 46L;
          inStream.Read(buffer, 0, buffer.Length);
          inStream.Close();
          to.Write(buffer);
        }
      }
    }

    private static int GetBodyLength(StreamList inStreams) => inStreams.Where<Stream>((Func<Stream, bool>) (stream => stream != null)).Sum<Stream>((Func<Stream, int>) (stream => (int) stream.Length - 46));
  }
}
