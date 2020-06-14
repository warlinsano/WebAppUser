window.addEventListener("load", function () {
    $('a:not([tabindex]):not([href*="#"]):not([dontLoad]), [doLoad]').click(showLoading);
    $("form:not([dontLoad])").submit(showLoading);
});

$(document).ready(function () {
    hideLoading();
});

$(this).keyup(function (e) {
    if (e.which == 27) {
        hideLoading();
    }
});

function showLoading() {
    $("#Loading").show();
};

function hideLoading() {
    $("#Loading").hide();
};

