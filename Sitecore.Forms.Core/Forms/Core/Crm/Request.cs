﻿// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Core.Crm.Request
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
  [XmlInclude(typeof (WinQuoteRequest))]
  [XmlInclude(typeof (WinOpportunityRequest))]
  [XmlInclude(typeof (WhoAmIRequest))]
  [XmlInclude(typeof (ValidateSavedQueryRequest))]
  [XmlInclude(typeof (ValidateRequest))]
  [XmlInclude(typeof (UtcTimeFromLocalTimeRequest))]
  [XmlInclude(typeof (UpdateUserSettingsSystemUserRequest))]
  [XmlInclude(typeof (UpdateRequest))]
  [XmlInclude(typeof (UnregisterSolutionRequest))]
  [XmlInclude(typeof (UnpublishDuplicateRuleRequest))]
  [XmlInclude(typeof (UnlockSalesOrderPricingRequest))]
  [XmlInclude(typeof (UnlockInvoicePricingRequest))]
  [XmlInclude(typeof (TransformImportRequest))]
  [XmlInclude(typeof (StatusUpdateBulkOperationRequest))]
  [XmlInclude(typeof (SetStateSystemUserRequest))]
  [XmlInclude(typeof (SetStateDynamicEntityRequest))]
  [XmlInclude(typeof (SetReportRelatedRequest))]
  [XmlInclude(typeof (SetRelatedRequest))]
  [XmlInclude(typeof (SetParentTeamRequest))]
  [XmlInclude(typeof (SetParentSystemUserRequest))]
  [XmlInclude(typeof (SetParentBusinessUnitRequest))]
  [XmlInclude(typeof (SetLocLabelsRequest))]
  [XmlInclude(typeof (SetBusinessSystemUserRequest))]
  [XmlInclude(typeof (SetBusinessEquipmentRequest))]
  [XmlInclude(typeof (SendTemplateRequest))]
  [XmlInclude(typeof (SendFaxRequest))]
  [XmlInclude(typeof (SendEmailRequest))]
  [XmlInclude(typeof (SendBulkMailRequest))]
  [XmlInclude(typeof (SearchRequest))]
  [XmlInclude(typeof (SearchByTitleKbArticleRequest))]
  [XmlInclude(typeof (SearchByKeywordsKbArticleRequest))]
  [XmlInclude(typeof (SearchByBodyKbArticleRequest))]
  [XmlInclude(typeof (RouteRequest))]
  [XmlInclude(typeof (RollupRequest))]
  [XmlInclude(typeof (RevokeAccessRequest))]
  [XmlInclude(typeof (ReviseQuoteRequest))]
  [XmlInclude(typeof (RetrieveVersionRequest))]
  [XmlInclude(typeof (RetrieveUserSettingsSystemUserRequest))]
  [XmlInclude(typeof (RetrieveUserPrivilegesRequest))]
  [XmlInclude(typeof (RetrieveTeamsSystemUserRequest))]
  [XmlInclude(typeof (RetrieveSubsidiaryUsersBusinessUnitRequest))]
  [XmlInclude(typeof (RetrieveSubsidiaryTeamsBusinessUnitRequest))]
  [XmlInclude(typeof (RetrieveSubGroupsResourceGroupRequest))]
  [XmlInclude(typeof (RetrieveSharedPrincipalsAndAccessRequest))]
  [XmlInclude(typeof (RetrieveRolePrivilegesRoleRequest))]
  [XmlInclude(typeof (RetrieveRequest))]
  [XmlInclude(typeof (RetrieveProvisionedLanguagesRequest))]
  [XmlInclude(typeof (RetrievePrivilegeSetRequest))]
  [XmlInclude(typeof (RetrievePrincipalAccessRequest))]
  [XmlInclude(typeof (RetrieveParsedDataImportFileRequest))]
  [XmlInclude(typeof (RetrieveParentGroupsResourceGroupRequest))]
  [XmlInclude(typeof (RetrieveMultipleRequest))]
  [XmlInclude(typeof (RetrieveMembersTeamRequest))]
  [XmlInclude(typeof (RetrieveMembersBulkOperationRequest))]
  [XmlInclude(typeof (RetrieveLocLabelsRequest))]
  [XmlInclude(typeof (RetrieveLicenseInfoRequest))]
  [XmlInclude(typeof (RetrieveInstalledLanguagePacksRequest))]
  [XmlInclude(typeof (RetrieveFormXmlRequest))]
  [XmlInclude(typeof (RetrieveExchangeRateRequest))]
  [XmlInclude(typeof (RetrieveDuplicatesRequest))]
  [XmlInclude(typeof (RetrieveDeprovisionedLanguagesRequest))]
  [XmlInclude(typeof (RetrieveDeploymentLicenseTypeRequest))]
  [XmlInclude(typeof (RetrieveByTopIncidentSubjectKbArticleRequest))]
  [XmlInclude(typeof (RetrieveByTopIncidentProductKbArticleRequest))]
  [XmlInclude(typeof (RetrieveByResourcesServiceRequest))]
  [XmlInclude(typeof (RetrieveByResourceResourceGroupRequest))]
  [XmlInclude(typeof (RetrieveByGroupResourceRequest))]
  [XmlInclude(typeof (RetrieveBusinessHierarchyBusinessUnitRequest))]
  [XmlInclude(typeof (RetrieveAvailableLanguagesRequest))]
  [XmlInclude(typeof (RetrieveAllChildUsersSystemUserRequest))]
  [XmlInclude(typeof (ResetDataFiltersRequest))]
  [XmlInclude(typeof (RescheduleRequest))]
  [XmlInclude(typeof (ReplacePrivilegesRoleRequest))]
  [XmlInclude(typeof (RenewContractRequest))]
  [XmlInclude(typeof (RemoveUserRolesRoleRequest))]
  [XmlInclude(typeof (RemoveSubstituteProductRequest))]
  [XmlInclude(typeof (RemoveRelatedRequest))]
  [XmlInclude(typeof (RemoveProductFromKitRequest))]
  [XmlInclude(typeof (RemovePrivilegeRoleRequest))]
  [XmlInclude(typeof (RemoveParentRequest))]
  [XmlInclude(typeof (RemoveMembersTeamRequest))]
  [XmlInclude(typeof (RemoveMemberListRequest))]
  [XmlInclude(typeof (RemoveItemCampaignRequest))]
  [XmlInclude(typeof (RemoveItemCampaignActivityRequest))]
  [XmlInclude(typeof (RegisterSolutionRequest))]
  [XmlInclude(typeof (ReassignObjectsSystemUserRequest))]
  [XmlInclude(typeof (QueryScheduleRequest))]
  [XmlInclude(typeof (QueryMultipleSchedulesRequest))]
  [XmlInclude(typeof (QueryExpressionToFetchXmlRequest))]
  [XmlInclude(typeof (QualifyMemberListRequest))]
  [XmlInclude(typeof (PublishXmlRequest))]
  [XmlInclude(typeof (PublishDuplicateRuleRequest))]
  [XmlInclude(typeof (PublishAllXmlRequest))]
  [XmlInclude(typeof (PropagateByExpressionRequest))]
  [XmlInclude(typeof (ProcessOneMemberBulkOperationRequest))]
  [XmlInclude(typeof (ProcessInboundEmailRequest))]
  [XmlInclude(typeof (ParseImportRequest))]
  [XmlInclude(typeof (ModifyAccessRequest))]
  [XmlInclude(typeof (MergeRequest))]
  [XmlInclude(typeof (MakeUnavailableToOrganizationTemplateRequest))]
  [XmlInclude(typeof (MakeUnavailableToOrganizationReportRequest))]
  [XmlInclude(typeof (MakeAvailableToOrganizationTemplateRequest))]
  [XmlInclude(typeof (MakeAvailableToOrganizationReportRequest))]
  [XmlInclude(typeof (LoseOpportunityRequest))]
  [XmlInclude(typeof (LogSuccessBulkOperationRequest))]
  [XmlInclude(typeof (LogFailureBulkOperationRequest))]
  [XmlInclude(typeof (LockSalesOrderPricingRequest))]
  [XmlInclude(typeof (LockInvoicePricingRequest))]
  [XmlInclude(typeof (LocalTimeFromUtcTimeRequest))]
  [XmlInclude(typeof (IsValidStateTransitionRequest))]
  [XmlInclude(typeof (IsBackOfficeInstalledRequest))]
  [XmlInclude(typeof (InstantiateTemplateRequest))]
  [XmlInclude(typeof (InitializeFromRequest))]
  [XmlInclude(typeof (ImportXmlWithProgressRequest))]
  [XmlInclude(typeof (ImportXmlRequest))]
  [XmlInclude(typeof (ImportTranslationsXmlWithProgressRequest))]
  [XmlInclude(typeof (ImportRecordsImportRequest))]
  [XmlInclude(typeof (ImportCompressedXmlWithProgressRequest))]
  [XmlInclude(typeof (ImportCompressedTranslationsXmlWithProgressRequest))]
  [XmlInclude(typeof (ImportCompressedAllXmlRequest))]
  [XmlInclude(typeof (ImportAllXmlRequest))]
  [XmlInclude(typeof (HandleRequest))]
  [XmlInclude(typeof (GrantAccessRequest))]
  [XmlInclude(typeof (GetTrackingTokenEmailRequest))]
  [XmlInclude(typeof (GetTimeZoneCodeByLocalizedNameRequest))]
  [XmlInclude(typeof (GetSalesOrderProductsFromOpportunityRequest))]
  [XmlInclude(typeof (GetReportHistoryLimitRequest))]
  [XmlInclude(typeof (GetQuoteProductsFromOpportunityRequest))]
  [XmlInclude(typeof (GetQuantityDecimalRequest))]
  [XmlInclude(typeof (GetInvoiceProductsFromOpportunityRequest))]
  [XmlInclude(typeof (GetHeaderColumnsImportFileRequest))]
  [XmlInclude(typeof (GetDistinctValuesImportFileRequest))]
  [XmlInclude(typeof (GetDecryptionKeyRequest))]
  [XmlInclude(typeof (GetAllTimeZonesWithDisplayNameRequest))]
  [XmlInclude(typeof (GenerateSalesOrderFromOpportunityRequest))]
  [XmlInclude(typeof (GenerateQuoteFromOpportunityRequest))]
  [XmlInclude(typeof (GenerateInvoiceFromOpportunityRequest))]
  [XmlInclude(typeof (FulfillSalesOrderRequest))]
  [XmlInclude(typeof (FindParentResourceGroupRequest))]
  [XmlInclude(typeof (FetchXmlToQueryExpressionRequest))]
  [XmlInclude(typeof (ExportXmlRequest))]
  [XmlInclude(typeof (ExportTranslationsXmlRequest))]
  [XmlInclude(typeof (ExportCompressedXmlRequest))]
  [XmlInclude(typeof (ExportCompressedTranslationsXmlRequest))]
  [XmlInclude(typeof (ExportCompressedAllXmlRequest))]
  [XmlInclude(typeof (ExportAllXmlRequest))]
  [XmlInclude(typeof (ExpandCalendarRequest))]
  [XmlInclude(typeof (ExecuteWorkflowRequest))]
  [XmlInclude(typeof (ExecuteFetchRequest))]
  [XmlInclude(typeof (ExecuteCampaignActivityRequest))]
  [XmlInclude(typeof (ExecuteByIdUserQueryRequest))]
  [XmlInclude(typeof (ExecuteByIdSavedQueryRequest))]
  [XmlInclude(typeof (DownloadReportDefinitionRequest))]
  [XmlInclude(typeof (DistributeCampaignActivityRequest))]
  [XmlInclude(typeof (DisassociateEntitiesRequest))]
  [XmlInclude(typeof (DetachFromQueueEmailRequest))]
  [XmlInclude(typeof (DeliverPromoteEmailRequest))]
  [XmlInclude(typeof (DeliverIncomingEmailRequest))]
  [XmlInclude(typeof (DeleteRequest))]
  [XmlInclude(typeof (CreateWorkflowFromTemplateRequest))]
  [XmlInclude(typeof (CreateRequest))]
  [XmlInclude(typeof (CreateActivitiesListRequest))]
  [XmlInclude(typeof (CopyMembersListRequest))]
  [XmlInclude(typeof (CopyCampaignRequest))]
  [XmlInclude(typeof (ConvertSalesOrderToInvoiceRequest))]
  [XmlInclude(typeof (ConvertQuoteToSalesOrderRequest))]
  [XmlInclude(typeof (ConvertProductToKitRequest))]
  [XmlInclude(typeof (ConvertKitToProductRequest))]
  [XmlInclude(typeof (CompoundUpdateRequest))]
  [XmlInclude(typeof (CompoundUpdateDuplicateDetectionRuleRequest))]
  [XmlInclude(typeof (CompoundCreateRequest))]
  [XmlInclude(typeof (CloseQuoteRequest))]
  [XmlInclude(typeof (CloseIncidentRequest))]
  [XmlInclude(typeof (CloneContractRequest))]
  [XmlInclude(typeof (CleanUpBulkOperationRequest))]
  [XmlInclude(typeof (CheckPromoteEmailRequest))]
  [XmlInclude(typeof (CheckIncomingEmailRequest))]
  [XmlInclude(typeof (CancelSalesOrderRequest))]
  [XmlInclude(typeof (CancelContractRequest))]
  [XmlInclude(typeof (CalculateTotalTimeIncidentRequest))]
  [XmlInclude(typeof (CalculateActualValueOpportunityRequest))]
  [XmlInclude(typeof (BulkOperationStatusCloseRequest))]
  [XmlInclude(typeof (BulkDetectDuplicatesRequest))]
  [XmlInclude(typeof (BulkDeleteRequest))]
  [XmlInclude(typeof (BookRequest))]
  [XmlInclude(typeof (BackgroundSendEmailRequest))]
  [XmlInclude(typeof (AutoMapEntityRequest))]
  [XmlInclude(typeof (AssociateEntitiesRequest))]
  [XmlInclude(typeof (AssignUserRolesRoleRequest))]
  [XmlInclude(typeof (AssignRequest))]
  [XmlInclude(typeof (AddSubstituteProductRequest))]
  [XmlInclude(typeof (AddProductToKitRequest))]
  [XmlInclude(typeof (AddPrivilegesRoleRequest))]
  [XmlInclude(typeof (AddMembersTeamRequest))]
  [XmlInclude(typeof (AddMemberListRequest))]
  [XmlInclude(typeof (AddItemCampaignRequest))]
  [XmlInclude(typeof (AddItemCampaignActivityRequest))]
  [GeneratedCode("wsdl", "2.0.50727.3038")]
  [DebuggerStepThrough]
  [DesignerCategory("code")]
  [XmlType(Namespace = "http://schemas.microsoft.com/crm/2007/WebServices")]
  [DebuggerNonUserCode]
  [Serializable]
  public abstract class Request
  {
    private OptionalParameter[] optionalParametersField;

    public OptionalParameter[] OptionalParameters
    {
      get => this.optionalParametersField;
      set => this.optionalParametersField = value;
    }
  }
}