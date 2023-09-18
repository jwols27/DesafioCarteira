using NHibernate;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace DesafioCarteira.Models.Mappings
{
    public class PessoaMap : ClassMapping<Pessoa>
    {
        public PessoaMap()
        {
            Id(x => x.Id, x => {
                x.Generator(Generators.Increment);
                x.Type(NHibernateUtil.Int32);
                x.Column("Id");
            });

            Property(b => b.Nome, x => {
                x.Length(50);
                x.Type(NHibernateUtil.String);
                x.NotNullable(true);
            });

            Property(b => b.Email, x => {
                x.Length(30);
                x.Type(NHibernateUtil.String);
                x.NotNullable(true);
            });

            Property(b => b.Salario, x => {
                x.Type(NHibernateUtil.Double);
                x.Scale(2);
                x.Precision(15);
                x.NotNullable(false);
            });

            Property(b => b.Limite, x => {
                x.Type(NHibernateUtil.Double);
                x.Scale(2);
                x.Precision(15);
                x.NotNullable(false);
            });

            Property(b => b.Minimo, x => {
                x.Type(NHibernateUtil.Double);
                x.Scale(2);
                x.Precision(15);
                x.NotNullable(true);
            });

            Property(b => b.Saldo, x => {
                x.Type(NHibernateUtil.Double);
                x.Scale(2);
                x.Precision(15);
                x.NotNullable(true);
            });

            Table("pessoas");
        }
    }
}
