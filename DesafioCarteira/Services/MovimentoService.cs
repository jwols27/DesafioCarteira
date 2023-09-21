using DesafioCarteira.Models;
using DesafioCarteira.Services.Exceptions;
using DesafioCarteira.Services.Interfaces;
using NHibernate;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioCarteira.Services
{
    public class MovimentoService : ICustomizableService<Movimento>
    {
        private ISession _session;

        public MovimentoService(ISession session) => _session = session;

        public async Task Add(Movimento mov)
        {
            ITransaction transaction = null;

            try
            {
                IsTypeValid(mov.GetType());
                transaction = _session.BeginTransaction();
                if (mov is Entrada)
                    await _session.SaveAsync(mov as Entrada);
                if (mov is Saida)
                    await _session.SaveAsync(mov as Saida);

                Pessoa pessoa = await _session.GetAsync<Pessoa>(mov.Pessoa.Id);
                if (pessoa == null)
                    throw new NotFoundException("Pessoa não encontrada");

                pessoa.Saldo = pessoa.Saldo + mov.Valor;

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

        public async Task Remove<Trans> (int id) where Trans : Movimento
        {
            ITransaction transaction = null;

            try
            {
                IsTypeValid<Trans>();
                transaction = _session.BeginTransaction();
                Trans mov = await _session.GetAsync<Trans>(id);
                await _session.DeleteAsync(mov);

                Pessoa pessoa = await _session.GetAsync<Pessoa>(mov.Id);
                if (pessoa == null)
                    throw new NotFoundException("Pessoa não encontrada");

                pessoa.Saldo = pessoa.Saldo - mov.Valor;

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

        public async Task Update(Movimento mov)
        {
            ITransaction transaction = null;

            try
            {
                IsTypeValid(mov.GetType());
                transaction = _session.BeginTransaction();
                if (mov is Entrada)
                    await _session.UpdateAsync(mov as Entrada);
                if (mov is Saida)
                    await _session.UpdateAsync(mov as Saida);

                Pessoa pessoa = await _session.GetAsync<Pessoa>(mov.Id);
                if (pessoa == null)
                    throw new NotFoundException("Pessoa não encontrada");

                

                Type antigoType = mov.GetType();
                Movimento antigo = await _session.GetAsync(antigoType, mov.Id) as Movimento;

                pessoa.Saldo = pessoa.Saldo + mov.Valor - antigo.Valor;

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


        public async Task<Trans> FindById<Trans>(int id)
        {
            try
            {
                IsTypeValid<Trans>();
                return await _session.GetAsync<Trans>(id);
            }
            catch (InvalidOperationException e)
            {
                // TODO: Implement Error
                Console.WriteLine(e);
            }
            return default;
        }

        public async Task<IEnumerable<Movimento>> FindAll()
        {
            IEnumerable<Entrada> entradas = await FindAll<Entrada>();
            IEnumerable<Saida> saidas = await FindAll<Saida>();
            return entradas.Concat<Movimento>(saidas);
        }

        public async Task<IEnumerable<Trans>> FindAll<Trans>()
        {
            try
            {
                IsTypeValid<Trans>();
                return await _session.Query<Trans>().ToListAsync();
            }
            catch (InvalidOperationException e)
            {
                // TODO: Implement Error
                Console.WriteLine(e);
            }
            return default;
        }


        public async Task<bool> HasAny() => await _session.Query<Movimento>().AnyAsync();

        public async Task<bool> HasAny<Trans>()
        {
            try
            {
                IsTypeValid<Trans>();
                return await _session.Query<Trans>().AnyAsync();
            }
            catch (InvalidOperationException e)
            {
                // TODO: Implement Error
                Console.WriteLine(e);
            }
            return default;
        }

        public bool IsTypeValid<T>()
        {
            if (typeof(T) != typeof(Entrada) && typeof(T) != typeof(Saida))
                throw new InvalidOperationException("Tipo inesperado: " + typeof(T));
            else return true;
        }

        public bool IsTypeValid(Type type)
        {
            if (type != typeof(Entrada) && type != typeof(Saida))
                throw new InvalidOperationException("Tipo inesperado: " + type);
            else return true;
        }


    }
}