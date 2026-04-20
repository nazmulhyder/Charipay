using Charipay.Application.Interfaces.Repositories;
using Charipay.Domain.Entities;
using Charipay.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charipay.Infrastructure.Repositories
{
    public class VolunteerApplicationHistoryRepository : Repository<VolunteerApplicationHistory>, IVolunteerApplicationHistoryRepository
    {
        private readonly AppDbContext _context;
        public VolunteerApplicationHistoryRepository(AppDbContext context) : base(context)
        {
           _context = context;
        }

        public async Task<bool> IsAlreadyAppliedAsync(Guid VolunteerTaskId, Guid UserId)
        {
            var getApplications = await _context.VolunteerUsers.Where(c => c.VolunteerTaskId == VolunteerTaskId && c.UserId == UserId)
                .Select(b=>b.VolunteerTaskId)
                .ToListAsync();

            return getApplications.Contains(VolunteerTaskId);
        }
    }
}
