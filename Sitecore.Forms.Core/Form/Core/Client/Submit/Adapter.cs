// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.Client.Submit.Adapter
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.WFFM.Abstractions.Data;
using System.Collections.Generic;

namespace Sitecore.Form.Core.Client.Submit
{
  public abstract class Adapter
  {
    public virtual string AdaptResult(IFieldItem field, object value) => value.ToString();

    public virtual string AdaptToFriendlyValue(IFieldItem field, string value) => value;

    public virtual string AdaptToSitecoreStandard(IFieldItem field, string value) => this.AdaptToFriendlyValue(field, value);

    public virtual IEnumerable<string> AdaptToFriendlyListValues(
      IFieldItem field,
      string value,
      bool returnTexts)
    {
      return (IEnumerable<string>) new List<string>((IEnumerable<string>) new string[1]
      {
        value
      });
    }
  }
}
