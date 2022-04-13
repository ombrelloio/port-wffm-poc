// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Mvc.Validators.MultiRegularExpressionAttribute
// Assembly: Sitecore.Forms.Mvc, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1AD2DE3-BABE-4597-B643-AA577F98B8D6
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Mvc.dll

using Sitecore.WFFM.Abstractions.Analytics;
using System;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace Sitecore.Forms.Mvc.Validators
{
  [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
  [DisplayName("TITLE_ERROR_MESSAGE_MULTI_REGULAR_EXPRESSION")]
  public class MultiRegularExpressionAttribute : DynamicRegularExpressionAttribute
  {
    public MultiRegularExpressionAttribute(string pattern, string property)
      : base(pattern, property)
    {
      this.EventId = PageEventIds.FieldOutOfBoundary.ToString();
    }

    public override string ClientRuleName => "multiregex";

    protected override bool IsMatch(string stringValue, string pattern) => Regex.IsMatch(stringValue, pattern);
  }
}
