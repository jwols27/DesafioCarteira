function createMovimentacao() {
    var errors = [];
    var movimentacao = Carteira.Movimentacao;

    var Pessoa = Carteira.SelectedPessoa();

    var Data = new Date(movimentacao.Data());
    if (isNaN(Data))
        errors.push({ fieldName: 'Data', errorMessage: 'Data inválida' });

    var Descricao = movimentacao.Descricao();
    if (!Descricao)
        errors.push({ fieldName: 'Descricao', errorMessage: 'Descrição é obrigatória' });
    else if (Descricao.length < 3 || Descricao.length > 50)
        errors.push({ fieldName: 'Descricao', errorMessage: 'Descrição deve ter no mínimo 3 e até 50 caracteres' });

    
    var Valor = parseFloat(convertCommaToDot(movimentacao.Valor()));
    if (isNaN(Valor))
        errors.push({ fieldName: 'Valor', errorMessage: 'Valor não é um número' });
    else if (!Valor)
        errors.push({ fieldName: 'Valor', errorMessage: 'Valor é obrigatório' });

    try {
        if (errors.length > 0) {
            throw errors;
        }

        clearFormErrors();
        return {
            Pessoa,
            Data,
            Descricao,
            Valor
        }
    } catch (es) {
        handleFormErrors(es);
    }
}

$(document).ready(function () {
    $("#confirmar-deslogar").click(() => {
        let content = {
            title: "Deslogar",
            text: "Você tem certeza que quer deslogar?",
            button: "btn-deslogar"
        }
        ConfirmModal.ShowConfirmModal(content);

        $("#btn-deslogar").click(() => {
            Carteira.SelectedPessoa(null);
            Carteira.updatePessoaInfo();
        })
    })

    getPessoas()
        .then((data) => {
            Carteira.Pessoas(data.pessoas);
        })
        .catch((error) => {
            console.error(error);
        });

    Carteira.getLoggedIn();

    $("#movimentacao-form").submit((event) => {
        event.preventDefault();
    })

    function movimentar(tipo) {
        let mov = createMovimentacao();
        if (!mov) return;
        postMovimentacao(mov, tipo)
            .then((data) => {
                findPessoa(data.pessoaId)
                    .then((data) => {
                        console.log(data.pessoa);
                        Carteira.SelectedPessoa().saldo = data.pessoa.saldo;
                        Carteira.updatePessoaInfo();
                        Carteira.Movimentacao.resetFields();
                    })
                    .catch((error) => {
                        console.log(error);
                    })
            })
            .catch((error) => {
                console.log(error);
            })
    }

    $("#btn-entrada").click(() => {
        movimentar('Entrada');
    })

    $("#btn-confirmar-saida").click(() => {
        let mov = createMovimentacao();
        if (!mov) return;

        let selected = Carteira.SelectedPessoa();
        if (mov.Valor > 0)
            mov.Valor = mov.Valor * -1

        let total = selected.saldo + mov.Valor;

        if (total < 0) {
            let xhr = {
                status: "de movimentação",
                responseText: "Saldo não pode ficar negativo."
            }
            ErrorModal.ShowModal(xhr);
            return;
        }

        if (total <= selected.minimo) {
            let content = {
                title: "Aviso de limite",
                text: "Saldo ficará abaixo do limite!",
                button: "btn-saida"
            }
            ConfirmModal.ShowConfirmModal(content);

            $("#btn-saida").click(() => {
                movimentar('Saida');
            })
        } else {
            movimentar('Saida');
        }
    })
});