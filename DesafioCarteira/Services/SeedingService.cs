using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using DesafioCarteira.Models;

namespace DesafioCarteira.Services
{
    public class SeedingService
    {
        private ISession _session;

        public SeedingService(ISession session) =>
            _session = session;

        public void Seed()
        {
            if (!_session.Query<Pessoa>().Any())
                SeedPessoas();

            //if (!_session.Query<Entrada>().Any())
            //    SeedMovimentacoes<Entrada>();

            //if (!_session.Query<Saida>().Any())
            //    SeedMovimentacoes<Saida>();
        }

        private void SeedPessoas()
        {
            IEnumerable<Pessoa> pessoas = new List<Pessoa>() {
                new Pessoa("June Egbert", "eggjune@ska.ia", 4130, 1000, 125.0, 413.0),
                new Pessoa("Jane Harley", "hawrley@ska.ia", 6120, 1000, 125.0, 612.0)
            };

            ITransaction transaction = null;
            try
            {
                transaction = _session.BeginTransaction();
                foreach (Pessoa pessoa in pessoas)
                {
                    _session.Save(pessoa);
                }
                transaction.Commit();

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                transaction?.Rollback();
            }
            finally
            {
                transaction?.Dispose();
            }
        }



        private void SeedMovimentacoes<Trans>() where Trans : Movimentacao
        {

            ITransaction transaction = null;
            try
            {
                Pessoa pessoa = _session.Get<Pessoa>(1);
                IEnumerable<Movimentacao> movs = new List<Movimentacao>() {
                    MovimentacaoFactory.CreateMovimentacao<Trans>(pessoa, DateTime.Today.AddDays(-1), "Transação", 100.0),
                    MovimentacaoFactory.CreateMovimentacao<Trans>(pessoa, DateTime.Today.AddDays(0), "Transação2", 101.0),
                };

                transaction = _session.BeginTransaction();
                foreach (Movimentacao mov in movs)
                {
                    _session.Save(mov as Trans);
                }
                transaction.Commit();

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                transaction?.Rollback();
            }
            finally
            {
                transaction?.Dispose();
            }
        }
    }
}
