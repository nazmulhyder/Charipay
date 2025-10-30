using AutoMapper;
using Charipay.Application.Common.Models;
using Charipay.Application.Common.Pagination;
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
    public class GetAllPagedCampaignsQueryHandler : IRequestHandler<GetAllPagedCampaignsQuery, ApiResponse<PageResult<CampaignDto>>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public GetAllPagedCampaignsQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<ApiResponse<PageResult<CampaignDto>>> Handle(GetAllPagedCampaignsQuery request, CancellationToken cancellationToken)
        {
            var (campaignList, totalCount) = await _unitOfWork.Campaigns.GetAllPagedCampaings(request.PageNumber, request.PageSize, request.Search);

            var resCampaignList = _mapper.Map<List<CampaignDto>>(campaignList);

            var pagedResult = new PageResult<CampaignDto>(resCampaignList, totalCount, request.PageNumber, request.PageSize);


            return ApiResponse<PageResult<CampaignDto>>.SuccessResponse(pagedResult, "Fetched successfully");
        }
    }
}
