function mountMovimento() {
    var movimento = Carteira.Movimento;
    return {
        Pessoa: Carteira.SelectedPessoa(),
        Data: movimento.Data(),
        Descricao: movimento.Descricao(),
        Valor: parseFloat(convertCommaToDot(movimento.Valor()))
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

    getPessoas(Carteira.Pessoas);

    $("#movimento-form").submit((event) => {
        event.preventDefault();
    })

    $("#btn-entrada").click(() => {
        mountMovimento();
        postMovimento(entrada, 'Entrada');
    })

    $("#btn-saida").click(() => {
        mountMovimento();
        postMovimento(entrada, 'Saida');
    })
});