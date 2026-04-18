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
    public class VolunteerTaskRepository : Repository<VolunteerTask>, IVolunteerTaskRepository
    {
        private readonly AppDbContext _context;

        public VolunteerTaskRepository(AppDbContext context) : base(context)
        {
            _context = context;

        }

        public async Task<(IEnumerable<VolunteerTask>, int TotalCount)> GetPagedVolunteerTaskAsync(int pageNumber, int pageSize, string? search = null)
        {
            var query = _context.VolunteerTasks
                .Include(c => c.Campaign)
                .ThenInclude(d => d.Charity)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                search = search.Trim().ToLower();

                query = query.Where(x =>
                x.Title.ToLower().Contains(search)
                || x.Location.ToLower().Contains(search)
                || x.Campaign.CampaignName.ToLower().Contains(search)
                || x.Campaign.Charity.Name.ToLower().Contains(search)
                );
               
            }


            var totalCount = await query.CountAsync();

            var items = await query
                .OrderByDescending(c => c.StartDate)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (items, totalCount);

        }

       
    }
}
