using Charipay.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charipay.Domain.Interfaces
{
    public interface ICharityRepository : IRepository<Charity>
    {
        Task<bool> GetCharityByContactEmailAsync(string email, CancellationToken token);
        Task<bool> GetCharityByRegistrationNumberAsync(string registrationNo, CancellationToken token);
        Task<(IEnumerable<Charity>, int TotalCount)> GetPagedCharities(int pageNumber, int pageSize, string? search = null);

    }
}
