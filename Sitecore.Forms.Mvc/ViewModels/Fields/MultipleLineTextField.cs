// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Mvc.ViewModels.Fields.MultipleLineTextField
// Assembly: Sitecore.Forms.Mvc, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1AD2DE3-BABE-4597-B643-AA577F98B8D6
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Mvc.dll

using System.ComponentModel.DataAnnotations;

namespace Sitecore.Forms.Mvc.ViewModels.Fields
{
  public class MultipleLineTextField : SingleLineTextField
  {
    public MultipleLineTextField()
    {
      this.Rows = 4;
      this.Columns = 1;
    }

    [DataType(DataType.MultilineText)]
    public override string Value { get; set; }

    public int Rows { get; set; }

    public int Columns { get; set; }

    public override string ResultParameters => "multipleline";
  }
}
