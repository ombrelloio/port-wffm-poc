// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.SchemaGenerator.RegisterDirective
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using System;
using System.Globalization;
using System.IO;
using System.Text;

namespace Sitecore.Form.Core.SchemaGenerator
{
  [Serializable]
  internal class RegisterDirective
  {
    protected string _tagPrefixKey;

    protected RegisterDirective()
    {
    }

    public RegisterDirective(string tagPrefix, string tagName, string appRelativeVirtualPath)
    {
      this.IsModified = true;
      this.IsPersisted = false;
      this.IsImplicit = false;
      this.TagPrefix = tagPrefix;
      this.TagName = tagName;
      this.Namespace = string.Empty;
      this.Assembly = string.Empty;
      this.SrcAppRelPath = appRelativeVirtualPath;
    }

    public RegisterDirective(
      string tagPrefix,
      string nameSpace,
      string assembly,
      string tagName,
      bool persisted,
      bool isImplicit)
    {
      this.IsModified = true;
      this.IsPersisted = persisted;
      this.IsImplicit = isImplicit;
      this.TagPrefix = tagPrefix;
      this.TagName = tagName;
      this.Namespace = nameSpace;
      this.Assembly = assembly;
    }

    public string ConstructCustomControlTypeName(string className)
    {
      if (string.IsNullOrEmpty(className) || !this.IsCustomControl)
        return (string) null;
      StringWriter stringWriter = new StringWriter((IFormatProvider) CultureInfo.InvariantCulture);
      if (!string.IsNullOrEmpty(this.Namespace))
      {
        stringWriter.Write(this.Namespace);
        stringWriter.Write(".");
      }
      stringWriter.Write(className);
      string assembly = this.Assembly;
      if (!string.IsNullOrEmpty(assembly))
      {
        stringWriter.Write(", ");
        stringWriter.Write(assembly);
      }
      else
      {
        stringWriter.Write(", ");
        stringWriter.Write("__code");
      }
      return stringWriter.ToString();
    }

    public override bool Equals(object obj2) => RegisterDirective.Equals((object) this, obj2);

    public override int GetHashCode() => base.GetHashCode();

    public new static bool Equals(object obj1, object obj2)
    {
      if (obj1 == null)
        return obj2 == null;
      if (obj2 == null)
        return false;
      RegisterDirective registerDirective1 = obj1 as RegisterDirective;
      RegisterDirective registerDirective2 = obj2 as RegisterDirective;
      if (!(registerDirective1 != (RegisterDirective) null) || !(registerDirective2 != (RegisterDirective) null) || (!string.IsNullOrEmpty(registerDirective1.TagPrefix) || !string.IsNullOrEmpty(registerDirective2.TagPrefix)) && !(registerDirective1.TagPrefix == registerDirective2.TagPrefix) || (!string.IsNullOrEmpty(registerDirective1.TagName) || !string.IsNullOrEmpty(registerDirective2.TagName)) && !(registerDirective1.TagName == registerDirective2.TagName) || !(registerDirective1.Namespace == registerDirective2.Namespace))
        return false;
      return string.IsNullOrEmpty(registerDirective1.Assembly) && string.IsNullOrEmpty(registerDirective2.Assembly) || registerDirective1.Assembly == registerDirective2.Assembly;
    }

    public string GetHtml(bool includeCode)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("<%@ Register");
      if (!string.IsNullOrEmpty(this.SrcAppRelPath))
        stringBuilder.AppendFormat(" Src=\"{0}\"", (object) this.SrcAppRelPath);
      if (!string.IsNullOrEmpty(this.TagPrefix))
        stringBuilder.AppendFormat(" TagPrefix=\"{0}\"", (object) this.TagPrefix);
      if (!string.IsNullOrEmpty(this.TagName))
        stringBuilder.AppendFormat(" TagName=\"{0}\"", (object) this.TagName);
      if (!string.IsNullOrEmpty(this.Namespace))
        stringBuilder.AppendFormat(" Namespace=\"{0}\"", (object) this.Namespace);
      if (!string.IsNullOrEmpty(this.Assembly))
        stringBuilder.AppendFormat(" Assembly=\"{0}\"", (object) this.Assembly);
      else if (includeCode && this.IsCustomControl)
      {
        stringBuilder.Append(" Assembly=\"");
        stringBuilder.Append("__code");
        stringBuilder.Append("\"");
      }
      stringBuilder.Append(" %>");
      return stringBuilder.ToString();
    }

    public static bool operator ==(RegisterDirective rd1, RegisterDirective rd2) => RegisterDirective.Equals((object) rd1, (object) rd2);

    public static bool operator !=(RegisterDirective rd1, RegisterDirective rd2) => !RegisterDirective.Equals((object) rd1, (object) rd2);

    public string Assembly { get; protected set; }

    public bool IsCustomControl => !string.IsNullOrEmpty(this.TagPrefix) && string.IsNullOrEmpty(this.TagName) && !string.IsNullOrEmpty(this.Namespace);

    public bool IsImplicit { get; protected set; }

    public bool IsModified { get; protected set; }

    public bool IsPersisted { get; protected set; }

    public bool IsUserControl => !string.IsNullOrEmpty(this.TagPrefix) && !string.IsNullOrEmpty(this.TagName) && string.IsNullOrEmpty(this.Namespace) && string.IsNullOrEmpty(this.Assembly);

    public bool IsValid
    {
      get
      {
        bool isCustomControl = this.IsCustomControl;
        bool isUserControl = this.IsUserControl;
        return !(!isUserControl | isCustomControl) || !(!isCustomControl | isUserControl);
      }
    }

    public string Namespace { get; protected set; }

    public string SrcAppRelPath { get; protected set; }

    public string TagName { get; protected set; }

    public string TagPrefix { get; protected set; }

    public string TagPrefixKey
    {
      get
      {
        if (this._tagPrefixKey == null)
        {
          string tagPrefix = this.TagPrefix;
          string tagName = this.TagName;
          if (!string.IsNullOrEmpty(tagPrefix))
          {
            this._tagPrefixKey = tagPrefix;
            if (!string.IsNullOrEmpty(tagName))
              this._tagPrefixKey = this._tagPrefixKey + ":" + tagName;
            this._tagPrefixKey = this._tagPrefixKey.ToLower(CultureInfo.InvariantCulture);
          }
        }
        return this._tagPrefixKey;
      }
    }
  }
}
