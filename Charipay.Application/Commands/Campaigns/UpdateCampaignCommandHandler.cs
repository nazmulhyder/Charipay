using AutoMapper;
using Charipay.Application.Common.Models;
using Charipay.Application.DTOs.Campaigns;
using Charipay.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charipay.Application.Commands.Campaigns
{
    public class UpdateCampaignCommandHandler : IRequestHandler<UpdateCampaignCommand, ApiResponse<CampaignDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICampaignRepository _campaignRepository;

        public UpdateCampaignCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ICampaignRepository campaignRepository)
        {
             _unitOfWork = unitOfWork;
             _mapper = mapper;
             _campaignRepository = campaignRepository;
        }
        public async Task<ApiResponse<CampaignDto>> Handle(UpdateCampaignCommand request, CancellationToken cancellationToken)
        {
            var existingCampgn = await _campaignRepository.GetByIdAsync(request.CampaignId, cancellationToken);

            if (existingCampgn == null)
                return ApiResponse<CampaignDto>.FailedResponse("Data not exists!");

            _mapper.Map(request, existingCampgn);

            await _unitOfWork.SaveChangesAsync();

            var updatedCampgn = await _campaignRepository.GetByIdAsync(request.CampaignId, cancellationToken);

            var result = _mapper.Map<CampaignDto>(updatedCampgn);

            return ApiResponse<CampaignDto>.SuccessResponse(result, "success");


        }
    }
}
