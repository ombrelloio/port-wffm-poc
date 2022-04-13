// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.Visual.ValidationType
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using System;

namespace Sitecore.Form.Core.Visual
{
  [Flags]
  public enum ValidationType
  {
    None = 0,
    Integer = 1,
    Email = 2,
    NotEmpty = 4,
    Date = 8,
  }
}
