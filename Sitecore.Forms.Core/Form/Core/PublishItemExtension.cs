// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.PublishItemExtension
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Events;
using Sitecore.Form.Core.Configuration;
using Sitecore.Publishing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Sitecore.Form.Core
{
  public class PublishItemExtension
  {
    private const string GuidPattern = "(\\{){0,1}[0-9a-fA-F]{8}\\-[0-9a-fA-F]{4}\\-[0-9a-fA-F]{4}\\-[0-9a-fA-F]{4}\\-[0-9a-fA-F]{12}(\\}){0,1}";

    public void PublishFormChildItems(object sender, EventArgs args)
    {
      string str1 = ((object) IDs.FormInterpreterID).ToString();
      string str2 = ((object) IDs.FormMvcInterpreterID).ToString();
      if (!((args is SitecoreEventArgs sitecoreEventArgs ? ((IEnumerable<object>) sitecoreEventArgs.Parameters).FirstOrDefault<object>() : (object) null) is Publisher publisher) || !publisher.Options.PublishRelatedItems || publisher.Options.Mode != PublishMode.SingleItem)
        return;
      string input = ((BaseItem) publisher.Options.RootItem).Fields["__Final renderings"]?.Value;
      if (input == null || !input.Contains(str1) && !input.Contains(str2))
        return;
      Database sourceDatabase = publisher.Options.SourceDatabase;
      foreach (object match in Regex.Matches(input, "(\\{){0,1}[0-9a-fA-F]{8}\\-[0-9a-fA-F]{4}\\-[0-9a-fA-F]{4}\\-[0-9a-fA-F]{4}\\-[0-9a-fA-F]{12}(\\}){0,1}"))
      {
        Item obj = sourceDatabase.GetItem(match.ToString());
        if (obj != null && (obj.TemplateID==IDs.FormTemplateID))
        {
          Database database = Database.GetDatabase("web");
          this.PublishItem(obj, sourceDatabase, database, (PublishMode) 3);
        }
      }
    }

    private void PublishItem(Item item, Database sourceDB, Database targetDB, PublishMode mode) => new Publisher(new PublishOptions(sourceDB, targetDB, mode, item.Language, DateTime.Now)
    {
      RootItem = item,
      Deep = true
    }).Publish();
  }
}
