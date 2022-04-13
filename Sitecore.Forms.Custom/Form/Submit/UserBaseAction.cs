// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Submit.UserBaseAction
// Assembly: Sitecore.Forms.Custom, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E620A323-75A8-42D2-B304-7093FE781621
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Custom.dll

using Microsoft.Extensions.DependencyInjection;
using Sitecore.Analytics;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Form.Core.Configuration;
using Sitecore.Form.Core.Submit;
using Sitecore.Forms.Core.Data;
using Sitecore.Security;
using Sitecore.Security.Accounts;
using Sitecore.Security.Domains;
using Sitecore.SecurityModel;
using Sitecore.WFFM.Abstractions.Actions;
using Sitecore.WFFM.Abstractions.Dependencies;
using System;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Web.Profile;
using System.Web.Security;

namespace Sitecore.Form.Submit
{
    [Serializable]
    public abstract class UserBaseAction : AuditSaveAction
    {
        public bool AssociateUserWithVisitor { get; set; }

        public string AuditField { get; set; }

        public string DomainField { get; set; }

        public string PasswordField { get; set; }

        public string ProfileItemId { get; set; }

        public string UserNameField { get; set; }

        public string UserNameIsEmpty { get; set; }

        public static string GetFullUserName(string domainName, string userName)
        {
            if (userName.Contains("\\"))
                return userName;
            return string.Join("\\", new string[2]
            {
        domainName,
        userName
            });
        }

        public static string Escape(string userName) => userName.Replace("@", "_at_").Replace(".", "_dot_");

        public static string GetValidUserName(string domainName, string userName)
        {
            string userName1 = userName;
            Domain domain = DomainManager.GetDomain(domainName);
            if ((domain != (Domain)null) && !domain.IsValidAccountName(userName1))
                userName1 = UserBaseAction.Escape(userName1);
            return UserBaseAction.GetFullUserName(domainName, userName1);
        }

        protected string GetProfileProperty(UserProfile profile, string profileproperty)
        {
            if (profileproperty == "Comment")
                return profile.Comment;
            if (profileproperty == "Email")
                return profile.Email;
            if (profileproperty == "Full Name")
                return profile.FullName;
            return profileproperty == "Name" ? profile.Name : profile[profileproperty];
        }

        protected string GetUserNameIfExist(string preUserName)
        {
            string username1 = preUserName;
            if (string.IsNullOrEmpty(username1))
                return (string)null;
            if (!username1.Contains("\\"))
                username1 = string.Join("\\", new string[2]
                {
          this.DomainField,
          username1
                });
            if (Membership.GetUser(username1) != null)
                return username1;
            string username2 = username1.Replace("@", "_at_").Replace(".", "_dot_");
            return Membership.GetUser(username2) != null ? username2 : (string)null;
        }

        protected virtual string ProccessBaseOperations(ID formId, AdaptedResultList fields) => this.ProccessBaseOperations(formId, fields, true);

        protected virtual string ProccessBaseOperations(
          ID formId,
          AdaptedResultList fields,
          bool createIfNotExist)
        {
            AdaptedControlResult entry1 = fields.GetEntry(this.UserNameField, "User name");
            string password = this.PasswordField;
            if (string.Compare(this.PasswordField, "blankPassword", StringComparison.OrdinalIgnoreCase) != 0 && string.Compare(this.PasswordField, "randomPassword", StringComparison.OrdinalIgnoreCase) != 0)
            {
                AdaptedControlResult entry2 = fields.GetEntry(this.PasswordField, "Password");
                password = entry2 != null ? entry2.Value : "blankPassword";
            }
            if (string.IsNullOrEmpty(entry1?.Value))
                throw new Exception(this.UserNameIsEmpty ?? DependenciesManager.ResourceManager.GetString("USER_NAME_IS_NULL_OR_EMPTY"));
            string str = UserBaseAction.GetFullUserName(this.DomainField, entry1.Value);
            if (Membership.GetUser(str, false) == null)
            {
                str = UserBaseAction.GetValidUserName(this.DomainField, entry1.Value);
                if (Membership.GetUser(str, false) == null && !createIfNotExist)
                    return (string)null;
                this.UpdatePassword(formId, str, password);
            }
            this.UpdateGlobalSession(str);
            return str;
        }

        protected virtual void UpdateEmail(string userName, string mail)
        {
            MembershipUser user = Membership.GetUser(userName, false);
            if (user == null)
                return;
            Match match = Regex.Match(mail, "^[A-Za-z0-9](([_\\.\\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\\.\\-]?[a-zA-Z0-9]+)*)\\.([A-Za-z]{2,})$");
            if (!match.Success || match.Index != 0 || match.Length != mail.Length)
                return;
            user.Email = mail;
            Membership.UpdateUser(user);
        }

        protected virtual void UpdateGlobalSession(string userName)
        {
            if (!this.AssociateUserWithVisitor || Tracker.Current == null || Tracker.Current.Session == null)
                return;

            var identificationManager = Sitecore.DependencyInjection.ServiceLocator.ServiceProvider.GetRequiredService<Sitecore.Analytics.Tracking.Identification.IContactIdentificationManager>();
            Sitecore.Analytics.Tracking.Identification.IdentificationResult result = identificationManager.IdentifyAs(new Sitecore.Analytics.Tracking.Identification.KnownContactIdentifier("wffm", Guid.NewGuid().ToString()));
            if (!result.Success)
            {
                DependenciesManager.Logger.Warn($"Cannot identify contact for user {userName}. {result.ErrorCode ?? "[EMPTY ERROR CODE]"}: {result.ErrorMessage ?? "[EMPTY ERROR MESSAGE]"}", this);
            }
        }

        protected virtual void UpdatePassword(ID formID, string userName, string password)
        {
            string str = password;
            if (this.PasswordField == "blankPassword")
                str = string.Empty;
            else if (this.PasswordField == "randomPassword")
                str = Membership.GeneratePassword(Membership.MinRequiredPasswordLength, Membership.MinRequiredNonAlphanumericCharacters);
            if (Membership.GetUser(userName, false) != null)
                return;
            User.Create(userName, str);
            Item innerItem = StaticSettings.ContextDatabase.GetItem(formID);
            string propertyValue = this.ProfileItemId;
            if (innerItem != null)
                propertyValue = new FormItem(innerItem).ProfileItem;
            if (string.IsNullOrEmpty(propertyValue))
                propertyValue = "{AE4C4969-5B7E-4B4E-9042-B2D8701CE214}";
            UserProfile profile = User.FromName(userName, true).Profile;
            this.UpdateProfileProperty(profile, "ProfileItemId", propertyValue);
            ((SettingsBase)profile).Save();
        }

        protected void UpdateProfileProperty(
          UserProfile profile,
          string profileproperty,
          string propertyValue)
        {
            try
            {
                if (profileproperty == "Comment")
                    profile.Comment = propertyValue;
                if (profileproperty == "Full Name")
                    profile.FullName = propertyValue;
                if (profileproperty == "Name")
                    profile.Name = propertyValue;
                else if (profileproperty == "Email")
                    profile.Email = propertyValue;
                else
                    profile[profileproperty] = propertyValue;
            }
            catch (Exception ex)
            {
                DependenciesManager.Logger.Warn(Sitecore.StringExtensions.StringExtensions.FormatWith("The Create User action: cannot update {0} profile[property:{1}]: {2}", new object[3]
                {
          (object) ((ProfileBase) profile).UserName,
          (object) profileproperty,
          (object) ex.Message
                }), (object)this);
            }
        }
    }
}
