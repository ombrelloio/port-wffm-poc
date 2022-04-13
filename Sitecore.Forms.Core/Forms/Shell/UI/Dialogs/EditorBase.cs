// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Shell.UI.Dialogs.EditorBase
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Data.Items;
using Sitecore.Form.Core.Data;
using Sitecore.Forms.Core.Data;
using Sitecore.Web;

namespace Sitecore.Forms.Shell.UI.Dialogs
{
  public abstract class EditorBase : SmartDialogPage
  {
    public ActionItem Action
    {
      get
      {
        Item obj = this.CurrentDatabase.GetItem(this.ActionID, this.CurrentLanguage);
        return obj != null ? new ActionItem(obj) : (ActionItem) null;
      }
    }

    public virtual string ActionID => WebUtil.GetQueryString("actionid");

    public virtual string UniqID => WebUtil.GetQueryString("uniqid");

    public FormItem CurrentForm
    {
      get
      {
        if (!string.IsNullOrEmpty(this.CurrentID))
        {
          Item innerItem = this.CurrentDatabase.GetItem(this.CurrentID, this.CurrentLanguage);
          if (innerItem != null)
            return new FormItem(innerItem);
        }
        return (FormItem) null;
      }
    }
  }
}
