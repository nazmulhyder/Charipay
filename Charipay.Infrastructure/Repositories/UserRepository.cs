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
            var query =  await _context.Users
            .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
            .OrderByDescending(u => u.CreatedAt)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();


            if (!string.IsNullOrEmpty(search))
            
            query = query.Where(q=>q.FullName.Contains(search) || q.Email.Contains(search)).ToList();

            var totalCount = query.Count();
            

            return (query, totalCount);
            
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
