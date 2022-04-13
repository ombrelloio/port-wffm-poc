// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Mvc.Validators.ReCaptchaResponse
// Assembly: Sitecore.Forms.Mvc, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1AD2DE3-BABE-4597-B643-AA577F98B8D6
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Mvc.dll

using Newtonsoft.Json;
using System.Collections.Generic;

namespace Sitecore.Forms.Mvc.Validators
{
  public class ReCaptchaResponse
  {
    [JsonProperty("success")]
    public bool Success { get; set; }

    [JsonProperty("error-codes")]
    public List<string> ErrorCodes { get; set; }
  }
}
