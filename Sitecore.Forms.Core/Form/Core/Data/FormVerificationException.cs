// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.Data.FormVerificationException
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using System;

namespace Sitecore.Form.Core.Data
{
  [Serializable]
  public class FormVerificationException : Exception
  {
    public FormVerificationException(string message, bool isCustomErrorMessage)
      : base(message)
    {
      this.IsCustomErrorMessage = isCustomErrorMessage;
    }

    public bool IsCustomErrorMessage { get; set; }
  }
}
