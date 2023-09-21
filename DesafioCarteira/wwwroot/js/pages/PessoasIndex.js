$(document).ready(function () {
    getPessoas()
        .then((data) => {
            PessoasIndex.Pessoas(data.pessoas);
        })
        .catch((error) => {
            console.error(error);
        });
});