// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.SchemaGenerator.RegisterDirectiveManager
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Form.Core.Utility;
using System;
using System.Collections.Generic;
using System.Web.UI;

namespace Sitecore.Form.Core.SchemaGenerator
{
  internal class RegisterDirectiveManager : IRegisterDirectiveService
  {
    private List<RegisterDirective> directives = new List<RegisterDirective>();
    private int uniqueDirective = -1;

    private RegisterDirective FindCustomControlDirective(
      string nameSpace,
      string assembly)
    {
      if (nameSpace != null)
      {
        string strA = nameSpace + assembly;
        foreach (RegisterDirective directive in (IEnumerable<RegisterDirective>) this.Directives)
        {
          if ((directive.IsCustomControl || directive.IsUserControl) && string.Compare(strA, directive.Namespace + directive.SrcAppRelPath + directive.Assembly, StringComparison.OrdinalIgnoreCase) == 0)
            return directive;
        }
      }
      return (RegisterDirective) null;
    }

    private string GetNextTagPrefix(string prefix)
    {
      ++this.uniqueDirective;
      return prefix + (object) this.uniqueDirective;
    }

    public RegisterDirective EnsureCustomControlRegisterDirective(object control)
    {
      string str = (string) null;
      if (control is UserControl)
        str = ((TemplateControl) control).AppRelativeVirtualPath;
      if (string.IsNullOrEmpty(str))
        return this.EnsureCustomControlRegisterDirective(control.GetType());
      string tagPrefix = (string) null;
      RegisterDirective controlDirective = this.FindCustomControlDirective(str, string.Empty);
      if (controlDirective != (RegisterDirective) null)
        tagPrefix = controlDirective.TagPrefix;
      if (string.IsNullOrEmpty(tagPrefix))
        tagPrefix = this.GetNextTagPrefix("cc");
      RegisterDirective registerDirective = new RegisterDirective(tagPrefix, control.GetType().Name, str);
      this.Directives.Add(registerDirective);
      return registerDirective;
    }

    public RegisterDirective EnsureCustomControlRegisterDirective(Type controlType)
    {
      string nspace;
      string asmName;
      ReflectionUtils.GetNamespaceAndAssemblyFromType(controlType, out nspace, out asmName);
      string tagPrefix = (string) null;
      RegisterDirective registerDirective = this.FindCustomControlDirective(nspace, asmName);
      if (registerDirective != (RegisterDirective) null)
        tagPrefix = registerDirective.TagPrefix;
      if (string.IsNullOrEmpty(tagPrefix))
      {
        foreach (TagPrefixAttribute customAttribute in controlType.Module.Assembly.GetCustomAttributes(typeof (TagPrefixAttribute), true))
        {
          if (string.Compare(customAttribute.NamespaceName, nspace, StringComparison.OrdinalIgnoreCase) == 0)
          {
            tagPrefix = customAttribute.TagPrefix;
            break;
          }
        }
        if (string.IsNullOrEmpty(tagPrefix))
          tagPrefix = this.GetNextTagPrefix("cc");
        registerDirective = new RegisterDirective(tagPrefix, nspace, asmName, (string) null, false, false);
        this.Directives.Add(registerDirective);
      }
      return registerDirective;
    }

    public IList<RegisterDirective> Directives => (IList<RegisterDirective>) this.directives;
  }
}
