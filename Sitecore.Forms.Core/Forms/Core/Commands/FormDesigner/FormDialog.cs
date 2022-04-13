// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Core.Commands.FormDesigner.FormDialog
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Form.Core.Configuration;
using Sitecore.Forms.Core.Data;
using Sitecore.Text;
using Sitecore.Web.UI.Sheer;
using Sitecore.WFFM.Abstractions.Shared;
using System.Collections.Specialized;

namespace Sitecore.Forms.Core.Commands.FormDesigner
{
  public class FormDialog
  {
    private readonly IResourceManager resourceManager;
    private readonly FormItem formItem;

    public FormDialog(FormItem formItem, IResourceManager resourceManager)
    {
      Assert.ArgumentNotNull((object) formItem, nameof (formItem));
      Assert.ArgumentNotNull((object) resourceManager, nameof (resourceManager));
      this.formItem = formItem;
      this.resourceManager = resourceManager;
    }

    public void ShowModalDialog(NameValueCollection urlParameters)
    {
      Assert.ArgumentNotNull((object) urlParameters, nameof (urlParameters));
      UrlString urlString = new UrlString(UIUtil.GetUri("control:Forms.FormDesigner"));
      foreach (string allKey in urlParameters.AllKeys)
      {
        string urlParameter = urlParameters[allKey];
        urlString.Add(allKey, urlParameter);
      }
      ApplicationItem application = ApplicationItem.GetApplication(Path.FormDesignerApplication);
      string str1 = (string) null;
      string str2 = (string) null;
      string str3 = this.formItem.FormName + " - ";
      string str4;
      if (application != null)
      {
        str1 = MainUtil.GetInt(application.Width, 1250).ToString();
        str2 = MainUtil.GetInt(application.Height, 500).ToString();
        str4 = str3 + ((CustomItemBase) application).DisplayName;
      }
      else
        str4 = str3 + this.resourceManager.GetString("FORM_DESIGNER");
      SheerResponse.ShowModalDialog(new ModalDialogOptions(((object) urlString).ToString())
      {
        Width = str1,
        Height = str2,
        Response = true,
        Header = str4
      });
    }
  }
}
