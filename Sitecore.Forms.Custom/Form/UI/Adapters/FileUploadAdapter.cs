// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.UI.Adapters.FileUploadAdapter
// Assembly: Sitecore.Forms.Custom, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E620A323-75A8-42D2-B304-7093FE781621
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Custom.dll

using Sitecore.Configuration;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Form.Core.Client.Submit;
using Sitecore.Form.Core.Configuration;
using Sitecore.Form.Core.Data;
using Sitecore.Form.Core.Pipelines.FormUploadFile;
using Sitecore.Pipelines;
using Sitecore.Pipelines.Upload;
using Sitecore.Resources;
using Sitecore.Resources.Media;
using Sitecore.Web.UI;
using Sitecore.WFFM.Abstractions.Actions;
using Sitecore.WFFM.Abstractions.Data;
using Sitecore.WFFM.Abstractions.Dependencies;
using Sitecore.WFFM.Abstractions.Shared;
using System;
using System.IO;
using System.Text;
using System.Web.UI;

namespace Sitecore.Form.UI.Adapters
{
  public class FileUploadAdapter : Adapter
  {
    private readonly IResourceManager resourceManager;

    public FileUploadAdapter()
      : this(DependenciesManager.ResourceManager)
    {
    }

    public FileUploadAdapter(IResourceManager resourceManager) => this.resourceManager = resourceManager;

    public override string AdaptResult(IFieldItem field, object value)
    {
      if (!(value is PostedFile postedFile))
        return string.Empty;
      if (string.IsNullOrEmpty(postedFile.Destination))
      {
        Log.Error(this.resourceManager.Localize("ERROR_PATH_NOT_EXIT"), (object) this);
        return string.Empty;
      }
      Item obj = StaticSettings.MasterDatabase.SelectSingleItem(postedFile.Destination);
      if (obj == null)
      {
        Log.Error(this.resourceManager.Localize("ERROR_PATH_NOT_EXIT"), (object) this);
        obj = StaticSettings.MasterDatabase.GetItem((ID) ItemIDs.MediaLibraryRoot);
        if (obj == null)
          throw new Sitecore.Form.Core.Data.FormSubmitException(string.Format(this.resourceManager.Localize("DESTINATION_NOT_EXIST"), (object) postedFile.Destination), string.Empty);
      }
      FormUploadFileArgs formUploadFileArgs = new FormUploadFileArgs()
      {
        File = postedFile,
        Folder = obj.Uri.GetPathOrId(),
        Overwrite = false,
        Versioned = Sitecore.Configuration.Settings.Media.UploadAsVersionableByDefault,
        Language = Context.Language,
        Destination = Sitecore.Configuration.Settings.Media.UploadAsFiles ? (UploadDestination) 1 : (UploadDestination) 0
      };
      CorePipeline.Run("formUploadFile", (PipelineArgs) formUploadFileArgs);
      return formUploadFileArgs.Result;
    }

    public override string AdaptToSitecoreStandard(IFieldItem field, string value)
    {
      if (!string.IsNullOrEmpty(value))
      {
        try
        {
          ItemUri itemUri = ItemUri.Parse(value);
          if ((itemUri!=(ItemUri) null))
          {
            Item obj = Database.GetItem(itemUri);
            if (obj != null)
              return MediaManager.GetMediaUrl(new MediaItem(obj));
          }
        }
        catch (Exception ex)
        {
          Log.Error(ex.Message, ex);
        }
      }
      return value;
    }

    public override string AdaptToFriendlyValue(IFieldItem field, string value)
    {
      if (string.IsNullOrEmpty(value))
        return value;
      StringBuilder sb = new StringBuilder();
      try
      {
        ItemUri itemUri = ItemUri.Parse(value);
        if (!(itemUri!=(ItemUri) null))
          return value;
        Item obj = Database.GetItem(itemUri);
        if (obj == null)
          return value;
        string themedImageSource = Images.GetThemedImageSource(((Appearance) obj.Appearance).Icon, (ImageDimension) 1);
        MediaUrlOptions.GetThumbnailOptions((MediaItem)(obj));
        sb.AppendFormat("<a href=\"{0}\" class='scDvUploadValue' onclick=\"", (object) themedImageSource.Replace("?h=16&thn=1&w=16&", "?"));
        sb.AppendFormat("javascript:return scForm.invoke('forms:itemopen(id={0},db={1},la={2},vs={3})')", (object) itemUri.ItemID, (object) itemUri.DatabaseName, (object) itemUri.Language.Name, (object) itemUri.Version.Number);
        sb.Append("\">");
        new ImageBuilder()
        {
          Src = themedImageSource,
          Attributes = {
            ["align"] = "absMiddle"
          },
          Class = "scIconItemIconImage"
        }.Render(new HtmlTextWriter((TextWriter) new StringWriter(sb)));
        sb.AppendFormat("{0}</a>", (object) obj.Paths.FullPath);
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
        return value;
      }
      return sb.ToString();
    }
  }
}
