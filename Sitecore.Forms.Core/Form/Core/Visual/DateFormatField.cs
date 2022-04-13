// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.Visual.DateFormatField
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
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sitecore.Form.Core.Visual
{
  public class DateFormatField : ValidationField
  {
    private readonly IItemRepository itemRepository;

    public DateFormatField()
      : this(DependenciesManager.Resolve<IItemRepository>())
    {
    }

    public DateFormatField(IItemRepository itemRepository)
      : base(HtmlTextWriterTag.Select.ToString())
    {
      Assert.IsNotNull((object) itemRepository, nameof (itemRepository));
      this.itemRepository = itemRepository;
    }

    public override bool IsCacheable => false;

    protected override void OnPreRender(object sender, EventArgs ev)
    {
      this.Controls.Clear();
      base.OnPreRender(sender, ev);
      Sitecore.Form.Core.Utility.Utils.SetUserCulture();
      foreach (Item child in this.itemRepository.GetItem(IDs.DateFormatsRoot).Children)
        this.Controls.Add((Control) new Literal()
        {
          Text = string.Format("<option {0} value='{1}' title='{1}'>{2}</option>", this.DefaultValue == child.Name ? (object) "selected='selected'" : (object) string.Empty, (object) child.Name, (object) this.GetSample(child.Name))
        });
    }

    protected string GetPart(string marker) => string.Format("{0:" + marker + "}", (object) DateTime.UtcNow);

    protected string GetSample(string format)
    {
      StringBuilder stringBuilder = new StringBuilder();
      if (format != null)
      {
        string str = format;
        char[] chArray = new char[1]{ '-' };
        foreach (string marker in str.Split(chArray))
          stringBuilder.AppendFormat("{0} ", (object) this.GetPart(marker));
      }
      return stringBuilder.ToString(0, stringBuilder.Length > 0 ? stringBuilder.Length - 1 : 0);
    }
  }
}
