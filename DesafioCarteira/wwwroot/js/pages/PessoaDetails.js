﻿$(document).ready(function () {
    $("#confirmar-deletar-pessoa").click(() => {
        let content = {
            title: "Deletar",
            text: "Você tem certeza que quer deletar esta pessoa?",
            button: "btn-deletar-pessoa"
        }
        ConfirmModal.ShowConfirmModal(content);

        $("#btn-deletar-pessoa").click(() => {
            deletePessoa(pessoaJson.id)
                .then((res) => {
                    window.location.href = res.redirectUrl;
                })
                .catch(function (error) {
                    console.error(error);
                });
        })
    })
});