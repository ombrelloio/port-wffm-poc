// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.Pipelines.FormUploadFile.FormUploadFileArgs
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Collections;
using Sitecore.Diagnostics;
using Sitecore.Globalization;
using Sitecore.Pipelines.Upload;
using Sitecore.Web.UI.Sheer;
using Sitecore.WFFM.Abstractions.Actions;
using System;

namespace Sitecore.Form.Core.Pipelines.FormUploadFile
{
  [Serializable]
  public class FormUploadFileArgs : ClientPipelineArgs
  {
    private SafeDictionary<string> _fileParameters;
    private PostedFile _file;
    private string _folder;
    private Language _language;
    private bool _overwrite;
    private bool _versioned;
    private bool useSecurity;
    private UploadDestination _destination;

    public FormUploadFileArgs()
    {
      this._fileParameters = new SafeDictionary<string>();
      this.useSecurity = false;
    }

    public string GetFileParameter(string key)
    {
      Assert.ArgumentNotNull((object) key, nameof (key));
      return ((SafeDictionary<string, string>) this._fileParameters)[key];
    }

    public void SetFileParameter(string key, string value)
    {
      Assert.ArgumentNotNull((object) key, nameof (key));
      Assert.ArgumentNotNull((object) value, nameof (value));
      ((SafeDictionary<string, string>) this._fileParameters)[key] = value;
    }

    public UploadDestination Destination
    {
      get => this._destination;
      set => this._destination = value;
    }

    public PostedFile File
    {
      get => this._file;
      set => this._file = value;
    }

    public string Folder
    {
      get => this._folder;
      set => this._folder = value;
    }

    public Language Language
    {
      get => this._language;
      set => this._language = value;
    }

    public bool Overwrite
    {
      get => this._overwrite;
      set => this._overwrite = value;
    }

    public bool Versioned
    {
      get => this._versioned;
      set => this._versioned = value;
    }

    public bool UseSecurity
    {
      get => this.useSecurity;
      set => this.useSecurity = value;
    }
  }
}
