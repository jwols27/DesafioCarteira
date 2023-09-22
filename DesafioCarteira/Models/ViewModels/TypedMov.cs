﻿namespace DesafioCarteira.Models.ViewModels
{
    public class TypedMov
    {
        public string Type { get; set; }
        public Movimentacao Mov { get; set; }

        public TypedMov(string type, Movimentacao mov)
        {
            Type = type;
            Mov = mov;
        }

        public string ExtMov()
        {
            switch (Type)
            {
                case "E":
                    return "Entrada";
                case "S":
                    return "Saída";
                default:
                    return "Não definido";
            }
        }
    }
}
