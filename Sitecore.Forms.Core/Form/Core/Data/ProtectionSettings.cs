// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.Data.ProtectionSettings
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

namespace Sitecore.Form.Core.Data
{
  public class ProtectionSettings
  {
    public static ProtectionSettings ServerNoProtection;
    public static ProtectionSettings SessionNoProtection = new ProtectionSettings(false, 60U, 100U)
    {
      RedirectPage = "{96F047CC-61BB-4D97-BB13-6D3AA6491891}"
    };
    private string placeholder;
    private bool redirectEnabled;
    private string redirectPage;

    static ProtectionSettings() => ProtectionSettings.ServerNoProtection = new ProtectionSettings(false, 60U, 100U)
    {
      RedirectPage = "{F6C596B7-5146-444C-B983-29B73CAB5657}"
    };

    public ProtectionSettings(uint minutesInterval, uint submitsNumber)
      : this(true, minutesInterval, submitsNumber)
    {
    }

    public ProtectionSettings(bool enabled = false, uint minutesInterval = 0, uint submitsNumber = 0)
    {
      this.Enabled = enabled;
      this.MinutesInterval = minutesInterval;
      this.SubmitsNumber = submitsNumber;
      this.Placeholder = "content";
    }

    public bool Enabled { get; set; }

    public uint MinutesInterval { get; private set; }

    public string Placeholder
    {
      get => this.placeholder ?? string.Empty;
      set => this.placeholder = value;
    }

    public bool RedirectEnabled
    {
      get => this.Enabled && this.redirectEnabled;
      set => this.redirectEnabled = value;
    }

    public string RedirectPage
    {
      get => this.redirectPage ?? string.Empty;
      set => this.redirectPage = value;
    }

    public uint SubmitsNumber { get; private set; }
  }
}
