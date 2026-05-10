using AutoMapper;
using Charipay.Application.Common.Models;
using Charipay.Application.Interfaces.Repositories;
using Charipay.Application.Interfaces.Storage;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charipay.Application.Commands.Campaigns
{
    public class UploadCampaignImageCommandHandler 
        (IUnitOfWork _unitofWork, IMapper _mapper, IFileStorageService _fileStorageService, ICampaignRepository _campaignRepository, ILogger<UploadCampaignImageCommandHandler> logger)
        : IRequestHandler<UploadCampaignImageCommand, ApiResponse<string>>
    {


        public async Task<ApiResponse<string>> Handle(UploadCampaignImageCommand request, CancellationToken cancellationToken)
        {

            var campaign = await _campaignRepository.GetByIdAsync(request.CampaignId, cancellationToken);

            if (request.File == null || request.File.Length == 0)
            {
                logger.LogWarning("No file uploaded for this campaign. Campaign ID: {campaignId}, Campaign Name: {campaignName}", request.CampaignId, campaign?.CampaignName);
                return ApiResponse<string>.FailedResponse("No file uploaded.");
            }

            var allowed = new HashSet<string> { "image/jpg", "image/jpeg", "image/png", "image/webp."};

            if (!allowed.Contains(request.File.ContentType)) 
            {
                logger.LogWarning("Invalid file type. Only JPG, PNG, WEBP is allowed for campaign. Campaign ID: {campaignId}, Campaign Name: {campaignName}", request.CampaignId, campaign?.CampaignName);
                return ApiResponse<string>.FailedResponse("Only JPG, PNG, WEBP is allowed.");
            }
               

            if(request.File.Length > 5 * 1024 * 1024)
                return ApiResponse<string>.FailedResponse("Max file size is 5MB.");

            // adjust this line to match your repository method name


            if(campaign == null)
            {
                logger.LogWarning("Failed to upload image because the campaign does not exist. Campaign ID: {campaignId}"
                , request.CampaignId);
                return ApiResponse<string>.FailedResponse("Campaign not found.");
            }


            logger.LogInformation("File upload started for campaign. Campaign ID: {campaignId}, Campaign Name: {campaignName}"
              , request.CampaignId, campaign.CampaignName);

            var oldUrl = campaign.ImageUrl;

            await using var stream = request.File.OpenReadStream();

            var imageUrl = await _fileStorageService.UploadAsync(
                stream, request.File.ContentType, 
                request.File.FileName, 
                folder: $"campaign/{campaign.CampaignId}", 
                cancellationToken
                );

            campaign.ImageUrl = imageUrl;

            await _unitofWork.SaveChangesAsync();

            logger.LogInformation("File uploaded successfully. Campaign ID: {campaignId}, Campaign Name: {campaignName}"
          , request.CampaignId, campaign.CampaignName);

            return ApiResponse<string>.SuccessResponse(imageUrl, "Campaign Image uploaded.");



        }
    }
}
