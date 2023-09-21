function MovimentoVM() {
    var self = this;
    self.Data = ko.observable(getDate(new Date()));
    self.Descricao = ko.observable("");
    self.Valor = ko.observable("");
}