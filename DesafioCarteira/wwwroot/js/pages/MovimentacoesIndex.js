$(document).ready(function () {
    getMovimentacoes()
        .then((data) => {
            MovIndex.Movs(data.movs);
        })
        .catch((error) => {
            console.error(error);
        });
});