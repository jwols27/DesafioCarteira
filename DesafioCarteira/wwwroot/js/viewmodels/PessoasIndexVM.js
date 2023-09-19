var PessoasIndex = new function PessoasIndexVM() {
    var self = this;
    self.formatNumber = function (number) {
        if (!number) return "-";
        return "R$" + number.toLocaleString('pt-BR', { minimumFractionDigits: 2, maximumFractionDigits: 2 });
    }

    self.teste = ko.observable(2);

    self.Pessoas = ko.observableArray([]);
}

ko.applyBindings(PessoasIndex, document.getElementById('pessoas-view'));