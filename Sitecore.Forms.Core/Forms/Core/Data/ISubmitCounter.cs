// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Core.Data.ISubmitCounter
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Data;

namespace Sitecore.Forms.Core.Data
{
  public interface ISubmitCounter
  {
    int GetSubmitCount(ID formID);

    void AddSubmit(ID form, uint keepItMinutes);
  }
}
