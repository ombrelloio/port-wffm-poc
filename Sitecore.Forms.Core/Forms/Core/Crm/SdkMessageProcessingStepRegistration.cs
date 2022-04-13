// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Core.Crm.SdkMessageProcessingStepRegistration
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Sitecore.Forms.Core.Crm
{
  [GeneratedCode("wsdl", "2.0.50727.3038")]
  [DebuggerStepThrough]
  [DesignerCategory("code")]
  [XmlType(Namespace = "http://schemas.microsoft.com/crm/2007/WebServices")]
  [DebuggerNonUserCode]
  [Serializable]
  public class SdkMessageProcessingStepRegistration
  {
    private string messageNameField;
    private string primaryEntityNameField;
    private string secondaryEntityNameField;
    private string descriptionField;
    private int stageField;
    private int modeField;
    private Guid impersonatingUserIdField;
    private int supportedDeploymentField;
    private string filteringAttributesField;
    private string pluginTypeFriendlyNameField;
    private string pluginTypeNameField;
    private string customConfigurationField;
    private int invocationSourceField;
    private SdkMessageProcessingStepImageRegistration[] imagesField;

    public string MessageName
    {
      get => this.messageNameField;
      set => this.messageNameField = value;
    }

    public string PrimaryEntityName
    {
      get => this.primaryEntityNameField;
      set => this.primaryEntityNameField = value;
    }

    public string SecondaryEntityName
    {
      get => this.secondaryEntityNameField;
      set => this.secondaryEntityNameField = value;
    }

    public string Description
    {
      get => this.descriptionField;
      set => this.descriptionField = value;
    }

    public int Stage
    {
      get => this.stageField;
      set => this.stageField = value;
    }

    public int Mode
    {
      get => this.modeField;
      set => this.modeField = value;
    }

    public Guid ImpersonatingUserId
    {
      get => this.impersonatingUserIdField;
      set => this.impersonatingUserIdField = value;
    }

    public int SupportedDeployment
    {
      get => this.supportedDeploymentField;
      set => this.supportedDeploymentField = value;
    }

    public string FilteringAttributes
    {
      get => this.filteringAttributesField;
      set => this.filteringAttributesField = value;
    }

    public string PluginTypeFriendlyName
    {
      get => this.pluginTypeFriendlyNameField;
      set => this.pluginTypeFriendlyNameField = value;
    }

    public string PluginTypeName
    {
      get => this.pluginTypeNameField;
      set => this.pluginTypeNameField = value;
    }

    public string CustomConfiguration
    {
      get => this.customConfigurationField;
      set => this.customConfigurationField = value;
    }

    public int InvocationSource
    {
      get => this.invocationSourceField;
      set => this.invocationSourceField = value;
    }

    public SdkMessageProcessingStepImageRegistration[] Images
    {
      get => this.imagesField;
      set => this.imagesField = value;
    }
  }
}
