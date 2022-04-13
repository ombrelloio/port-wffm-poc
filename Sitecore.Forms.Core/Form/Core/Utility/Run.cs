// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.Utility.Run
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Analytics.Data.Items;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Shell.Framework;
using Sitecore.Shell.Framework.Commands;
using Sitecore.Text;
using Sitecore.Web.UI.Sheer;

namespace Sitecore.Form.Core.Utility
{
  public class Run
  {
    public static void ContentEditor(ItemUri uri) => ((Command) new Edit()).Execute(new CommandContext(Database.GetItem(uri)));

    public static void MarketingCenter(Item goal)
    {
      UrlString urlString = new UrlString();
      urlString.Append("ro", ((object)Sitecore.Analytics.Data.Items.ItemIDs.DefinitionsRoot).ToString());
      urlString.Append("id", ((object) goal.ID).ToString());
      urlString.Append("fo", ((object) goal.ID).ToString());
      urlString.Append("la", goal.Language.Name);
      urlString.Append("vs", goal.Version.Number.ToString());
      urlString.Add("he", "Marketing Center");
      urlString.Add("pa", "0");
      urlString.Add("ic", "People/16x16/megaphone.png");
      Windows.RunApplication("Content Editor", ((object) urlString).ToString());
    }

    public static void SetLanguage(BaseForm form, ItemUri uri)
    {
      Message message = Message.Parse((object) form, "item:selectlanguage");
      Item obj = Database.GetItem(uri);
      if (obj == null)
        return;
      Dispatcher.Dispatch(message, obj);
    }

    public static void FormDesigner(BaseForm form, ItemUri uri) => Run.Dispatch(Message.Parse((object) form, "forms:designer"), uri);

    public static void ExportToAscx(BaseForm form, ItemUri uri) => Run.Dispatch(Message.Parse((object) form, "forms:exporttoascx"), uri);

    private static void Dispatch(Message message, ItemUri uri)
    {
      Item obj = Database.GetItem(uri);
      if (obj == null)
        return;
      Dispatcher.Dispatch(message, obj);
    }

    public static void FormDesigner(ClientRequest args, ItemUri form)
    {
      Message message = Message.Parse(args);
      ReflectionUtils.SetField(typeof (Message), (object) message, "m_name", (object) "forms:designer");
      ReflectionUtils.SetField(typeof (Message), (object) message, "m_raw", (object) "forms:designer");
      Run.Dispatch(message, form);
    }
  }
}
