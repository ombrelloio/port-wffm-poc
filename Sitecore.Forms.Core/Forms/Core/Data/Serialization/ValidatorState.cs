// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Core.Data.Serialization.ValidatorState
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using System;
using System.Web.UI;

namespace Sitecore.Forms.Core.Data.Serialization
{
  [Serializable]
  public class ValidatorState
  {
    public ValidatorState(IValidator validator)
    {
      this.ValidatorType = validator.GetType();
      this.ErrorMessage = validator.ErrorMessage;
      this.IsValid = validator.IsValid;
    }

    public Type ValidatorType { get; set; }

    public string ErrorMessage { get; set; }

    public bool IsValid { get; set; }
  }
}
