jQuery(".selector-field-captcha-play").click(function (event) {
  event.preventDefault();
  var container = jQuery(".field-captcha-audio");
  container.empty();
  var handlerUrl = jQuery(".mscaptcha").data('audiourl')
  var embedCode = "<audio style='display:none;' autoplay><source src='" + handlerUrl + "' ></audio>";
  container.append(embedCode);
});

jQuery(".selector-field-captcha-refresh").click(function (event) {
  event.preventDefault();
  window.location.reload();
});
