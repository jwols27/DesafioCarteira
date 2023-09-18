using Newtonsoft.Json;

namespace DesafioCarteira.Models
{
    public class Pessoa
    {
        [JsonProperty("id")]
        public virtual int Id { get; set; }

        [JsonProperty("nome")]
        public virtual string Nome { get; set; }

        [JsonProperty("email")]
        public virtual string Email { get; set; }

        [JsonProperty("salario")]
        public virtual double Salario { get; set; }

        [JsonProperty("limite")]
        public virtual double Limite { get; set; }

        [JsonProperty("minimo")]
        public virtual double Minimo { get; set; }

        [JsonProperty("saldo")]
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
