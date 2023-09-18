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

            if (!_session.Query<Entrada>().Any())
                SeedMovimentos<Entrada>();

            if (!_session.Query<Saida>().Any())
                SeedMovimentos<Saida>();
        }

        private void SeedPessoas()
        {
            IEnumerable<Pessoa> pessoas = new List<Pessoa>() {
                new Pessoa("Egbert", "eggjune@ska.ia", 4136.12, 1000.51, 500.0, 3000.0)
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



        private void SeedMovimentos<Trans>() where Trans : Movimento
        {

            ITransaction transaction = null;
            try
            {
                Pessoa pessoa = _session.Get<Pessoa>(1);
                IEnumerable<Movimento> movs = new List<Movimento>() {
                    MovimentoFactory.CreateMovimento<Trans>(pessoa, DateTime.Today.AddDays(-1), "Transação", 100.0),
                    MovimentoFactory.CreateMovimento<Trans>(pessoa, DateTime.Today.AddDays(0), "Transação2", 101.0),
                };

                transaction = _session.BeginTransaction();
                foreach (Movimento mov in movs)
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
