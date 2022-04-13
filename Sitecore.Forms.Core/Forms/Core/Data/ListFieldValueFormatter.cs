// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Core.Data.ListFieldValueFormatter
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Diagnostics;
using Sitecore.WFFM.Abstractions.Actions;
using Sitecore.WFFM.Abstractions.Shared;
using System.Collections.Generic;
using System.Text;

namespace Sitecore.Forms.Core.Data
{
  public class ListFieldValueFormatter
  {
    private readonly ISettings settings;

    public ListFieldValueFormatter(ISettings settings)
    {
      Assert.ArgumentNotNull((object) settings, nameof (settings));
      this.settings = settings;
    }

    public ControlResult GetFormattedResult(
      string title,
      IEnumerable<string> values,
      IEnumerable<string> texts)
    {
      return this.GetFormattedResult((string) null, title, values, texts);
    }

    public ControlResult GetFormattedResult(
      string fieldItemId,
      string title,
      IEnumerable<string> values,
      IEnumerable<string> texts)
    {
      string str = string.Empty;
      string parameters = string.Empty;
      if (values != null)
      {
        string delimiterCharacter = this.settings.ListFieldItemsDelimiterCharacter;
        str = string.IsNullOrEmpty(delimiterCharacter) ? this.DefaultFormatValue(values) : this.FormatValue(values, delimiterCharacter);
        parameters = string.Join(",", texts);
      }
      return new ControlResult(fieldItemId, title, (object) str, parameters);
    }

    private string DefaultFormatValue(IEnumerable<string> values)
    {
      StringBuilder stringBuilder = new StringBuilder();
      foreach (string str in values)
        stringBuilder.AppendFormat("<item>{0}</item>", (object) str);
      return stringBuilder.ToString();
    }

    private string FormatValue(IEnumerable<string> values, string delimiter) => string.Join(delimiter, values);
  }
}
