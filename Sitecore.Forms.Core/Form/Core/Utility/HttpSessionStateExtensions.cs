// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.Utility.HttpSessionStateExtensions
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Forms.Core.Data;
using System.Web.SessionState;

namespace Sitecore.Form.Core.Utility
{
  public static class HttpSessionStateExtensions
  {
    internal static SubmitCounter GetSubmitCounter(this HttpSessionState state)
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
  }
}
