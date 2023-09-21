function postMovimento(movimento, tipo) {
    return new Promise((resolve, reject) => {
        $.ajax({
            url: '/Carteira/Create' + tipo,
            type: 'POST',
            data: { movimento: JSON.stringify(movimento) },
            success: function (data) {
                resolve(data);
            },
            error: function (xhr) {
                ErrorModal.ShowModal(xhr);
            }
        });
    })
}