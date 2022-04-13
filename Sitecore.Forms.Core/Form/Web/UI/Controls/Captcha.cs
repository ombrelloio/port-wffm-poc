// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Web.UI.Controls.Captcha
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Configuration;
using Sitecore.Globalization;
using Sitecore.Resources;
using System;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sitecore.Form.Web.UI.Controls
{
  [PersistChildren(false)]
  public class Captcha : WebControl, INamingContainer
  {
    protected CaptchaControl captchaImage = new CaptchaControl();
    protected ImageButton captchaPlayButton = new ImageButton();
    protected ImageButton captchaRefreshButton = new ImageButton();
    protected Panel captchaImageHolder = new Panel();
    protected Panel playerHolder = new Panel();

    public Captcha()
      : this(HtmlTextWriterTag.Div)
    {
      this.CaptchaImage.CacheStrategy = MSCaptcha.CaptchaControl.cacheType.Session;
      this.CaptchaImage.CaptchaMaxTimeout = (int) Settings.GetTimeSpanSetting("Captcha.MaxTimeout", "00:01:30").TotalSeconds;
      this.CaptchaImage.CaptchaMinTimeout = (int) Settings.GetTimeSpanSetting("Captcha.MinTimeout", "00:00:03").TotalSeconds;
      this.CaptchaRefreshButton.ImageUrl = Images.GetThemedImageSource("Applications/16x16/refresh.png");
      this.CaptchaRefreshButton.AlternateText = Translate.Text("Display another text.");
      this.CaptchaRefreshButton.ToolTip = this.CaptchaRefreshButton.AlternateText;
      this.CaptchaPlayButton.ImageUrl = Images.GetThemedImageSource("People/16x16/loudspeaker.png");
      this.CaptchaPlayButton.AlternateText = Translate.Text("Play audio version of text.");
      this.CaptchaPlayButton.ToolTip = this.CaptchaPlayButton.AlternateText;
    }

    public Captcha(HtmlTextWriterTag tag)
      : base(tag)
    {
    }

    public void ValidateCaptcha(string captcha) => this.CaptchaImage.ValidateCaptcha(captcha);

    protected override void OnInit(EventArgs e)
    {
      base.OnInit(e);
      Table table = new Table()
      {
        CellPadding = 0,
        CellSpacing = 0
      };
      this.Controls.Add((Control) table);
      TableRow row1 = new TableRow();
      this.captchaImageHolder.Controls.Add((Control) this.captchaImage);
      TableCell cell1 = new TableCell();
      cell1.Controls.Add((Control) this.captchaImageHolder);
      row1.Cells.Add(cell1);
      TableCell cell2 = new TableCell();
      cell2.Controls.Add((Control) this.captchaRefreshButton);
      cell2.Controls.Add((Control) new Literal()
      {
        Text = "<br/>"
      });
      cell2.Controls.Add((Control) this.captchaPlayButton);
      row1.Cells.Add(cell2);
      table.Rows.Add(row1);
      if (UIUtil.IsIE())
      {
        this.playerHolder.Style.Add("margin-top", "-20px");
        this.playerHolder.Style.Add("position", "relative");
        this.playerHolder.Style.Add("z-index", "-1");
        TableRow row2 = new TableRow();
        TableCell cell3 = new TableCell();
        cell3.Controls.Add((Control) this.playerHolder);
        row2.Cells.Add(cell3);
        table.Rows.Add(row2);
      }
      else
        this.Controls.Add((Control) this.playerHolder);
    }

    protected override void OnPreRender(EventArgs e)
    {
      base.OnPreRender(e);
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("$scw(document).ready(function() {");
      stringBuilder.AppendFormat("$scw.webform.controls.attachCaptchaHandler('{0}', '{1}', '{2}');", (object) this.captchaPlayButton.ClientID, (object) this.captchaImageHolder.ClientID, (object) this.playerHolder.ClientID);
      stringBuilder.Append(" });");
      this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "sc-webform-makecapthcaspell" + this.ClientID, stringBuilder.ToString(), true);
    }

    [PersistenceMode(PersistenceMode.InnerProperty)]
    public CaptchaControl CaptchaImage => this.captchaImage;

    [PersistenceMode(PersistenceMode.InnerProperty)]
    public ImageButton CaptchaRefreshButton => this.captchaRefreshButton;

    [PersistenceMode(PersistenceMode.InnerProperty)]
    public ImageButton CaptchaPlayButton => this.captchaPlayButton;

    public bool UserValidated => this.CaptchaImage.UserValidated;

    public event ImageClickEventHandler RefreshButtonClick
    {
      add => this.CaptchaRefreshButton.Click += value;
      remove => this.CaptchaRefreshButton.Click -= value;
    }

    public override string ID
    {
      get => base.ID;
      set
      {
        base.ID = value;
        this.captchaPlayButton.ID = this.ClientID + "pb";
        this.captchaImageHolder.ID = this.ClientID + "im";
        this.playerHolder.ID = this.ClientID + "ph";
      }
    }
  }
}
