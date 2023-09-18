﻿using System;

namespace DesafioCarteira.Models
{
    public class Entrada : Movimento
    {
        public Entrada() { }

        public Entrada(Pessoa pessoa, DateTime data, string descricao, double valor)
           : base(pessoa, data, descricao, valor)
        { }
    }
}
