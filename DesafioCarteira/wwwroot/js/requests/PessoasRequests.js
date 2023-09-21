function findPessoa(id) {
    return new Promise((resolve, reject) => {
        $.ajax({
            url: '/Pessoas/Find/' + id,
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

function getPessoas() {
    return new Promise((resolve, reject) => {
        $.ajax({
            url: '/Pessoas/Get',
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

function postPessoa(pessoa){
    return new Promise((resolve, reject) => {
        $.ajax({
            url: '/Pessoas/Create',
            type: 'POST',
            data: { pessoa: JSON.stringify(pessoa) },
            success: function (data) {
                resolve(data);
            },
            error: function (xhr) {
                ErrorModal.ShowModal(xhr);
            }
        });
    })
}

function deletePessoa(pessoaId) {
    return new Promise(function (resolve, reject) {
        $.ajax({
            url: '/Pessoas/Delete/' + pessoaId,
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