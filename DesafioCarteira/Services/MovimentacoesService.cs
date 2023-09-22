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
    public class MovimentacoesService : IComplexService<Movimentacao>
    {
        private ISession _session;

        public MovimentacoesService(ISession session) => _session = session;

        public async Task Add(Movimentacao mov)
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

        public async Task Remove<Trans> (int id, int pessoaId) where Trans : Movimentacao
        {
            ITransaction transaction = null;

            try
            {
                IsTypeValid<Trans>();
                transaction = _session.BeginTransaction();
                Trans mov = await _session.GetAsync<Trans>(id);

                Pessoa pessoa = await _session.GetAsync<Pessoa>(mov.Pessoa.Id);
                if (pessoa == null)
                    throw new NotFoundException("Pessoa não encontrada");

                if (pessoaId != pessoa.Id)
                    throw new NotFoundException("Acesso não autorizado");

                double novoSaldo = pessoa.Saldo - mov.Valor;

                pessoa.Saldo = novoSaldo;

                await _session.DeleteAsync(mov);
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

        public async Task Update(Movimentacao mov, int pessoaId)
        {
            ITransaction transaction = null;

            try
            {
                IsTypeValid(mov.GetType());
                transaction = _session.BeginTransaction();

                Pessoa pessoa = await _session.GetAsync<Pessoa>(mov.Id);
                if (pessoa == null)
                    throw new NotFoundException("Pessoa não encontrada");

                if (pessoaId != pessoa.Id)
                    throw new NotFoundException("Acesso não autorizado");

                Type antigoType = mov.GetType();
                Movimentacao antigo = await _session.GetAsync(antigoType, mov.Id) as Movimentacao;

                double novoSaldo = pessoa.Saldo + mov.Valor - antigo.Valor;
                pessoa.Saldo = novoSaldo;

                if (mov is Entrada)
                    await _session.MergeAsync(mov as Entrada);
                if (mov is Saida)
                    await _session.MergeAsync(mov as Saida);
                
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


        public async Task<Trans> FindById<Trans>(int id, int pessoaId) where Trans : Movimentacao
        {
            try
            {
                var trans = await _session.GetAsync<Trans>(id);
                if (pessoaId != trans.Pessoa.Id)
                    throw new UnauthorizedAccessException("Acesso não autorizado");
                return trans;
            }
            catch (InvalidOperationException e)
            {
                // TODO: Implement Error
                Console.WriteLine(e);
            }
            return default;
        }

        public async Task<IEnumerable<Movimentacao>> FindAllById(int pessoaId)
        {
            IEnumerable<Entrada> entradas = await FindAllById<Entrada>(pessoaId);
            IEnumerable<Saida> saidas = await FindAllById<Saida>(pessoaId);
            return entradas.Concat<Movimentacao>(saidas)
                .OrderByDescending(t => t.Data);
        }

        public async Task<IEnumerable<Trans>> FindAllById<Trans>(int pessoaId) where Trans : Movimentacao
        {
            try
            {
                return await _session.Query<Trans>()
                    .Where(t => t.Pessoa.Id == pessoaId)
                    .OrderByDescending(t => t.Data)
                    .ToListAsync();
            }
            catch (InvalidOperationException e)
            {
                // TODO: Implement Error
                Console.WriteLine(e);
            }
            return default;
        }


        public async Task<bool> HasAny(int pessoaId)
        {
            bool n1 = await HasAny<Entrada>(pessoaId);
            bool n2 = await HasAny<Saida>(pessoaId);
            return n1 || n2;
        }
            

        public async Task<bool> HasAny<Trans>(int pessoaId) where Trans : Movimentacao
        {
            try
            {
                IsTypeValid<Trans>();
                return await _session.Query<Trans>().AnyAsync(t => t.Pessoa.Id == pessoaId);
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