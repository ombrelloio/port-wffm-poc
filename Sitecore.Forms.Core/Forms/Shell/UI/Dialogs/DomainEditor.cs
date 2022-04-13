// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Shell.UI.Dialogs.DomainEditor
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Security.Domains;
using Sitecore.SecurityModel;
using System.Web.UI.WebControls;

namespace Sitecore.Forms.Shell.UI.Dialogs
{
  public abstract class DomainEditor : EditorBase
  {
    protected virtual void FillDomain(DropDownList domainList, string defaultValue)
    {
      domainList.Items.Clear();
      bool flag = false;
      foreach (Domain domain in DomainManager.GetDomains())
      {
        ListItem listItem = new ListItem(domain.Name, domain.Name);
        if (string.IsNullOrEmpty(defaultValue) && (domain.Name == "extranet" || domain.IsDefault) && !flag)
        {
          listItem.Selected = true;
          flag = true;
        }
        else if (listItem.Value == defaultValue)
          listItem.Selected = true;
        domainList.Items.Add(listItem);
      }
    }
  }
}
