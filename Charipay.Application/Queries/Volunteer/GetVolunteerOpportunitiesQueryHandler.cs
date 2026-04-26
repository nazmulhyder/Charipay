using AutoMapper;
using Charipay.Application.Common.Models;
using Charipay.Application.Common.Pagination;
using Charipay.Application.DTOs.Admin.Volunteer;
using Charipay.Application.DTOs.Volunteer;
using Charipay.Application.Interfaces.Common;
using Charipay.Application.Interfaces.Repositories;
using Charipay.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charipay.Application.Queries.Volunteer
{
    public class GetVolunteerOpportunitiesQueryHandler : IRequestHandler<GetVolunteerOpportunitiesQuery, ApiResponse<PageResult<VolunteerOpportunityDto>>>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService currentUserService;

        public GetVolunteerOpportunitiesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, ICurrentUserService _currentUserService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            currentUserService = _currentUserService;
        }

        public async Task<ApiResponse<PageResult<VolunteerOpportunityDto>>> Handle(GetVolunteerOpportunitiesQuery request, CancellationToken cancellationToken)
        {
            var (volunteerTasks, totalCount) = await _unitOfWork.VolunteerTask.GetPagedVolunteerTaskAsync(request.PageNumber, request.PageSize, request.Search);


            var items = volunteerTasks.Select(v =>
            {
                var activeApplications = v.VolunteerUsers
                    .Where(a =>
                        a.IsActive &&
                        a.Status.ToLower() != "pending" &&
                        a.Status.ToLower() != "withdrawn" &&
                        a.Status.ToLower() != "cancelled" &&
                        a.Status.ToLower() != "rejected")
                    .ToList();

                var blockingStatuses = new[] { "pending", "approved", "started", "completed" };

                return new VolunteerOpportunityDto
                {
                    VolunteerTaskId = v.VolunteerTaskId,
                    Title = v.Title,
                    Location = v.Location,
                    StartDate = v.StartDate,
                    EndDate = v.EndDate,
                    Description = v.Description,

                    CampaignId = v.CampaignId,
                    CampaignName = v.Campaign.CampaignName,

                    CharityId = v.Campaign.CharityId,
                    CharityName = v.Campaign.Charity.Name,

                    MaxVolunteer = v.MaxVolunteer,

                    AppliedCount = activeApplications.Count,

                    RemainingSlots = v.MaxVolunteer - activeApplications.Count,

                    IsFull = v.MaxVolunteer - activeApplications.Count <= 0,

                    AlreadyApplied = v.VolunteerUsers.Any(u =>
                        u.UserId == currentUserService.UserId.Value &&
                        u.IsActive &&
                        blockingStatuses.Contains(u.Status.ToLower()))
                };
            }).ToList();

            var result = new PageResult<VolunteerOpportunityDto>(
               items,
               totalCount,
               request.PageNumber,
               request.PageSize
            );


            return ApiResponse<PageResult<VolunteerOpportunityDto>>.SuccessResponse(result, "Success");
        }
    }
}
