// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.Pipelines.FormUploadFile.Save
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Common;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Form.Core.Configuration;
using Sitecore.Form.Core.Media;
using Sitecore.SecurityModel;
using System;
using System.Collections.Generic;

namespace Sitecore.Form.Core.Pipelines.FormUploadFile
{
  public class Save
  {
    public void Process(FormUploadFileArgs args)
    {
      Assert.ArgumentNotNull((object) args, nameof (args));
      Assert.ArgumentNotNull((object) args.File, "file");
      if (string.IsNullOrEmpty(args.File.FileName))
        return;
      SecurityDisabler securityDisabler = (SecurityDisabler) null;
      if (!args.UseSecurity)
        securityDisabler = new SecurityDisabler();
      try
      {
        MediaUploaderEx mediaUploaderEx = new MediaUploaderEx();
        mediaUploaderEx.File = args.File;
        mediaUploaderEx.Unpack = false;
        mediaUploaderEx.Folder = args.Folder;
        mediaUploaderEx.Versioned = args.Versioned;
        mediaUploaderEx.Language = args.Language;
        mediaUploaderEx.AlternateText = args.GetFileParameter("alt");
        mediaUploaderEx.Overwrite = args.Overwrite;
        mediaUploaderEx.Database = StaticSettings.MasterDatabase;
        mediaUploaderEx.FileBased = args.Destination == Sitecore.Pipelines.Upload.UploadDestination.File;
        List<MediaUploadResultEx> mediaUploadResultExList = mediaUploaderEx.Upload();
        Log.Audit((object) this, "Upload: {0}", new string[1]
        {
          args.File.FileName
        });
        foreach (MediaUploadResultEx mediaUploadResultEx in mediaUploadResultExList)
          this.ProcessItem(args, (MediaItem)(mediaUploadResultEx.Item), mediaUploadResultEx.Path);
      }
      catch (Exception ex)
      {
        Log.Error("Could not save posted file: " + args.File.FileName, ex, (object) this);
        throw;
      }
      finally
      {
        ((Switcher<SecurityState, SecurityState>) securityDisabler)?.Dispose();
      }
    }

    private void ProcessItem(FormUploadFileArgs args, MediaItem mediaItem, string path)
    {
      Assert.ArgumentNotNull((object) args, nameof (args));
      Assert.ArgumentNotNull((object) mediaItem, nameof (mediaItem));
      Assert.ArgumentNotNull((object) path, nameof (path));
      if (mediaItem != null)
      {
        if (args.Destination == null)
          Log.Info("Media Item has been uploaded to database: " + path, (object) this);
        else
          Log.Info("Media Item has been uploaded to file system: " + path, (object) this);
      }
      else
        Log.Error("Failed to create Media Item in database: " + path, (object) this);
      args.Result = ((object) ((CustomItemBase) mediaItem).InnerItem.Uri).ToString();
    }
  }
}
