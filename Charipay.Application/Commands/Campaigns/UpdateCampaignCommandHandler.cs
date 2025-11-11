using AutoMapper;
using Charipay.Application.Common.Models;
using Charipay.Application.DTOs.Campaigns;
using Charipay.Domain.Interfaces;
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
        public UpdateCampaignCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }
        public async Task<ApiResponse<CampaignDto>> Handle(UpdateCampaignCommand request, CancellationToken cancellationToken)
        {
            var existingCampgn = await _unitOfWork.Campaigns.GetByIdAsync(request.CampaignId);

            if (existingCampgn == null)
                return ApiResponse<CampaignDto>.FailedResponse("Data not exists!");

            _mapper.Map(request, existingCampgn);

            await _unitOfWork.SaveChangesAsync();

            var updatedCampgn = await _unitOfWork.Campaigns.GetByIdAsync(request.CampaignId);

            var result = _mapper.Map<CampaignDto>(updatedCampgn);

            return ApiResponse<CampaignDto>.SuccessResponse(result, "success");


        }
    }
}
