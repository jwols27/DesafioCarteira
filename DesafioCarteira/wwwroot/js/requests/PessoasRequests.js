function getPessoas(callback){
    $.ajax({
        url: '/Pessoas/Find',
        type: 'GET',
        success: function (data) {
            callback(data);
        },
        error: function (xhr) {
            ErrorModal.ShowModal(xhr);
        }
    });
}

function postPessoa(pessoa){
    $.ajax({
        url: '/Pessoas/Create',
        type: 'POST',
        data: { pessoa: JSON.stringify(pessoa) },
        success: function (data) {
            console.log(data);
        },
        error: function (xhr) {
            ErrorModal.ShowModal(xhr);
        }
    });
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