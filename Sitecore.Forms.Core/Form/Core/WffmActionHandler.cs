// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.WffmActionHandler
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Configuration;
using Sitecore.ContentSearch.Utilities;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Data.Managers;
using Sitecore.Diagnostics;
using Sitecore.Events;
using Sitecore.Form.Core.Configuration;
using Sitecore.Publishing;
using Sitecore.Security.Accounts;
using Sitecore.WFFM.Abstractions.Actions;
using Sitecore.WFFM.Abstractions.Dependencies;
using Sitecore.WFFM.Abstractions.Shared;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Authentication;
using System.Web.Hosting;
using System.Web.Security;
using System.Xml.Serialization;

namespace Sitecore.Form.Core
{
  public class WffmActionHandler
  {
    private readonly ISettings _settings;
    private readonly IActionExecutor _actionExecutor;

    public WffmActionHandler(IActionExecutor actionExecutor, ISettings settings)
    {
      Assert.ArgumentNotNull((object) actionExecutor, nameof (actionExecutor));
      Assert.ArgumentNotNull((object) settings, nameof (settings));
      this._actionExecutor = actionExecutor;
      this._settings = settings;
    }

    public static void Run(WffmActionEvent remoteEvent)
    {
      Assert.ArgumentNotNull((object) remoteEvent, nameof (remoteEvent));
      Event.RaiseEvent("wffm:action:remote", new object[1]
      {
        (object) new WffmActionEventArgs(remoteEvent)
      });
    }

    protected void OnWffmActionEventFired(object sender, EventArgs args)
    {
      Assert.ArgumentNotNull((object) args, nameof (args));
      if (args is WffmActionEventArgs wffmActionEventArgs)
      {
        string b = string.IsNullOrEmpty(this._settings.InstanceName) ? HostingEnvironment.SiteName : this._settings.InstanceName;
        if (!string.IsNullOrEmpty(wffmActionEventArgs.RemoteEvent.CMInstanceName) && !string.Equals(wffmActionEventArgs.RemoteEvent.CMInstanceName, b, StringComparison.InvariantCultureIgnoreCase))
          return;
        if (wffmActionEventArgs.RemoteEvent.Fields != null)
          EnumerableExtensions.ForEach<ControlResult>((IEnumerable<ControlResult>) wffmActionEventArgs.RemoteEvent.Fields, (Action<ControlResult>) (f =>
          {
            if (f.Value == null)
              return;
            using (TextReader textReader = (TextReader) new StringReader(f.Value.ToString()))
            {
              f.Value = new XmlSerializer(Type.GetType(f.FieldType)).Deserialize(textReader);
              if (!(f.Value is PostedFileData fileData16))
                return;
              Database database = (Database) null;
              Stream blob = this.FindBlob(fileData16, out database);
              Assert.IsNotNull((object) blob, "Failed to find blob for file upload.");
              try
              {
                PostedFile postedFile = new PostedFile()
                {
                  Destination = fileData16.Destination,
                  FileName = fileData16.FileName
                };
                using (MemoryStream memoryStream = new MemoryStream())
                {
                  blob.CopyTo((Stream) memoryStream);
                  postedFile.Data = memoryStream.ToArray();
                }
                f.Value = (object) postedFile;
              }
              finally
              {
                blob.Dispose();
                ItemManager.RemoveBlobStream(fileData16.BlobId, database);
              }
            }
          }));
        this._actionExecutor.ExecuteSaving(wffmActionEventArgs.RemoteEvent.FormID, wffmActionEventArgs.RemoteEvent.Fields, wffmActionEventArgs.RemoteEvent.Actions, false, ID.Parse(wffmActionEventArgs.RemoteEvent.SessionIDGuid));
      }
      else
        DependenciesManager.Logger.Warn("OnWffmActionEventFired excepted args of WffmActionEventArgs type", (object) this);
    }

    private Stream FindBlob(PostedFileData fileData, out Database database)
    {
      database = (Database) null;
      List<string> stringList = new List<string>();
      if (((IEnumerable<string>) Factory.GetDatabaseNames()).Any<string>((Func<string, bool>) (name => name == fileData.DatabaseName)))
        stringList.Add(fileData.DatabaseName);
      foreach (Item publishingTarget in (List<Item>) PublishManager.GetPublishingTargets(Database.GetDatabase(Sitecore.Form.Core.Configuration.Settings.MasterDatabaseName)))
        stringList.Add(((BaseItem) publishingTarget)[(ID) FieldIDs.PublishingTargetDatabase]);
      foreach (string str in stringList)
      {
        Database database1 = Database.GetDatabase(str);
        Stream blobStream = ItemManager.GetBlobStream(fileData.BlobId, database1);
        if (blobStream != null)
        {
          database = database1;
          return blobStream;
        }
      }
      return (Stream) null;
    }

    private void Login(string userName, string password)
    {
      if (!Context.IsLoggedIn || ((Account) Context.User).Name.Equals(userName, StringComparison.OrdinalIgnoreCase))
        return;
      
      Sitecore.Security.Authentication.AuthenticationManager.Logout();
      if (!Membership.ValidateUser(userName, password))
        throw new AuthenticationException("Authentification failed");
      UserSwitcher.Enter(User.FromName(userName, true));
    }
  }
}
