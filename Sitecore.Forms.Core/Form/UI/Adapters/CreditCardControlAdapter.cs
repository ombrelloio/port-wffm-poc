// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.UI.Adapters.CreditCardControlAdapter
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Form.Core.Client.Submit;
using Sitecore.Form.Core.Utility;
using Sitecore.WFFM.Abstractions.Data;
using System.Collections.Generic;

namespace Sitecore.Form.UI.Adapters
{
  public class CreditCardControlAdapter : Adapter
  {
    public override IEnumerable<string> AdaptToFriendlyListValues(
      IFieldItem field,
      string value,
      bool returnTexts)
    {
      return ParametersUtil.XmlToStringArray(value);
    }

    public override string AdaptToFriendlyValue(IFieldItem field, string value) => !string.IsNullOrEmpty(value) && value.Contains("<item>") ? string.Join(", ", new List<string>(this.AdaptToFriendlyListValues(field, value, true)).ToArray()) : value ?? string.Empty;
  }
}
