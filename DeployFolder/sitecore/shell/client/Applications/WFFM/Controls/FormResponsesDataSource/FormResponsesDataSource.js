define(["sitecore", "jquery"], function(sc, $) {
  var model = sc.Definitions.Models.ControlModel.extend({
    initialize: function() {
      this._super();

      this.set("items", []);
      this.set("data", []);
      this.set("selectedSegmentData", []);
      this.getReport();
    },

    getReport: function() {
      var params = sc.Helpers.url.getQueryParameters(window.location.href);
      var id = this.get("itemId") || params.itemId || this.get("formId") || params.formId || this.get("id") || params.id;

      $.ajax({
        cache: false,
        type: 'POST',
        url: "/api/sitecore/FormReports/GetFormFieldsStatistics",
        context: this,
        data: JSON.stringify({ "id": id }),
        dataType: 'json',
        contentType: 'application/json',
        success: function(data) {
          this._buildData(data);
        }
      });
    },

    _buildData: function(data) {
      var items = JSON.parse(data.Items);

      var dataChart = {
        "dataset": [{ "data": [] }]
      };

      this.set({ "selectedSegmentData": dataChart });

      var listData = {};

      for (var i = 0; i < items.length; i++) {
        var categoryName = items[i].fieldName;
        if (categoryName.length > 9) {
          categoryName = categoryName.substring(0, 7) + "...";
        }

        dataChart.dataset[0].data.push({ "category": categoryName, "value": items[i].count, "channel": data.legend });

        if (items[i].isList) {
          listData[categoryName] = this._generateDataChartForListFields(items[i].Values);
        }
      }

      this.set({ "items": dataChart });
      this.set({ "data": listData });
    },

    _generateDataChartForListFields: function(obj) {
      var dataChart = {
        "dataset": [{ "data": [] }]
      };

      for (var i = 0; i < obj.length; i++) {
        var categoryName = obj[i].FieldValue;
        if (categoryName.length > 9) {
          categoryName = categoryName.substring(0, 7) + "...";
        }

        dataChart.dataset[0].data.push({ "category": categoryName, "value": obj[i].Count, "channel": "Values" });
      }

      return dataChart;
    }
  });

  var view = sc.Definitions.Views.ControlView.extend({
    initialize: function() {
      this._super();

      // Set initial settings
      this.model.set("itemId", this.$el.attr("data-sc-itemid") || null);
    }
  });

  sc.Factories.createComponent("FormResponsesDataSource", model, view, ".sc-FormResponsesDataSource");
});