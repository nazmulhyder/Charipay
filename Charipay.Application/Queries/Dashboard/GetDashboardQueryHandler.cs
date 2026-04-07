using AutoMapper;
using Charipay.Application.Common.Models;
using Charipay.Application.DTOs.Dashboard;
using Charipay.Application.Interfaces.Repositories;
using Charipay.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charipay.Application.Queries.Dashboard
{
    public class GetDashboardQueryHandler : IRequestHandler<GetDashboardQuery, ApiResponse<DashboardDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetDashboardQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ApiResponse<DashboardDto>> Handle(GetDashboardQuery request, CancellationToken cancellationToken)
        {
            var users = await _unitOfWork.Users.GetAllAsync(cancellationToken);
            var charities = await _unitOfWork.Charities.GetAllAsync(cancellationToken);
            var campaigns = await _unitOfWork.Campaigns.GetAllAsync(cancellationToken);
            var donations = await _unitOfWork.Donations.GetAllAsync(cancellationToken);

            var totalUsers = users.Count();
            var totalCharities = charities.Count();
            var totalCampaigns = campaigns.Count();
            var totalDonations = donations.Count();

            var recentDonations = donations
            .OrderByDescending(d => d.DonationDate)
            .Take(5)
            .Select(d => new RecentDonationDto
            {
                DonorName = d.User != null ? d.User.FullName : "Anonymous",
                CampaignName = d.Campaign != null ? d.Campaign.CampaignName : string.Empty,
                Amount = d.Amount,
                CreatedAt = d.DonationDate
            })
            .ToList();

            return ApiResponse<DashboardDto>.SuccessResponse(new DashboardDto()
            {
                TotalUsers = users.Count(),
                TotalCharities = charities.Count(),
                TotalCampaigns = campaigns.Count(),
                TotalDonations = donations.Count(),
                RecentDonations = recentDonations
            });
                }

         
    }
}
