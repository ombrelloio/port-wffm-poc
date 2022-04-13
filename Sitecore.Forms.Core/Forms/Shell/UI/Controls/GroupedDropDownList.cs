// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Shell.UI.Controls.GroupedDropDownList
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sitecore.Forms.Shell.UI.Controls
{
  public class GroupedDropDownList : DropDownList
  {
    protected override void RenderContents(HtmlTextWriter writer)
    {
      if (this.Items.Count <= 0)
        return;
      bool flag1 = false;
      bool flag2 = false;
      for (int index = 0; index < this.Items.Count; ++index)
      {
        ListItem listItem = this.Items[index];
        if (listItem.Enabled)
        {
          if (listItem.Attributes["optgroup"] != null)
          {
            if (flag2)
              writer.WriteEndTag("optgroup");
            writer.WriteBeginTag("optgroup");
            writer.WriteAttribute("label", listItem.Text);
            writer.Write('>');
            writer.WriteLine();
            flag2 = true;
          }
          else
          {
            writer.WriteBeginTag("option");
            if (listItem.Selected)
            {
              if (flag1)
                this.VerifyMultiSelect();
              flag1 = true;
              writer.WriteAttribute("selected", "selected");
            }
            writer.WriteAttribute("value", listItem.Value, true);
            if (listItem.Attributes.Count > 0)
              listItem.Attributes.Render(writer);
            if (this.Page != null)
              this.Page.ClientScript.RegisterForEventValidation(this.UniqueID, listItem.Value);
            writer.Write('>');
            HttpUtility.HtmlEncode(listItem.Text, (TextWriter) writer);
            writer.WriteEndTag("option");
            writer.WriteLine();
          }
        }
      }
      if (!flag2)
        return;
      writer.WriteEndTag("optgroup");
    }
  }
}
