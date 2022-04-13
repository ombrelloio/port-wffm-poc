// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.Validators.IsoDateValidatior
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using System.Web.UI.WebControls;

namespace Sitecore.Form.Core.Validators
{
  public class IsoDateValidatior : CustomValidator
  {
    protected override bool OnServerValidate(string value) => string.IsNullOrEmpty(value) || DateUtil.IsIsoDate(value);
  }
}
