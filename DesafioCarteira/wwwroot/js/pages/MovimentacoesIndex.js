$(document).ready(function () {
    $("#movtype-group").change((x) => {
        getMovimentacoes(1, MovIndex.MovOption())
            .then((data) => {
                console.log(data.movs);
                MovIndex.Movs(data.movs);
            })
            .catch((error) => {
                console.error(error);
            });
    })
    $("#movtype-group").trigger('change');

});