$(document).ready(() => {
    $('.form-control').each((i, element) => {
        if ($(element).hasClass('input-validation-error')) {
            $(element).addClass('is-invalid');
        }
    });
});