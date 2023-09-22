function getMovimentacoes(pessoaId, type) {
    return new Promise((resolve, reject) => {
        $.ajax({
            url: '/Movimentacoes/Get/' + pessoaId,
            type: 'GET',
            data: { type },
            success: function (data) {
                resolve(data);
            },
            error: function (xhr) {
                ErrorModal.ShowModal(xhr);
            }
        });
    })
}

function findMovimentacao(movId, pessoaId, type) {
    return new Promise((resolve, reject) => {
        $.ajax({
            url: '/Movimentacoes/Details/' + movId,
            type: 'GET',
            data: { pessoaId, type },
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

function editMovimentacao(mov, type) {
    return new Promise((resolve, reject) => {
        $.ajax({
            url: '/Movimentacoes/Edit/' + mov.Id,
            type: 'POST',
            data: { mov: JSON.stringify(mov), type },
            success: function (data) {
                resolve(data);
            },
            error: function (xhr) {
                ErrorModal.ShowModal(xhr);
            }
        });
    })
}

function deleteMovimentacao(movId, pessoaId, type) {
    return new Promise(function (resolve, reject) {
        $.ajax({
            url: '/Movimentacoes/Delete/' + movId,
            type: 'DELETE',
            data: { pessoaId, type },
            success: function (data) {
                resolve(data);
            },
            error: function (xhr) {
                ErrorModal.ShowModal(xhr);
            }
        });
    });
}