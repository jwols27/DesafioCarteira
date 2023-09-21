using NHibernate;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace DesafioCarteira.Models.Mappings
{
    public class SaidaMap : ClassMapping<Saida>
    {
        public SaidaMap()
        {
            Id(x => x.Id, x => {
                x.Generator(Generators.Increment);
                x.Type(NHibernateUtil.Int32);
                x.Column("Id");
            });

            ManyToOne(x => x.Pessoa, m => {
                m.Column("Id_Pessoa");
                m.Class(typeof(Pessoa));
                m.Cascade(Cascade.Persist);
            });

            Property(b => b.Data, x => {
                x.Type(NHibernateUtil.DateTime);
                x.NotNullable(true);
            });

            Property(b => b.Descricao, x => {
                x.Length(50);
                x.Type(NHibernateUtil.String);
                x.NotNullable(true);
            });

            Property(b => b.Valor, x => {
                x.Type(NHibernateUtil.Double);
                x.Scale(2);
                x.Precision(15);
                x.NotNullable(true);
            });

            Table("movimentacao_saida");
        }
    }
}
