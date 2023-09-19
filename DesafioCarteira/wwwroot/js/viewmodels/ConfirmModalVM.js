var ConfirmModal = new function ConfirmModalVM() {
    var self = this;
    self.ModalAction = ko.observable("");
    self.ModalButton = ko.observable("");
    self.ModalTitle = ko.observable();
}

ko.applyBindings(ConfirmModal, document.getElementById('confirm-modal'));