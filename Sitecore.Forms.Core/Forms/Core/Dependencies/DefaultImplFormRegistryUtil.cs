// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Core.Dependencies.DefaultImplFormRegistryUtil
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Web.UI.HtmlControls;
using Sitecore.WFFM.Abstractions.Utils;
using System;

namespace Sitecore.Forms.Core.Dependencies
{
  [Serializable]
  public class DefaultImplFormRegistryUtil : IFormRegistryUtil
  {
    private const string BasePath = "/Current_User/Web Forms for Marketers/{0}/{1}";
    private const string ExportPrefix = "Export";
    private const string FieldsPrefix = "Fields";

    public string GetString(string prefix, string form, string defaultvalue) => Registry.GetString(string.Format("/Current_User/Web Forms for Marketers/{0}/{1}", (object) prefix, (object) form), defaultvalue);

    public void SetString(string prefix, string form, string value) => Registry.SetString(string.Format("/Current_User/Web Forms for Marketers/{0}/{1}", (object) prefix, (object) form), value);

    public string GetFieldsRestriction(string form, string defaultvalue) => this.GetString("Fields", form, defaultvalue);

    public string GetExportRestriction(string form, string defaultvalue) => this.GetString("Export", form, defaultvalue);

    public void SetFieldsRestriction(string form, string value) => this.SetString("Fields", form, value);

    public void SetExportRestriction(string form, string value) => this.SetString("Export", form, value);
  }
}
