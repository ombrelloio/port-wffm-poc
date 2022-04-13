// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Shell.UI.Controls.AttributeCollectionExtensions
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Diagnostics;
using System.Collections;
using System.Web.UI;

namespace Sitecore.Forms.Shell.UI.Controls
{
  public static class AttributeCollectionExtensions
  {
    public static void CopyFrom(
      this AttributeCollection attributeCollection,
      AttributeCollection inputCollection)
    {
      Assert.ArgumentNotNull((object) attributeCollection, nameof (attributeCollection));
      if (inputCollection == null || inputCollection.Count <= 0)
        return;
      foreach (string key in (IEnumerable) inputCollection.Keys)
        attributeCollection[key] = inputCollection[key];
    }
  }
}
