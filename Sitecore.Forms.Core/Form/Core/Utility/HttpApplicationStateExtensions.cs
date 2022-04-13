// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.Utility.HttpApplicationStateExtensions
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Data;
using Sitecore.Diagnostics;
using Sitecore.Forms.Core.Data;
using System.Web;

namespace Sitecore.Form.Core.Utility
{
  public static class HttpApplicationStateExtensions
  {
    internal static SubmitCounter GetSubmitCounter(this HttpApplicationState state)
    {
      if (state == null)
        return (SubmitCounter) null;
      string name = "SC_WFM_SUBMIT_COUNTER";
      SubmitCounter submitCounter = (SubmitCounter) state[name];
      if (submitCounter == null)
      {
        submitCounter = new SubmitCounter();
        state[name] = (object) submitCounter;
      }
      return submitCounter;
    }

    public static T GetValue<T>(this HttpApplicationState state, ID keyID)
    {
      Assert.ArgumentNotNull((object) state, nameof (state));
      return !ID.IsNullOrEmpty(keyID) ? (T) state[SessionUtil.GetSessionKey(keyID)] : default (T);
    }

    public static void SetValue<T>(this HttpApplicationState state, ID keyID, T value)
    {
      Assert.ArgumentNotNull((object) state, nameof (state));
      Assert.IsFalse(ID.IsNullOrEmpty(keyID), nameof (keyID));
      state[SessionUtil.GetSessionKey(keyID)] = (object) value;
    }
  }
}
