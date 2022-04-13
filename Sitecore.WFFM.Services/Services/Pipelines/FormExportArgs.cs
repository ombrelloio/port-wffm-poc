// Decompiled with JetBrains decompiler
// Type: Sitecore.WFFM.Services.Pipelines.FormExportArgs
// Assembly: Sitecore.WFFM.Services, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A002B65D-6063-457D-AAC3-54DF69ADFBA9
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.WFFM.Services.dll

using Sitecore.Text;
using Sitecore.WFFM.Abstractions.Data;
using Sitecore.WFFM.Abstractions.Pipeline;
using Sitecore.WFFM.Speak.ViewModel;
using System;

namespace Sitecore.WFFM.Services.Pipelines
{
  [Serializable]
  public class FormExportArgs : ExportArgs
  {
    public FormExportArgs(IFormItem item, FormPacket packet, string fileName, string contentType)
      : base(string.Empty, contentType, fileName)
    {
      this.Item = item;
      this.Packet = packet;
    }

    public IFormItem Item { get; private set; }

    public FormPacket Packet { get; private set; }

    public ListString RestrictionIds { get; set; }
  }
}
