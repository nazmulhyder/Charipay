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
    public class CharityRepository : Repository<Charity>, ICharityRepository
    {
        private readonly AppDbContext _appDbContext;
        public CharityRepository(AppDbContext context) : base(context)
        {
            _appDbContext = context;
        }

        public async Task<bool> GetCharityByContactEmailAsync(string email, CancellationToken token)
        {
            return await _appDbContext.Charities.AnyAsync(c => c.ContactEmail == email, token);
        }

        public async Task<bool> GetCharityByRegistrationNumberAsync(string registrationNo, CancellationToken token)
        {
            return await _appDbContext.Charities.AnyAsync(c => c.RegistrationNumber == registrationNo, token);
        }
    }
}
