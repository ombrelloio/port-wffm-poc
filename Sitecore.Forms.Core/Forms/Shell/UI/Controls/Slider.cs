// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Shell.UI.Controls.Slider
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Diagnostics;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sitecore.Forms.Shell.UI.Controls
{
  public class Slider : WebControl
  {
    private string data;
    private string value;

    public Slider() => this.Interval = 1;

    public string Data
    {
      get => this.data;
      set
      {
        Assert.ArgumentNotNull((object) value, nameof (value));
        this.data = value;
      }
    }

    public int MaxValue { get; set; }

    public int MinValue { get; set; }

    public int Interval { get; set; }

    public string Value
    {
      get => this.value ?? string.Empty;
      set
      {
        Assert.ArgumentNotNull((object) value, nameof (value));
        this.value = value;
      }
    }

    protected override void Render(HtmlTextWriter output)
    {
      Assert.ArgumentNotNull((object) output, nameof (output));
      output.Write("<script src='/sitecore/shell/controls/lib/scriptaculous/slider.js' type='text/javascript'></script>");
      output.Write(Sitecore.StringExtensions.StringExtensions.FormatWith("<div id='slider_{0}' style='padding: 0px 4px 0px 4px' class='scSlider'>", new object[1]
      {
        (object) this.ID
      }));
      this.RenderSlider(output);
      output.Write("</div>");
      this.RenderScript(output);
      output.Write(Sitecore.StringExtensions.StringExtensions.FormatWith("<input type='hidden' id='{0}' value='{1}' />", new object[2]
      {
        (object) this.ID,
        (object) this.Value
      }));
    }

    protected override void OnLoad(EventArgs e)
    {
      base.OnLoad(e);
      if (!this.Page.IsPostBack || string.IsNullOrEmpty(this.Page.Request.Form[this.ID]))
        return;
      this.Value = this.Page.Request.Form[this.ID];
    }

    private void RenderScript(HtmlTextWriter output)
    {
      Assert.ArgumentNotNull((object) output, nameof (output));
      string newValue1 = string.Empty;
      if (this.MinValue != 0 || this.MaxValue != 0)
        newValue1 = Sitecore.StringExtensions.StringExtensions.FormatWith("range: $R({0}, {1}),", new object[2]
        {
          (object) this.MinValue,
          (object) this.MaxValue
        });
      string newValue2 = string.Empty;
      if (!string.IsNullOrEmpty(this.Data))
        newValue2 = Sitecore.StringExtensions.StringExtensions.FormatWith("values: [{0}],", new object[1]
        {
          (object) this.Data
        });
      if (string.IsNullOrEmpty(newValue2) && (this.MinValue != 0 || this.MaxValue != 0))
      {
        for (int minValue = this.MinValue; minValue <= this.MaxValue; ++minValue)
        {
          if (minValue > this.MinValue)
            newValue2 += ", ";
          newValue2 += (string) (object) minValue;
        }
        newValue2 = Sitecore.StringExtensions.StringExtensions.FormatWith("values: getSliderValues({0}, {1}, {2}),", new object[3]
        {
          (object) this.MinValue,
          (object) this.MaxValue,
          (object) this.Interval
        });
      }
      string newValue3 = string.Empty;
      if (!string.IsNullOrEmpty(this.Value))
        newValue3 = Sitecore.StringExtensions.StringExtensions.FormatWith("sliderValue: {0},", new object[1]
        {
          (object) this.Value
        });
      string str = "<script type='text/javascript' language='javascript'>" + "Event.observe(window, 'load', function() {\r\n          if (!$('{ID}_handle')) return;\r\n          if (typeof(scSlider{ID}) != 'undefined') return;\r\n\r\n          $('{ID}_track').slider = new Control.Slider('{ID}_handle', '{ID}_track', {\r\n            {Range}\r\n            {Values}\r\n            {Value}\r\n          {Disabled}\r\n\t\t\t\t    onSlide: function(v) { $('{ID}_value').innerHTML = v; },\r\n\t\t\t\t    onChange: function(v) { $('{ID}_value').innerHTML = v; $('{ID}').value = v; }\r\n\t\t\t    });\r\n\r\n          scSlider{ID} = true;\r\n        });\r\n      ".Replace("{ID}", this.ID).Replace("{Value}", newValue3).Replace("{Range}", newValue1).Replace("{Values}", newValue2).Replace("{Disabled}", !this.Enabled ? "disabled : true," : string.Empty) + "</script>";
      output.Write(str);
    }

    private void RenderSlider(HtmlTextWriter output)
    {
      Assert.ArgumentNotNull((object) output, nameof (output));
      string str = Sitecore.StringExtensions.StringExtensions.FormatWith("<div id='{0}_track' [Disabled] style='width:100%; position:relative; background-color:#ccc; height:9px; cursor: pointer; background:transparent url({1}/track-repeat.png) repeat-x top right'>\r\n\t\t\t    <div id='{0}_track-left' style='position:absolute; width: 5px; height: 9px; background: transparent url({1}/track-left.png) no-repeat top left;'></div>\r\n          <div id='{0}_handle' style='width:10px; height:15px; [Cursor] position: relative'>\r\n            <img src='{3}/handle[ImageSuffix].png' alt='' style='float:left'/>\r\n            <div id='{0}_value' style='padding-top: 4px; width: 20px; position: relative; left: 2px; text-align: center; white-space: nowrap'>{2}</div>\r\n          </div>\r\n          <div id='{0}_track-right' style='position:absolute; top: 0px; right: 0px; width: 5px; height: 9px; background: transparent url({1}/track-right.png) no-repeat top right;'></div>\r\n\t\t    </div>\r\n\t\t  ", new object[4]
      {
        (object) this.ID,
        (object) "/sitecore/shell/Themes/Standard/Default/WFM",
        (object) this.Value,
        (object) "/sitecore/shell/Themes/Standard/Reports"
      }).Replace("[Cursor]", !this.Enabled ? string.Empty : "cursor: move;").Replace("[ImageSuffix]", !this.Enabled ? "_d" : string.Empty).Replace("[Disabled]", !this.Enabled ? "disabled = 'true'" : string.Empty);
      output.Write(str);
    }
  }
}
