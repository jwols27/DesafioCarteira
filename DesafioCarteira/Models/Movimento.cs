using System;
using System.ComponentModel.DataAnnotations;

namespace DesafioCarteira.Models
{
    public abstract class Movimento
    {
        public virtual int Id { get; set; }

        [Required(ErrorMessage = "{0} é obrigatório")]
        public virtual Pessoa Pessoa { get; set; }

        [Required(ErrorMessage = "{0} é obrigatório")]
        [DataType(DataType.DateTime)]
        public virtual DateTime Data { get; set; }

        [Required(ErrorMessage = "{0} é obrigatório")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "{0} deve ter no mínimo {2} e menos de {1} caracteres")]
        [Display(Name = "Descrição")]
        public virtual string Descricao { get; set; }

        [Required(ErrorMessage = "{0} é obrigatório")]
        [DataType(DataType.Currency)]
        public virtual double Valor { get; set; }

        protected Movimento() { }

        protected Movimento(Pessoa pessoa, DateTime data, string descricao, double valor)
        {
            Pessoa = pessoa;
            Data = data;
            Descricao = descricao;
            Valor = valor;
        }
    }
}
