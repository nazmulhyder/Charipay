using AutoMapper;
using Charipay.Application.Common.Models;
using Charipay.Application.Common.Pagination;
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
    public class GetAllCampaignsAdminQueryHandler : IRequestHandler<GetAllCampaignsAdminQuery, ApiResponse<PageResult<CampaignDto>>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICampaignRepository _campaignRepository;
        public GetAllCampaignsAdminQueryHandler(IMapper mapper, IUnitOfWork unitOfWork, ICampaignRepository campaignRepository)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _campaignRepository = campaignRepository;
        }

        public async Task<ApiResponse<PageResult<CampaignDto>>> Handle(GetAllCampaignsAdminQuery request, CancellationToken cancellationToken)
        {
            var (campaignList, totalCount) = await
                _campaignRepository.GetAdminPagedCampaigns(request.PageNumber, request.PageSize, request.IsFeatured,request.IsActive ,cancellationToken, request.Search);

            var resCampaignList = _mapper.Map<List<CampaignDto>>(campaignList);

            var pagedResult = new PageResult<CampaignDto>(resCampaignList, totalCount, request.PageNumber, request.PageSize);


            return ApiResponse<PageResult<CampaignDto>>.SuccessResponse(pagedResult, "Fetched successfully");
        }
    }
}
