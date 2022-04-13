define(["sitecore", "jquery"], function(sc, $) {
  var self;

  return sc.Definitions.App.extend({
    initialized: function() {
      self = this;

      this.FormFieldsBarChart.on("segmentSelected", selectSegment);
    },

    exportToExcel: function() {
      exportData(this, 1);
    },

    exportToXml: function() {
      exportData(this, 0);
    },
  });

  function exportData(app, format) {
    var params = sc.Helpers.url.getQueryParameters(window.location.href);
    var id = app.get("itemId") || params.itemId || app.get("formId") || params.formId || app.get("id") || params.id;

    $.ajax({
      type: 'POST',
      url: "/api/sitecore/ExportFormData/Export?id=" + id + "&format=" + format,
      data: JSON.stringify({ "id": id }),
      contentType: 'application/json',
      dataType: 'json',
      success: function(returnValue) {
        window.location = "/api/sitecore/ExportFormData/Download?file=" + returnValue.File + "&format=" + format;
      }
    });
  }

  function selectSegment(obj) {
    self.trigger("close:DetailedFieldsReportSmartPanel");
    var dataSourceItems = self.FormResponsesDataSource.attributes.data;

    var listDetails = dataSourceItems[obj.id];
    if (listDetails == null) {
      return;
    }

    self.FormResponsesDataSource.set("selectedSegmentData", listDetails);

    self.trigger("open:DetailedFieldsReportSmartPanel");
  }
});