// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.Data.PlaceholderManager
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Form.Core.Configuration;
using Sitecore.Layouts;
using Sitecore.Text;
using Sitecore.Web;

namespace Sitecore.Form.Core.Data
{
  public class PlaceholderManager
  {
    internal static readonly string allowedRenderingKey = "sc_allowrend";
    internal static readonly string onlyNamesPlaceholdersKey = "onlyNames";
    internal static readonly string placeholderQueryKey = "sc_placeholder";
    internal static readonly string selectedPlaceholderKey = "sc_selectedplaceholder";
    private static readonly string placeholderRequestKey = "&screquestplacholderid=";
    private readonly string deviceID;
    private readonly Item item;

    public PlaceholderManager(
      Item item,
      string deviceID,
      string allowedRendering,
      string selectedPlaceholder)
    {
      this.item = item;
      this.deviceID = deviceID;
      this.AllowedRendering = allowedRendering;
      this.SelectedPlaceholder = selectedPlaceholder;
    }

    public string AllowedRendering { get; private set; }

    public DeviceDefinition Device => this.Layouts.GetDevice(this.deviceID);

    public LayoutDefinition Layouts => LayoutDefinition.Parse(((BaseItem) this.item).Fields["__renderings"].Value);

    public string PlaceholdersUrl
    {
      get
      {
        UrlString urlString = new UrlString("/default.aspx");
        urlString.Append("sc_database", StaticSettings.ContextDatabase.Name);
        urlString.Append("sc_itemid", ((object) this.item.ID).ToString());
        urlString.Append("sc_duration", "temporary");
        urlString.Append("sc_mode", "preview");
        urlString.Append(PlaceholderManager.allowedRenderingKey, this.AllowedRendering ?? string.Empty);
        urlString.Append(PlaceholderManager.selectedPlaceholderKey, this.SelectedPlaceholder ?? string.Empty);
        urlString.Append(PlaceholderManager.placeholderQueryKey, "1");
        urlString.Append("sc_device", this.deviceID);
        return WebUtil.GetServerUrl() + PlaceholderManager.Sign(urlString.ToString() + this.GetDeviceQueryString());
      }
    }

    public string SelectedPlaceholder { get; private set; }

    public static string GetPlaceholdersUrl(
      Item item,
      string deviceID,
      string allowedRendering,
      string selectedPlaceholder)
    {
      return new PlaceholderManager(item, deviceID, allowedRendering, selectedPlaceholder).PlaceholdersUrl;
    }

    public string GetDeviceQueryString()
    {
      Item obj = this.item.Database.GetItem(this.deviceID);
      if (obj == null || string.IsNullOrEmpty(((BaseItem) obj)["query string"]))
        return string.Empty;
      return string.Join(string.Empty, new string[2]
      {
        "&",
        ((BaseItem) obj)["query string"]
      });
    }

    public LayoutItem LayoutByID(string id) => (LayoutItem)(this.item.Database.GetItem(id));

    internal static string GetOnlyPlaceholderNamesUrl(string baseUrl)
    {
      if (string.IsNullOrEmpty(baseUrl))
        return string.Empty;
      UrlString urlString = new UrlString(baseUrl);
      urlString.Append(PlaceholderManager.placeholderQueryKey, "1");
      urlString.Append(PlaceholderManager.onlyNamesPlaceholdersKey, "1");
      urlString.Append("sc_database", StaticSettings.ContextDatabase.Name);
      urlString.Append(PlaceholderManager.allowedRenderingKey, IDs.FormInterpreterID.ToString() + "|" + (object) IDs.FormMvcInterpreterID);
      return WebUtil.GetServerUrl() + PlaceholderManager.Sign(((object) urlString).ToString());
    }

    internal static bool IsValid(string url)
    {
      Assert.ArgumentNotNullOrEmpty(url, nameof (url));
      int length = url.IndexOf(PlaceholderManager.placeholderRequestKey);
      if (length > -1)
      {
        string str1 = url.Substring(0, length);
        string str2 = url.Substring(length + PlaceholderManager.placeholderRequestKey.Length);
        if (((object) MainUtil.GetMD5Hash(str1).ToShortID()).ToString() == str2)
          return true;
      }
      return false;
    }

    internal static string Sign(string url) => string.Join(string.Empty, new string[3]
    {
      url,
      PlaceholderManager.placeholderRequestKey,
      ((object) MainUtil.GetMD5Hash(url).ToShortID()).ToString()
    });
  }
}
