using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DesafioCarteira.Services.Interfaces
{
    public interface IComplexService<T>
    {
        Task Add(T item);
        Task Remove<U>(int id, int pv_id) where U : T;
        Task Update(T item, int pv_id);
        Task<U> FindById<U>(int id, int pv_id) where U : T;
        Task<IEnumerable<T>> FindAllById(int id, DateTime? t1, DateTime? t2);
        Task<bool> HasAny(int pv_id);
    }
}
