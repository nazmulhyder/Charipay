using AutoMapper;
using Charipay.Application.Common.Models;
using Charipay.Application.DTOs.Donor.Donation;
using Charipay.Application.Interfaces.Common;
using Charipay.Application.Interfaces.Repositories;
using Charipay.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charipay.Application.Commands.Donations
{
    public class CreateDonationCommandHandler (IUnitOfWork _unitOfWork, IMapper _mapper, ICurrentUserService _currentUser,  IDonationRepository _donationRepository, ICampaignRepository _campaignRepository, ILogger<CreateDonationCommandHandler> logger)
        : IRequestHandler<CreateDonationCommand, ApiResponse<DonationResponseDto>>
    {


        public async Task<ApiResponse<DonationResponseDto>> Handle(CreateDonationCommand request, CancellationToken cancellationToken)
        {
            var campaign = await _campaignRepository.GetByIdAsync(request.CampaignId, cancellationToken);

            if (campaign == null) 
            {
                logger.LogWarning("Donation creation failed. Because campaign not found. Campaign ID: {campaignID}", request.CampaignId);
                return ApiResponse<DonationResponseDto>.FailedResponse("Campaign not found.");
            }

            if (!campaign.IsActive)
            {
                logger.LogWarning("Donation creation failed. Because this campaign isn't active. Campaign ID: {campaignID}, campaign name: {campaignName}", request.CampaignId, campaign.CampaignName);
                return ApiResponse<DonationResponseDto>.FailedResponse("Campaign is not active.");
            }

            if (request.Amount <= 0)
            {
                logger.LogWarning("Donation creation failed. Because this campaign amount is low. Campaign ID: {campaignID}, campaign name: {campaignName}", request.CampaignId, campaign.CampaignName);
                return ApiResponse<DonationResponseDto>.FailedResponse("Amount must be greater than zero.");
            }

            if (_currentUser.UserId == null && !request.IsAnonymous)
            {
                logger.LogWarning("Donation creation failed. Because donor is not indentified neither anonymous. Campaign ID: {campaignID}, campaign name: {campaignName}", request.CampaignId, campaign.CampaignName);
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


            logger.LogInformation("Donation creation started. Campaign ID: {campaignID}, campaign name: {campaignName}", request.CampaignId, campaign.CampaignName);

            await _donationRepository.AddAsync(donation);
                if (donation.PaymentStatus == "Succeeded")
                {
                    campaign.CurrentAmount += donation.Amount;
                    _campaignRepository.Update(campaign, cancellationToken);
                }

                await _unitOfWork.SaveChangesAsync();

            logger.LogInformation("Donation created successfully. Campaign ID: {campaignID}, campaign name: {campaignName}", request.CampaignId, campaign.CampaignName);


            var responseDto = _mapper.Map<DonationResponseDto>(donation);


                return ApiResponse<DonationResponseDto>.SuccessResponse(responseDto, "Donation successful");
            }

         
        
    }
}
