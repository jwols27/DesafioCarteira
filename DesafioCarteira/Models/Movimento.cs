using System;

namespace DesafioCarteira.Models
{
    public abstract class Movimento
    {
        public virtual int Id { get; set; }
        public virtual Pessoa Pessoa { get; set; }
        public virtual DateTime Data { get; set; }
        public virtual string Descricao { get; set; }
        public virtual double Valor { get; set; }

    }
}
