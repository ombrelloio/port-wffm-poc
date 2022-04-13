// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Mvc.Constants
// Assembly: Sitecore.Forms.Mvc, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1AD2DE3-BABE-4597-B643-AA577F98B8D6
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Mvc.dll

namespace Sitecore.Forms.Mvc
{
  public class Constants
  {
    public static readonly string Id = nameof (Id);
    public static readonly string FormId = nameof (FormId);
    public static readonly string PageId = nameof (PageId);
    public static readonly string FormItemId = nameof (FormItemId);
    public static readonly string FieldItemId = nameof (FieldItemId);
    public static readonly string FieldTypeItemId = nameof (FieldTypeItemId);
    public static readonly string ReadQueryString = nameof (ReadQueryString);
    public static readonly string FormRepository = "wffmRepository";
    public static readonly string FormRenderingContext = "wffmRenderingContext";
    public static readonly string FormAutoMapper = "wffmAutoMapper";
    public static readonly string FormProcessor = "wffmProcessor";
    public static readonly string Container = nameof (Container);
    public static readonly string ScriptsBaseUrl = "~/sitecore%20modules/Web/Web%20Forms%20for%20Marketers/mvc/";
    public static readonly string Tracking = "data-tracking";
    public static readonly string Wffm = "data-wffm";

    public static class Routes
    {
      public static string Form = nameof (Form);
      public static string ClientEvent = nameof (ClientEvent);
    }

    public static class Bootstrap
    {
      public static readonly string ControlLabel = "control-label";
      public static readonly string FormGroup = "form-group";
      public static readonly string FormControl = "form-control";
      public static readonly string HelpBlock = "help-block";
    }
  }
}
