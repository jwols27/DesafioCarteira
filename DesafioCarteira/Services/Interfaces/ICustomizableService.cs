using System.Collections.Generic;
using System.Threading.Tasks;

namespace DesafioCarteira.Services.Interfaces
{
    public interface ICustomizableService<T>
    {
        Task Add(T item);
        Task Remove<U>(int id);
        Task Update(T item);
        Task<U> FindById<U>(int id);
        Task<IEnumerable<T>> FindAll();
        Task<bool> HasAny();
    }
}
