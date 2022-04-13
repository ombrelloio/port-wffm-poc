// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Core.Crm.ConditionOperator
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Sitecore.Forms.Core.Crm
{
  [GeneratedCode("wsdl", "2.0.50727.3038")]
  [XmlType(Namespace = "http://schemas.microsoft.com/crm/2006/Query")]
  [Serializable]
  public enum ConditionOperator
  {
    Equal,
    NotEqual,
    GreaterThan,
    LessThan,
    GreaterEqual,
    LessEqual,
    Like,
    NotLike,
    In,
    NotIn,
    Between,
    NotBetween,
    Null,
    NotNull,
    Yesterday,
    Today,
    Tomorrow,
    Last7Days,
    Next7Days,
    LastWeek,
    ThisWeek,
    NextWeek,
    LastMonth,
    ThisMonth,
    NextMonth,
    On,
    OnOrBefore,
    OnOrAfter,
    LastYear,
    ThisYear,
    NextYear,
    LastXHours,
    NextXHours,
    LastXDays,
    NextXDays,
    LastXWeeks,
    NextXWeeks,
    LastXMonths,
    NextXMonths,
    LastXYears,
    NextXYears,
    EqualUserId,
    NotEqualUserId,
    EqualBusinessId,
    NotEqualBusinessId,
    EqualUserLanguage,
    NotOn,
    OlderThanXMonths,
  }
}
