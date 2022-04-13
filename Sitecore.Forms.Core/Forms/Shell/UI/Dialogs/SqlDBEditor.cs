// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Shell.UI.Dialogs.SqlDBEditor
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Web;
using Sitecore.Web.UI.HtmlControls;
using Sitecore.Web.UI.Pages;
using Sitecore.Web.UI.WebControls;
using System;
using System.Data.SqlClient;
using System.Web;

namespace Sitecore.Forms.Shell.UI.Dialogs
{
  public class SqlDBEditor : DialogForm
  {
    public static readonly string connectionOk = "OK";
    public static readonly string paramName = "ConnectionString=";
    protected Button Apply;
    protected Button Close;
    protected Edit Server;
    protected Edit EbDBName;
    protected Edit EbAttachDBName;
    protected Radiobutton NTAuthentication;
    protected Radiobutton SQLAuthentication;
    protected Radiobutton DbFileNameMode;
    protected Radiobutton DbNameMode;
    protected Border SourceModeGroup;
    protected Edit Password;
    protected Edit User;
    protected Literal Status;
    protected GridPanel Credential;

    public bool TestConnection(string connectionString, out string error)
    {
      SqlConnection sqlConnection = new SqlConnection(connectionString);
      try
      {
        sqlConnection.Open();
      }
      catch (Exception ex)
      {
        error = "Connection error: " + ex.Message;
        return false;
      }
      finally
      {
        sqlConnection.Close();
      }
      error = string.Empty;
      return true;
    }

    protected override void OnLoad(EventArgs args)
    {
      base.OnLoad(args);
      if (Context.ClientPage.IsEvent)
        return;
      SqlConnectionStringBuilder connectionStringBuilder = new SqlConnectionStringBuilder();
      connectionStringBuilder.ConnectionString = this.Params;
      ((Control) this.Server).Value = connectionStringBuilder.DataSource;
      if (string.IsNullOrEmpty(connectionStringBuilder.AttachDBFilename))
      {
        ((Control) this.EbDBName).Value = connectionStringBuilder.InitialCatalog;
        this.DbNameMode.Checked = true;
      }
      else
      {
        ((Control) this.EbAttachDBName).Value = connectionStringBuilder.AttachDBFilename;
        this.DbFileNameMode.Checked = true;
      }
      if (connectionStringBuilder.IntegratedSecurity)
        this.NTAuthentication.Checked = true;
      else
        this.SQLAuthentication.Checked = true;
      ((Control) this.User).Value = connectionStringBuilder.UserID;
      ((Control) this.Password).Value = connectionStringBuilder.Password;
      this.ServerAuthentication();
      this.SourceMode();
    }

    protected void OnChangeTypeAuthentication() => this.ServerAuthentication();

    protected void OnChangeSource() => this.SourceMode();

    protected void OnClose() => this.OnCancel((object) this, new EventArgs());

    protected void OnTest()
    {
      string error;
      if (!this.TestConnection(this.ConnectionString, out error))
        this.Status.Text = error;
      else
        this.Status.Text = SqlDBEditor.connectionOk;
    }

    protected void OnApply()
    {
      this.Params = this.ConnectionString;
      this.OnOK((object) this, new EventArgs());
    }

    public string ConnectionString
    {
      get
      {
        SqlConnectionStringBuilder connectionStringBuilder = new SqlConnectionStringBuilder();
        connectionStringBuilder.DataSource = ((Control) this.Server).Value;
        if (this.DbNameMode.Checked)
          connectionStringBuilder.InitialCatalog = ((Control) this.EbDBName).Value;
        else
          connectionStringBuilder.AttachDBFilename = ((Control) this.EbAttachDBName).Value;
        connectionStringBuilder.IntegratedSecurity = this.NTAuthentication.Checked;
        connectionStringBuilder.UserID = ((Control) this.User).Value;
        connectionStringBuilder.Password = ((Control) this.Password).Value;
        return connectionStringBuilder.ToString();
      }
    }

    protected void ServerAuthentication()
    {
      if (this.NTAuthentication.Checked)
      {
        this.NTAuthentication.Checked = true;
        this.SQLAuthentication.Checked = false;
        ((Control) this.User).Disabled = true;
        ((Control) this.Password).Disabled = true;
      }
      else
      {
        this.NTAuthentication.Checked = false;
        this.SQLAuthentication.Checked = true;
        ((Control) this.User).Disabled = false;
        ((Control) this.Password).Disabled = false;
      }
    }

    protected void SourceMode()
    {
      if (this.DbNameMode.Checked)
      {
        this.DbNameMode.Checked = true;
        this.DbFileNameMode.Checked = false;
        ((Control) this.EbDBName).Disabled = false;
        ((Control) this.EbAttachDBName).Disabled = true;
      }
      else
      {
        this.DbNameMode.Checked = false;
        this.DbFileNameMode.Checked = true;
        ((Control) this.EbDBName).Disabled = true;
        ((Control) this.EbAttachDBName).Disabled = false;
      }
    }

    public string Params
    {
      get
      {
        string s = HttpContext.Current.Session[WebUtil.GetQueryString("params")] as string;
        if (s.StartsWith(SqlDBEditor.paramName))
          s = s.Substring(SqlDBEditor.paramName.Length, s.Length - SqlDBEditor.paramName.Length);
        return HttpContext.Current.Server.HtmlDecode(s);
      }
      set
      {
        string str = SqlDBEditor.paramName + HttpContext.Current.Server.HtmlEncode(value);
        HttpContext.Current.Session.Add(WebUtil.GetQueryString("params"), (object) str);
      }
    }
  }
}
