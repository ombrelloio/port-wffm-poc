// Decompiled with JetBrains decompiler
// Type: Sitecore.WFFM.Abstractions.Utils.IFormRegistryUtil
// Assembly: Sitecore.WFFM.Abstractions, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 893E7AF0-E7C2-4A02-B47B-5687A98493FC
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.WFFM.Abstractions.dll

namespace Sitecore.WFFM.Abstractions.Utils
{
  public interface IFormRegistryUtil
  {
    string GetString(string prefix, string form, string defaultvalue);

    void SetString(string prefix, string form, string value);

    string GetFieldsRestriction(string form, string defaultvalue);

    string GetExportRestriction(string form, string defaultvalue);

    void SetFieldsRestriction(string form, string value);

    void SetExportRestriction(string form, string value);
  }
}
