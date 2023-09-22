var MovEdit = new MovimentacaoVM(
    movJson.mov.id, movJson.mov.pessoa, movJson.mov.data, movJson.mov.descricao, movJson.mov.valor.toString()
)

ko.applyBindings(MovEdit, document.getElementById('editar-mov-view'));