using DesafioCarteira.Services.Interfaces;
using DesafioCarteira.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NHibernate;
using NHibernate.Linq;
using NHibernate.Exceptions;
using SalesWebMvc.Services.Exceptions;

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
            catch (GenericADOException e)
            {
                await transaction?.RollbackAsync();
                var sqlException = e.InnerException as System.Data.SqlClient.SqlException;
                if (sqlException != null && sqlException.Number == 547)
                {
                    throw new IntegrityException("Não é possível deletar pessoa porque ela tem movimentações");
                }
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
                Pessoa antigo = await _session.GetAsync<Pessoa>(pessoa.Id);
                pessoa.Saldo = antigo.Saldo;

                await _session.MergeAsync(pessoa);
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
