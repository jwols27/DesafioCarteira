function getPessoas() {
    $.ajax({
        url: '/Pessoas/Find', // Replace with the actual URL of your action method
        type: 'GET',
        success: function (data) {
            console.log(data);
            Carteira.Pessoas(data); // Assuming viewModel is your existing ViewModel
        },
        error: function () {
            console.log('Error fetching data.');
        }
    });
}