// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.Submit.FormThresholdExceededTracker
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Form.Core.Ascx.Controls;
using Sitecore.Form.Core.Data;
using Sitecore.WFFM.Abstractions.Analytics;
using Sitecore.WFFM.Abstractions.Dependencies;
using Sitecore.WFFM.Abstractions.Mail;
using Sitecore.WFFM.Abstractions.Shared;
using System;
using System.Threading;

namespace Sitecore.Form.Core.Submit
{
  public class FormThresholdExceededTracker
  {
    public FormThresholdExceededTracker(ProtectionSchema schema, string thresholdExceededMessage)
    {
      this.RobotDetection = schema;
      this.ThresholdExceededMessage = thresholdExceededMessage;
    }

    protected string ThresholdExceededMessage { get; private set; }

    public ProtectionSchema RobotDetection { get; private set; }

    public void SendWarningMail()
    {
      if (string.IsNullOrEmpty(this.RobotDetection.WarningEmail.To))
        return;
      new Thread(new ThreadStart(this.SendMail)).Start();
    }

    private void SendMail()
    {
      IMailSender mailSender = DependenciesManager.MailSender;
      ISettings settings = DependenciesManager.Settings;
      if (mailSender == null)
      {
        DependenciesManager.Logger.Warn("Web Forms for Marketers: a threshold warning email can't be sent", (object) this);
      }
      else
      {
        EmailAttributes emailAttributes = new EmailAttributes(settings.EmailFromAddress, settings.MailServer, settings.MailServerUserName, settings.MailServerPassword, settings.MailServerPort)
        {
          To = this.RobotDetection.WarningEmail.To,
          CC = this.RobotDetection.WarningEmail.CC,
          Subject = this.RobotDetection.WarningEmail.Subject,
          Mail = this.RobotDetection.WarningEmail.Body
        };
        try
        {
          mailSender.SendMailWithGlobalParameters((IEmailAttributes) emailAttributes);
        }
        catch (Exception ex)
        {
          DependenciesManager.Logger.Warn("Web Forms for Marketers: a threshold warning email can't be sent", (object) ex);
        }
      }
    }

    public void TrackFormThresholdExceeded(object sender, EventArgs e)
    {
      this.UpdateAnalytics((SimpleForm) sender);
      this.SendWarningMail();
    }

    public void UpdateAnalytics(SimpleForm form)
    {
      if (!form.IsAnalyticsEnabled || form.FastPreview)
        return;
      DependenciesManager.AnalyticsTracker.TriggerEvent(new ServerEvent()
      {
        FieldID = ((object) form.FormID).ToString(),
        FormID = ((object) form.FormID).ToString(),
        Type = "Form Threshold Exceeded",
        Value = this.ThresholdExceededMessage
      });
    }
  }
}
