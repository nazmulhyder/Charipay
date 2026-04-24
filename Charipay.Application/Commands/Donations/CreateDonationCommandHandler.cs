using AutoMapper;
using Charipay.Application.Common.Models;
using Charipay.Application.DTOs.Donor.Donation;
using Charipay.Application.Interfaces.Common;
using Charipay.Application.Interfaces.Repositories;
using Charipay.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charipay.Application.Commands.Donations
{
    public class CreateDonationCommandHandler : IRequestHandler<CreateDonationCommand, ApiResponse<DonationResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUser;
        public CreateDonationCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ICurrentUserService currentUser)
        {
            _mapper = mapper;
            _currentUser = currentUser;
            _unitOfWork = unitOfWork;
        }

        public async Task<ApiResponse<DonationResponseDto>> Handle(CreateDonationCommand request, CancellationToken cancellationToken)
        {
            var campaign = await _unitOfWork.Campaigns.GetByIdAsync(request.CampaignId, cancellationToken);

            if (campaign == null) 
            {
                return ApiResponse<DonationResponseDto>.FailedResponse("Campaign not found.");
            }

            if (!campaign.IsActive)
            {
                return ApiResponse<DonationResponseDto>.FailedResponse("Campaign is not active.");
            }

            if (request.Amount <= 0)
            {
                return ApiResponse<DonationResponseDto>.FailedResponse("Amount must be greater than zero.");
            }

            if (_currentUser.UserId == null && !request.IsAnonymous)
            {
                return ApiResponse<DonationResponseDto>.FailedResponse("User id not found.");
            }

            var donation = new Donation
            {
                DonationId = Guid.NewGuid(),
                UserId = _currentUser.UserId,
                Amount = request.Amount,
                DonationDate = DateTime.UtcNow,
                IsAnonymous = request.IsAnonymous,
                PaymentMethod = request.PaymentMethod,
                TransactionId = Guid.NewGuid().ToString(),
                PaymentStatus = "Succeeded",
                ReceiptUrl = null,
                CampaignId = request.CampaignId,

            };

            try
            {
                await _unitOfWork.Donations.AddAsync(donation);
                if (donation.PaymentStatus == "Succeeded")
                {
                    campaign.CurrentAmount += donation.Amount;
                    _unitOfWork.Campaigns.Update(campaign, cancellationToken);
                }

                await _unitOfWork.SaveChangesAsync();

                var responseDto = _mapper.Map<DonationResponseDto>(donation);


                return ApiResponse<DonationResponseDto>.SuccessResponse(responseDto, "Donation successful");
            }

            catch (Exception ex) {
                return ApiResponse<DonationResponseDto>.FailedResponse(
     ex.InnerException?.Message ?? ex.Message
 );
            }
           
        }
    }
}
