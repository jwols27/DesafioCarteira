function postPessoa(pessoa) {
    $.ajax({
        url: '/Pessoas/Create',
        type: 'POST',
        data: { pessoa: JSON.stringify(pessoa) },
        success: function (data) {
            console.log(data);
        },
        error: function () {
            console.log('Error posting data.');
        }
    });
}