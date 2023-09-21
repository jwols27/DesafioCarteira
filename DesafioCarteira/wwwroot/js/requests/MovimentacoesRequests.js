function getMovimentacao() {
    return new Promise((resolve, reject) => {
        $.ajax({
            url: '/Movimentacoes/Get',
            type: 'GET',
            success: function (data) {
                resolve(data);
            },
            error: function (xhr) {
                ErrorModal.ShowModal(xhr);
            }
        });
    })
}

function postMovimentacao(movimentacao, tipo) {
    return new Promise((resolve, reject) => {
        $.ajax({
            url: '/Carteira/Create' + tipo,
            type: 'POST',
            data: { movimentacao: JSON.stringify(movimentacao) },
            success: function (data) {
                resolve(data);
            },
            error: function (xhr) {
                ErrorModal.ShowModal(xhr);
            }
        });
    })
}

function deleteMovimentacao(movId) {
    return new Promise(function (resolve, reject) {
        $.ajax({
            url: '/Movimentacoes/Delete/' + movId,
            type: 'DELETE',
            success: function (data) {
                resolve(data);
            },
            error: function (xhr) {
                ErrorModal.ShowModal(xhr);
            }
        });
    });
}