// Decompiled with JetBrains decompiler
// Type: Sitecore.WFFM.Abstractions.Analytics.Constants
// Assembly: Sitecore.WFFM.Abstractions, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 893E7AF0-E7C2-4A02-B47B-5687A98493FC
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.WFFM.Abstractions.dll

namespace Sitecore.WFFM.Abstractions.Analytics
{
  public class Constants
  {
    public static readonly string FormBeginEvent = "Form Begin";
    public static readonly string FormDropoutEvent = "Form Dropout";
    public static readonly string SubmitSuccessEvent = "Submit Success Event";
    public static readonly string FieldCompletedEvent = "Field Completed";
    public static readonly string WffmKey = "scwffm";
    public static readonly string OldWffmKey = "sc.wffm";
    public static readonly string FormsCollectionName = "WebForms";
    public static readonly string SessionFormBeginTrack = "sc-webform-form-begin";
    public static readonly string TrackEvent = "trackevent";
    public static readonly string FieldId = "fieldid";
    public static readonly string FieldValue = "value";
    public static readonly string Main = "main";
    public static readonly string DataKey = "dataKey";
    public static readonly string Dropout = nameof (Dropout);
    public static readonly string Failure = nameof (Failure);
    public static readonly string Success = nameof (Success);
  }
}
