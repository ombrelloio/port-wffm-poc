// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.Media.MediaUploaderEx
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Diagnostics;
using Sitecore.Extensions;
using Sitecore.IO;
using Sitecore.Pipelines.GetMediaCreatorOptions;
using Sitecore.Resources.Media;
using Sitecore.WFFM.Abstractions.Actions;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;

namespace Sitecore.Form.Core.Media
{
    public class MediaUploaderEx : MediaUploader
    {
        public new List<MediaUploadResultEx> Upload()
        {
            List<MediaUploadResultEx> list = new List<MediaUploadResultEx>();
            if (string.Compare(Path.GetExtension(this.File.FileName), ".zip", StringComparison.OrdinalIgnoreCase) == 0 && this.Unpack)
            {
                this.UnpackToDatabase(list);
                return list;
            }
            this.UploadToDatabase(list);
            return list;
        }

        private void UnpackToDatabase(List<MediaUploadResultEx> list)
        {
            Assert.ArgumentNotNull((object)list, nameof(list));
            string filename = FileUtil.MapPath(TempFolder.GetFilename("temp.zip"));
            this.File.SaveAs(filename);
            FileStream fileStream = System.IO.File.Open(filename, FileMode.Open, FileAccess.Read, FileShare.Read);
            try
            {
                using (ZipArchive zipArchive = new ZipArchive((Stream)fileStream))
                {
                    foreach (ZipArchiveEntry entry in zipArchive.Entries)
                    {
                        if (!entry.FullName.EndsWith("/") && !entry.IsMacOSMetaEntry())
                        {
                            MediaUploadResultEx mediaUploadResultEx = new MediaUploadResultEx();
                            list.Add(mediaUploadResultEx);
                            mediaUploadResultEx.Path = FileUtil.MakePath(this.Folder, entry.Name, '/');
                            mediaUploadResultEx.ValidMediaPath = MediaPathManager.ProposeValidMediaPath(mediaUploadResultEx.Path);
                            MediaCreatorOptions mediaCreatorOptions = new MediaCreatorOptions()
                            {
                                Language = this.Language,
                                Versioned = this.Versioned,
                                OverwriteExisting = this.Overwrite,
                                Destination = mediaUploadResultEx.ValidMediaPath,
                                FileBased = this.FileBased,
                                Database = this.Database
                            };
                            mediaCreatorOptions.Build((string)GetMediaCreatorOptionsArgs.UploadContext);
                            using (Stream stream = entry.Open())
                            {
                                using (MemoryStream memoryStream = new MemoryStream())
                                {
                                    stream.CopyTo((Stream)memoryStream);
                                    mediaUploadResultEx.Item = MediaManager.Creator.CreateFromStream(stream, mediaUploadResultEx.Path, mediaCreatorOptions);

                                }
                            }
                        }
                    }
                }
            }
            finally
            {
                FileUtil.Delete(filename);
            }
        }

        private void UploadToDatabase(List<MediaUploadResultEx> list)
        {
            Assert.ArgumentNotNull((object)list, nameof(list));
            if (Context.Database == null)
                Context.Database = this.Database;
            MediaUploadResultEx mediaUploadResultEx = new MediaUploadResultEx();
            list.Add(mediaUploadResultEx);
            mediaUploadResultEx.Path = FileUtil.MakePath(this.Folder, Path.GetFileName(this.File.FileName), '/');
            mediaUploadResultEx.ValidMediaPath = MediaPathManager.ProposeValidMediaPath(mediaUploadResultEx.Path);
            MediaCreatorOptions mediaCreatorOptions = new MediaCreatorOptions()
            {
                Versioned = this.Versioned,
                Language = this.Language,
                OverwriteExisting = this.Overwrite,
                Destination = mediaUploadResultEx.ValidMediaPath,
                FileBased = this.FileBased,
                AlternateText = this.AlternateText,
                Database = this.Database
            };
            mediaCreatorOptions.Build((string)GetMediaCreatorOptionsArgs.UploadContext);
            mediaUploadResultEx.Item = MediaManager.Creator.CreateFromStream(this.File.GetInputStream(), mediaUploadResultEx.Path, mediaCreatorOptions);
        }

        public new PostedFile File { get; set; }
    }
}
