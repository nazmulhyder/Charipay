using Charipay.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charipay.Application.Interfaces.Repositories
{
    public interface IDonationRepository : IRepository<Donation>
    {
        Task<List<Donation>> GetDonationsByUserIdAsync(Guid userId);
        Task<(List<Donation> Items, int totalCount)> Donations(Guid DonorUserId, int pageNumber, int pageSize, string? search = null);
        Task<Donation> Donation(Guid donationId, Guid UserId);


    }
}
