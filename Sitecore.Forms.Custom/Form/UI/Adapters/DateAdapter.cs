// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.UI.Adapters.DateAdapter
// Assembly: Sitecore.Forms.Custom, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E620A323-75A8-42D2-B304-7093FE781621
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Custom.dll

using Sitecore.Form.Core.Client.Submit;
using Sitecore.Form.Core.Configuration;
using Sitecore.Form.Core.Utility;
using Sitecore.WFFM.Abstractions.Data;

namespace Sitecore.Form.UI.Adapters
{
  public class DateAdapter : Adapter
  {
    public override string AdaptToFriendlyValue(IFieldItem field, string value)
    {
      try
      {
        Sitecore.Form.Core.Utility.Utils.SetUserCulture();
        return !DateUtil.IsIsoDate(value) ? value : DateUtil.IsoDateToDateTime(DateUtil.IsoDateToServerTimeIsoDate(value)).Date.ToString(Settings.DefaultDateFormat);
      }
      catch
      {
      }
      return value;
    }
  }
}
