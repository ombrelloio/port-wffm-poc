// Decompiled with JetBrains decompiler
// Type: Sitecore.WFFM.Abstractions.Actions.ExecuteResult
// Assembly: Sitecore.WFFM.Abstractions, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 893E7AF0-E7C2-4A02-B47B-5687A98493FC
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.WFFM.Abstractions.dll

namespace Sitecore.WFFM.Abstractions.Actions
{
  public struct ExecuteResult
  {
    public ExecuteResult.Failure[] Failures { get; set; }

    public struct Failure
    {
      public string ApiErrorMessage { get; set; }

      public string ErrorMessage { get; set; }

      public string FailedAction { get; set; }

      public string StackTrace { get; set; }

      public bool IsCustom { get; set; }

      public bool IsMessageUnchangeable { get; set; }
    }
  }
}
