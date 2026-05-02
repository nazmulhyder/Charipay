using Charipay.Application.Common.Models;
using Charipay.Application.DTOs.Donor;
using Charipay.Application.DTOs.Donor.Donation;
using Charipay.Application.Interfaces.Common;
using Charipay.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Charipay.Application.Queries.Donor
{
    public class GetDonorDashboardQueryHandler : IRequestHandler<GetDonorDashboardQuery, ApiResponse<DonorDashboardDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private ICurrentUserService _currentUserService;
        private readonly IDonationRepository donationRepository;

        public GetDonorDashboardQueryHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService, IDonationRepository _donationRepository)
        {
            _unitOfWork = unitOfWork;
            _currentUserService = currentUserService;
            donationRepository = _donationRepository;
        }

        public async Task<ApiResponse<DonorDashboardDto>> Handle(GetDonorDashboardQuery request, CancellationToken cancellationToken)
        {
            var donations = await donationRepository.GetDonationsByUserIdAsync(_currentUserService.UserId.Value);

            var response = new DonorDashboardDto
            {
                TotalDonated = donations.Sum(d => d.Amount),
                TotalDonations = donations.Count,
                CampaignsSupported = donations
                .Select(d => d.CampaignId)
                .Distinct()
                .Count(),

             RecentDonations = donations
            .Take(5)
            .Select(d => new RecentDonationDto
            {
                DonationId = d.DonationId,
                CampaignName = d.Campaign.CampaignName,
                CharityName = d.Campaign.Charity.Name,
                Amount = d.Amount,
                DonationDate = d.DonationDate,
                PaymentStatus = d.PaymentStatus
            })
           .ToList()
            };

            return ApiResponse<DonorDashboardDto>.SuccessResponse(response);
        }
  
      
    }
   
}
