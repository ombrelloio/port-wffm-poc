// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Core.Dependencies.DefaultImplRequirementsChecker
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Diagnostics;
using Sitecore.WFFM.Abstractions;
using Sitecore.WFFM.Abstractions.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Sitecore.Forms.Core.Dependencies
{
  [Serializable]
  public class DefaultImplRequirementsChecker : IRequirementsChecker
  {
    private readonly ISettings settings;
    private readonly ILogger logger;

    public DefaultImplRequirementsChecker(ISettings settings, ILogger logger)
    {
      Assert.ArgumentNotNull((object) settings, nameof (settings));
      Assert.ArgumentNotNull((object) logger, nameof (logger));
      this.settings = settings;
      this.logger = logger;
    }

    public bool CheckRequirements(Attribute[] attributes)
    {
      Assert.ArgumentNotNull((object) attributes, nameof (attributes));
      foreach (Attribute attribute in attributes)
      {
        if (attribute is RequiredAttribute requiredAttribute1)
        {
          PropertyInfo property = this.settings.GetType().GetProperty(requiredAttribute1.PropertyName);
          if (property == (PropertyInfo) null)
            throw new Exception("There is not required property " + requiredAttribute1.PropertyName);
          if (property.PropertyType != typeof (bool))
            throw new Exception("Required property is not bool type");
          if ((bool) property.GetValue((object) this.settings) != requiredAttribute1.PropertyValue)
            return false;
        }
      }
      return true;
    }

    public bool CheckRequirements(
      Attribute[] attributes,
      string message,
      LogMessageType logMessageType)
    {
      Assert.ArgumentNotNull((object) attributes, nameof (attributes));
      if (this.CheckRequirements(attributes))
        return true;
      string str = string.Join(",", (IEnumerable<string>) attributes.OfType<RequiredAttribute>().Select<RequiredAttribute, string>((Func<RequiredAttribute, string>) (a => a.PropertyName + ":" + a.PropertyValue.ToString())).ToList<string>());
      this.logger.Log(string.Format("{0}, required attributes are: {1}", (object) message, (object) str), (object) this, logMessageType);
      return false;
    }

    public bool CheckRequirements(Type objType)
    {
      Assert.ArgumentNotNull((object) objType, nameof (objType));
      return this.CheckRequirements(this.GetRequiredAttributes(objType));
    }

    public bool CheckRequirements(MemberInfo memberInfo)
    {
      Assert.ArgumentNotNull((object) memberInfo, nameof (memberInfo));
      return this.CheckRequirements(this.GetRequiredAttributes(memberInfo));
    }

    public bool CheckRequirements(Type objType, string message, LogMessageType logMessageType)
    {
      Assert.ArgumentNotNull((object) objType, nameof (objType));
      return this.CheckRequirements(this.GetRequiredAttributes(objType), message, logMessageType);
    }

    public bool CheckRequirements(
      MemberInfo memberInfo,
      string message,
      LogMessageType logMessageType)
    {
      Assert.ArgumentNotNull((object) memberInfo, nameof (memberInfo));
      return this.CheckRequirements(this.GetRequiredAttributes(memberInfo), message, logMessageType);
    }

    private Attribute[] GetRequiredAttributes(Type objType) => Attribute.GetCustomAttributes((MemberInfo) objType, typeof (RequiredAttribute));

    private Attribute[] GetRequiredAttributes(MemberInfo memberInfo) => memberInfo.DeclaringType == (Type) null ? new Attribute[0] : Attribute.GetCustomAttributes(memberInfo.DeclaringType.GetMember(memberInfo.Name.Replace("get_", string.Empty))[0], typeof (RequiredAttribute));
  }
}
