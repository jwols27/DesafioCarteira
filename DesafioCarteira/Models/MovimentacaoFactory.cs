using System;

namespace DesafioCarteira.Models
{
    public static class MovimentacaoFactory
    {
        public static Movimentacao CreateMovimentacao<Trans>(Pessoa pessoa, DateTime data, string descricao, double valor)
        {
            if(typeof(Trans) == typeof(Entrada))
            {
                if (valor < 0) valor = valor * -1;
                return new Entrada(pessoa, data, descricao, valor);
            }
                
            else if (typeof(Trans) == typeof(Saida))
            {
                if (valor > 0) valor = valor * -1;
                return new Saida(pessoa, data, descricao, valor);
            }
            else
                throw new InvalidOperationException("Tipo inesperado: " + typeof(Trans));
        }
    }
}
