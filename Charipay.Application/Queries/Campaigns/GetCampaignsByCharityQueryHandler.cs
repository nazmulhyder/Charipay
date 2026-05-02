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

namespace Charipay.Application.Queries.Campaigns
{
    public class GetCampaignsByCharityQueryHandler : IRequestHandler<GetCampaignsByCharityQuery, ApiResponse<List<CampaignDropdownDto>>>
    {

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICampaignRepository _campaignRepository;

        public GetCampaignsByCharityQueryHandler(IMapper mapper, IUnitOfWork unitOfWork, ICampaignRepository campaignRepository)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _campaignRepository = campaignRepository;
        }

        public async Task<ApiResponse<List<CampaignDropdownDto>>> Handle(GetCampaignsByCharityQuery request, CancellationToken cancellationToken)
        {
            var items = await _campaignRepository.GetCampaignsByCharityAsync(request.CharityId);

            var mappedDropdownData = _mapper.Map<List<CampaignDropdownDto>>(items);

            return ApiResponse<List<CampaignDropdownDto>>.SuccessResponse(mappedDropdownData, "success");
        }
    }
}
