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
    public class VolunteerUserRepository : Repository<VolunteerUser>, IVolunteerUserRepository
    {
        private readonly AppDbContext _context;
        public VolunteerUserRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> HasUserAlreadyAppliedAsync(Guid userId, Guid? volunteerTaskId)
        {
            var existingData = await _context.VolunteerUsers.AnyAsync(c=> c.UserId == userId && c.VolunteerTaskId == volunteerTaskId && c.IsActive);

            return existingData;
        }

        public async Task<int> GetActiveApplicationCountAsync(Guid volunteerTaskId)
        {
            var counts = await _context.VolunteerUsers.CountAsync(c=>c.VolunteerTaskId == volunteerTaskId);

            return counts == 0 ? 0 : counts;
        }
    }
}
