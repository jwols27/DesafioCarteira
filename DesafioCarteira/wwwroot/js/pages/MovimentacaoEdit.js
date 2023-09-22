function createMovimentacaoEdit() {
    let errors = [];

    let Id = MovEdit.Id();
    let Pessoa = MovEdit.Pessoa();
    if (!Id || !Pessoa)
        errors.push({ fieldName: 'Extra', errorMessage: 'Não autorizado' });

    let Data = new Date(MovEdit.Data());
    if (isNaN(Data))
        errors.push({ fieldName: 'Data', errorMessage: 'Data inválida' });

    let Descricao = MovEdit.Descricao();
    if (!Descricao)
        errors.push({ fieldName: 'Descricao', errorMessage: 'Descrição é obrigatória' });
    else if (Descricao.length < 3 || Descricao.length > 50)
        errors.push({ fieldName: 'Descricao', errorMessage: 'Descrição deve ter no mínimo 3 e até 50 caracteres' });


    let Valor = parseFloat(convertCommaToDot(MovEdit.Valor()));
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
            Id,
            Pessoa,
            Data,
            Descricao,
            Valor
        }
    } catch (es) {
        handleFormErrors(es);
    }
}

function editarMov(tipo) {
    let mov = createMovimentacaoEdit();
    console.log(mov)
    if (!mov) return;
    editMovimentacao(mov, tipo)
        .then((data) => {
            findPessoa(data.pessoaId)
                .then((data) => {
                    Carteira.SelectedPessoa().saldo = data.pessoa.saldo;
                    Carteira.updatePessoaInfo();
                    Carteira.Movimentacao.resetFields();
                    data.redirectUrl;
                })
                .catch((error) => {
                    console.log(error);
                })
        })
        .catch((error) => {
            console.log(error);
        })
}

$(document).ready(function () {


    $("#movimentacao-form").submit((event) => {
        event.preventDefault();
    })

    $("#btn-salvar-edit").click(() => {
        editarMov(movJson.type);
    })
})