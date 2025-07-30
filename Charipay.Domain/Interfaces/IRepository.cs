using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charipay.Domain.Interfaces
{
    public interface IRepository<T> where T : class
    {
        // READ
        Task<T?> GetByIdAsync(Guid id);
        Task<IEnumerable<T>> GetAllAsync();
        IQueryable<T> Query(); // For advanced querying (e.g., filtering, sorting)

        // CREATE
        Task<T> AddAsync(T entity);
        Task AddRangeAsync(IEnumerable<T> entities);

        // UPDATE
        void Update(T entity);

        // DELETE
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
    }

}
