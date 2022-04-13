define(["jquery", "sitecore"], function($, sc) {
  "use strict";

  var model = sc.Definitions.Models.ComponentModel.extend({
    initialize: function() {
      this._super();

      this.set("itemId", null);
      this.set("items", []);

      this.set("isBusy", false);
      this.set("pageSize", 20);
      this.set("pageIndex", 0);
      this.set("sorting", "");

      this.isReady = false;
      this.lastPage = 0;
      this.pendingRequests = 0;

      this.on("change:itemId change:pageSize change:pageIndex change:sorting", this.refresh, this);
    },

    refresh: function() {
      this.set("pageIndex", 0);
      this.lastPage = 0;
      this.getFormData(this.lastPage);
    },

    next: function() {
      this.lastPage++;
      this.getFormData(this.lastPage);
    },

    getFormData: function(pageIndex) {
      if (!this.isReady) {
        return;
      }

      var params = sc.Helpers.url.getQueryParameters(window.location.href);
      var formId = this.get("itemId") || params.itemId || this.get("formId") || params.formId || this.get("id") || params.id;

      this.pendingRequests++;
      this.set("isBusy", true);

      var pageCriteria = {
        'PageIndex': pageIndex,
        'PageSize': this.get("pageSize"),
        'Sorting': this.get("sorting")
      };

      $.ajax({
        cache: false,
        type: 'POST',
        url: "/api/sitecore/FormReports/GetFormContactsPage",
        data: JSON.stringify({ "id": formId, pageCriteria: pageCriteria }),
        dataType: 'json',
        context: this,
        contentType: 'application/json',
        success: function(data) {
          this.set("isBusy", false);
          this.set({ "items": JSON.parse(data.Items) });
          this.set({ "hasResults": JSON.parse(data.HasResults) });

          if (this.pendingRequests <= 0) {
            this.set("isBusy", false);
            this.pendingRequests = 0;
          }
        },
        error: function() {
          this.set("items", null);
          this.pendingRequests--;
          if (this.pendingRequests <= 0) {
            this.set("isBusy", false);
            this.pendingRequests = 0;
          }
        }
      });
    }
  });

  var view = sc.Definitions.Views.ComponentView.extend({
    listen: window._.extend({}, sc.Definitions.Views.ComponentView.prototype.listen, {
      "refresh:$this": "refresh",
      "next:$this": "next"
    }),

    initialize: function() {
      this._super();
      this.model.set("itemId", this.$el.attr("data-sc-itemsid"));
      this.model.set("pageIndex", parseInt(this.$el.attr("data-sc-pageindex"), 10) || 0);
      this.model.set("pageSize", parseInt(this.$el.attr("data-sc-pagesize"), 20) || 20);
      this.model.set("sorting", this.$el.attr("data-sc-sorting"));
    },

    beforeRender: function() {
      this.model.isReady = true;
      this.refresh();
    },

    refresh: function() {
      this.model.refresh();
    },

    next: function() {
      this.model.next();
    }
  });

  sc.Factories.createComponent("FormReportDataSource", model, view, ".sc-FormReportDataSource");
});