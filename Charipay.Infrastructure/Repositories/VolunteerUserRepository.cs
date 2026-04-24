using Charipay.Application.DTOs.Admin.Volunteer;
using Charipay.Application.DTOs.Volunteer;
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

        public async Task<(List<MyVolunteerApplicationDto> Items, int TotalCount)> GetMyApplicationsAsync(Guid UserId, int pageNumber, int pageSize, string? search = null, string? status = null)
        {
            var query = _context.VolunteerUsers.Where(c => c.UserId == UserId)
                .Include(d => d.VolunteerTask)
                .ThenInclude(e => e.Campaign)
                .ThenInclude(f => f.Charity)
                .AsQueryable();

           

            if (!string.IsNullOrWhiteSpace(search))
            {
                search = search.Trim().ToLower();

                query = query.Where(x =>
                x.Status.ToLower().Contains(search)
                || x.VolunteerTask.Title.ToLower().Contains(search)
                || x.VolunteerTask.Location.ToLower().Contains(search)
                || x.VolunteerTask.Campaign.CampaignName.ToLower().Contains(search)
                || x.VolunteerTask.Campaign.Charity.Name.ToLower().Contains(search)
                );

            }

            if (!string.IsNullOrWhiteSpace(status))
            {
                status = status.Trim().ToLower();

                query = query.Where(x => x.Status != null && x.Status.ToLower() == status);
            }

            var items = await query.OrderByDescending(a=>a.SignupDate)
                .Skip((pageNumber -1) * pageSize)
                .Take(pageSize)
                 .Select(vu => new MyVolunteerApplicationDto
                 {
                     VolunteerUserId = vu.VolunteerUserId,
                     VolunteerTaskId = vu.VolunteerTaskId,

                     Title = vu.VolunteerTask.Title,
                     Description = vu.VolunteerTask.Description,
                     Location = vu.VolunteerTask.Location,

                     StartDate = vu.VolunteerTask.StartDate,
                     EndDate = vu.VolunteerTask.EndDate,

                     CampaignId = vu.VolunteerTask.CampaignId,
                     CampaignName = vu.VolunteerTask.Campaign.CampaignName,

                     CharityId = vu.VolunteerTask.Campaign.CharityId,
                     CharityName = vu.VolunteerTask.Campaign.Charity.Name,

                     SignupDate = vu.SignupDate,
                     IsActive = vu.IsActive,
                     Status = vu.Status ?? string.Empty,

                     VolunteerMessage = vu.VolunteerMessage,
                     AvailabilityNote = vu.AvailabilityNote,
                     AdminNote = vu.AdminNote,

                     ReviewedAt = vu.ReviewedAt,
                     StartedAt = vu.StartedAt,
                     CompletedAt = vu.CompletedAt
                 }).ToListAsync();


            var totalCount = await query.CountAsync();
            return (items, totalCount);

        }

        public async Task<VolunteerUser?> GetByIdAndUserIdAsync(Guid volunteerUserId, Guid userId)
        {
            var task = await _context.VolunteerUsers.Where(a=>a.VolunteerUserId ==  volunteerUserId && a.UserId == userId).FirstOrDefaultAsync();
            
            return task;
        }
    }
}
