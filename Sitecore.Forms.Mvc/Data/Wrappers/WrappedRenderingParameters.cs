// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Mvc.Data.Wrappers.WrappedRenderingParameters
// Assembly: Sitecore.Forms.Mvc, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1AD2DE3-BABE-4597-B643-AA577F98B8D6
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Mvc.dll

using Sitecore.Mvc.Presentation;

namespace Sitecore.Forms.Mvc.Data.Wrappers
{
  public class WrappedRenderingParameters : IRenderingParameters
  {
    private readonly RenderingParameters renderingParameters;

    public WrappedRenderingParameters(RenderingParameters parameters) => this.renderingParameters = parameters;

    public string this[string parameterName] => this.renderingParameters[parameterName];
  }
}
