// Decompiled with JetBrains decompiler
// Type: Sitecore.WFFM.Abstractions.Shared.IRequirementsChecker
// Assembly: Sitecore.WFFM.Abstractions, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 893E7AF0-E7C2-4A02-B47B-5687A98493FC
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.WFFM.Abstractions.dll

using Sitecore.WFFM.Abstractions.Dependencies;
using System;
using System.Reflection;

namespace Sitecore.WFFM.Abstractions.Shared
{
  [DependencyPath("wffm/requirementsChecker")]
  public interface IRequirementsChecker
  {
    bool CheckRequirements(Attribute[] attributes);

    bool CheckRequirements(Attribute[] attributes, string message, LogMessageType logMessageType);

    bool CheckRequirements(Type objType);

    bool CheckRequirements(MemberInfo memberInfo);

    bool CheckRequirements(Type objType, string message, LogMessageType logMessageType);

    bool CheckRequirements(MemberInfo memberInfo, string message, LogMessageType logMessageType);
  }
}
