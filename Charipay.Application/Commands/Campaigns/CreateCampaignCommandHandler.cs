using AutoMapper;
using Charipay.Application.Common.Models;
using Charipay.Application.DTOs.Campaigns;
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

namespace Charipay.Application.Commands.Campaigns
{
    public class CreateCampaignCommandHandler
        (IUnitOfWork _unitOfWork, IMapper _mapper, ICurrentUserService _currentUser, ICampaignRepository _campaignRepository, ILogger<CreateCampaignCommandHandler> _logger, ICharityRepository charityRepository)
        : IRequestHandler<CreateCampaignCommand, ApiResponse<CampaignDto>>
    {

        public async Task<ApiResponse<CampaignDto>> Handle(CreateCampaignCommand request, CancellationToken cancellationToken)
        {
            var exists = await _campaignRepository.GetCampaignByCharityId(request.CharityId, request.CampaignName, cancellationToken);
            var charity = await charityRepository.GetByIdAsync(request.CharityId, cancellationToken);

            if (exists)
            {
                _logger.LogWarning("Failed to create Campaign. Because, campaign already exists with this Charity. Charity Name: {CharityName}, Campaign name: {CampaignName}", charity?.Name, request?.CampaignName);
                return ApiResponse<CampaignDto>.FailedResponse("Campaign already exists with this Charity", new List<string> { "Campaign already exists with this Charity" });
            }

            request.CreatedById = _currentUser.UserId;
            var data = _mapper.Map<Campaign>(request);


            _logger.LogInformation("Charity campaign creation in progress. Charity Name: {CharityName}, Campaign name: {CampaignName}", charity?.Name, request?.CampaignName);

            await _campaignRepository.AddAsync(data);
            await _unitOfWork.SaveChangesAsync();

            _logger.LogInformation("Charity campaign created successfully. Charity Name: {CharityName}, Campaign name: {CampaignName}", charity?.Name, request?.CampaignName);


            var response = _mapper.Map<CampaignDto>(data);

            return ApiResponse<CampaignDto>.SuccessResponse(response, "Campaign saved successfully!");
        }
    }
}
