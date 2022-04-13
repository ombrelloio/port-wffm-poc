// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.Configuration.ApplicationItem
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Data.Items;

namespace Sitecore.Form.Core.Configuration
{
  public class ApplicationItem : CustomItem
  {
    public ApplicationItem(Item innerItem)
      : base(innerItem)
    {
    }

    public string Height => ((BaseItem) ((CustomItemBase) this).InnerItem)["height"];

    public string Width => ((BaseItem) ((CustomItemBase) this).InnerItem)["width"];

    public static ApplicationItem GetApplication(string path)
    {
      if (StaticSettings.CoreDatabase != null)
      {
        Item innerItem = StaticSettings.CoreDatabase.SelectSingleItem(Path.ApplicationRoot + path);
        if (innerItem != null)
          return new ApplicationItem(innerItem);
      }
      return (ApplicationItem) null;
    }
  }
}
