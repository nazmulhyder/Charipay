using Charipay.Domain.Interfaces;
using Charipay.Infrastructure.Data;
using Charipay.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charipay.Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        public IUserRepository Users { get; }

        public IUserRoleRepository UserRoles { get; }

        public ICharityRepository Charity { get; }

        public UnitOfWork(AppDbContext context, IUserRepository userRepository, IUserRoleRepository userRoles,
            ICharityRepository _charity
            )
        {
            _context = context;
            Users = userRepository;
            UserRoles = userRoles;
            Charity = _charity;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }

}
