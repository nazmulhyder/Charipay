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

        public async Task<(IEnumerable<Charity>, int TotalCount)> GetPagedCharities(int pageNumber, int pageSize, string? search = null)
        {
            var query = _context.Charities.AsQueryable();

            if (!string.IsNullOrEmpty(search))
                query = query.Where(c => c.Name.Contains(search) || c.RegistrationNumber.Contains(search));
            
            var result = await query
                .Skip((pageNumber-1)* pageSize)
                .Take(pageSize)
                .OrderByDescending(c => c.CreatedAt)
                .ToListAsync();

            var TotalCount = result.Count();

            return (result, TotalCount);
        }
    }
}
