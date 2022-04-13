// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Web.UI.Controls.GridColumnCollectionExtensions
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using ComponentArt.Web.UI;

namespace Sitecore.Form.Web.UI.Controls
{
  public static class GridColumnCollectionExtensions
  {
    public static void MoveColumnTo(
      this GridColumnCollection collection,
      string columnDataKey,
      int newPosition)
    {
      if (collection[(object) columnDataKey] == null)
        return;
      GridColumn gridColumn = collection[(object) columnDataKey];
      collection.Remove(gridColumn);
      collection.Insert(newPosition, gridColumn);
    }
  }
}
