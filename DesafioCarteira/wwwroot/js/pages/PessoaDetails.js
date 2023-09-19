$(document).ready(function () {

    var text = $("#pessoa-id").text();

    ConfirmModal.ModalAction("deletar esta pessoa?");
    ConfirmModal.ModalButton("btn-deletar-pessoa");
    ConfirmModal.ModalTitle("Deletar");

    $("#btn-deletar-pessoa").click(() => {
        deletePessoa(parseInt(text.trim()))
            .then((res) => {
                if (res.success) {
                    window.location.href = res.redirectUrl;
                }
            })
            .catch(function (error) {
                console.error(error);
            });

    })
});