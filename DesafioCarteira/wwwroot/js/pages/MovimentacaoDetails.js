$(document).ready(function () {
    $("#confirmar-deletar-mov").click(() => {
        let content = {
            title: "Deletar",
            text: "Você tem certeza que quer deletar esta movimentação?",
            button: "btn-deletar-mov"
        }
        ConfirmModal.ShowConfirmModal(content);

        $("#btn-deletar-mov").click(() => {
            const { type, mov } = typedMovJson;
            let pessoaId = localStorage.getItem('pessoaId');

            deleteMovimentacao(mov.id, pessoaId, type)
                .then((res) => {
                    window.location.href = res.redirectUrl;
                })
                .catch(function (error) {
                    console.error(error);
                });
        })
    })
});