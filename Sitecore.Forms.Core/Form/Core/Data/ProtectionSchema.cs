// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.Data.ProtectionSchema
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Data;
using Sitecore.Diagnostics;
using Sitecore.Forms.Core.Data;
using System.Web;
using System.Xml.Linq;

namespace Sitecore.Form.Core.Data
{
  public class ProtectionSchema
  {
    public static readonly ProtectionSchema NoProtection = new ProtectionSchema(false, ProtectionSettings.SessionNoProtection, ProtectionSettings.ServerNoProtection);
    private string placeholder;
    private string redirectToPage;

    public ProtectionSchema(ProtectionSettings session, ProtectionSettings server)
      : this(true, session, server, (ISubmitCounter) SubmitCounter.Server, (ISubmitCounter) SubmitCounter.Session)
    {
    }

    public ProtectionSchema(bool enabled, ProtectionSettings session, ProtectionSettings server)
      : this(enabled, session, server, (ISubmitCounter) SubmitCounter.Server, (ISubmitCounter) SubmitCounter.Session)
    {
    }

    public ProtectionSchema(
      bool enabled,
      ProtectionSettings session,
      ProtectionSettings server,
      ISubmitCounter serverSubmitCounter,
      ISubmitCounter sessionSubmitCounter)
    {
      Assert.ArgumentNotNull((object) session, nameof (session));
      Assert.ArgumentNotNull((object) server, nameof (server));
      this.Session = session;
      this.Server = server;
      this.Enabled = enabled;
      this.RedirectToPage = "{FBF67C79-6FD7-454A-AB66-C93D872EA7A6}";
      this.Placeholder = "content";
      this.ServerSubmitCounter = serverSubmitCounter;
      this.SessionSubmitCounter = sessionSubmitCounter;
      this.WarningEmail = new ProtectionSchema.Message();
    }

    public bool Enabled { get; private set; }

    public ISubmitCounter SessionSubmitCounter { get; private set; }

    public ISubmitCounter ServerSubmitCounter { get; private set; }

    public string Placeholder
    {
      get => this.placeholder ?? string.Empty;
      set => this.placeholder = value;
    }

    public bool RedirectRobots => true;

    public string RedirectToPage
    {
      get => this.redirectToPage ?? string.Empty;
      set => this.redirectToPage = value;
    }

    public ProtectionSettings Server { get; private set; }

    public ProtectionSettings Session { get; private set; }

    public ProtectionSchema.Message WarningEmail { get; private set; }

    public static ProtectionSchema Parse(string xml)
    {
      try
      {
        if (!string.IsNullOrEmpty(xml))
        {
          XElement xelement1 = XElement.Parse(xml);
          XElement xelement2 = xelement1.Element((XName) "session");
          XElement xelement3 = xelement1.Element((XName) "server");
          if (xelement2 == null || xelement3 == null)
            return new ProtectionSchema(false, ProtectionSettings.SessionNoProtection, ProtectionSettings.ServerNoProtection);
          ProtectionSettings session = Sitecore.Extensions.XElementExtensions.XElementExtensions.GetAttributeValue(xelement2, "e") == "1" ? new ProtectionSettings(uint.Parse(Sitecore.Extensions.XElementExtensions.XElementExtensions.GetAttributeValue(xelement2, "m")), uint.Parse(Sitecore.Extensions.XElementExtensions.XElementExtensions.GetAttributeValue(xelement2, "s"))) : ProtectionSettings.SessionNoProtection;
          session.RedirectPage = Sitecore.Extensions.XElementExtensions.XElementExtensions.GetAttributeValue(xelement2, "page");
          session.Placeholder = Sitecore.Extensions.XElementExtensions.XElementExtensions.GetAttributeValue(xelement2, "placeholder");
          session.RedirectEnabled = Sitecore.Extensions.XElementExtensions.XElementExtensions.GetAttributeValue(xelement2, "re") == "1";
          ProtectionSettings server = Sitecore.Extensions.XElementExtensions.XElementExtensions.GetAttributeValue(xelement3, "e") == "1" ? new ProtectionSettings(uint.Parse(Sitecore.Extensions.XElementExtensions.XElementExtensions.GetAttributeValue(xelement3, "m")), uint.Parse(Sitecore.Extensions.XElementExtensions.XElementExtensions.GetAttributeValue(xelement3, "s"))) : ProtectionSettings.ServerNoProtection;
          server.RedirectPage = Sitecore.Extensions.XElementExtensions.XElementExtensions.GetAttributeValue(xelement3, "page");
          server.Placeholder = Sitecore.Extensions.XElementExtensions.XElementExtensions.GetAttributeValue(xelement3, "placeholder");
          server.RedirectEnabled = Sitecore.Extensions.XElementExtensions.XElementExtensions.GetAttributeValue(xelement3, "re") == "1";
          ProtectionSchema protectionSchema = new ProtectionSchema(session, server);
          protectionSchema.RedirectToPage = Sitecore.Extensions.XElementExtensions.XElementExtensions.GetAttributeValue(xelement1, "page");
          protectionSchema.Placeholder = Sitecore.Extensions.XElementExtensions.XElementExtensions.GetAttributeValue(xelement1, "placeholder");
          protectionSchema.Enabled = Sitecore.Extensions.XElementExtensions.XElementExtensions.GetAttributeValue(xelement1, "e") == "1";
          XElement xelement4 = xelement1.Element((XName) "message");
          if (xelement4 != null)
          {
            protectionSchema.WarningEmail.To = Sitecore.Extensions.XElementExtensions.XElementExtensions.GetAttributeValue(xelement4, "to");
            protectionSchema.WarningEmail.CC = Sitecore.Extensions.XElementExtensions.XElementExtensions.GetAttributeValue(xelement4, "cc");
            protectionSchema.WarningEmail.Subject = Sitecore.Extensions.XElementExtensions.XElementExtensions.GetAttributeValue(xelement4, "s");
            protectionSchema.WarningEmail.Body = Sitecore.Extensions.XElementExtensions.XElementExtensions.GetAttributeValue(xelement4, "b");
          }
          return protectionSchema;
        }
      }
      catch
      {
      }
      return new ProtectionSchema(false, ProtectionSettings.SessionNoProtection, ProtectionSettings.ServerNoProtection);
    }

