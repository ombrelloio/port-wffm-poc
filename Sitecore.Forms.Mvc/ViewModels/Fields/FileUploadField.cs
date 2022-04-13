// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Mvc.ViewModels.Fields.FileUploadField
// Assembly: Sitecore.Forms.Mvc, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1AD2DE3-BABE-4597-B643-AA577F98B8D6
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Mvc.dll

using Sitecore.WFFM.Abstractions.Actions;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Web;

namespace Sitecore.Forms.Mvc.ViewModels.Fields
{
  public class FileUploadField : ValuedFieldViewModel<HttpPostedFileBase>
  {
    public FileUploadField()
    {
      if (!string.IsNullOrEmpty(this.UploadTo))
        return;
      this.UploadTo = "/sitecore/media library";
    }

    [DefaultValue("/sitecore/media library")]
    public string UploadTo { get; set; }

    [DataType(DataType.Upload)]
    public override HttpPostedFileBase Value { get; set; }

    public override string ResultParameters => "medialink";

    public override ControlResult GetResult()
    {
      HttpPostedFileBase httpPostedFileBase = this.Value;
      if (httpPostedFileBase == null)
        return new ControlResult(this.FieldItemId, this.Title, (object) null, this.ResultParameters);
      MemoryStream memoryStream = new MemoryStream();
      httpPostedFileBase.InputStream.CopyTo((Stream) memoryStream);
      return new ControlResult(this.FieldItemId, this.Title, (object) new PostedFile(memoryStream.ToArray(), httpPostedFileBase.FileName, this.UploadTo), this.ResultParameters);
    }

    public override void SetValueFromQuery(string valueFromQuery)
    {
    }
  }
}
