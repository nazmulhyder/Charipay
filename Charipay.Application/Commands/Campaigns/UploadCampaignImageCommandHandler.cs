using AutoMapper;
using Charipay.Application.Common.Models;
using Charipay.Application.Interfaces.Repositories;
using Charipay.Application.Interfaces.Storage;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charipay.Application.Commands.Campaigns
{
    public class UploadCampaignImageCommandHandler : IRequestHandler<UploadCampaignImageCommand, ApiResponse<string>>
    {

        private readonly IUnitOfWork _unitofWork;
        private readonly IMapper _mapper;
        private readonly IFileStorageService _fileStorageService;

        public UploadCampaignImageCommandHandler(IUnitOfWork unitofWork, IMapper mapper, IFileStorageService fileStorageService)
        {
            _mapper = mapper;
            _unitofWork = unitofWork;
            _fileStorageService = fileStorageService;
        }


        public async Task<ApiResponse<string>> Handle(UploadCampaignImageCommand request, CancellationToken cancellationToken)
        {
            if (request.File == null || request.File.Length == 0)
                return ApiResponse<string>.FailedResponse("No file uploaded.");

            var allowed = new HashSet<string> { "image/jpg", "image/jpeg", "image/png", "image/webp."};

            if(!allowed.Contains(request.File.ContentType))
                return ApiResponse<string>.FailedResponse("Only JPG, PNG, WEBP is allowed.");

            if(request.File.Length > 5 * 1024 * 1024)
                return ApiResponse<string>.FailedResponse("Max file size is 5MB.");

            // adjust this line to match your repository method name

            var campaign = await _unitofWork.Campaigns.GetByIdAsync(request.CampaignId, cancellationToken);

            if(campaign == null)
                return ApiResponse<string>.FailedResponse("Campaign not fould.");

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

            return ApiResponse<string>.SuccessResponse(imageUrl, "Campaign Image uploaded.");



        }
    }
}