    public bool IsServerThresholdExceeded(ID formID, uint count) => this.Server.Enabled && (long) this.ServerSubmitCounter.GetSubmitCount(formID) > (long) count;

    public bool IsServerThresholdExceeded(ID formID) => this.IsServerThresholdExceeded(formID, this.Server.SubmitsNumber);

    public bool IsSessionThresholdExceeded(ID formID, uint count) => this.Session.Enabled && (long) this.SessionSubmitCounter.GetSubmitCount(formID) > (long) count;

    public bool IsSessionThresholdExceeded(ID formId) => this.IsSessionThresholdExceeded(formId, this.Session.SubmitsNumber);

    public virtual void AddSubmitToSession(ID formId) => this.SessionSubmitCounter.AddSubmit(formId, this.Session.MinutesInterval);

    public virtual void AddSubmitToServer(ID formId) => this.ServerSubmitCounter.AddSubmit(formId, this.Server.MinutesInterval);

    public override string ToString() => new XElement((XName) "schema", new object[7]
    {
      (object) new XAttribute((XName) "page", (object) this.RedirectToPage),
      (object) new XAttribute((XName) "placeholder", (object) this.Placeholder),
      (object) new XAttribute((XName) "a", this.RedirectRobots ? (object) "1" : (object) "0"),
      (object) new XAttribute((XName) "e", this.Enabled ? (object) "1" : (object) "0"),
      (object) new XElement((XName) "session", new object[6]
      {
        (object) new XAttribute((XName) "e", this.Session.Enabled ? (object) "1" : (object) "0"),
        (object) new XAttribute((XName) "s", (object) this.Session.SubmitsNumber),
        (object) new XAttribute((XName) "m", (object) this.Session.MinutesInterval),
        (object) new XAttribute((XName) "page", (object) this.Session.RedirectPage),
        (object) new XAttribute((XName) "placeholder", (object) this.Session.Placeholder),
        (object) new XAttribute((XName) "re", this.Session.RedirectEnabled ? (object) "1" : (object) "0")
      }),
      (object) new XElement((XName) "server", new object[6]
      {
        (object) new XAttribute((XName) "e", this.Server.Enabled ? (object) "1" : (object) "0"),
        (object) new XAttribute((XName) "s", (object) this.Server.SubmitsNumber),
        (object) new XAttribute((XName) "m", (object) this.Server.MinutesInterval),
        (object) new XAttribute((XName) "page", (object) this.Server.RedirectPage),
        (object) new XAttribute((XName) "placeholder", (object) this.Server.Placeholder),
        (object) new XAttribute((XName) "re", this.Server.RedirectEnabled ? (object) "1" : (object) "0")
      }),
      (object) new XElement((XName) "message", new object[4]
      {
        (object) new XAttribute((XName) "to", (object) this.WarningEmail.To),
        (object) new XAttribute((XName) "cc", (object) this.WarningEmail.CC),
        (object) new XAttribute((XName) "s", (object) this.WarningEmail.Subject),
        (object) new XAttribute((XName) "b", (object) HttpUtility.HtmlEncode(HttpUtility.HtmlEncode(this.WarningEmail.Body)))
      })
    }).ToString();

    public static implicit operator ProtectionSchema(string xml) => ProtectionSchema.Parse(xml);

    public class Message
    {
      private string body;
      private string cc;
      private string subject;
      private string to;

      public string Body
      {
        get => this.body ?? string.Empty;
        set
        {
          Assert.ArgumentNotNull((object) value, nameof (value));
          this.body = value;
        }
      }

      public string CC
      {
        get => this.cc ?? string.Empty;
        set => this.cc = value;
      }

      public string Subject
      {
        get => this.subject ?? string.Empty;
        set => this.subject = value;
      }

      public string To
      {
        get => this.to ?? string.Empty;
        set => this.to = value;
      }
    }
  }
}
