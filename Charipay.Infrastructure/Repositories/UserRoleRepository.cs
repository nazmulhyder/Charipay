using Charipay.Application.Interfaces.Repositories;
using Charipay.Domain.Entities;
using Charipay.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charipay.Infrastructure.Repositories
{
    public class UserRoleRepository : Repository<UserRole>, IUserRoleRepository
    {
        private readonly AppDbContext _context;

        public UserRoleRepository(AppDbContext context) : base(context)
        {
            _context = context;

        }

      
    }
}
