using AutoMapper;
using Charipay.Application.Common.Models;
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
    public class DeleteCampaignCommandHandler(IUnitOfWork _unitofWork, IMapper _mapper, ICampaignRepository _campaignRepository, ILogger<DeleteCampaignCommandHandler> _logger) 
        : IRequestHandler<DeleteCampaignCommand, ApiResponse<string>>
    {

        public async Task<ApiResponse<string>> Handle(DeleteCampaignCommand request, CancellationToken cancellationToken)
        {
            var existingData = await _campaignRepository.GetByIdAsync(request.CampaignId, cancellationToken);

            if (existingData == null)
            {
                _logger.LogWarning("Failed to delete campaign because the campaign does not exist. Campaign ID: {campaignId}"
                    , request.CampaignId);
                return ApiResponse<string>.FailedResponse("Data not exists.");
            }

            _logger.LogWarning("Campaign deletion started. Campaign ID: {campaignId}, Campaign Name:{campaignName}"
                  , request.CampaignId, existingData.CampaignName);

            _campaignRepository.Remove(existingData, cancellationToken);
            await _unitofWork.SaveChangesAsync();

            _logger.LogWarning("Campaign deletion successfully. Campaign ID: {campaignId}, Campaign Name:{campaignName}"
                 , request.CampaignId, existingData.CampaignName);

            return ApiResponse<string>.SuccessResponse("Deleted successfully", "Deleted successfully");
        }
    }
}
