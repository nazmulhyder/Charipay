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

namespace Charipay.Application.Queries.Campaigns
{
    public class GetByIdCampaignQueryHandler : IRequestHandler<GetByIdCampaignQuery, ApiResponse<CampaignDto>>
    {
        private readonly IUnitOfWork _unitofWork;
        private readonly IMapper _mapper;

        public GetByIdCampaignQueryHandler(IUnitOfWork unitofWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitofWork = unitofWork;
        }


        public async Task<ApiResponse<CampaignDto>> Handle(GetByIdCampaignQuery request, CancellationToken cancellationToken)
        {
            var result = await _unitofWork.Campaigns.GetByIdAsync(request.CampaignId);

            if (result == null)
                return ApiResponse<CampaignDto>.FailedResponse("Data not exists!");

            var mapResult = _mapper.Map<CampaignDto>(result);

            return ApiResponse<CampaignDto>.SuccessResponse(mapResult);
        }
    }
}
