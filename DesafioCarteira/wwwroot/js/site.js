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

function getToday() {
    var currentDate = new Date();
    return currentDate.toISOString().slice(0, 16);
}

function convertCommaToDot(inputString) {
    return inputString.replace(',', '.');
}