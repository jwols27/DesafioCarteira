const getMovs = (before = "", after = "") => {
    let id = localStorage.getItem('pessoaId');
    if (!id) return;
    getMovimentacoes(id, MovIndex.MovOption(), before, after)
        .then((data) => {
            MovIndex.Movs(data.movs);
        })
        .catch((error) => {
            console.error(error);
        });
}

$(document).ready(function () {
    $("#buscar").click(() => {
        let selected = MovIndex.selectedTempo();
        let before, after;

        if (!selected?.text)
            return getMovs();

        if (selected.text === 'Personalizado') {
            before = $("#date-before").val();
            after = $("#date-after").val();
        } else {
            before = getCompleteDate(selected.time.before);
            after = getCompleteDate(selected.time.after);
        }
        
        getMovs(before, after)
    })
});