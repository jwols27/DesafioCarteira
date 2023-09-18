
namespace DesafioCarteira.Models
{
    public class Pessoa
    {
        public virtual int Id { get; set; }
        public virtual string Nome { get; set; }
        public virtual string Email { get; set; }
        public virtual double Salario { get; set; }
        public virtual double Limite { get; set; }
        public virtual double Minimo { get; set; }
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
