ko.bindingHandlers.fadeSlide = {
    isFirstUpdate: true,

    init: function (element, valueAccessor) {
        let shouldDisplay = ko.unwrap(valueAccessor());
        $(element).toggle(!!shouldDisplay);
    },
    update: function (element, valueAccessor) {
        let shouldDisplay = ko.unwrap(valueAccessor());
        if (ko.bindingHandlers.fadeSlide.isFirstUpdate) {
            ko.bindingHandlers.fadeSlide.isFirstUpdate = false;
        } else {
            toggleFadeAnimation(element, shouldDisplay);
        }
    }
};