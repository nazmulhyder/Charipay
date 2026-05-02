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
        private readonly IUserRepository userRepository;
        private readonly ICharityRepository charityRepository;
        private readonly ICampaignRepository campaignRepository;
        private readonly IDonationRepository donationRepository;

        public GetDashboardQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IUserRepository _userRepository, ICharityRepository _charityRepository, ICampaignRepository _campaignRepository
            , IDonationRepository _donationRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            userRepository = _userRepository;
            charityRepository = _charityRepository;
            campaignRepository = _campaignRepository;
            donationRepository = _donationRepository;
        }

        public async Task<ApiResponse<DashboardDto>> Handle(GetDashboardQuery request, CancellationToken cancellationToken)
        {
            var users = await userRepository.GetAllAsync(cancellationToken);
            var charities = await charityRepository.GetAllAsync(cancellationToken);
            var campaigns = await campaignRepository.GetAllAsync(cancellationToken);
            var donations = await donationRepository.GetAllAsync(cancellationToken);

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
