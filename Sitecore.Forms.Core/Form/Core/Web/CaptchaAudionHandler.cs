// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.Web.CaptchaAudionHandler
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using MSCaptcha;
using Sitecore.Configuration;
using Sitecore.Diagnostics;
using Sitecore.Form.Core.Media;
using Sitecore.Forms.Core.Data;
using Sitecore.Web;
using Sitecore.WFFM.Abstractions.Dependencies;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;

namespace Sitecore.Form.Core.Web
{
  public class CaptchaAudionHandler : IHttpHandler
  {
    public bool IsReusable => true;

    public void ProcessRequest(HttpContext context)
    {
      HttpApplication applicationInstance = HttpContext.Current.ApplicationInstance;
      string str1 = applicationInstance.Request.QueryString["guid"];
      CaptchaImage captchaImage = (CaptchaImage) null;
      if (str1 != string.Empty)
        captchaImage = !string.IsNullOrEmpty(applicationInstance.Request.QueryString["s"]) ? (CaptchaImage) HttpContext.Current.Session[str1] : (CaptchaImage) HttpRuntime.Cache.Get(str1);
      if (captchaImage == null)
      {
        applicationInstance.Response.StatusCode = 404;
        context.ApplicationInstance.CompleteRequest();
      }
      else
      {
        string str2;
        do
        {
          str2 = Path.Combine(MainUtil.MapPath(Settings.TempFolderPath), Path.GetRandomFileName());
        }
        while (File.Exists(str2));
        using (FileStream fileStream = File.Create(str2, 8192))
          fileStream.Close();
        this.DoSpeech(captchaImage.Text, str2);
        context.Response.Clear();
        context.Response.ContentType = "audio/mpeg";
        context.Response.Cache.SetExpires(DateUtil.ToServerTime(DateTime.UtcNow).AddMinutes(5.0));
        context.Response.Cache.SetCacheability(HttpCacheability.Public);
        context.Response.StatusCode = 200;
        using (FileStream fileStream = File.Open(str2, FileMode.Open))
        {
          context.Response.AddHeader("Content-Length", fileStream.Length.ToString((IFormatProvider) CultureInfo.InvariantCulture));
          CaptchaAudionHandler.Transfer.TransmitStream((Stream) fileStream, context.Response, 512);
        }
        try
        {
          File.Delete(str2);
        }
        catch (IOException ex)
        {
        }
        context.ApplicationInstance.CompleteRequest();
      }
    }

    private void DoSpeech(string text, string fileName)
    {
      using (FileStream fileStream = new FileStream(fileName, FileMode.Create, FileAccess.Write))
      {
        using (BinaryWriter to = new BinaryWriter((Stream) fileStream))
        {
          List<string> stringList = new List<string>()
          {
            "Sitecore.WFFM.Core.Resources.Captcha.ding.mp3"
          };
          stringList.AddRange(text.ToLower().Select<char, string>((Func<char, string>) (c => "Sitecore.WFFM.Core.Resources.Captcha._" + c.ToString() + ".mp3")));
          using (StreamList streams = this.GetStreams(stringList.ToArray()))
            Wave.Concat(streams, to);
        }
      }
    }

    private StreamList GetStreams(string[] resIdentifiers)
    {
      StreamList streamList = new StreamList();
      if (resIdentifiers != null)
      {
        foreach (string resIdentifier in resIdentifiers)
        {
          if (!string.IsNullOrEmpty(resIdentifier))
            streamList.Add((Stream) DependenciesManager.ResourceManager.GetObject(resIdentifier));
        }
      }
      return streamList;
    }

    private class Transfer
    {
      public static void TransmitStream(Stream stream, HttpResponse response, int blockSize)
      {
        Assert.ArgumentNotNull((object) stream, nameof (stream));
        Assert.ArgumentNotNull((object) response, nameof (response));
        if (stream.Length == 0L)
          return;
        if (stream.CanSeek)
          stream.Seek(0L, SeekOrigin.Begin);
        byte[] buffer = new byte[blockSize];
        bool flag = true;
        while (flag)
        {
          flag = false;
          int count = stream.Read(buffer, 0, blockSize);
          if (count != 0)
          {
            response.OutputStream.Write(buffer, 0, count);
            try
            {
              response.Flush();
              flag = true;
            }
            catch (Exception ex)
            {
              Log.Error("Response.Flush attempt failed", ex, typeof (WebUtil));
              response.End();
              flag = true;
            }
          }
        }
      }
    }
  }
}
