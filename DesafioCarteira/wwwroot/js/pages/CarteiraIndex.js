$(document).ready(function () {
    ConfirmModal.ModalAction("deslogar");
    ConfirmModal.ModalButton("btn-deslogar");
    ConfirmModal.ModalTitle("Deslogar");

    $("#btn-deslogar").click(() => {
        Carteira.SelectedPessoa(undefined);
        Carteira.updatePessoaInfo();
    })

    getPessoas(Carteira.Pessoas);
});