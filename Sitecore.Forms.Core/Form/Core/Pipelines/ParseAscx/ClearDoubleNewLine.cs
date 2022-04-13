// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.Pipelines.ParseAscx.ClearDoubleNewLine
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Form.Core.Pipeline.ParseAscx;
using System.Text.RegularExpressions;

namespace Sitecore.Form.Core.Pipelines.ParseAscx
{
  public class ClearDoubleNewLine
  {
    public static readonly string DoubleNewLine = "\r\n";

    public void Process(ParseAscxArgs args)
    {
      args.AscxContent = Regex.Replace(args.AscxContent, "\r\n[\\s]*\r\n", "\r\n", RegexOptions.IgnoreCase | RegexOptions.Multiline);
      args.AscxContent = args.AscxContent.Replace(StringUtil.Repeat(ClearDoubleNewLine.DoubleNewLine, 3), ClearDoubleNewLine.DoubleNewLine).Replace(StringUtil.Repeat(ClearDoubleNewLine.DoubleNewLine, 2), ClearDoubleNewLine.DoubleNewLine);
    }
  }
}
