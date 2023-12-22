var GAME_QUANTITY_IN_STOCK = 0;

$(document).ready(() => {
  const qtyInStock = parseInt($("#disc-stock").val());
  GAME_QUANTITY_IN_STOCK = qtyInStock;
  $(".quantity-left-alert-text").text(`${qtyInStock} left in stock`);
  if (qtyInStock < 10) {
    $(".quantity-left-alert").addClass("show");
  } else {
    $(".quantity-left-alert").removeClass("show");
  }
  if (qtyInStock == 0) {
    $(".quantity-left-alert").addClass("show");
    $(".quantity-left-alert-text").text("Out of stock");
    $("#gametype-disc").prop("disabled", true);
  } else {
    $("#gametype-disc").prop("disabled", false);
  }
});

$(".about-table [name=game-type]").click(function () {
  const type = $(this).val();
  if (type == 1) {
    // disc
    $(".about-table .game-quantity").addClass("show");
  } else {
    $(".about-table .game-quantity").removeClass("show");
    $(".about-table .game-quantity input").val("1");
  }
});

$(".about-table [game-action*='change-quantity']").click(function () {
  const q = $(".about-table .game-quantity input");
  const current = parseInt(q.val());
  if (isNaN(current)) {
    q.val(1);
    return;
  }
  if ($(this).attr("game-action") == "change-quantity-minus") {
    if (current > 1) {
      q.val(current - 1);
    } else {
      notyf.error("Quantity must be at least 1");
    }
  }
  if ($(this).attr("game-action") == "change-quantity-plus") {
    if (current + 1 > GAME_QUANTITY_IN_STOCK) {
      notyf.error("Out of stock");
      return;
    }
    q.val(current + 1);
  }
});

$(".about-table .game-quantity input").on("input", function () {
  const current = parseInt($(this).val());
  if (isNaN(current)) {
    $(this).val(1);
    return;
  }
  if (current < 1) {
    $(this).val(1);
    notyf.error("Quantity must be at least 1");
    return;
  }
  if (current > GAME_QUANTITY_IN_STOCK) {
    notyf.error("Out of stock");
    $(this).val(GAME_QUANTITY_IN_STOCK);
    return;
  }
});
