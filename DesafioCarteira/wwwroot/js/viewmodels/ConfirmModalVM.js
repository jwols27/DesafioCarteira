var ConfirmModal = new function ConfirmModalVM() {
    var self = this;
    self.ModalAction = ko.observable("");
    self.ModalButton = ko.observable("");
    self.ModalTitle = ko.observable();

    self.ShowConfirmModal = (content) => {
        self.ModalTitle(content.title);
        self.ModalAction(content.text);
        self.ModalButton(content.button);
        $('#confirm-modal').modal('show');
    };
}

ko.applyBindings(ConfirmModal, document.getElementById('confirm-modal'));