var ErrorModal = new function ErrorModalVM() {
    var self = this;
    self.ModalErrorMessage = ko.observable("");
    self.ModalError = ko.observable("");

    self.ShowModal = (xhr) => {
        ErrorModal.ModalError("Erro " + xhr.status);
        ErrorModal.ModalErrorMessage(xhr.responseText);
        $('#error-modal').modal('show');
    };
}

ko.applyBindings(ErrorModal, document.getElementById('error-modal'));