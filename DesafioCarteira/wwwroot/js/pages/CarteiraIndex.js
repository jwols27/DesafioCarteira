function createMovimento() {
    var errors = [];
    var movimento = Carteira.Movimento;

    var Pessoa = Carteira.SelectedPessoa();

    var Data = new Date(movimento.Data());
    if (isNaN(Data))
        errors.push({ fieldName: 'Data', errorMessage: 'Data inválida' });

    var Descricao = movimento.Descricao();
    if (!Descricao)
        errors.push({ fieldName: 'Descricao', errorMessage: 'Descrição é obrigatória' });
    else if (Descricao.length < 3 || Descricao.length > 50)
        errors.push({ fieldName: 'Descricao', errorMessage: 'Descrição deve ter no mínimo 3 e até 50 caracteres' });

    
    var Valor = parseFloat(convertCommaToDot(movimento.Valor()));
    if (isNaN(Valor))
        errors.push({ fieldName: 'Valor', errorMessage: 'Valor não é um número' });
    else if (!Valor)
        errors.push({ fieldName: 'Valor', errorMessage: 'Valor é obrigatório' });

    if (errors.length > 0) {
        throw errors; // Throwing an array of errors
    }

    return {
        Pessoa,
        Data,
        Descricao,
        Valor
    }
}

$(document).ready(function () {
    ConfirmModal.ModalAction("deslogar");
    ConfirmModal.ModalButton("btn-deslogar");
    ConfirmModal.ModalTitle("Deslogar");

    $("#btn-deslogar").click(() => {
        Carteira.SelectedPessoa(null);
        Carteira.updatePessoaInfo();
    })

    getPessoas()
        .then((data) => {
            Carteira.Pessoas(data.pessoas);
        })
        .catch((error) => {
            console.error(error);
        });

    $("#movimento-form").submit((event) => {
        event.preventDefault();
    })

    $("#btn-entrada").click(() => {
        try {
            var entrada = createMovimento();
            postMovimento(entrada, 'Entrada')
                .then((data) => {
                    findPessoa(data.pessoaId)
                        .then((data) => {
                            console.log(data.pessoa);
                            Carteira.SelectedPessoa().saldo = data.pessoa.saldo;
                            Carteira.updatePessoaInfo();
                        })
                        .catch((error) => {
                            console.log(error);
                        })
                })
                .catch((error) => {
                    console.log(error);
                })
            clearFormErrors();
        } catch (es) {
            handleFormErrors(es);
        }
    })

    $("#btn-saida").click(() => {
        try {
            var saida = createMovimento();
            postMovimento(saida, 'Saida')
                .then((data) => {
                    findPessoa(data.pessoaId)
                        .then((data) => {
                            console.log(data.pessoa);
                            Carteira.SelectedPessoa().saldo = data.pessoa.saldo;
                            Carteira.updatePessoaInfo();
                        })
                        .catch((error) => {
                            console.log(error);
                        })
                })
                .catch((error) => {
                    console.log(error);
                })
            clearFormErrors();
        } catch (es) {
            handleFormErrors(es);
        }
    })
});