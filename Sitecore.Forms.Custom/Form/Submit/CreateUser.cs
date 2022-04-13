// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Submit.CreateUser
// Assembly: Sitecore.Forms.Custom, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E620A323-75A8-42D2-B304-7093FE781621
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Custom.dll

using Sitecore.Data;
using Sitecore.Form.Core.Utility;
using Sitecore.Forms.Core.Data;
using Sitecore.Security.Accounts;
using Sitecore.WFFM.Abstractions.Actions;
using Sitecore.WFFM.Abstractions.Dependencies;
using System;
using System.Configuration;
using System.Xml.Linq;

namespace Sitecore.Form.Submit
{
  public class CreateUser : UserBaseAction
  {
    private ID formID;

    public override void Execute(
      ID formId,
      AdaptedResultList adaptedFields,
      ActionCallContext actionCallContext = null,
      params object[] data)
    {
      this.formID = formId;
      string str = this.ProccessBaseOperations(formId, adaptedFields);
      this.UpdateProfile(User.FromName(str, true), adaptedFields);
      this.UpdateAudit(formId, User.FromName(str, true));
    }

    protected virtual void UpdateAudit(ID formid, User user)
    {
      if (string.IsNullOrEmpty(this.AuditField) || !(this.AuditField != "NoAudit"))
        return;
      this.UpdateProfileProperty(user.Profile, this.AuditField, this.DumpAuditInfomration(this.GetProfileProperty(user.Profile, this.AuditField)));
      ((SettingsBase) user.Profile).Save();
    }

    protected virtual void UpdateProfile(User user, AdaptedResultList fields)
    {
      if (string.IsNullOrEmpty(this.Mapping))
        return;
      try
      {
        foreach (XElement element in XDocument.Parse(this.Mapping).Root.Elements())
        {
          string id = element.Attribute((XName) "fieldid").Value;
          string str = element.Element((XName) "profile").Attribute((XName) "fieldid").Value;
          AdaptedControlResult entry = fields.GetEntry(id, "fieldid");
          if (entry != null && !string.IsNullOrEmpty(str))
          {
            if (this.OverwriteProfile || string.IsNullOrEmpty(this.GetProfileProperty(user.Profile, str)))
            {
              this.UpdateProfileProperty(user.Profile, str, FieldReflectionUtil.GetSitecoreStyleValue(entry.FieldID, entry.Value));
              this.AuditUpdatedField(entry.FieldName, str, entry.Value);
            }
            else
              this.AuditSkippedField(entry.FieldName, str, entry.Value);
          }
        }
        ((SettingsBase) user.Profile).Save();
      }
      catch (Exception ex)
      {
        DependenciesManager.Logger.Warn("The Create User action: the mapping section cannot be parsed", ex, (object) this);
      }
    }

    public bool OverwriteProfile { get; set; }

    public string Mapping { get; set; }

    public override FormItem CurrentForm => FormItem.GetForm(this.formID);
  }
}
