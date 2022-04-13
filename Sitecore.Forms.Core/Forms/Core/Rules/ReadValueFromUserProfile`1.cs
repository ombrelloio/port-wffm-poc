// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Core.Rules.ReadValueFromUserProfile`1
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Form.Core.Data;
using Sitecore.Security.Accounts;

namespace Sitecore.Forms.Core.Rules
{
  public class ReadValueFromUserProfile<T> : ReadValue<T> where T : ConditionalRuleContext
  {
    protected override object GetValue() => (Account) Context.User != (Account) null ? Context.User.GetProfileValue(this.Name) : (object) null;
  }
}
