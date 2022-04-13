(function($) {
  $.noConflict();

  $(document).ready(function() {
    $("form[data-wffm]").each(function() { $(this).wffmForm(); });
  });
})(jQuery);