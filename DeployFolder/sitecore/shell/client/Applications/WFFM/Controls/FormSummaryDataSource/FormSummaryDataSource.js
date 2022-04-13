define(["sitecore", "jquery"], function(sc, $) {
  var model = sc.Definitions.Models.ControlModel.extend({
    initialize: function() {
      this._super();

      this.set({
        visits: 0,
        dropouts: 0,
        submitsCount: 0,
        success: 0,
        hasData: false,
        title: ""
      });

      this.getReport();
    },

    getReport: function() {
      var params = sc.Helpers.url.getQueryParameters(window.location.href);
      var id = this.get("itemId") || params.itemId || this.get("formId") || params.formId || this.get("id") || params.id;

      $.ajax({
        cache: false,
        type: 'POST',
        url: "/api/sitecore/FormReports/GetFormSummary",
        data: JSON.stringify({ "id": id }),
        context: this,
        dataType: 'json',
        contentType: 'application/json',
        success: function(data) {
          var dataObj = JSON.parse(data);
          this.set({ "dropouts": dataObj.dropouts });
          this.set({ "title": dataObj.title });
          this.set({ "success": dataObj.success });
          this.set({ "visits": dataObj.visits });
          this.set({ "submitsCount": dataObj.submitsCount });
          this.set({ "hasData": dataObj.submitsCount > 0 });
        }
      });
    }
  });


  var view = sc.Definitions.Views.ControlView.extend({
    initialize: function() {
      this._super();

      // Set initial settings
      this.model.set("itemId", this.$el.attr("data-sc-itemid") || null);
    }
  });

  sc.Factories.createComponent("FormSummaryDataSource", model, view, ".sc-FormSummaryDataSource");
});