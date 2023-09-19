using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace DesafioCarteira.Models
{
    public class Pessoa
    {
        [JsonProperty("id")]
        [Display(Name = "ID")]
        public virtual int Id { get; set; }

        [JsonProperty("nome")]
        [Required(ErrorMessage = "{0} é obrigatório")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "{0} deve ter no mínimo {2} e menos de {1} caracteres")]
        public virtual string Nome { get; set; }

        [JsonProperty("email")]
        [Required(ErrorMessage = "{0} é obrigatório")]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "{0} deve ter no mínimo {2} e menos de {1} caracteres")]
        public virtual string Email { get; set; }

        [JsonProperty("salario")]
        [Display(Name = "Salário")]
        [DataType(DataType.Currency)]
        public virtual double? Salario { get; set; }

        [JsonProperty("limite")]
        [DataType(DataType.Currency)]
        public virtual double? Limite { get; set; }

        [JsonProperty("minimo")]
        [Display(Name = "Mínimo")]
        [DataType(DataType.Currency)]
        public virtual double Minimo { get; set; }

        [JsonProperty("saldo")]
        [DataType(DataType.Currency, ErrorMessage = "Invalid currency value. Please enter a valid number.")]
        public virtual double Saldo { get; set; }

        public Pessoa () { }

        public Pessoa(string nome, string email, double salario, double limite, double minimo, double saldo)
        {
            Nome = nome;
            Email = email;
            Salario = salario;
            Limite = limite;
            Minimo = minimo;
            Saldo = saldo;
        }
    }
}
