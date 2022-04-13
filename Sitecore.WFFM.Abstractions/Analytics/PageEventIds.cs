// Decompiled with JetBrains decompiler
// Type: Sitecore.WFFM.Abstractions.Analytics.PageEventIds
// Assembly: Sitecore.WFFM.Abstractions, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 893E7AF0-E7C2-4A02-B47B-5687A98493FC
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.WFFM.Abstractions.dll

using System;

namespace Sitecore.WFFM.Abstractions.Analytics
{
  public class PageEventIds
  {
    public static Guid SubmitSuccessEvent = new Guid("{2FF08489-2A47-4F04-8D8E-0C25A294B2C4}");
    public static Guid FormBegin = new Guid("{6412D1D8-CA83-49B0-9096-6417D5909890}");
    public static Guid FormSubmit = new Guid("{A2C2CF51-9360-4084-9BA0-1A15F1A41096}");
    public static Guid FormDropout = new Guid("{57F43622-ED49-42FF-BDBB-2106F0304120}");
    public static Guid FormSaveActionFailure = new Guid("{DE302E25-E5AF-465E-8BB1-5C3E8145615D}");
    public static Guid FormCheckActionError = new Guid("{7144C4C9-E56A-4529-B3C1-A1DEDC454AB8}");
    public static Guid InvalidFieldSyntax = new Guid("{844BBD40-91F6-42CE-8823-5EA4D089ECA2}");
    public static Guid FieldCompleted = new Guid("{F0113A93-570A-4F69-8C7C-BA08037D1E34}");
    public static readonly Guid FieldOutOfBoundary = new Guid("{F3D7B20C-675C-4707-84CC-5E5B4481B0EE}");
    public static readonly Guid FieldNotCompleted = new Guid("{7E86B2F5-ACEC-4C60-8922-4EB5AE5D9874}");
  }
}
