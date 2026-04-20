using Charipay.Application.DTOs.Admin.Volunteer;
using Charipay.Application.DTOs.Volunteer;
using Charipay.Application.Interfaces.QueryService;
using Charipay.Domain.Entities;
using Charipay.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charipay.Infrastructure.QueryService
{
    public class AdminVolunteerRequestQueryService : IAdminVolunteerRequestQueryService
    {
        private readonly AppDbContext _context;
        public AdminVolunteerRequestQueryService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<(List<AdminVolunteerRequestDto> Items, int TotalCount)> GetVolunteerApplicationRequestsAsync(int pageNumber, int pageSize, string? search, string? status)
        {
            var query = _context.VolunteerUsers
                 .Include(a => a.User)
                 .Include(b => b.VolunteerTask)
                 .ThenInclude(c => c.Campaign)
                 .ThenInclude(e => e.Charity)
                 .AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {

                search = search.Trim().ToLower();

                query = query.Where(a => a.User.FullName.ToLower().Contains(search)
                 || a.User.Email.ToLower().Contains(search)
                 || a.VolunteerTask.Title.ToLower().Contains(search)
                 || a.VolunteerTask.Campaign.CampaignName.ToLower().Contains(search)
                 || a.VolunteerTask.Campaign.Charity.Name.ToLower().Contains(search)
                );
            }

                if (!string.IsNullOrWhiteSpace(status))
                {
                    query = query.Where(a => a.Status.ToLower().Contains(status));
                }

                var totalCount = await query.CountAsync();

                var items = await query
                    .OrderByDescending(a => a.SignupDate)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .Select(vu => new AdminVolunteerRequestDto
                    {
                        VolunteerUserId = vu.VolunteerUserId,
                        UserId = vu.UserId,
                        VolunteerName = vu.User.FullName,
                        VolunteerEmail = vu.User.Email,

                        VolunteerTaskId = vu.VolunteerTaskId,
                        TaskTitle = vu.VolunteerTask.Title,
                        TaskLocation = vu.VolunteerTask.Location,
                        TaskStartDate = vu.VolunteerTask.StartDate,
                        TaskEndDate = vu.VolunteerTask.EndDate,

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
                    })
                    .ToListAsync();

                return (items, totalCount);
            }
        
    }
}
