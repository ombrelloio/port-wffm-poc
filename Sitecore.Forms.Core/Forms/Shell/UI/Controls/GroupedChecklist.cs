// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Shell.UI.Controls.GroupedChecklist
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sitecore.Forms.Shell.UI.Controls
{
  public class GroupedChecklist : CheckBoxList
  {
    protected override void RenderItem(
      ListItemType itemType,
      int repeatIndex,
      RepeatInfo repeatInfo,
      HtmlTextWriter writer)
    {
      ListItem listItem = this.Items[repeatIndex];
      if (listItem.Attributes["optgroup"] != null)
      {
        if (repeatIndex == 0)
        {
          HtmlTextWriter writer1 = new HtmlTextWriter((TextWriter) new StringWriter());
          base.RenderItem(itemType, repeatIndex, repeatInfo, writer1);
        }
        Label label = new Label();
        label.Font.CopyFrom(this.Font);
        label.Font.Bold = true;
        label.Text = listItem.Text;
        label.Width = Unit.Percentage(100.0);
        label.Attributes.CopyFrom(listItem.Attributes);
        label.RenderControl(writer);
      }
      else
        base.RenderItem(itemType, repeatIndex, repeatInfo, writer);
    }
  }
}
