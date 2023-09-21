var MovIndex = new function MovimentacoesIndex() {
    var self = this;

    self.formatNumber = function (number) {
        if (!number) return "-";
        return "R$" + number.toLocaleString('pt-BR', { minimumFractionDigits: 2, maximumFractionDigits: 2 });
    }

    self.Movs = ko.observableArray();

    self.removerMov = function (mov) {
        deleteMovimentacao(mov.id)
            .then((res) => {
                self.Movs.remove(mov);
            })
            .catch(function (error) {
                console.error(error);
            });
    }
}

ko.applyBindings(MovIndex, document.getElementById('consulta-view'));