using System;

namespace DesafioCarteira.Models
{
    public class Saida : Movimento
    {
        public Saida() { }

        public Saida(Pessoa pessoa, DateTime data, string descricao, double valor)
           : base(pessoa, data, descricao, valor)
        { }
    }
}