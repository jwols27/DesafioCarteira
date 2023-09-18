using DesafioCarteira.Services.Interfaces;
using DesafioCarteira.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NHibernate;
using NHibernate.Linq;

namespace DesafioCarteira.Services
{
    public class PessoasService : IService<Pessoa>
    {
        private ISession _session;
        public PessoasService(ISession session) => _session = session;

        public async Task Add(Pessoa pessoa)
        {
            ITransaction transaction = null;

            try
            {
                transaction = _session.BeginTransaction();
                await _session.SaveAsync(pessoa);
                await transaction.CommitAsync();
            }
            catch (Exception e)
            {
                // TODO: Implement Error
                Console.WriteLine(e);
                await transaction?.RollbackAsync();
            }
            finally
            {
                transaction?.Dispose();
            }
        }

        public async Task Remove(int id)
        {
            ITransaction transaction = null;

            try
            {
                transaction = _session.BeginTransaction();
                Pessoa pessoa = await _session.GetAsync<Pessoa>(id);
                await _session.DeleteAsync(pessoa);

                await transaction.CommitAsync();
            }
            catch (Exception e)
            {
                // TODO: Implement Error
                Console.WriteLine(e);
                await transaction?.RollbackAsync();
            }
            finally
            {
                transaction?.Dispose();
            }
        }

        public async Task Update(Pessoa pessoa)
        {
            ITransaction transaction = null;

            try
            {
                transaction = _session.BeginTransaction();
                await _session.UpdateAsync(pessoa);
                await transaction.CommitAsync();
            }
            catch (Exception e)
            {
                // TODO: Implement Error
                Console.WriteLine(e);
                await transaction?.RollbackAsync();
            }
            finally
            {
                transaction?.Dispose();
            }
        }

        public async Task<Pessoa> FindById(int id) => await _session.GetAsync<Pessoa>(id);

        public async Task<IEnumerable<Pessoa>> FindAll() => await _session.Query<Pessoa>().ToListAsync();


        public async Task<bool> HasAny() => await _session.Query<Pessoa>().AnyAsync();

    }
}
