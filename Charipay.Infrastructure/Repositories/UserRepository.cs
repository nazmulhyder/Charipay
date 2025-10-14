using Charipay.Domain.Entities;
using Charipay.Domain.Interfaces;
using Charipay.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charipay.Infrastructure.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly AppDbContext _context; 
   
        public UserRepository(AppDbContext context) : base(context)
        {
            _context = context;
           
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users
                .Include(u => u.UserRoles)
                 .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<(IEnumerable<User>, int TotalCount)> GetPagedUserAsync(int pageNumber, int pageSize, string? search = null)
        {
            var query = _context.Users.AsQueryable();

            if(!string.IsNullOrEmpty(search))
            
            query = query.Where(q=>q.FullName.Contains(search) || q.Email.Contains(search));

            var totalCount = await query.CountAsync();
            

            var result = await query
                    .OrderByDescending(c=>c.CreatedAt)
                    .Skip((pageNumber-1)* pageSize)
                    .Take(pageSize)
                    .ToListAsync();

            return (result, totalCount);
            
        }

        public Task<User?> GetUserWithRolesAsync(Guid userId)
        {
            throw new NotImplementedException();
            //    return await _context.Users
            //        .Include(u => u.UserRoles)
            //            .ThenInclude(ur => ur.Role)
            //        .FirstOrDefaultAsync(u => u.UserID == userId);
        }
    }

}
