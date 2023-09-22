
function toggleFadeAnimation(element, condition) {
    if (condition) {
        $(element).toggle(true);
        $(element).animate({ height: $(element).get(0).scrollHeight, opacity: 1 }, 400,
            function () {
                $(this).height('auto');
            }
        );
    } else {
        $(element).animate({ height: 0, opacity: 0 }, 400,
            function () {
                 $(element).toggle(false);
            }
        );
    }
}

function getDate(date) {
    date.setMinutes(date.getMinutes() - date.getTimezoneOffset());
    return date.toISOString().slice(0, 16);
}

function formatDateString(dateString) {
    const options = { year: 'numeric', month: '2-digit', day: '2-digit', hour: '2-digit', minute: '2-digit', second: '2-digit' };
    const formattedDate = new Date(dateString).toLocaleString('pt-BR', options);
    return formattedDate.replace(',', ' às');
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