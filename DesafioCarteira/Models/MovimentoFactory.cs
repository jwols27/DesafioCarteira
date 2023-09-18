using System;

namespace DesafioCarteira.Models
{
    public static class MovimentoFactory
    {
        public static Movimento CreateMovimento<Trans>(Pessoa pessoa, DateTime data, string descricao, double valor)
        {
            if(typeof(Trans) == typeof(Entrada))
                return new Entrada(pessoa, data, descricao, valor);
            else if (typeof(Trans) == typeof(Saida))
                return new Saida(pessoa, data, descricao, valor);
            else
                throw new InvalidOperationException("Tipo inesperado: " + typeof(Trans));
        }
    }
}
