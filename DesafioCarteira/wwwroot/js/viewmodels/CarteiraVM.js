ko.bindingHandlers.fadeSlide = {
    init: function (element, valueAccessor) {
        var shouldDisplay = valueAccessor();
        $(element).toggle(!!shouldDisplay);
    },
    update: function (element, valueAccessor) {
        var shouldDisplay = valueAccessor();
        toggleFadeAnimation(element, !shouldDisplay);
    }
};


var Carteira = new function CarteiraVM() {
    var self = this;
    self.Pessoas = ko.observableArray([]);
    self.SelectedPessoa = ko.observable();
    self.PessoaNome = ko.observable("");
    self.PessoaSaldo = ko.observable(0);
    self.displayOption = (item) => {
        return item.nome + ' - (Saldo: ' + self.formatNumber(item.saldo) + ')'
    }

    self.FazerMovimento = ko.observable(false);

    self.formatNumber = function (number) {
        if (!number) return "R$0,00";
        return "R$" + number.toLocaleString('pt-BR', { minimumFractionDigits: 2, maximumFractionDigits: 2 });
    }

    self.ChecarMinimo = ko.pureComputed(() => {
        var selectedPessoa = self.SelectedPessoa();
        if (!selectedPessoa) return false;
        return self.PessoaSaldo() <= selectedPessoa.minimo;
    })

    self.updatePessoaInfo = () => {
        var selected = self.SelectedPessoa();
        if (selected) {
            self.PessoaNome(selected.nome);
            self.PessoaSaldo(selected.saldo);
        }
    }

    self.Movimento = new MovimentoVM();
}

ko.applyBindings(Carteira, document.getElementById('carteira-view'));