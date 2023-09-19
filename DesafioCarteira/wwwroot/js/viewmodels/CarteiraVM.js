var Carteira = new function CarteiraVM() {
    var self = this;
    self.Pessoas = ko.observableArray([]);
    self.SelectedPessoa = ko.observable();
    self.PessoaNome = ko.observable("");
    self.PessoaSaldo = ko.observable(0);

    self.formatNumber = function (number) {
        if (!number) return "R$0,00";
        return "R$" + number.toLocaleString('pt-BR', { minimumFractionDigits: 2, maximumFractionDigits: 2 });
    }

    self.PessoaSalario = ko.pureComputed(() => {
        var selectedPessoa = self.SelectedPessoa();
        if (!selectedPessoa) return "R$0,00";
        return self.formatNumber(selectedPessoa.salario);
    })

    self.ChecarMinimo = ko.pureComputed(() => {
        var selectedPessoa = self.SelectedPessoa();
        if (!selectedPessoa) return false;
        return selectedPessoa.saldo <= selectedPessoa.minimo;
    })

    self.updatePessoaInfo = () => {
        var selected = self.SelectedPessoa();
        if (selected) {
            self.PessoaNome(selected.nome);
            self.PessoaSaldo(selected.saldo);
        }
        else {
            self.PessoaNome("");
            self.PessoaSaldo(0);
        }
    }
}

ko.applyBindings(Carteira, document.getElementById('carteira-view'));