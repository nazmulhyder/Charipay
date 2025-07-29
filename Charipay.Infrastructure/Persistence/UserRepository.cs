using Charipay.Domain.Entities;
using Charipay.Domain.Interfaces;
using Charipay.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charipay.Infrastructure.Persistence
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
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public Task<User?> GetUserWithRolesAsync(int userId)
        {
            throw new NotImplementedException();
            //    return await _context.Users
            //        .Include(u => u.UserRoles)
            //            .ThenInclude(ur => ur.Role)
            //        .FirstOrDefaultAsync(u => u.UserID == userId);
        }
    }

}
