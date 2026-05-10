using AutoMapper;
using Charipay.Application.Common.Models;
using Charipay.Application.DTOs.Campaigns;
using Charipay.Application.Interfaces.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charipay.Application.Commands.Campaigns
{
    public class UpdateCampaignCommandHandler (IUnitOfWork _unitOfWork, IMapper _mapper, ICampaignRepository _campaignRepository, ILogger<UpdateCampaignCommandHandler> logger) : IRequestHandler<UpdateCampaignCommand, ApiResponse<CampaignDto>>
    {

        public async Task<ApiResponse<CampaignDto>> Handle(UpdateCampaignCommand request, CancellationToken cancellationToken)
        {
            var existingCampgn = await _campaignRepository.GetByIdAsync(request.CampaignId, cancellationToken);

            if (existingCampgn == null)
            {
                logger.LogWarning("Failed to update campaign because the campaign does not exist. Campaign ID: {campaignId}"
              , request.CampaignId);
                return ApiResponse<CampaignDto>.FailedResponse("Data not exists!");
            }

            logger.LogInformation("Campaign update started. Campaign ID: {campaignId}, Campaign Name:{campaignName}"
                 , request.CampaignId, request.CampaignName);

            _mapper.Map(request, existingCampgn);
            await _unitOfWork.SaveChangesAsync();

            var updatedCampgn = await _campaignRepository.GetByIdAsync(request.CampaignId, cancellationToken);

            var result = _mapper.Map<CampaignDto>(updatedCampgn);

            logger.LogInformation("Campaign updated successfully. Campaign ID: {campaignId}, Campaign Name:{campaignName}"
                , result.CampaignId, result.CampaignName);

            return ApiResponse<CampaignDto>.SuccessResponse(result, "Campaign updated successfully");


        }
    }
}
