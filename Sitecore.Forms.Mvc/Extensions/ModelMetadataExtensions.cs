// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Mvc.Extensions.ModelMetadataExtensions
// Assembly: Sitecore.Forms.Mvc, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1AD2DE3-BABE-4597-B643-AA577F98B8D6
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Mvc.dll

using Sitecore.Diagnostics;
using Sitecore.Forms.Mvc.ViewModels;
using System.Web.Mvc;

namespace Sitecore.Forms.Mvc.Extensions
{
  public static class ModelMetadataExtensions
  {
    public static T GetContainerPropertyValue<T>(this ModelMetadata metadata, string propertyName)
    {
      Assert.ArgumentNotNull((object) metadata, nameof (metadata));
      T obj = default (T);
      return string.IsNullOrEmpty(propertyName) || !metadata.AdditionalValues.ContainsKey(Constants.Container) || !(metadata.AdditionalValues[Constants.Container] is FieldViewModel additionalValue) ? obj : additionalValue.GetPropertyValue<T>(propertyName);
    }
  }
}
