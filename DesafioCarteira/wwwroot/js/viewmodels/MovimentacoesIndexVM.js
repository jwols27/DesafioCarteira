var MovIndex = new function MovimentacoesIndex() {
    var self = this;

    self.formatNumber = function (number) {
        if (!number) return "-";
        return "R$" + number.toLocaleString('pt-BR', { minimumFractionDigits: 2, maximumFractionDigits: 2 });
    }

    self.formatDate = (date) => {
        return formatDateString(date);
    }

    self.MovOption = ko.observable("A");
    self.MovDisplay = ko.pureComputed(() => {
        switch (self.MovOption()) {
            case 'E': return 'Entradas';
            case 'S': return 'Saídas';
            case 'A': return 'Ambos';
            default: return 'Não definido'
        }
    });

    self.Movs = ko.observableArray();
    self.MovStatus = (mov) => {
        switch (mov) {
            case 'E': return 'table-success';
            case 'S': return 'table-danger';
            default: return ''
        }
    };

    //TO-DO: UPDATE ID
    self.detalharMov = function (movimento) {
        const { mov, type } = movimento;
        findMovimentacao(mov.id, localStorage.getItem('pessoaId'), type)
            .then((res) => {
                window.location.href = '/Movimentacoes/Details/' + mov.id
                    + '?pessoaId=' + localStorage.getItem('pessoaId') + '&type=' + type;
            })
            .catch(function (error) {
                console.error(error);
            });
    }

    self.editarMov = function (movimento) {
        const { mov, type } = movimento;
        console.log(mov)
        try {
            window.location.href = '/Movimentacoes/Edit/' + mov.id
                + '?pessoaId=' + localStorage.getItem('pessoaId') + '&type=' + type;
        } catch (e) {}
    }

    self.removerMov = function (movimento) {
        const { mov, type } = movimento;
        deleteMovimentacao(mov.id, localStorage.getItem('pessoaId'), type)
            .then((res) => {
                self.Movs.remove(movimento);
            })
            .catch(function (error) {
                console.error(error);
            });
    }
}

ko.applyBindings(MovIndex, document.getElementById('consulta-view'));