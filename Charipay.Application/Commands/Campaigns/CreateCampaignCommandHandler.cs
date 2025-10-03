using AutoMapper;
using Charipay.Application.Common.Models;
using Charipay.Application.DTOs.Campaigns;
using Charipay.Domain.Entities;
using Charipay.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charipay.Application.Commands.Campaigns
{
    public class CreateCampaignCommandHandler : IRequestHandler<CreateCampaignCommand, ApiResponse<CampaignDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateCampaignCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<ApiResponse<CampaignDto>> Handle(CreateCampaignCommand request, CancellationToken cancellationToken)
        {
            var exists = await _unitOfWork.Campaigns.GetCampaignByCharityId(request.CharityId, request.CampaignName, cancellationToken);


            if (exists)
                return ApiResponse<CampaignDto>.FailedResponse("Campain already exists with this Charity", new List<string> { "Campain already exists with this Charity" });

            var data = _mapper.Map<Campaign>(request);

            await _unitOfWork.Campaigns.AddAsync(data);
            await _unitOfWork.SaveChangesAsync();

            var response = _mapper.Map<CampaignDto>(data);

            return ApiResponse<CampaignDto>.SuccessResponse(response, "Campaign saved successfully!");
        }
    }
}
