function MovimentacaoVM() {
    var self = this;
    self.Data = ko.observable(getDate(new Date()));
    self.Descricao = ko.observable("");
    self.Valor = ko.observable("");

    self.resetFields = () => {
        self.Data(getDate(new Date()));
        self.Descricao("");
        self.Valor("");
    }
}