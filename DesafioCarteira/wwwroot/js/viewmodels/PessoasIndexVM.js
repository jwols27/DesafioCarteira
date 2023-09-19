var PessoasIndex = new function PessoasIndexVM() {
    var self = this;
    self.formatNumber = function (number) {
        if (!number) return "-";
        return "R$" + number.toLocaleString('pt-BR', { minimumFractionDigits: 2, maximumFractionDigits: 2 });
    }

    self.Pessoas = ko.observableArray([]);

    self.removerPessoa = function (pessoa) {
        deletePessoa(pessoa.id)
            .then((res) => {
                if (res.success) {
                    self.Pessoas.remove(pessoa);
                }
            })
            .catch(function (error) {
                console.error(error);
            });
    }
}

ko.applyBindings(PessoasIndex, document.getElementById('pessoas-view'));