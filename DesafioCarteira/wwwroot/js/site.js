// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function toggleFadeAnimation(element, condition) {
    if (condition) {
        $(element).animate({ height: 0, opacity: 0 }, 400,
            function () {
                $(element).toggle(false);
            }
        );

    } else {
        $(element).toggle(true);
        $(element).animate({ height: $(element).get(0).scrollHeight, opacity: 1 }, 400,
            function () {
                $(this).height('auto');
            }
        );
    }
}

function getDate(date) {
    date.setMinutes(date.getMinutes() - date.getTimezoneOffset());
    return date.toISOString().slice(0, 16);
}

function convertCommaToDot(inputString) {
    return inputString.replace(',', '.');
}

function handleFormErrors(es) {
    clearFormErrors();
    for (var i = 0; i < es.length; i++) {
        $("#" + es[i].fieldName).addClass("is-invalid");
        $("#" + es[i].fieldName + "Error").text(es[i].errorMessage);
    }
}

function clearFormErrors() {
    $(".is-invalid").removeClass("is-invalid");
    $(".form-error").text('');
}