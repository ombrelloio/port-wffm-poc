// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.Visual.ListChoiceField
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Form.Core.Configuration;
using Sitecore.Form.Core.Utility;
using Sitecore.WFFM.Abstractions.Dependencies;
using Sitecore.WFFM.Abstractions.Shared;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sitecore.Form.Core.Visual
{
  public class ListChoiceField : ValidationField
  {
    private readonly IItemRepository itemRepository;

    public ListChoiceField()
      : this((string) null)
    {
    }

    public ListChoiceField(string choicesRoot)
      : this(DependenciesManager.Resolve<IItemRepository>(), choicesRoot)
    {
    }

    public ListChoiceField(IItemRepository itemRepository, string choicesRoot)
      : base(HtmlTextWriterTag.Select.ToString())
    {
      Assert.IsNotNull((object) itemRepository, nameof (itemRepository));
      this.itemRepository = itemRepository;
      this.ChoicesRoot = string.IsNullOrEmpty(choicesRoot) ? "{7EDF6BCC-1620-4B5F-908A-0752727D68E9}" : choicesRoot;
    }

    protected override void OnPreRender(object sender, EventArgs ev)
    {
      base.OnPreRender(sender, ev);
      Sitecore.Form.Core.Utility.Utils.SetUserCulture();
      foreach (Item child in this.itemRepository.GetItem(this.ChoicesRoot).Children)
      {
        string str = this.GetValue(child);
        this.Controls.Add((Control) new Literal()
        {
          Text = string.Format("<option {0} value='{1}' title='{2}'>{2}</option>", this.DefaultValue == str ? (object) "selected='selected'" : (object) string.Empty, (object) str, (object) child.DisplayName)
        });
      }
    }

    protected virtual string GetValue(Item child)
    {
      Assert.ArgumentNotNull((object) child, "Parameter child is null");
      return ((BaseItem) child).Fields[Sitecore.Form.Core.Configuration.FieldIDs.MetaDataListItemValue] != null && !string.IsNullOrEmpty(((BaseItem) child).Fields[Sitecore.Form.Core.Configuration.FieldIDs.MetaDataListItemValue].Value) ? ((BaseItem) child).Fields[Sitecore.Form.Core.Configuration.FieldIDs.MetaDataListItemValue].Value : child.Name;
    }

    public string ChoicesRoot { get; private set; }
  }
}
