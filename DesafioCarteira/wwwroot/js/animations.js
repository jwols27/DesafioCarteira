ko.bindingHandlers.fadeSlide = {
    isFirstUpdate: true,

    init: function (element, valueAccessor) {
        let params = ko.unwrap(valueAccessor());
        let shouldDisplay = ko.unwrap(params.cond);
        $(element).toggle(!!shouldDisplay);
    },
    update: function (element, valueAccessor) {
        let params = ko.unwrap(valueAccessor());
        let shouldDisplay = ko.unwrap(params.cond);
        let noFirst = ko.unwrap(params.noFirst);

        if (ko.bindingHandlers.fadeSlide.isFirstUpdate && noFirst) {
            ko.bindingHandlers.fadeSlide.isFirstUpdate = false;
        } else {
            toggleFadeAnimation(element, shouldDisplay);
        }
    }
};