// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Shell.UI.Controls.PlaceholderList
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using HtmlAgilityPack;
using Sitecore.Collections;
using Sitecore.Configuration;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Form.Core.Configuration;
using Sitecore.Form.Core.Data;
using Sitecore.Globalization;
using Sitecore.Layouts;
using Sitecore.Publishing;
using Sitecore.Resources;
using Sitecore.Sites;
using Sitecore.Text;
using Sitecore.Web;
using Sitecore.Web.UI.HtmlControls;
using Sitecore.Web.UI.WebControls;
using Sitecore.Web.UI.XmlControls;
using Sitecore.WFFM.Abstractions.Dependencies;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sitecore.Forms.Shell.UI.Controls
{
  public class PlaceholderList : Sitecore.Web.UI.XmlControls.XmlControl
  {
    protected GenericControl ActiveUrl;
    protected DataContext DeviceDataContext;
    protected TreePicker DevicePicker;
    protected Border DeviceScope;
    protected Sitecore.Web.UI.HtmlControls.Literal DevicesLiteral;
    protected Border PlaceholderHidden;
    protected Sitecore.Web.UI.HtmlControls.Literal PlaceholdersLiteral;
    protected Border __LIST;

    protected virtual void OnChangedDevice(object sender, EventArgs e)
    {
      Item obj = this.Item;
      (this.__LIST).Controls.Clear();
      (this.__LIST).Controls.Add(new Sitecore.Web.UI.HtmlControls.Literal("<span class='scFbProgressScope'><img class='scFbProgressImg' src='/sitecore/shell/themes/standard/images/loading15x15.gif' align='absmiddle'>" + DependenciesManager.ResourceManager.Localize("LOADING") + "</span>"));
      string deviceId = this.DeviceID;
      string allowedRendering = this.AllowedRendering;
      string selectedPlaceholder = this.SelectedPlaceholder;
      string placeholdersUrl = PlaceholderManager.GetPlaceholdersUrl(obj, deviceId, allowedRendering, selectedPlaceholder);
            if (this.ActiveUrl != null)
            {
                ((WebControl)this.ActiveUrl).Attributes["value"] = placeholdersUrl;
                Sitecore.Context.ClientPage.ClientResponse.SetOuterHtml((this.ActiveUrl).ID, this.ActiveUrl);
            }
            else
            {
                Sitecore.Context.ClientPage.ClientResponse.SetAttribute("ActiveUrl", "value", placeholdersUrl);
            }
      Sitecore.Context.ClientPage.ClientResponse.SetOuterHtml("__LIST", this.__LIST);
      Sitecore.Context.ClientPage.ClientResponse.Eval("Sitecore.Wfm.PlaceholderManager.load(this, event)");
    }

    protected override void OnLoad(EventArgs e)
    {
      // ISSUE: explicit non-virtual call
      base.OnLoad(e);
      if (!Sitecore.Context.ClientPage.IsEvent)
      {
        (this.DevicePicker).ID = Sitecore.Web.UI.HtmlControls.Control.GetUniqueID(ID);
        (this.DeviceDataContext).ID = Sitecore.Web.UI.HtmlControls.Control.GetUniqueID(ID);
        (this.DevicePicker).DataContext = (this.DeviceDataContext).ID;
        (this.__LIST).ID = "__LIST";
        this.ActiveUrl = new GenericControl("input");
        (this.ActiveUrl).ID = "ActiveUrl";
        (this.ActiveUrl).Attributes["type"] = "hidden";
        (this.PlaceholderHidden).Controls.Add(this.ActiveUrl);
        (this.PlaceholderHidden).Controls.Add(new Sitecore.Web.UI.HtmlControls.Literal("<input ID=\"__LISTVALUE\" Type=\"hidden\" />"));
        (this.PlaceholderHidden).Controls.Add(new Sitecore.Web.UI.HtmlControls.Literal("<input ID=\"__LISTACTIVE\" Type=\"hidden\" />"));
        (this.PlaceholderHidden).Controls.Add(new Sitecore.Web.UI.HtmlControls.Literal("<input ID=\"__NO_PLACEHOLDERS_MESSAGE\" Type=\"hidden\" value=\"" + DependenciesManager.ResourceManager.Localize("THERE_ARE_NO_PLACEHOLDERS_TO_INSERT") + "\" />"));
        this.OnChangedDevice((object) this, (EventArgs) null);
        this.PlaceholdersLiteral.Text = DependenciesManager.ResourceManager.Localize("PLACEHOLDERS");
        this.DevicesLiteral.Text = DependenciesManager.ResourceManager.Localize("DEVICES");
      }
      this.DevicePicker.OnChanged += new EventHandler(this.OnChangedDevice);
    }

    private static void RenderPlaceholder(
      HtmlTextWriter output,
      KeyValuePair<string, string> placeholder,
      string selectedPlaceholderKey)
    {
      Assert.ArgumentNotNull((object) output, nameof (output));
      Assert.ArgumentNotNull((object) placeholder, nameof (placeholder));
      Assert.ArgumentNotNull((object) selectedPlaceholderKey, nameof (selectedPlaceholderKey));
      string str1 = placeholder.Value;
      string str2 = Regex.Replace(placeholder.Value, "\\W", "_");
      string str3 = str1;
      string str4 = string.Empty;
      int num = str1.LastIndexOf('/');
      if (num >= 0)
      {
        str4 = str1;
        str3 = StringUtil.Mid(str1, num + 1);
      }
      string str5 = " style=\"margin-left:" + (object) (0 * 16) + "px\"";
      string empty = string.Empty;
      Item placeholderItem = Client.Page.GetPlaceholderItem(placeholder.Key, Sitecore.Context.Database);
      if (placeholderItem != null)
        empty = ((BaseItem) placeholderItem)["Description"];
      string str6 = str1 == selectedPlaceholderKey ? "scPalettePlaceholderSelected" : "scPalettePlaceholder";
      output.Write("<a id=\"ph_");
      output.Write(str2);
      output.Write("\" href=\"#\" class=\"");
      output.Write(str6);
      output.Write("\"");
      output.Write(str5);
      output.Write(" onclick=\"Sitecore.Wfm.PlaceholderManager.onPlaceholderClick(this,event,'");
      output.Write(str1);
      output.Write("')\" onmouseover=\"javascript:return Sitecore.Wfm.PlaceholderManager.highlightPlaceholder(this,event,'");
      output.Write(str2);
      output.Write("')\" onmouseout=\"javascript:return Sitecore.Wfm.PlaceholderManager.highlightPlaceholder(this,event,'");
      output.Write(str2);
      output.Write("')\"");
      if (string.IsNullOrEmpty(empty))
      {
        output.Write(" title=\"");
        output.Write(str4);
        output.Write("\"");
      }
      output.Write(">");
      output.Write("<div class=\"scPalettePlaceholderIcon\"><span class=\"scPalettePlaceholderIconBorder\">");
      string str7 = ((object) new ImageBuilder()
      {
        Src = (placeholderItem != null ? ((Appearance) placeholderItem.Appearance).Icon : "Imaging/24x24/layer_blend.png"),
        Width = 24,
        Height = 24,
        Align = "absmiddle"
      }).ToString().Replace("'", "&quot;");
      output.Write(str7);
      output.Write("</span></div>");
      output.Write("<div class=\"scPalettePlaceholderTitle\">");
      output.Write(str3);
      output.Write("</div>");
      output.Write("<div class=\"scPalettePlaceholderTooltip\" style=\"display:none\">");
      output.Write(empty);
      output.Write("</div>");
      output.Write("</a>");
    }

    internal static void RenderPlaceholderPalette(
      HtmlTextWriter output,
      string allowedPlaceholder,
      string selectedPlaceholderKey)
    {
      Assert.ArgumentNotNull((object) output, nameof (output));
      Assert.ArgumentNotNull((object) selectedPlaceholderKey, nameof (selectedPlaceholderKey));
      foreach (KeyValuePair<string, string> availablePlaceholder in PlaceholderList.GetAvailablePlaceholders(allowedPlaceholder))
        PlaceholderList.RenderPlaceholder(output, availablePlaceholder, selectedPlaceholderKey);
    }

    protected static List<string> GetPagePlaceholders()
    {
      try
      {
        PreviewManager.RestoreUser();
        UrlString webSiteUrl = SiteContext.GetWebSiteUrl(StaticSettings.ContextDatabase);
        webSiteUrl.Add("sc_mode", "edit");
        webSiteUrl.Add("sc_itemid", ((object) Sitecore.Context.Item.ID).ToString());
        HttpWebRequest httpWebRequest = (HttpWebRequest) WebRequest.Create(WebUtil.GetFullUrl(((object) webSiteUrl).ToString()));
        httpWebRequest.Method = "Get";
        HttpCookie cookie = HttpContext.Current.Request.Cookies["sitecore_userticket"];
        if (cookie != null)
        {
          httpWebRequest.CookieContainer = new CookieContainer();
          httpWebRequest.CookieContainer.Add(new Cookie()
          {
            Domain = httpWebRequest.RequestUri.Host,
            Name = cookie.Name,
            Path = cookie.Path,
            Secure = cookie.Secure,
            Value = cookie.Value
          });
        }
        StreamReader streamReader = new StreamReader(httpWebRequest.GetResponse().GetResponseStream());
        HtmlDocument htmlDocument = new HtmlDocument();
        using (new ThreadCultureSwitcher(Language.Parse("en").CultureInfo))
          htmlDocument.LoadHtml(streamReader.ReadToEnd());
        return htmlDocument.DocumentNode.SelectNodes("//*").Where<HtmlNode>((Func<HtmlNode, bool>) (x => x.Name == "code" && x.Attributes.Contains("chromeType") && x.Attributes["chromeType"].Value == "placeholder" && x.Attributes["key"] != null)).Select<HtmlNode, string>((Func<HtmlNode, string>) (x => x.Attributes["key"].Value)).ToList<string>();
      }
      catch (Exception ex)
      {
      }
      return new List<string>();
    }

    internal static Dictionary<string, string> GetAvailablePlaceholders(
      string allowedPlaceholder)
    {
      Dictionary<string, string> source = new Dictionary<string, string>();
      Database database = Factory.GetDatabase(WebUtil.GetQueryString("sc_database", "master"), false);
      if (database != null)
      {
        List<string> list = (((ReadOnlyList<Placeholder>) Sitecore.Context.Page.Placeholders).Count == 0 ? (IEnumerable<string>) ((IEnumerable<RenderingReference>) Sitecore.Context.Page.Renderings).Select<RenderingReference, string>((Func<RenderingReference, string>) (x => x.Placeholder)).ToList<string>() : (IEnumerable<string>) ((IEnumerable<Placeholder>) Sitecore.Context.Page.Placeholders).Select<Placeholder, string>((Func<Placeholder, string>) (x => x.Key)).ToList<string>()).Concat<string>((IEnumerable<string>) PlaceholderList.GetPagePlaceholders()).ToList<string>();
        Regex regex = new Regex("[^/]+$");
        foreach (string input in list)
        {
          string key1 = input.Contains("/") ? regex.Match(input).Value : input;
          if (!source.ContainsKey(key1))
          {
            Item placeholderItem = Client.Page.GetPlaceholderItem(key1, database);
            if (placeholderItem != null && PlaceholderList.CanDesign(placeholderItem) && ((BaseItem) placeholderItem)["Allowed Controls"].Contains(allowedPlaceholder))
            {
              string key2 = key1;
              int num = key2.LastIndexOf('/');
              if (num >= 0)
                key2 = StringUtil.Mid(key2, num + 1);
              source.Add(key2, input);
            }
          }
        }
        source = source.OrderBy<KeyValuePair<string, string>, string>((Func<KeyValuePair<string, string>, string>) (x => x.Key)).ToDictionary<KeyValuePair<string, string>, string, string>((Func<KeyValuePair<string, string>, string>) (x => x.Key), (Func<KeyValuePair<string, string>, string>) (x => x.Value));
      }
      return source;
    }

    private static bool CanDesign(Item placeholderItem)
    {
      if (placeholderItem == null)
        return true;
      if (!placeholderItem.Access.CanRead())
        return false;
      if (Sitecore.Context.PageMode.IsExperienceEditorEditing)
        return true;
      string str = ((BaseItem) placeholderItem)["Allowed Controls"];
      return string.IsNullOrEmpty(str) || ((IEnumerable<string>) new ListString(str)).Select<string, Item>(new Func<string, Item>(placeholderItem.Database.GetItem)).Any<Item>((Func<Item, bool>) (item => item != null));
    }

    public string AllowedRendering
    {
      get => (string) (this).ViewState["allowedrendering"] ?? string.Empty;
      set
      {
        Assert.ArgumentNotNullOrEmpty(value, nameof (value));
        (this).ViewState["allowedrendering"] = (object) value;
      }
    }

    public string DeviceID
    {
      get => (this.DevicePicker).Value;
      set
      {
        string str = value;
        if (string.IsNullOrEmpty(this.DeviceID))
          str = "{FE5D7FDF-89C0-4D99-9AA3-B5FBD009C9F3}";
        Item obj = StaticSettings.MasterDatabase.GetItem(str);
        (this.DevicePicker).Value = obj == null || !(((object) obj.TemplateID).ToString() == "{B6F7EEB4-E8D7-476F-8936-5ACE6A76F20B}") ? "{FE5D7FDF-89C0-4D99-9AA3-B5FBD009C9F3}" : ((object) obj.ID).ToString();
      }
    }

    public Item Item => string.IsNullOrEmpty(this.ItemUri) ? (Item) null : Sitecore.Data.Database.GetItem(Sitecore.Data.ItemUri.Parse(this.ItemUri));

    public string ItemUri
    {
      get => (string) (this).ViewState["item"] ?? string.Empty;
      set
      {
        Assert.ArgumentNotNullOrEmpty(value, nameof (value));
        (this).ViewState["item"] = (object) value;
      }
    }

    public string SelectedPlaceholder
    {
      get
      {
        string str = Sitecore.Context.ClientPage.ClientRequest.Form["__LISTACTIVE"];
        return !string.IsNullOrEmpty(str) && !str.StartsWith("emptyValue") ? Sitecore.Context.ClientPage.ClientRequest.Form["__LISTVALUE"] : (string) null;
      }
    }

    public bool ShowDeviceTree
    {
      get => (this.DeviceScope).Visible;
      set => (this.DeviceScope).Visible = value;
    }
  }
}
