﻿function reloadCount() {
    $.post("/subscribers/getcount", { id: id }, function (data) {
        link.html(data);
    });
}
function subscribe() {
    button.html('Подождите');
    button.on('click', function () { });
    button.addClass("disabled");
    button.attr("disabled", true);
    $.post("/subscribe", { id: id }, function (data) {
        if (data) {
            button.html('Не пойти');
            button.get()[0].onclick = unsubscribe;
        }
        else {
            location.assign('/User/Login?returnURL='+returnURL);
        }
        reloadCount();
        ReloadSubscr($('#subscribers_onhover'));
        button.removeClass("disabled");
        button.removeAttr("disabled");
    });
}
function unsubscribe() {
    button.html('Подождите');
    button.on('click', function () { });
    button.addClass("disabled");
    button.attr("disabled", true);
    $.post("/unsubscribe", { id: id }, function (data) {
        if (data) {
            button.html('Пойти');
            button.get()[0].onclick = subscribe;
        }
        else {
            location.assign('/user/login');
        }
        reloadCount();
        ReloadSubscr($('#subscribers_onhover'));
        button.removeClass("disabled");
        button.removeAttr("disabled");
    });
}
var button, id, link;
window.onload = function () {
    $("body").toggleClass("loaded");
    button = $('#submit');
    button.addClass("disabled");
    button.attr("disabled", true);
    link = $('#subscribers a');
    id = location.pathname.split('/').pop();
    ReloadSubscr($('#subscribers_onhover'));
    $('#subscribers_onhover').addClass("hover");
    $.post("/issubscribed", {id:id}, function (data) {
        if (data) {
            button.html('Не пойти');
            button.get()[0].onclick = unsubscribe;
        }
        else {
            button.html('Пойти');
            button.get()[0].onclick = subscribe;
        }
        button.removeClass("disabled");
        button.removeAttr("disabled");
    });
}

$("#subscribed").mouseover(
  function () {
      if ($("#sub_wrapper").css('display') == 'none') {
          $("#subscribers_onhover").removeClass("hover");
      }
  }).mouseout(function () {
      $("#subscribers_onhover").addClass("hover");
  }
);

$("#subscribers").on("click", function () {
    $("#subscribers_onhover").addClass("hover");
    $("#sub_wrapper").show().css({ "z-index": "1000000" }).next().addClass("overscreen");
});

$("#sub_wrapper").on('click', function (e) {
    var div = $("#sub_inner");
    if (!div.is(e.target) // если клик был не по нашему блоку
        && div.has(e.target).length === 0) { // и не по его дочерним элементам
        $("#sub_wrapper").hide().next().removeClass("overscreen");
    }
});
$("#sub_inner header span").on("click", function () {
    $("#sub_wrapper").hide().next().removeClass("overscreen");
});

