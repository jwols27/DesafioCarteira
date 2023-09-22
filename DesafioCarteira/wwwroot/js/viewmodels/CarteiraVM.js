
var Carteira = new function CarteiraVM() {
    var self = this;
    self.Pessoas = ko.observableArray([]);
    self.SelectedPessoa = ko.observable(
        JSON.parse(localStorage.getItem('pessoa')) || undefined
    );
    self.PessoaNome = ko.observable(
        JSON.parse(localStorage.getItem('pessoa'))?.nome || ""
    );
    self.PessoaSaldo = ko.observable("");
    self.displayOption = (item) => {
        return item.nome + ' - (Saldo: ' + self.formatNumber(item.saldo) + ')'
    }

    self.FazerMovimentacao = ko.observable(false);

    self.formatNumber = function (number) {
        if (!number) return "R$0,00";
        return "R$" + number.toLocaleString('pt-BR', { minimumFractionDigits: 2, maximumFractionDigits: 2 });
    }

    self.ChecarMinimo = ko.pureComputed(() => {
        let selectedPessoa = self.SelectedPessoa();
        if (self.PessoaSaldo() === "") return false;
        if (!selectedPessoa) return false;
        return self.PessoaSaldo() <= selectedPessoa.minimo;
    })

    self.getLoggedIn = () => {
        let pessoa;
        try {
            pessoa = JSON.parse(localStorage.getItem('pessoaId'));
        } catch (e) {}
            
        if (!pessoa) return;
        findPessoa(pessoa).then(({ pessoa }) => {
            localStorage.setItem('pessoaId', pessoa?.id)
            localStorage.setItem('pessoa', JSON.stringify(pessoa))
            self.SelectedPessoa(pessoa);
            self.updatePessoaInfo();
        })
    }

    self.updatePessoaInfo = () => {
        let selected = self.SelectedPessoa();
        if (selected) {
            self.PessoaNome(selected.nome);
            self.PessoaSaldo(selected.saldo);
        }
        // TO-DO: REMOVE
        localStorage.setItem('pessoaId', selected?.id || undefined)
        localStorage.setItem('pessoa', JSON.stringify(selected) || undefined)
    }

    self.Movimentacao = new MovimentacaoVM();
}

ko.applyBindings(Carteira, document.getElementById('carteira-view'));