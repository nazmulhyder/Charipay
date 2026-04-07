using Charipay.Application.Interfaces.Repositories;
using Charipay.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charipay.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly AppDbContext _context;
        private readonly DbSet<T> _dbSet;
   
        public Repository(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<T?> GetByIdAsync(Guid id, CancellationToken token)
        {
            return await _dbSet.FindAsync(id, token);
        }

        public async Task<IEnumerable<T>> GetAllAsync(CancellationToken token)
        { 
            return await _dbSet.ToListAsync(token);
        }

        public IQueryable<T> Query(CancellationToken token)
        {
            return _dbSet.AsQueryable();
        }

        public async Task<T?> AddAsync(T entity)
        {
            var entry = await _dbSet.AddAsync(entity);     
            
            return entry.Entity;
        }

        public async Task AddRangeAsync(IEnumerable<T> entities, CancellationToken token)
        {
            await _dbSet.AddRangeAsync(entities, token);
        }

        public void Update(T entity, CancellationToken token)
        {
            _dbSet.Update(entity);
        }

        public void Remove(T entity, CancellationToken token)
        {
            _dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities, CancellationToken token)
        {
            _dbSet.RemoveRange(entities);
        }
    }

}
