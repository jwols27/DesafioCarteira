using System.Collections.Generic;
using System.Threading.Tasks;

namespace DesafioCarteira.Services.Interfaces
{
    public interface IService<T>
    {
        Task Add(T item);
        Task Remove<M>(int id);
        Task Update(T item);
        Task<T> FindById<T>(int id);
        Task<IEnumerable<T>> FindAll();
        Task<bool> HasAny();
    }
}
