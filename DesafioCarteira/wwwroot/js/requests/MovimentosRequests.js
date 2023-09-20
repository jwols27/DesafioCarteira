//function getMovimentos(callback) {
//    $.ajax({
//        url: '/Carteira/Find',
//        type: 'GET',
//        success: function (data) {
//            callback(data);
//        },
//        error: function (xhr) {
//            ErrorModal.ShowModal(xhr);
//        }
//    });
//}

function postMovimento(movimento, tipo) {
    $.ajax({
        url: '/Carteira/Create' + tipo,
        type: 'POST',
        data: { movimento: JSON.stringify(movimento) },
        success: function (data) {
            console.log(data);
        },
        error: function (xhr) {
            ErrorModal.ShowModal(xhr);
        }
    });
}

//function deleteMovimento(pessoaId) {
//    return new Promise(function (resolve, reject) {
//        $.ajax({
//            url: '/Carteira/Delete/' + movimentoId,
//            type: 'DELETE',
//            success: function (data) {
//                resolve(data);
//            },
//            error: function (xhr) {
//                ErrorModal.ShowModal(xhr);
//            }
//        });
//    });
//}