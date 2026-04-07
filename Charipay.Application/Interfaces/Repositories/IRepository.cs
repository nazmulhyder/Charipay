using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charipay.Application.Interfaces.Repositories
{
    public interface IRepository<T> where T : class
    {
        // READ
        Task<T?> GetByIdAsync(Guid id, CancellationToken token);
        Task<IEnumerable<T>> GetAllAsync(CancellationToken token);
        IQueryable<T> Query(CancellationToken token); // For advanced querying (e.g., filtering, sorting)

        // CREATE
        Task<T> AddAsync(T entity);
        Task AddRangeAsync(IEnumerable<T> entities, CancellationToken token);

        // UPDATE
        void Update(T entity, CancellationToken token);

        // DELETE
        void Remove(T entity, CancellationToken token); 
        void RemoveRange(IEnumerable<T> entities, CancellationToken token);
    }

}
