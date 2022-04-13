// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Core.Data.Texts
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Globalization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Sitecore.Forms.Core.Data
{
  public class Texts
  {
    private static Texts instance;
    public const string Browse = "Browse";
    public const string SelectTriggeredMessageYouWantToUse = "Select the triggered message that you want to use.";
    public const string TriggeredMessage = "Triggered Message";
    public const string MessageAndRecipient = "Message & Recipient";
    public const string SelectTriggeredMessageAndSubscriberYouWantToUse = "Select the triggered message that you want to send and the user that you want to send the message to.";
    public const string PersonalizeMessage = "Personalize Message";
    public const string YouCanPersonalizeMessage = "You can personalize the triggered message with information stored in form fields. To create a token in the triggered message, use the $token_name$ format and select a form field. To preview the message, click 'Next'.";
    public const string PreviewMessage = "Preview Message";
    public const string ThisPageDisplaysFinalAppearance = "This page displays the message you have selected and modified. Click 'Back' if you want to modify the message. Click 'Finish' to close the page.";
    public const string Domain = "Domain:";
    public const string UserName = "User Name:";
    public const string UserNameIsRequired = "The user name is required.";
    public const string DomainIsRequired = "The domain is required.";
    public const string Message = "Message";
    public const string Recipient = "Recipient";
    public const string SendMessageTo = "Send the message to";
    public const string EmailAddressOfCurrentVisitor = "Email address of the current visitor";
    public const string EmailAddressOfExistingVisitor = "Email address of an existing user";
    public const string UserNameBasedOnInformation = "The user name is based on information entered in form fields:";
    public const string SelectFormFieldWhoseValue = "Select the form field whose values will be displayed in place of the token.";
    public const string WaitForSendConfirmationReceipt = "Wait for the send confirmation receipt";
    public const string From = "FROM";
    public const string Subject = "SUBJECT";
    public const string GlobalSettingsItemMissedOrNoRights = "The Global Settings item is missing or you do not have the appropriate security rights.";

    public static Texts Instance
    {
      get => Texts.instance ?? (Texts.instance = new Texts());
      set
      {
        if (value == null)
          return;
        Texts.instance = value;
      }
    }

    public static string Localize(string text, params object[] parameters) => Texts.Localize((Language) null, text, parameters);

    public static string Localize(Language language, string text, params object[] parameters) => Texts.Instance.LocalizeText(language, text, parameters);

    public virtual string LocalizeText(Language language, string text, params object[] parameters)
    {
      string str = (language == (Language) null) ? Translate.Text(text) : Translate.TextByLanguage(text, language);
      return parameters.Length == 0 ? str : Sitecore.StringExtensions.StringExtensions.FormatWith(str, parameters);
    }

    public static string[] GetStrings() => ((IEnumerable<FieldInfo>) typeof (Texts).GetFields(BindingFlags.Static | BindingFlags.Public)).Where<FieldInfo>((Func<FieldInfo, bool>) (field => field.FieldType == typeof (string))).Select<FieldInfo, string>((Func<FieldInfo, string>) (field => field.GetValue((object) null) as string)).Where<string>((Func<string, bool>) (text => !string.IsNullOrEmpty(text))).ToArray<string>();
  }
}
